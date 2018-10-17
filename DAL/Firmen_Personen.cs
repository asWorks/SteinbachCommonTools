using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class Firmen_Personen
    {


        public Firmen_Personen()
        {
            FillFirmenAdresse();


        }

        private void FillFirmenAdresse()
        {

            try
            {
                if (id != null && id != 0)
                {
                    if (id_Standort.HasValue && id_Standort != 0)
                    {
                        using (var db = new SteinbachEntities())
                        {
                            var f = db.Firmen_Adressen.Where(i => i.id == id_Standort).SingleOrDefault();
                            if (f != null)
                            {
                                PLZ2 = f.PLZ;
                                Strasse2 = f.Straße;
                                Ort2 = f.Ort;
                            }



                        }

                    }
                }
            }
            catch (Exception)
            {

               
            }



        }

        partial void Onid_StandortChanged()
        {
            FillFirmenAdresse();
        }








        private string _Fullname;

        public string Fullname
        {
            get { return Nachname + ", " + Vorname; }

        }



        public string Kontakt_FullInfo
        {
            get
            {
                return string.Format("{0}, {1}, {2}, {3}", Nachname, Vorname, Abteilung, Funktion);

            }

        }


        private string _Ort2;
        public string Ort2
        {
            get { return _Ort2; }
            set
            {
                if (value != _Ort2)
                {
                    _Ort2 = value;
                    OnPropertyChanged("Ort2");

                }
            }
        }


        private string _Strasse2;
        public string Strasse2
        {
            get { return _Strasse2; }
            set
            {
                if (value != _Strasse2)
                {
                    _Strasse2 = value;
                    OnPropertyChanged("Strasse2");
                }
            }
        }

        private string _PLZ2;
        public string PLZ2
        {
            get { return _PLZ2; }
            set
            {
                if (value != _PLZ2)
                {
                    _PLZ2 = value;
                    OnPropertyChanged("PLZ2");
                }
            }
        }



        private ListboxPersonenKategorienViewModel _ListboxPersonenKategorienVM;
        public ListboxPersonenKategorienViewModel ListboxPersonenKategorienVM
        {
            get { return _ListboxPersonenKategorienVM; }
            set
            {
                if (value != _ListboxPersonenKategorienVM)
                {
                    _ListboxPersonenKategorienVM = value;
                    OnPropertyChanged("ListboxPersonenKategorienVM");
                }
            }
        }

        private ListboxPersonenEventsViewModel _ListboxPersonenEventsVM;
        public ListboxPersonenEventsViewModel ListboxPersonenEventsVM
        {
            get { return _ListboxPersonenEventsVM; }
            set
            {
                if (value != _ListboxPersonenEventsVM)
                {
                    _ListboxPersonenEventsVM = value;
                    OnPropertyChanged("ListboxPersonenEventsVM");
                   
                }
            }
        }



        protected override void OnPropertyChanged(string property)
        {
            base.OnPropertyChanged(property);
        }



    }
}
