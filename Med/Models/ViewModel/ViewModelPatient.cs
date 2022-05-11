using System.Collections.ObjectModel;
using System.Windows;
using Med.DB;
using Med.Models.BaseCommand;
using Med.Models.BindingModel;

namespace Med.Models.ViewModel
{
    public class ViewModelPatient : BaseRelayCommand
    {
        public ViewModelNewPatient NewPatient { get; set; }

        //Модель пациент
        public PatientBindingModel SelectedPatientBinding { get => patientBinding; set { patientBinding = value; OnAllPropertyChanged(); } }

        //Таблица (Пациент)
        public ObservableCollection<PatientBindingModel> Table { get => table; set { table = value; OnPropertyChanged(); } }

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
                        SQL.Execute($"SELECT * FROM [dbo].[Patient] WHERE Surname Like '%{Search}%'", Table);
                        break;
                    case 1:
                        SQL.Execute($"SELECT * FROM [dbo].[Patient] WHERE Name Like '%{Search}%'", Table);
                        break;
                    case 2:
                        SQL.Execute($"SELECT * FROM [dbo].[Patient] WHERE Patronymic Like '%{Search}%'", Table);
                        break;
                    case 3:
                        SQL.Execute($"SELECT * FROM [dbo].[Patient] WHERE Passport Like %{Search}%", Table);
                        break;
                    case 4:
                        SQL.Execute($"SELECT * FROM [dbo].[Patient] WHERE Address Like '%{Search}%'", Table);
                        break;
                    case 5:
                        SQL.Execute($"SELECT * FROM [dbo].[Patient] WHERE Phone Like '%{Search}%'", Table);
                        break;
                    case 6:
                        SQL.Execute($"SELECT * FROM [dbo].[Patient] WHERE Policy_number Like %{Search}%", Table);
                        break;


                }
                OnPropertyChanged();
            }
        }

        //Конструктор
        public ViewModelPatient()
        {
            Table = new ObservableCollection<PatientBindingModel>();
            NewPatient = new ViewModelNewPatient();
            NewPatient.InsertDataBase += NewPatient_InsertDataBase;

            //Выполняем запрос
            NewPatient_InsertDataBase();
        }

        private void NewPatient_InsertDataBase()
        {
            Table.Clear();
            SQL.Execute($"SELECT * FROM [dbo].[Patient]", Table);
        }

        //Команда добавить
        public override RelayCommand Add => add ?? (add = new RelayCommand(arg =>
        {
            //Новый пациент
            Windows.WindowPatientNewData newData = new Windows.WindowPatientNewData(NewPatient);
            NewPatient.PatientBinding = new PatientBindingModel();
            newData.ShowDialog();
        }));

        //Команда редактировать
        public override RelayCommand Edit => edit ?? (edit = new RelayCommand(arg =>
        {
            if (SelectedPatientBinding != null)
            {
                //Редактирование пациента
                Windows.WindowPatientNewData newData = new Windows.WindowPatientNewData(NewPatient);
                NewPatient.IsEdit = true;
                NewPatient.PatientBinding = SelectedPatientBinding;
                newData.ShowDialog();
            }
            else
                MessageBox.Show("Для редактирование данных, нужно выбрать строку в таблице", "Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }));

        //Команда удалить
        public override RelayCommand Delete => delete ?? (delete = new RelayCommand(arg =>
        {
            if (SelectedPatientBinding != null)
            {
                if (MessageBox.Show($"Вы действительно хотите удалить ({SelectedPatientBinding.Surname} {SelectedPatientBinding.Name} { SelectedPatientBinding.Patronymic })?", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Hand) == MessageBoxResult.OK)
                {
                    SQL.ExecuteNonQuery($"DELETE FROM [dbo].[Patient] WHERE id={SelectedPatientBinding.ID}");
                    Table.Remove(SelectedPatientBinding);

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

        private PatientBindingModel patientBinding;
        private ObservableCollection<PatientBindingModel> table;
    }
}
