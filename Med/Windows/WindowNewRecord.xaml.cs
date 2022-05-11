using System.Windows;
using System.Windows.Input;
using Med.Models.ViewModel;

namespace Med.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowNewRecord.xaml
    /// </summary>
    public partial class WindowNewRecord : Window
    {
        public WindowNewRecord(ViewModelNewRecord newRecord)
        {
            InitializeComponent();
            DataContext = newRecord;
        }

        private void WD_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
