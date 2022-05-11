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

namespace Med.Models.ViewModel
{
    public class ViewModelNewAdmin : BaseRelayCommand
    {
        //Событие отвечает за внесение данных в бд 
        public event Action InsertDataBase;

        public bool IsEdit { get; set; }
        

        //Модель доктор
        public AuthorizationBindingModel AdminBinding { get => adminBinding; set { adminBinding = value; OnPropertyChanged(); } }

        public bool CheckState { get => checkState; set { checkState = value; OnAllPropertyChanged(); } }

        //Команда сохранить
        public override RelayCommand Save => save ?? (save = new RelayCommand(arg =>
        {
             AdminBinding.Access = CheckState? "Полный" : "Ограниченный";
            //Условие на редактирование или добавление
            if (!IsEdit)
            {
                //Добавляем данные в бд
                SQL.ExecuteNonQuery($@"INSERT INTO [dbo].[Authorizations]
                                                        ([IdDoctor]
                                                        ,[Login]
                                                        ,[Password]
                                                        ,[Access])
                                                  VALUES
                                                        ({AdminBinding.IdDoctor}
                                                        ,'{AdminBinding.Login}'
                                                        ,'{AdminBinding.Password}'
                                                        ,'{AdminBinding.Access}');");

                //Вызываем событие что было внесение данных
                InsertDataBase?.Invoke();

            }
            else
            {
                ////Обновляем данные
                SQL.ExecuteNonQuery($@"UPDATE [dbo].[Authorizations]
                                               SET [IdDoctor] = {AdminBinding.IdDoctor}
                                                  ,[Login] = '{AdminBinding.Login}'
                                                  ,[Password] = '{AdminBinding.Password}'
                                                  ,[Access] = '{AdminBinding.Access}'
                                               WHERE [ID]={AdminBinding.ID};");
            }

            //Закрываем окно
            Close.Execute(arg);
        }));

        //Команда закрыть окно
        public override RelayCommand Close => close ?? (close = new RelayCommand(arg =>
        {
            if (arg != null)
                (arg as Window).Close();
        }));

        public RelayCommand WDDoctor => wdDoctor ?? (wdDoctor = new RelayCommand(arg =>
        {
            WindowDoctor wd = new WindowDoctor();
            wd.ShowDialog();
            var data = (wd.DataContext as ViewModelDoctor).SelectedDoctorBinding;
            if (data != null)
            {
                // RecordBinding.SDoctor = data.
                AdminBinding.IdDoctor = data.ID;
                AdminBinding.Name = $"{data.Surname} {data.Name} {data.Patronymic}";
            }

        }));
      

       

        private AuthorizationBindingModel adminBinding;
        private RelayCommand wdDoctor;
        private bool checkState;

    }
}
