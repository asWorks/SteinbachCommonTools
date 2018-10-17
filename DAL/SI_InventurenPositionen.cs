using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class SI_InventurenPositionen
    {

        #region "PropertyChanged"

        partial void OnSollmengeChanged()
        {
            Differenz = Zaehlmenge - Sollmenge;
        }
        partial void OnZaehlmengeChanged()
        {
            if ((IstGebucht.HasValue && IstGebucht == 1) && AfterInit)
            {
                ResetZaehlmenge();
            }
            else
            {
                Differenz = Zaehlmenge - Sollmenge;
            }



        }

        partial void OnDifferenzChanged()
        {
            OnPropertyChanged("Differenz");
        }
        partial void OnidChanged()
        {

        }

        partial void Onid_artikelChanged()
        {
            initArtikel();
        }

        partial void OnIstGebuchtChanged()
        {
            if (IstGebucht.HasValue)
            {
                if (IstGebucht == 1)
                {
                    isDeletingEnabled = false;
                }
                else
                {
                    isDeletingEnabled = true;
                }

            }
        }

        partial void OnZaehlmengeChanging(int? value)
        {
            
        }
        #endregion

        #region "Properties"

        bool ResetDone = false;
        private string _Artikelnummer;
        public string Artikelnummer
        {
            get { return _Artikelnummer; }
            set
            {
                if (value != _Artikelnummer)
                {
                    _Artikelnummer = value;
                    OnPropertyChanged("Artikelnummer");
                }
            }
        }

        private string _Artikelname;
        public string Artikelname
        {
            get { return _Artikelname; }
            set
            {
                if (value != _Artikelname)
                {
                    _Artikelname = value;
                    OnPropertyChanged("Artikelname");

                }
            }
        }

        private bool _isDeletingEnabled;
        public bool isDeletingEnabled
        {
            get { return _isDeletingEnabled; }
            set
            {
                if (value != _isDeletingEnabled)
                {
                    _isDeletingEnabled = value;
                    _isEditingEnabled = !value;
                    OnPropertyChanged("isDeletingEnabled");

                }
            }
        }

        private bool _isEditingEnabled;

        public bool isEditingEnabled
        {
            get { return _isEditingEnabled; }
            set
            {
                _isEditingEnabled = value;
                OnPropertyChanged("isEditingEnabled");

            }
        }

        private string _lagerort;

        public string Lagerort
        {
            get { return _lagerort; }
            set
            {
                _lagerort = value;
                OnPropertyChanged("Lagerort");

            }
        }

        private bool _AfterInit;

        public bool AfterInit
        {
            get { return _AfterInit; }
            set
            {
                _AfterInit = value;
                if (AfterInit == true)
                {
                    OldZaehlmenge = (int)Zaehlmenge;
                }

            }
        }




        private int OldZaehlmenge;


        #endregion


        #region "Constructors"
        public SI_InventurenPositionen()
        {
            AfterInit = false;

        }
        #endregion

        #region "Functions"

        void ResetZaehlmenge()
        {
            if (ResetDone)
            {
                ResetDone = false;
            }
            else
            {
                ResetDone = true;
                Zaehlmenge = OldZaehlmenge;

            }

        }

        private void initArtikel()
        {

            if (id_artikel != null && id_artikel != 0)
            {
                using (var db = new SteinbachEntities())
                {
                    var res = db.lagerlisten.Where(t => t.id == id_artikel).SingleOrDefault();
                    Artikelnummer = res.artikelnr;
                    Artikelname = res.bezeichnung;
                    //OldZaehlmenge = Zaehlmenge.HasValue ? (int)Zaehlmenge : 0;
                    Lagerort = String.Format("{0} / {1}", res.ortregal, res.ortbox);
                    int buf = 0;
                    foreach (var item in res.Lagerbestaende)
                    {
                        buf += item.Lagerbestand.HasValue ? (int)item.Lagerbestand : 0;
                    }
                    Sollmenge = buf;

                }
            }



        }

        #endregion





    }
}

