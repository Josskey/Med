using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Med.DB;
using Med.Models.BaseCommand;
using Med.Models.BindingModel;
using Med.ViewModel;
using Med.Windows;

namespace Med.Models.ViewModel
{
    public class ViewModelNewRecord : BaseRelayCommand
    {
       
        //Событие отвечает за внесение данных в бд
        public event Action InsertDataBase;

        public bool IsEdit { get; set; }

        //Модель доктор
        public RecordBindingModel RecordBinding { get => recordBinding; set { recordBinding = value; OnPropertyChanged(); } }
        //Команда сохранить
        public override RelayCommand Save => save ?? (save = new RelayCommand(arg =>
        {
            //Условие на редактирование или добавление
            if (!IsEdit)
            {
                //Добавляем данные в бд
                SQL.ExecuteNonQuery($@"INSERT INTO [dbo].[Record]
                                                        ([ID_Doctor]
                                                        ,[ID_Patient]
                                                        ,[ID_Diseases]
                                                        ,[Date_Time],[Description])
                                                  VALUES
                                                        ({RecordBinding.ID_Doctor}
                                                        ,{RecordBinding.ID_Patient}
                                                        ,{RecordBinding.ID_Diseases}
                                                        ,'{RecordBinding.Date_Time}', '{RecordBinding.Description}');");

                //Вызываем событие что было внесение данных
                InsertDataBase?.Invoke();

            }
            else
            {
                ////Обновляем данные
                SQL.ExecuteNonQuery($@"UPDATE [dbo].[Record]
                                               SET [ID_Doctor] = {RecordBinding.ID_Doctor}
                                                  ,[ID_Patient] = {RecordBinding.ID_Patient}
                                                  ,[ID_Diseases] = {RecordBinding.ID_Diseases}
                                                  ,[Date_Time] = '{RecordBinding.Date_Time}'
                                                  ,[Description] = '{RecordBinding.Description}'
                                               WHERE [ID]={RecordBinding.ID};");
            }

            //Закрываем окно
            Close.Execute(arg);
        }));

        //Команда закрыть окно
        public override RelayCommand Close => close ?? (close = new RelayCommand(arg =>
        {
            if(arg != null)
            (arg as Window).Close();
        }));

        public RelayCommand WDDoctor => wdDoctor ?? (wdDoctor = new RelayCommand(arg =>
        {
            WindowDoctor wd = new WindowDoctor();
            wd.ShowDialog();
            var data = (wd.DataContext as ViewModelDoctor).SelectedDoctorBinding;
            if(data != null)
            {
               // RecordBinding.SDoctor = data.
                RecordBinding.ID_Doctor = data.ID;
                RecordBinding.SDoctor = $"{data.Surname} {data.Name} {data.Patronymic}";
            }
            
        }));
        public RelayCommand WDPatient => wdPatient ?? (wdPatient = new RelayCommand(arg =>
        {
            WindowPatient wd = new WindowPatient();
            wd.ShowDialog();
            var data = (wd.DataContext as ViewModelPatient).SelectedPatientBinding;
            if (data != null)
            {
                RecordBinding.ID_Patient = data.ID;
                RecordBinding.SPatient = $"{data.Surname} {data.Name} {data.Patronymic}";
            }
            
        }));
        public RelayCommand WDDiseases => wdDiseases ?? (wdDiseases = new RelayCommand(arg =>
        {
            WindowDiseases wd = new WindowDiseases();
            wd.ShowDialog();
            var data = (wd.DataContext as ViewModelDiseases).SelectedDiseasesBinding;
            if (data != null)
            {
                RecordBinding.ID_Diseases = data.ID;
                RecordBinding.Name = data.Name;
            }
           
        }));


        private RecordBindingModel recordBinding;
        private RelayCommand wdDoctor;
        private RelayCommand wdPatient;
        private RelayCommand wdDiseases;

    }
}
