using System.Windows;
using System.Windows.Input;
using Med.Models.ViewModel;

namespace Med.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowNewAdmin.xaml
    /// </summary>
    public partial class WindowNewAdmin : Window
    {
        public WindowNewAdmin(ViewModelNewAdmin newAdmin)
        {
            InitializeComponent();
            DataContext = newAdmin;
        }

        private void WD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
