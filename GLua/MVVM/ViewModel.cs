using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLua.MVVM
{
    class ViewModel:ViewModelBase
    {
        Wiki wiki = new Wiki();

        public ViewModel()
        {
            wiki.LoadJsonObj();
            
        }

        public List<Function> Wiki {
            get
            {
                return wiki.Functions;
            }
            set
            {
                if (Equals(wiki.Functions, value))
                {
                    return;
                }            
                wiki.Functions = value;
                OnPropertyChanged("Wiki");
            }
        }
    }
}
