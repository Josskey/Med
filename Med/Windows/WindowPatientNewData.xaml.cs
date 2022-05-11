using System.Windows;
using System.Windows.Input;
using Med.Models.ViewModel;

namespace Med.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowPatientNewData.xaml
    /// </summary>
    public partial class WindowPatientNewData : Window
    {
        public WindowPatientNewData(ViewModelNewPatient newPatient)
        {
            InitializeComponent();
            DataContext = newPatient;
        }

        private void WD_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
