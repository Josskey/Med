using System.Windows;
using System.Windows.Input;

namespace Med.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowRecord.xaml
    /// </summary>
    public partial class WindowRecord : Window
    {
        public WindowRecord()
        {
            InitializeComponent();
        }

        private void WD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
