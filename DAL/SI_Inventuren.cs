using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace DAL
{
    public partial class SI_Inventuren
    {

        public string VeranwortlicherName { get; set; }
        public string InventurartName { get; set; }


        partial void OnInventurartChanged()
        {
            try
            {
                using (var db = new SteinbachEntities())
                {
                    InventurartName = db.StammInventuren.Where(id => id.id == Inventurart).SingleOrDefault().Bezeichnung;
                    OnPropertyChanged("InventurartName");
                }
            }
            catch (Exception)
            {


            }

        }


        partial void OnVerantwortlicherChanged()
        {
            try
            {
                if (Verantwortlicher != null)
                {
                    using (var db = new SteinbachEntities())
                    {
                        VeranwortlicherName = db.personen.Where(id => id.id == Verantwortlicher).SingleOrDefault().benutzername;
                        OnPropertyChanged("VerantwortlicherName");
                    }
                }

            }
            catch (Exception)
            {


            }
        }

    }

}