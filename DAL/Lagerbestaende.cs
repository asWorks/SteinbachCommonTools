using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class Lagerbestaende
    {

        public bool newItem { get; set; }


        private string _Bezeichnung;
        public string Bezeichnung
        {
            get { return _Bezeichnung; }
            set
            {
                if (value != _Bezeichnung)
                {
                    _Bezeichnung = value;
                   OnPropertyChanged("Bezeichnung");
                   
                }
            }
        }

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


        partial void Onid_LagerlisteChanged()
        {
            if (id_Lagerliste != null)
            {
                using (var db = new SteinbachEntities())
                {
                    var art = db.lagerlisten.Where(n => n.id == id_Lagerliste).SingleOrDefault();
                    Bezeichnung = art.bezeichnung;
                    Artikelnummer = art.artikelnr;

                }
            }
        }


        public Lagerbestaende()
        {
            newItem = false;
        }


    }
}
