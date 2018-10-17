using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial  class StammTextbaustein
    {

        private string _Sprache;

        public string Sprache
        {
            get { return _Sprache; }
            set
            {
                _Sprache = value;
                OnPropertyChanged("Sprache");
            }
        }
        

        public StammTextbaustein()
        {
            created = DateTime.Now;
  
        }


        partial void Onid_SpracheChanged()
        {
            if (id_Sprache != null)
            {
                using (var db = new SteinbachEntities())
                {
                    var query = db.AuswahlEintraege.Where(n => n.Gruppe == "TypSprache" &&  n.id_This_int == id_Sprache);
                    if (query.Any())
                    {
                        if (query.Count() == 1)
                        {
                            Sprache = query.SingleOrDefault().Eintrag;

                        }
                    }

                }
            }
        }


    }
}
