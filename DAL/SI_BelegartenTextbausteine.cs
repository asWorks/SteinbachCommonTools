using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class SI_BelegartenTextbausteine
    {

        private string _Caption;

        public string Caption
        {
            get { return _Caption; }
            set
            {
                _Caption = value;
                OnPropertyChanged("Caption");
            }
        }
        
        
        partial void Onid_TextbausteinChanged()
        {
            using (var db = new SteinbachEntities())
            {
                if (id_Textbaustein.HasValue && id_Textbaustein != 0)
                {
                    var res = db.StammTextbausteine.Where(n => n.id == id_Textbaustein).SingleOrDefault();
                    if (res != null)
                    {
                         Caption = res.Caption;
                    }
                    else
                    {
                        Caption = String.Format("Textbaustein Nr. {0} wurde nicht gefunden.", id_Textbaustein);
                    }
                  
                }
                
            }
        }
    }
}
