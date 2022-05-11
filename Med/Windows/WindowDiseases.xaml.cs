using System.Windows;
using System.Windows.Input;

namespace Med.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowDiseases.xaml
    /// </summary>
    public partial class WindowDiseases : Window
    {
        public WindowDiseases()
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
