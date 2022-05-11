using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Med.Models.BindingModel
{
    public class BindingModelBase : OnPropertyChangedClass
    {
        public int ID { get => id; set { id = value; OnPropertyChanged(); } }
        public string Surname { get => surname; set { surname = value; OnPropertyChanged(); } }
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        public string Patronymic { get => patronymic; set { patronymic = value; OnPropertyChanged(); } }
        public string Phone { get => phone; set { phone = value; OnPropertyChanged(); } }

        private int id;
        private string surname;
        private string name;
        private string patronymic;
        private string phone;
    }
}
