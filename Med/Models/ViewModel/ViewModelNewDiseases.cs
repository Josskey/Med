using System;
using System.Windows;
using Med.DB;
using Med.Models.BaseCommand;
using Med.Models.BindingModel;

namespace Med.Models.ViewModel
{
    public class ViewModelNewDiseases : BaseRelayCommand
    {
        //Событие отвечает за внесение данных в бд
        public event Action InsertDataBase;

        public bool IsEdit { get; set; }
        //Модель болезни
        public DiseasesBindingModel DiseasesBinding { get => diseasesBinding; set { diseasesBinding = value; OnPropertyChanged(); } }

        //Команда сохранить
        public override RelayCommand Save => save ?? (save = new RelayCommand(arg =>
        {
            //Условие на редактирование или добавление
            if (!IsEdit)
            {
                //Добавляем данные в бд
                SQL.ExecuteNonQuery($@"INSERT INTO [dbo].[Diseases]
                                              ([Name])
                                        VALUES
                                              ('{DiseasesBinding.Name}');");

                //Вызываем событие что было внесение данных
                InsertDataBase?.Invoke();

            }
            else
            {
                //Обновляем данные
                SQL.ExecuteNonQuery($@"UPDATE [dbo].[Diseases]
                                                SET [Name] = '{DiseasesBinding.Name}' 
                                             WHERE [ID]={DiseasesBinding.ID};");
            }

            //Закрываем окно
            Close.Execute(arg);
        }));

        //Команда закрыть окно
        public override RelayCommand Close => close ?? (close = new RelayCommand(arg =>
        {
            (arg as Window).Close();
        }));

        private DiseasesBindingModel diseasesBinding;
    }
}
