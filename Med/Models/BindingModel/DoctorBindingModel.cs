using System;
using System.Windows.Media.Imaging;

namespace Med.Models.BindingModel
{
    public class DoctorBindingModel : BindingModelBase
    {
        
        public BitmapImage Image { get => image; set { image = value; OnPropertyChanged(); } }

        public string img
        {
            get => _img; set
            {
                _img = value;
                Image = new BitmapImage();
                Image.BeginInit();
                Image.UriSource = (_img == "user.jpg") ? new Uri($"{Environment.CurrentDirectory}\\img\\{img}") : new Uri(img);
                Image.EndInit();
                OnPropertyChanged();
            }
        }

        public string Specialization { get => specialization; set { specialization = value; OnPropertyChanged(); } }
        public string ChartWork { get => chartWork; set { chartWork = value; OnPropertyChanged(); } }

        public DoctorBindingModel()
        {
            img = "user.jpg";
        }

       
        private string _img;
        private string specialization;
        private string chartWork;
        private BitmapImage image;



    }
}
