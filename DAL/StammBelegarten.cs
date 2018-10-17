using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class StammBelegarten
    {

        public StammBelegarten()
        {
            Mandantory = false;

        }

        private bool _Mandantory;
        public bool Mandantory
        {
            get { return _Mandantory; }
            set
            {
                if (value != _Mandantory)
                {
                    _Mandantory = value;
                    OnPropertyChanged("Mandantory");

                }
            }
        }

        
    }


}
