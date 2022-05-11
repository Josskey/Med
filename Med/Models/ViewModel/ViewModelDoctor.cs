using System.Collections.ObjectModel;
using System.Windows;
using Med.DB;
using Med.Models.BaseCommand;
using Med.Models.BindingModel;

namespace Med.ViewModel
{
    public class ViewModelDoctor : BaseRelayCommand
    {
        public ViewModelNewDoctor NewDoctor { get; set; }

        private bool status;
        public bool Status { get => status; set { status = value; OnPropertyChanged(); } }

        //Модель доктор
        public DoctorBindingModel SelectedDoctorBinding { get => doctorBinding; set { doctorBinding = value; OnAllPropertyChanged(); } }

        //Таблица (Доктор)
        public ObservableCollection<DoctorBindingModel> Table { get => table; set { table = value; OnPropertyChanged(); } }

        //Параметр поиска
        public override int ParamSearch { get => paramSearch; set { paramSearch = value; OnPropertyChanged(); } }

        //Поиск
        public override string Search
        {
            get => search;
            set
            {
                search = value;

                Table.Clear();
                switch (ParamSearch)
                {
                    case 0:
                        SQL.Execute($"SELECT * FROM [dbo].[Doctor] WHERE Surname Like '%{Search}%'", Table);
                        break;
                    case 1:
                        SQL.Execute($"SELECT * FROM [dbo].[Doctor] WHERE Name Like '%{Search}%'", Table);
                        break;
                    case 2:
                        SQL.Execute($"SELECT * FROM [dbo].[Doctor] WHERE Patronymic Like '%{Search}%'", Table);
                        break;
                    case 3:
                        SQL.Execute($"SELECT * FROM [dbo].[Doctor] WHERE Phone Like '%{Search}%'", Table);
                        break;


                }
                OnPropertyChanged();
            }
        }

        //Конструктор
        public ViewModelDoctor()
        {
            Table = new ObservableCollection<DoctorBindingModel>();
            NewDoctor = new ViewModelNewDoctor();
            NewDoctor.InsertDataBase += NewDoctor_InsertDataBase;
            Status = User.Authorization.CheckStat;
            //Выполняем запрос
            NewDoctor_InsertDataBase();
        }

        private void NewDoctor_InsertDataBase()
        {
            Table.Clear();
            SQL.Execute($"SELECT * FROM [dbo].[Doctor]", Table);
        }

        //Команда добавить
        public override RelayCommand Add => add ?? (add = new RelayCommand(arg =>
        {
            //Новый доктор
            Windows.WindowNewData newData = new Windows.WindowNewData(NewDoctor);
            NewDoctor.DoctorBinding = new DoctorBindingModel();
            newData.ShowDialog();
        }));

        //Команда редактировать
        public override RelayCommand Edit => edit ?? (edit = new RelayCommand(arg =>
        {
            if (SelectedDoctorBinding != null)
            {
                //Новый доктор
                Windows.WindowNewData newData = new Windows.WindowNewData(NewDoctor);
                NewDoctor.IsEdit = true;
                NewDoctor.DoctorBinding = SelectedDoctorBinding;
                newData.ShowDialog();
            }
            else
                MessageBox.Show("Для редактирование данных, нужно выбрать строку в таблице", "Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }));

        //Команда удалить
        public override RelayCommand Delete => delete ?? (delete = new RelayCommand(arg =>
        {
            if (SelectedDoctorBinding != null)
            {
                if (MessageBox.Show($"Вы действительно хотите удалить ({SelectedDoctorBinding.Surname} {SelectedDoctorBinding.Name} { SelectedDoctorBinding.Patronymic })?", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Hand) == MessageBoxResult.OK)
                {
                    SQL.ExecuteNonQuery($"DELETE FROM [dbo].[Doctor] WHERE id={SelectedDoctorBinding.ID}");
                    Table.Remove(SelectedDoctorBinding);
                  
                }
            }
            else
                MessageBox.Show("Для удаление данных, нужно выбрать строку в таблице", "Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }));

        //Команда закрыть окно
        public override RelayCommand Close => close ?? (close = new RelayCommand(arg =>
        {
            (arg as Window).Close();
        }));

       

        private DoctorBindingModel doctorBinding;
       
        private ObservableCollection<DoctorBindingModel> table;

        

    }
}
