using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Med.DB;
using Med.Models.BaseCommand;
using Med.Models.BindingModel;

namespace Med.Models.ViewModel
{
    
    public class ViewModelNewPatient : BaseRelayCommand
    {
        //Событие отвечает за внесение данных в бд
        public event Action InsertDataBase;

        public bool IsEdit { get; set; }
        //Модель пациент
        public PatientBindingModel PatientBinding { get => patientBinding; set { patientBinding = value; OnPropertyChanged(); } }

        //Команда сохранить
        public override RelayCommand Save => save ?? (save = new RelayCommand(arg =>
        {
            //var dateFormat = PatientBinding.DateBirth.ToString("dd.MM.yyyy");
            //Условие на редактирование или добавление
            if (!IsEdit)
            {
                //Добавляем данные в бд
                SQL.ExecuteNonQuery($@"INSERT INTO [dbo].[Patient]
                                              ([Surname]
                                              ,[Name]
                                              ,[Patronymic]
                                              ,[Passport]
                                              ,[Address]
                                              ,[Phone]
                                              ,[Policy_number]
                                              ,[Card_number]
                                              ,[Sex]
                                              ,[DateBirth])
                                        VALUES
                                              ('{PatientBinding.Surname}'
                                              ,'{PatientBinding.Name}'
                                              ,'{PatientBinding.Patronymic}'
                                              ,{PatientBinding.Passport}
                                              ,'{PatientBinding.Address}'
                                              ,'{PatientBinding.Phone}'
                                              ,{PatientBinding.Policy_number}
                                              ,{PatientBinding.Card_number}
                                              ,'{PatientBinding.Sex}'
                                              ,'{PatientBinding.DateBirth}');");

                //Вызываем событие что было внесение данных
                InsertDataBase?.Invoke();
            }
            else
            {
                //Обновляем данные
                SQL.ExecuteNonQuery($@"UPDATE [dbo].[Patient]
                                               SET [Surname] = '{PatientBinding.Surname}'
                                                  ,[Name] = '{PatientBinding.Name}'
                                                  ,[Patronymic] = '{PatientBinding.Patronymic}'
                                                  ,[Passport] = {PatientBinding.Passport}
                                                  ,[Address] = '{PatientBinding.Patronymic}'
                                                  ,[Phone] = '{PatientBinding.Phone}'
                                                  ,[Policy_number] = {PatientBinding.Policy_number}
                                                  ,[Card_number] = {PatientBinding.Card_number}
                                                  ,[Sex] = '{PatientBinding.Sex}'
                                                  ,[DateBirth] = '{PatientBinding.DateBirth}'
                                             WHERE [ID]={PatientBinding.ID};");
            }

            //Закрываем окно
            Close.Execute(arg);
        }));

        //Команда закрыть окно
        public override RelayCommand Close => close ?? (close = new RelayCommand(arg =>
        {
            (arg as Window).Close();
        }));
   
        private PatientBindingModel patientBinding;

    }
}
