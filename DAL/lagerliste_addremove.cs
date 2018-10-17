using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class lagerliste_addremove
    {

        public lagerliste_addremove()
        {

        }

        private string _Bewegungsart;
        public string Bewegungsart
        {
            get { return _Bewegungsart; }
            set
            {
                if (value != _Bewegungsart)
                {
                    _Bewegungsart = value;
                    OnPropertyChanged("Bewegungsart");
                }
            }
        }

        private string _ProjektnummerFromID;
        public string ProjektnummerFromID
        {
            get { return _ProjektnummerFromID; }
            set
            {
                if (value != _ProjektnummerFromID)
                {
                    _ProjektnummerFromID = value;
                    OnPropertyChanged("ProjektnummerFromID");
                }
            }
        }

        private string _Username;
        public string Username
        {
            get { return _Username; }
            set
            {
                if (value != _Username)
                {
                    _Username = value;
                    OnPropertyChanged("Username");
                    
                }
            }
        }

        private string _Lieferantenname;
        public string Lieferantenname
        {
            get { return _Lieferantenname; }
            set
            {
                if (value != _Lieferantenname)
                {
                    _Lieferantenname = value;
                    OnPropertyChanged("Lieferantenname");
                   
                }
            }
        }

        private string _SchiffUndProjekt;
        public string SchiffUndProjekt
        {
            get { return _SchiffUndProjekt; }
            set
            {
                if (value != _SchiffUndProjekt)
                {
                    _SchiffUndProjekt = value;
                    OnPropertyChanged("SchiffUndProjekt");
                    
                }
            }
        }







        partial void OnaddtypeChanged()
        {
           
        }

        partial void OnidChanged()
        {
            //using (var db = new SteinbachEntities())
            //{
            //    StammBewegungsarten sb = db.StammBewegungsarten.Where(b => b.id == this.addtype).SingleOrDefault();
            //    Bewegungsart = sb.Bewegungsart;

            //    projekt p = db.projekte.Where(b => b.id == id_projekt).SingleOrDefault();
            //    if (p != null)
            //    {
            //        ProjektnummerFromID = p.projektnummer;
            //        SchiffUndProjekt = String.Format("{0} - {1}", p.projektnummer, p.projekt_schiff);
            //    }
            //    else
            //    {
            //        ProjektnummerFromID = "0";
            //        SchiffUndProjekt = " - ";
            //    }

            //    DAL.firma fa = db.firmen.Where(n => n.id == id_Firma).SingleOrDefault();
            //    if (fa != null)
            //    {
            //        Lieferantenname = fa.name;
            //    }
            //    else
            //    {
            //        Lieferantenname = "-";   
            //    }

            //    var benutzer = db.personen.Where(n => n.id == id_user).SingleOrDefault();
            //    if (benutzer != null)
            //    {
            //        Username = benutzer.benutzername;
            //    }
            //    else
            //    {
            //        Username = "-";
            //    }

            //}


        }



        protected override void OnPropertyChanged(string property)
        {
            base.OnPropertyChanged(property);
        }



    }
}
