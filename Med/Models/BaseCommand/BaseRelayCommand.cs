using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Med.Models.BaseCommand
{
    public abstract class BaseRelayCommand : OnPropertyChangedClass
    {
        public virtual int ParamSearch { get; set; }
        public virtual string Search { get; set; }

        public virtual RelayCommand Add { get; }
        public virtual RelayCommand Edit { get; }
        public virtual RelayCommand Delete { get; }
        public virtual RelayCommand Save { get; }
        public abstract RelayCommand Close { get; }

        protected RelayCommand add;
        protected RelayCommand edit;
        protected RelayCommand delete;
        protected RelayCommand save;
        protected RelayCommand close;

        protected string search;
        protected int paramSearch;
    }
}
