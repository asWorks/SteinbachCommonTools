using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class SI_Belege
    {

        public string kundenname { get; set; }
        //  public string firmenname { get; set; }
        //  public string type { get; set; }
        public string projekt_schiff { get; set; }
        public string BelegartName { get; set; }
        //  public string BeziehungPartner { get; set; }
        //public string Projektnummer { get; set; }


        //  private bool _isFirmaEnabled;


        //  public bool isFirmaEnabled
        //  {
        //      get { return _isFirmaEnabled; }
        //      set
        //      {
        //          _isFirmaEnabled = value;
        //          OnPropertyChanged("isFirmaEnabled");

        //      }
        //  }



        //  private bool _isProjektEnabled;
        //  public bool isProjektEnabled
        //  {
        //      get { return _isProjektEnabled; }
        //      set
        //      {
        //          _isProjektEnabled = value;
        //          OnPropertyChanged("isProjektEnabled");

        //      }
        //  }

        //  private string _EKVK;
        //  public string EKVK
        //  {
        //      get { return _EKVK; }
        //      set
        //      {
        //          _EKVK = value;
        //          OnPropertyChanged("EKVK");
        //          if (EKVK.ToLower().Trim() != "vk")
        //          {
        //              isProjektEnabled = false;


        //          }
        //          else
        //          {
        //              isProjektEnabled = isEnabled;

        //          }
        //          if (EKVK.ToLower().Trim() == "intern")
        //          {
        //              isFirmaEnabled = false;
        //          }
        //          else
        //          {
        //              isFirmaEnabled = isEnabled;
        //          }


        //      }
        //  }



        //  private bool _isEnabled;
        //  public bool isEnabled
        //  {
        //      get { return _isEnabled; }
        //      set
        //      {
        //          _isEnabled = value;
        //          OnPropertyChanged("isEnabled");
        //      }
        //  }


        partial void Onid_ProjektChanged()
        {

            if (id_Projekt != null)
            {

                using (var db = new SteinbachEntities())
                {
                    var project = db.projekte.Where(n => n.id == id_Projekt).SingleOrDefault();
                    //kundenname = project.kundenname;
                    //firmenname = project.firmenname;
                    id_Firma = db.firmen.Where(n => n.KdNr == project.KdNr && n.IstKunde == 1).SingleOrDefault().id;
                    //type = project.type;
                    projekt_schiff = project.projekt_schiff;
                    //Projektnummer = project.projektnummer;

                    // OnPropertyChanged("kundenname");
                    //OnPropertyChanged("firmenname");
                    //OnPropertyChanged("type");
                    OnPropertyChanged("projekt_schiff");
                    //OnPropertyChanged("Projektnummer");

                }
            }
            else
            {
                //kundenname = string.Empty;
                //firmenname = string.Empty;
                projekt_schiff = string.Empty;
                //type = string.Empty;
                //Projektnummer = string.Empty;
                //OnPropertyChanged("kundenname");
                //OnPropertyChanged("firmenname");
                //OnPropertyChanged("type");
                OnPropertyChanged("projekt_schiff");
                //OnPropertyChanged("Projektnummer");

            }


        }


        partial void OnBelegartChanged()
        {
            InitBelegart();


        }

        private void InitBelegart()
        {
            if (Belegart != null)
            {
                using (var db = new SteinbachEntities())
                {
                    var ba = db.StammBelegarten.Where(id => id.id == Belegart).SingleOrDefault();
                    if (ba != null)
                    {
                        BelegartName = ba.Belegart;
                        //BeziehungPartner = ba.BeziehungPartner;
                        //EKVK = ba.EKVK;
                        OnPropertyChanged("Belegart");
                        //OnPropertyChanged("BeziehungPartner");


                    }
                }

            }
        }


        //  partial void OnistGebuchtChanged()
        //  {

        //      isEnabled = istGebucht == 1 ? false : true;


        //  }

        partial void Onid_FirmaChanged()
        {

            if (id_Firma != null)
            {
                try
                {
                    using (var db = new SteinbachEntities())
                    {
                        string buf = db.firmen.Where(n => n.id == id_Firma).SingleOrDefault().name;
                        kundenname = buf;
                        OnPropertyChanged("kundenname");
                    }
                }
                catch (Exception)
                {

                    
                }




            }

        }




    }
}
