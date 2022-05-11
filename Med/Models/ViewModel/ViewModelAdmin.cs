using System.Collections.ObjectModel;
using System.Windows;
using Med.DB;
using Med.Models.BaseCommand;
using Med.Models.BindingModel;

namespace Med.Models.ViewModel
{
    public class ViewModelAdmin : BaseRelayCommand
    {
        string Select = @"SELECT [Authorizations].[ID]
                          ,CONCAT([Doctor].Surname,' ', [Doctor].Name,' ', [Doctor].Patronymic) as Name 
                                ,[Login]
                                ,[Password]
                                ,[Access]
                                ,[IdDoctor]
                            FROM [dbo].[Authorizations]
                            JOIN [Doctor] ON [Doctor].ID = [Authorizations].IdDoctor";
        public ViewModelNewAdmin NewAdmin { get; set; }

        //Модель доктор
        public AuthorizationBindingModel SelectedAdminBinding { get => adminBinding; set { adminBinding = value; OnAllPropertyChanged(); } }

        //Таблица (Доктор)
        public ObservableCollection<AuthorizationBindingModel> Table { get => table; set { table = value; OnPropertyChanged(); } }

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
                        SQL.Execute($"{Select} WHERE [Doctor].[Surname] Like '%{Search}%'", Table);
                        break;
                    case 1:
                        SQL.Execute($"{Select} WHERE [Doctor].[Name] Like '%{Search}%'", Table);
                        break;
                    case 2:
                        SQL.Execute($"{Select} WHERE [Doctor].[Patronymic] Like '%{Search}%'", Table);
                        break;
                    case 3:
                        SQL.Execute($"{Select} WHERE [Doctor].[Login] Like '%{Search}%'", Table);
                        break;
                    case 4:
                        SQL.Execute($"{Select} WHERE [Doctor].[Password] Like '%{Search}%'", Table);
                        break;
                }
                OnPropertyChanged();
            }
        }
        //Конструктор
        public ViewModelAdmin()
        {
            Table = new ObservableCollection<AuthorizationBindingModel>();
            NewAdmin = new ViewModelNewAdmin();
            NewAdmin.InsertDataBase += NewDoctor_InsertDataBase;

            //Выполняем запрос
            NewDoctor_InsertDataBase();
        }

        private void NewDoctor_InsertDataBase()
        {
            Table.Clear();
            SQL.Execute(Select, Table);
        }

        //Команда добавить
        public override RelayCommand Add => add ?? (add = new RelayCommand(arg =>
        {

            Windows.WindowNewAdmin newData = new Windows.WindowNewAdmin(NewAdmin);
            NewAdmin.AdminBinding = new AuthorizationBindingModel();
            newData.ShowDialog();
        }));

        //Команда редактировать
        public override RelayCommand Edit => edit ?? (edit = new RelayCommand(arg =>
        {
            if (SelectedAdminBinding != null)
            {
                Windows.WindowNewAdmin newData = new Windows.WindowNewAdmin(NewAdmin);
                NewAdmin.IsEdit = true;
                NewAdmin.AdminBinding = SelectedAdminBinding;
                newData.ShowDialog();
            }
            else
                MessageBox.Show("Для редактирование данных, нужно выбрать строку в таблице", "Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }));

        //Команда удалить
        public override RelayCommand Delete => delete ?? (delete = new RelayCommand(arg =>
        {
            if (SelectedAdminBinding != null)
            {
                if (MessageBox.Show($"Вы действительно хотите удалить ({SelectedAdminBinding.Name})?", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Hand) == MessageBoxResult.OK)
                {
                    SQL.ExecuteNonQuery($"DELETE FROM [dbo].[Authorizations] WHERE id={SelectedAdminBinding.ID}");
                    Table.Remove(SelectedAdminBinding);

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

        private AuthorizationBindingModel adminBinding;

        private ObservableCollection<AuthorizationBindingModel> table;
    }
}
