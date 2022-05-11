using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Med.DB;
using Med.Models;
using Med.Models.BaseCommand;
using Med.Models.BindingModel;

namespace Med.ViewModel
{
    public class ViewModelAuthorization : BaseRelayCommand
    {
        //Строка подключения
        string connectionstr = @"Data Source=DESKTOP-38F71EP\SQLEXPRESS;Initial Catalog=YouMed;Integrated Security=True";

        //Модель авторизации
        public AuthorizationBindingModel AuthorizationBinding { get => authorizationBinding; set { authorizationBinding = value; OnPropertyChanged(); } }




        //Конструктор
        public ViewModelAuthorization()
        {
            //Задаем строку подключения
            SQL.Connection = connectionstr;

            //Создаем свойство
            AuthorizationBinding = new AuthorizationBindingModel();
        }

        //Команда закрыть окно
        public override RelayCommand Close => close ?? (close = new RelayCommand(arg =>
        {
           if (arg != null)
             (arg as Window).Close();
  
        }));

        public RelayCommand Input => input ?? (input = new RelayCommand(arg =>
        {

            //Выполняем запрос и проходим авторизацию
            var user = SQL.Execute<AuthorizationBindingModel>($"SELECT * FROM [Authorizations] WHERE [Login]='{AuthorizationBinding.Login}'");

            User.Authorization = user;
            //Получаем пароль пользователя
            var password = user.Password;

            ////Проверяем условие
            if (password != null)
            {
                //Проверяем условие на ввод пароля
                if (password == (arg as System.Windows.Controls.PasswordBox).Password)
                {
                   
                    Windows.WindowMed med = new Windows.WindowMed();
                    med.ShowDialog();

                }
                else
                    MessageBox.Show("Неверный пароль!");
            }
            else
                MessageBox.Show($"Учетная запись с \"{AuthorizationBinding.Login}\" отсутствует!");

        }));
        private AuthorizationBindingModel authorizationBinding;

        private RelayCommand input;

    }
}
