using System.Windows;
using System.Windows.Input;

namespace Med.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowNewDisease.xaml
    /// </summary>
    public partial class WindowNewDisease : Window
    {
        public WindowNewDisease(Models.ViewModel.ViewModelNewDiseases newDiseases)
        {
            InitializeComponent();
            DataContext = newDiseases;
        }

        private void WD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
