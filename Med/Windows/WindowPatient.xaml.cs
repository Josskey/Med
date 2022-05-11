using System.Windows;
using System.Windows.Input;

namespace Med.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowPatient.xaml
    /// </summary>
    public partial class WindowPatient : Window
    {
        public WindowPatient()
        {
            InitializeComponent();
        }

        private void WD_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
