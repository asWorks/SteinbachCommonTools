using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Media;

namespace DAL
{
    public partial class SI_BelegePositionen
    {
        public enum ChangeType
        {
            id_Artikel,
            Preis,
            Rabatt
        }


        #region "events"
        public event Action<DAL.DataTypes.SI_BelegePositionChangedEventArgs, ChangeType> PositionChanged;
        public event Action DataChanged;

        private void OnDataChanged()
        {
            if (DataChanged != null)
            {
                DataChanged();
            }
        }

        private void OnPositionChanged(ChangeType Type)
        {
            if (PositionChanged != null)
            {
                DAL.DataTypes.SI_BelegePositionChangedEventArgs PosArgs = new DataTypes.SI_BelegePositionChangedEventArgs(this);
                PositionChanged(PosArgs, Type);
            }



        }

        #endregion


        #region "Properties"
        private bool bPreisChanged = false;
        private decimal? bufferPreis = 0;
        private bool bMengeChanged = false;
        private double? bufferMenge = 0;
        public string Kennzeichen { get; set; }


        private double _WidthPreis;
        public double WidthPreis
        {
            get { return _WidthPreis; }
            set
            {
                _WidthPreis = value;
                OnPropertyChanged("WidthPreis");
            }
        }

        private bool _PreiseAnzeigen;
        public bool PreiseAnzeigen
        {
            get { return _PreiseAnzeigen; }
            set
            {


                if (value == false)
                {
                    WidthPreis = 0;
                    HeaderPreis = "";
                    HeaderPreisGesamt = "";
                }
                else
                {
                    WidthPreis = 100;
                    HeaderPreis = "Preis";
                    HeaderPreisGesamt = "Gesamt";
                }

                _PreiseAnzeigen = value;
                OnPropertyChanged("PreiseAnzeigen");
                OnPropertyChanged("HeaderPreis");
                OnPropertyChanged("HeaderPreisGesamt");


            }
        }

        private bool _EditingEnabled;
        public bool EditingEnabled
        {
            get { return _EditingEnabled; }
            set
            {
                _EditingEnabled = value;
                OnPropertyChanged("EditingEnabled");
            }
        }

        public string HeaderPreisGesamt { get; set; }
        public string HeaderPreis { get; set; }

        public int? BestandLO { get; set; }

        private Brush _PriceBrush;

        public Brush PriceBrush
        {
            get { return _PriceBrush; }
            set { _PriceBrush = value; }
        }



        private Brush _BackgroundX;
        public Brush BackgroundX
        {
            get { return _BackgroundX; }
            set
            {
                _BackgroundX = value;
                OnPropertyChanged("BackgroundX");

            }
        }

        private kalkulationstabelle _Kalkulationstabelle;
        public kalkulationstabelle Kalkulationstabelle
        {
            get { return _Kalkulationstabelle; }
            set
            {
                if (value != _Kalkulationstabelle)
                {
                    _Kalkulationstabelle = value;
                    OnPropertyChanged("Kalkulationstabelle");

                }
            }
        }

        private StammBelegarten _Belegart;
        public StammBelegarten Belegart
        {
            get { return _Belegart; }
            set
            {
                if (value != _Belegart)
                {
                    _Belegart = value;
                    if (value != null)
                    {
                        if (value.Kalkulationstabellenpflicht == null || value.Kalkulationstabellenpflicht == 0)
                        {
                            BackgroundX = Brushes.Moccasin;
                        }
                        else
                        {
                            BackgroundX = Brushes.Lime;
                        }
                    }
                    OnPropertyChanged("Belegart");

                }
            }
        }

        #endregion

        #region "Internal Events"
        #endregion

        #region "Functions"






        void CalcGesamt()
        {
            if (Preis.HasValue && Menge.HasValue)
            {
                if (Belegart != null)
                {
                    if (Preis.HasValue && Menge.HasValue)
                    {
                        Endpreis = Preis * (decimal)Menge;
                    }
                    else
                    {
                        Endpreis = 0;
                    }

                    // OnPropertyChanged("PreisGesamt");
                    OnPropertyChanged("Endpreis");
                    OnDataChanged();
                }

            }

        }

        private void RechneRabatt()
        {
            if (PreisVorRabatt.HasValue)
            {
                decimal r = 0m;
                if (Rabatt.HasValue)
                {
                    r = (decimal)Rabatt;
                }

                //decimal rSumme = (decimal)PreisVorRabatt * (((decimal)Rabatt * -1) / 100);
                decimal rSumme = (decimal)PreisVorRabatt * ((r * -1) / 100);
                Preis = PreisVorRabatt - rSumme;
                CalcGesamt();
            }
        }

