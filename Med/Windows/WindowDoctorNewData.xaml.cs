using System.Windows;
using System.Windows.Input;
using Med.ViewModel;

namespace Med.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowNewData.xaml
    /// </summary>
    public partial class WindowNewData : Window
    {
        public WindowNewData(ViewModelNewDoctor NewDoctor)
        {
            InitializeComponent();
            DataContext = NewDoctor;
        }

        private void WD_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
