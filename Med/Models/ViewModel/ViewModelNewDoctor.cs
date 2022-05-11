using System;
using System.IO;
using System.Windows;
using Med.DB;
using Med.Models.BaseCommand;
using Med.Models.BindingModel;
using Microsoft.Win32;

namespace Med.ViewModel
{
    
    public class ViewModelNewDoctor : BaseRelayCommand
    {
        //Событие отвечает за внесение данных в бд
        public event Action InsertDataBase;

        public bool IsEdit { get; set; }

        //Модель доктор
        public DoctorBindingModel DoctorBinding { get => doctorBinding; set { doctorBinding = value; OnPropertyChanged(); } }

        //Команда сохранить
        public override RelayCommand Save => save ?? (save = new RelayCommand(arg =>
        {
            //Условие на редактирование или добавление
            if (!IsEdit)
            {
                //Добавляем данные в бд
                SQL.ExecuteNonQuery($@"INSERT INTO [dbo].[Doctor]
                                              ([Surname]
                                              ,[Name]
                                              ,[Patronymic]
                                              ,[Phone]
                                              ,[img],[ChartWork],[Specialization])
                                        VALUES
                                              ('{DoctorBinding.Surname}'
                                              ,'{DoctorBinding.Name}'
                                              ,'{DoctorBinding.Patronymic}'
                                              ,'{DoctorBinding.Phone}'
                                              ,'{DoctorBinding.img}', '{DoctorBinding.ChartWork}', '{DoctorBinding.Specialization}');");

                //Вызываем событие что было внесение данных
                InsertDataBase?.Invoke();

            }
            else
            {
                //Обновляем данные
                SQL.ExecuteNonQuery($@"UPDATE [dbo].[Doctor]
                                               SET [Surname] = '{DoctorBinding.Surname}'
                                                  ,[Name] = '{DoctorBinding.Name}'
                                                  ,[Patronymic] = '{DoctorBinding.Patronymic}'
                                                  ,[Phone] = '{DoctorBinding.Phone}'
                                                  ,[img] = '{DoctorBinding.img}'
                                                  ,[ChartWork] ='{DoctorBinding.ChartWork}'
                                                  ,[Specialization] ='{DoctorBinding.Specialization}'
                                               WHERE [ID]={DoctorBinding.ID};");
            }

            
            //Закрываем окно
            Close.Execute(arg);

        }));

        //Диалоговое окно для выбора фото
        OpenFileDialog dialog = new OpenFileDialog();
        string pathMove = "";
        //Команда загрузить фото
        public RelayCommand LoadImage => load ?? (load = new RelayCommand(arg =>
        {
           
            dialog.DefaultExt = ".png";
            dialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
            if (dialog.ShowDialog().Value)
            {

                string from = dialog.FileName;
                string to = $"{Environment.CurrentDirectory}\\img\\{Path.GetFileName(dialog.FileName)}";
                if (File.Exists(to))
                    DoctorBinding.img = to;
                else
                    File.Copy(from, to);
            }

        }));

        //Команда закрыть окно
        public override RelayCommand Close => close ?? (close = new RelayCommand(arg =>
        {
            (arg as Window).Close();
        }));

        //private RelayCommand save;
        private RelayCommand load;

        private DoctorBindingModel doctorBinding;

    }
}
