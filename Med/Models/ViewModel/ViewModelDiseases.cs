using System.Collections.ObjectModel;
using System.Windows;
using Med.DB;
using Med.Models.BaseCommand;
using Med.Models.BindingModel;

namespace Med.Models.ViewModel
{
    public class ViewModelDiseases : BaseRelayCommand
    {
        public ViewModelNewDiseases NewDiseases { get; set; }

        //Модель доктор
        public DiseasesBindingModel SelectedDiseasesBinding { get => diseasesBinding; set { diseasesBinding = value; OnAllPropertyChanged(); } }

        //Таблица (Доктор)
        public ObservableCollection<DiseasesBindingModel> Table { get => table; set { table = value; OnPropertyChanged(); } }

        //Поиск
        public override string Search
        {
            get => search;
            set
            {
                search = value;

                Table.Clear();

                SQL.Execute($"SELECT * FROM [dbo].[Diseases] WHERE Name Like '%{Search}%'", Table);

                OnPropertyChanged();
            }
        }

        //Конструктор
        public ViewModelDiseases()
        {
            Table = new ObservableCollection<DiseasesBindingModel>();
            NewDiseases = new ViewModelNewDiseases();
            NewDiseases.InsertDataBase += NewDoctor_InsertDataBase;

            //Выполняем запрос
            NewDoctor_InsertDataBase();
        }

        private void NewDoctor_InsertDataBase()
        {
            Table.Clear();
            SQL.Execute($"SELECT * FROM [dbo].[Diseases]", Table);
        }

        //Команда добавить
        public override RelayCommand Add => add ?? (add = new RelayCommand(arg =>
        {
           
            Windows.WindowNewDisease newData = new Windows.WindowNewDisease(NewDiseases);
            NewDiseases.DiseasesBinding = new DiseasesBindingModel();
            newData.ShowDialog();
        }));

        //Команда редактировать
        public override RelayCommand Edit => edit ?? (edit = new RelayCommand(arg =>
        {
            if (SelectedDiseasesBinding != null)
            {
                Windows.WindowNewDisease newData = new Windows.WindowNewDisease(NewDiseases);
                NewDiseases.IsEdit = true;
                NewDiseases.DiseasesBinding = SelectedDiseasesBinding;
                newData.ShowDialog();
            }
            else
                MessageBox.Show("Для редактирование данных, нужно выбрать строку в таблице", "Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }));

        //Команда удалить
        public override RelayCommand Delete => delete ?? (delete = new RelayCommand(arg =>
        {
            if (SelectedDiseasesBinding != null)
            {
                if (MessageBox.Show($"Вы действительно хотите удалить ({SelectedDiseasesBinding.Name})?", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Hand) == MessageBoxResult.OK)
                {
                    SQL.ExecuteNonQuery($"DELETE FROM [dbo].[Diseases] WHERE id={SelectedDiseasesBinding.ID}");
                    Table.Remove(SelectedDiseasesBinding);

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

        private DiseasesBindingModel diseasesBinding;

        private ObservableCollection<DiseasesBindingModel> table;
    }
}
