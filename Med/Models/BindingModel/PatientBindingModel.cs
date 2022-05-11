using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Med.Models.BindingModel
{
    public class PatientBindingModel : BindingModelBase
    {
        public int Passport { get => passport; set { passport = value; OnPropertyChanged(); } }
        public string Address { get => address; set { address = value; OnPropertyChanged(); } }
        public int Policy_number { get => policy_number; set { policy_number = value; OnPropertyChanged(); } }
        public int Card_number { get => card_number; set { card_number = value; OnPropertyChanged(); } }
        public string Sex { get => sex; set { sex = value; OnPropertyChanged(); } }
        public string DateBirth
        {
            get => dataBirth; set
            {
                dataBirth = value;
                OnPropertyChanged();
            }
        }
        public DateTime SDateBirth
        {
            get => sdataBirth;
            set
            {
                sdataBirth = value;
                dataBirth = value.ToString("dd.MM.yyyy");
                OnPropertyChanged();
            }
        }

        private int passport;
        private string address;
        private int policy_number;
        private int card_number;
        private string sex;
        private string dataBirth;
        private DateTime sdataBirth;

        public PatientBindingModel()
        {
            SDateBirth = DateTime.Now;
        }
    }
}
