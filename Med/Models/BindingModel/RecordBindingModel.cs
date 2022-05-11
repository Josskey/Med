using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Med.Models.BindingModel
{
    public class RecordBindingModel : OnPropertyChangedClass
    {
        public int ID { get => id; set => id = value; }
        public string SDoctor { get => sDoctor; set { sDoctor = value; OnPropertyChanged(); } }
        public string SPatient { get => spatient; set { spatient = value; OnPropertyChanged(); } }
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        public int Policy_number { get => policy_number; set { policy_number = value; OnPropertyChanged(); } }
        public string Date_Time { get => date_Time; set { date_Time = value; OnPropertyChanged(); } }

        public int ID_Doctor { get => id_Dorctor; set { id_Dorctor = value; OnPropertyChanged(); } }
        public int ID_Patient { get => id_Patient; set { id_Patient = value; OnPropertyChanged(); } }
        public int ID_Diseases { get => id_Sieases; set { id_Sieases = value; OnPropertyChanged(); } }

        public string Description { get => description; set { description = value; OnPropertyChanged(); } }

        public DateTime SDate_Time { get => sdate_Time; set { sdate_Time = value; date_Time = value.ToString("dd.MM.yyyy"); OnPropertyChanged(); } }

        public int Card_number { get => card_number; set { card_number = value; OnPropertyChanged(); } }

        public RecordBindingModel()
        {
            SDate_Time = DateTime.Now;
        }

        private int policy_number;
        private string sDoctor;
        private string spatient;
        private string name;
        private int id_Dorctor;
        private int id_Patient;
        private int id_Sieases;
        private int id;
        private string date_Time;
        private DateTime sdate_Time;
        private string description;
        private int card_number;

    }
}
