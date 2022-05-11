using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using Med.DB;
using Med.Models.BindingModel;

namespace Med.Models.ViewModel
{
    public class ViewModelDetails : BaseCommand.BaseRelayCommand
    {
        string Select = @"SELECT [Patient].Card_number as [Номер карточки]
                                , Concat([Patient].[Surname],' ', [Patient].[Name],' ',  [Patient].[Patronymic]) as [Пациент]
                                ,[Patient].DateBirth as [Дата рождения]
                                ,[Patient].Sex As [Пол]
                                ,[Patient].[Policy_number] as [Полис]
                                ,[Patient].Address as [Адрес]
                                ,[Patient].Phone as [Телефон]
                                ,[Diseases].[Name] as [Диагноз]
                                ,[Description] as [Описание]
                                ,[Date_Time] as [Дата записи]
                                
                                
                                FROM [dbo].[Record]
                                JOIN [Doctor] ON [Doctor].[ID] = [Record].[ID_Doctor]
                                JOIN [Patient] ON [Patient].[ID] = [Record].[ID_Patient]
                                JOIN [Diseases] ON [Diseases].[ID] = [Record].[ID_Diseases] ";

        public DataTableEx Table { get => table; set { table = value; OnPropertyChanged(); } }
        //Таблица (Пациент)
        //public ObservableCollection<RecordBindingModel> Table { get => table; set { table = value; OnPropertyChanged(); } }
        public ViewModelDetails()
        {
            //Table = new ObservableCollection<RecordBindingModel>();
            // Table.Clear();
            Table = SQL.Execute($"{Select} WHERE [Patient].[ID]={User.PatientId}");
        }
        public override RelayCommand Close => close ?? (close = new RelayCommand(arg =>
        {
            (arg as Window).Close();
        }));
        public RelayCommand Print => print ?? (print = new RelayCommand(arg =>
        {
            var dataGrid1 = (arg as DataGrid);
            FlowDocument fd = new FlowDocument();
            System.Windows.Controls.PrintDialog Printdlg = new System.Windows.Controls.PrintDialog();

            if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                dataGrid1.Measure(pageSize);
                dataGrid1.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));
        
                Printdlg.PrintVisual(dataGrid1, "История");
            }
        }));
        private DataTableEx table;
        private RelayCommand print;
    }
}
