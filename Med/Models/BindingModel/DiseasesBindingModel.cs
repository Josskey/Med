using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Med.Models.BindingModel
{
    public class DiseasesBindingModel : OnPropertyChangedClass
    {
        public int ID { get => id; set { id = value; OnPropertyChanged(); } }
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

        private int id;
        private string name;
    }
}
