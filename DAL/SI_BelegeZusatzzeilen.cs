using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class SI_BelegeZusatzzeilen
    {

        bool DiffValue = false;

        public event Action<bool> DataChanged;

        private void OnDataChanged(bool Sort)
        {
            if (DataChanged != null)
            {
                DataChanged(Sort);
            }
        }

        partial void OnidxChanging(int? value)
        {
            if (idx.HasValue)
            {
                if (value.HasValue && idx != value)
                {
                    DiffValue = true;
                }
                else
                {
                    DiffValue = false;
                }
            }

        }


        partial void OnidxChanged()
        {

            OnDataChanged(DiffValue);
            DiffValue = false;

        }


     
        partial void OnWertChanged()
        {
            if (Wert != 0)
            {
               
                Prozent = 0;
                
                OnPropertyChanged("Wert");
                OnDataChanged(false);
            }
        }

        partial void OnProzentChanged()
        {
            if (Prozent != 0)
            {
                Wert = 0;
                
                OnPropertyChanged("Prozent");
                OnDataChanged(false);
            }

        }
    }
}