        public int GetLagerbestand(int LagerOrtID)
        {
            if (_id_Artikel != null)
            {
                using (var db = new SteinbachEntities())
                {
                    var lo = db.Lagerbestaende.Where(n => n.id_Lagerliste == id_Artikel && n.id_Lagerort == LagerOrtID).SingleOrDefault();
                    if (lo != null)
                    {
                        BestandLO = lo.Lagerbestand;
                        return (int)lo.Lagerbestand;
                    }

                }


            }

            return 0;

        }



        private void SetPreiseAnzeigen()
        {
            if (SI_Belege != null)
            {

                var b = (SI_Belege)SI_Belege;
                var ba = (StammBelegarten)b.StammBelegarten;
                PreiseAnzeigen = Convert.ToBoolean(ba.PreiseAnzeigen);
                //  EditingEnabled = b.istGebucht == 0 ? true : false;
                {

                }

            }
            else
            {
                PreiseAnzeigen = false;
            }
        }
        #endregion

        #region "Constructors"

        public SI_BelegePositionen(StammBelegarten ba)
            : base()
        {
            Belegart = ba;
            SetPreiseAnzeigen();
        }


        public SI_BelegePositionen()
            : base()
        {
            SetPreiseAnzeigen();
        }
        #endregion

        partial void OnistGebuchtChanged()
        {
            EditingEnabled = istGebucht == 1 ? false : true;
            OnPropertyChanged("EditingEnabled");
            OnPropertyChanged("istGebucht");

        }

        partial void OnPreisVorRabattChanged()
        {
            RechneRabatt();
        }


        partial void OnRabattChanged()
        {
            RechneRabatt();


        }




        partial void Onid_ArtikelChanged()
        {

            try
            {

                if (id_Artikel != null)
                {
                    using (var db = new SteinbachEntities())
                    {

                        lagerliste art;
                        var artQuery = db.lagerlisten.Where(n => n.id == id_Artikel);

                        if (artQuery.Any())
                        {
                            art = artQuery.SingleOrDefault();
                        }
                        else
                        {
                            return;
                        }


                        Einheit = art.einheit;
                        Bezeichnung = art.bezeichnung;
                        Beschreibung = art.beschreibung;
                        Description = art.beschreibungeng;
                        Artikelnummer = art.artikelnr;
                        Handelsware = art.Handelsware;

                        if (Belegart != null)
                        {
                            if (Kennzeichen != "calc")
                            {
                                PreisVorRabatt = WaWiTools.SI_BelegeHelper.GetArticlePrice(Kalkulationstabelle, Belegart, Artikelnummer);
                                Preis = PreisVorRabatt;
                            }



                        }




                        wkz = art.wkz;

                        CalcGesamt();
                        OnPositionChanged(ChangeType.id_Artikel);

                        OnPropertyChanged("Bezeichnung");
                        OnPropertyChanged("Artikelnummer");
                        OnPropertyChanged("Preis");
                        EditingEnabled = istGebucht == 1 ? false : true;
                        OnPropertyChanged("EditingEnabled");
                        OnPropertyChanged("PreisGesamt");
                        OnPropertyChanged("Endpreis");
                    }
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                BackgroundX = Brushes.Red;
                throw new ArgumentOutOfRangeException(ex.Message);
            }

            catch (Exception ex)
            {

                throw;
            }



        }


        partial void OnMengeChanging(double? value)
        {
            if (_istGebucht == 1)
            {

                if (!bMengeChanged)
                {
                    if (value != Menge)
                    {
                        bufferMenge = Menge;
                        bMengeChanged = true;
                    }
                }
                else
                {
                    bMengeChanged = false;
                    bufferMenge = 0;
                }



            }
        }

        partial void OnMengeChanged()
        {

            if (Belegart != null)
            {
                if (bMengeChanged)
                {
                    Menge = bufferMenge;

                }


            }


            CalcGesamt();
            OnPropertyChanged("Menge");
        }





        partial void OnPreisChanging(decimal? value)
        {
            if (_istGebucht == 1)
            {

                if (!bPreisChanged)
                {
                    if (value != Preis)
                    {
                        bufferPreis = Preis;
                        bPreisChanged = true;
                    }
                }
                else
                {
                    bPreisChanged = false;
                    bufferPreis = 0;
                }
            }
        }

        partial void OnPreisChanged()
        {
            if (bPreisChanged)
            {
                Preis = bufferPreis;

            }

            CalcGesamt();
            OnPropertyChanged("Preis");
        }



    }
}
