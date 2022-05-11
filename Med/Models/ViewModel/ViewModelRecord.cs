using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Med.DB;
using Med.Models.BaseCommand;
using Med.Models.BindingModel;

namespace Med.Models.ViewModel
{
    public class ViewModelRecord : BaseRelayCommand
    {
        string Select = @"SELECT [Record].[ID]
                            	  , Concat([Doctor].[Surname],' ', [Doctor].[Name],' ',  [Doctor].[Patronymic]) as [SDoctor]
                            	  , Concat([Patient].[Surname],' ', [Patient].[Name],' ',  [Patient].[Patronymic]) as [SPatient]
                                  ,[Patient].[Policy_number]
                            	  ,[Diseases].[Name]
                            	  ,[Date_Time]
                                  ,[Description]
                                  ,[Card_number]
                                  ,[ID_Doctor]
                                  ,[ID_Patient]
                                  ,[ID_Diseases]
                              FROM [dbo].[Record]
                              JOIN [Doctor] ON [Doctor].[ID] = [Record].[ID_Doctor]
                              JOIN [Patient] ON [Patient].[ID] = [Record].[ID_Patient]
                              JOIN [Diseases] ON [Diseases].[ID] = [Record].[ID_Diseases] ";
        public ViewModelNewRecord NewRecord { get; set; }

        public RecordBindingModel SelectedRecordBinding { get => recordBinding; set { recordBinding = value; OnAllPropertyChanged(); } }

        //Таблица (Пациент)
        public ObservableCollection<RecordBindingModel> Table { get => table; set { table = value; OnPropertyChanged(); } }

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
                        SQL.Execute($"{Select} WHERE [Patient].[Surname] Like '%{Search}%'", Table);
                        break;
                    case 4:
                        SQL.Execute($"{Select} WHERE [Patient].[Name] Like '%{Search}%'", Table);
                        break;
                    case 5:
                        SQL.Execute($"{Select} WHERE [Patient].[Patronymic] Like '%{Search}%'", Table);
                        break;
                    case 6:
                        SQL.Execute($"{Select} WHERE [Diseases].[Name] Like '%{Search}%'", Table);
                        break;
                    case 7:
                        SQL.Execute($"{Select} WHERE [Patient].[Policy_number] Like %{Search}%", Table);
                        break;
                    case 8:
                        SQL.Execute($"{Select} WHERE [Record].[Date_Time] Like '%{Search}%'", Table);
                        break;


                }
                OnPropertyChanged();
            }
        }

        //Конструктор
        public ViewModelRecord()
        {
            Table = new ObservableCollection<RecordBindingModel>();
            NewRecord = new ViewModelNewRecord();
            NewRecord.InsertDataBase += NewRecord_InsertDataBase;

            //Выполняем запрос
            NewRecord_InsertDataBase();
        }

        private void NewRecord_InsertDataBase()
        {
            Table.Clear();
            SQL.Execute(Select, Table);
        }

        //Команда добавить
        public override RelayCommand Add => add ?? (add = new RelayCommand(arg =>
        {

            Windows.WindowNewRecord newData = new Windows.WindowNewRecord(NewRecord);
            NewRecord.RecordBinding = new RecordBindingModel();
            newData.ShowDialog();
        }));

        //Команда редактировать
        public override RelayCommand Edit => edit ?? (edit = new RelayCommand(arg =>
        {
            if (SelectedRecordBinding != null)
            {
                ////Редактирование пациента
                Windows.WindowNewRecord newData = new Windows.WindowNewRecord(NewRecord);
                NewRecord.IsEdit = true;
                NewRecord.RecordBinding = SelectedRecordBinding;
                newData.ShowDialog();
            }
            else
                MessageBox.Show("Для редактирование данных, нужно выбрать строку в таблице", "Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }));

        //Команда удалить
        public override RelayCommand Delete => delete ?? (delete = new RelayCommand(arg =>
        {
            if (SelectedRecordBinding != null)
            {
                if (MessageBox.Show($"Вы действительно хотите удалить с пациентом ({SelectedRecordBinding.SPatient})?", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Hand) == MessageBoxResult.OK)
                {
                    SQL.ExecuteNonQuery($"DELETE FROM [dbo].[Record] WHERE id={SelectedRecordBinding.ID}");
                    Table.Remove(SelectedRecordBinding);

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

        private RecordBindingModel recordBinding;
        private ObservableCollection<RecordBindingModel> table;
    }
}
