using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace DAL
{
    public partial class projekt
    {
      
         public bool isVisible { get; set; }
         public bool isEnabled { get; set; }
         private string _KundennameFromKdNr;

         public string KundennameFromKdNr
         {
             get { return _KundennameFromKdNr; }
             set
             {
                 _KundennameFromKdNr = value;
                 OnPropertyChanged("KundennameFromKdNr");
             
             }
         }
         
        


        public projekt()
        {
            isVisible = true;
            isEnabled = true;
            auftrag = 0;
            si = 0;
            landx = 0;
            schiff = 0;

        }


        partial void OnKdNrChanged()
        {
            if (id != null)
            {
                using (var db = new SteinbachEntities())
                {
                    var query = db.firmen.Where(n => n.KdNr == KdNr && n.IstKunde == 1);
                    if (query.Any())
                    {
                        if (query.Count() == 1)
                        {
                            KundennameFromKdNr = query.SingleOrDefault().name;
                          
                        }
                    }

                }
            }
        }

     

    }
}
