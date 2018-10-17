using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.WaWiTools
{
    public class SI_BelegeHelper
    {


        public const string ArtikelnummerNichtVorhandenMessage = "Die Artikelnummer existiert nicht";
        public const string ArtikelnummerKannNichtNullOderLeerSeinMessage = "Es wurde keine Artikelnummer angegeben.";
        public const string ArtikelnummerMehrfachVorhandenMessage = "Die Artikelnummer existiert mehrfach";
        public const string BelegartKannNichtNullSeinMessage = "Die Belegart kann nicht null sein";
        public const string ArgumentKannNichtNullSeinMessage = "Das Argument kann nicht null sein";
        private const string KalkulationstabelleOderArtikelnummerNichtGefundenMessage = "Die Kalkulationstabelle oder die Artikelnummer wurden nicht gefunden";


        public static decimal GetSelectedCalculationArticlePrice(kalkulationstabelle SelectedKalkulation, string ArtikelNr)
        {

            try
            {
                if (SelectedKalkulation == null)
                {
                    throw new ArgumentOutOfRangeException("Es wurde keine Kalulationstabelle ausgewählt.", KalkulationstabelleOderArtikelnummerNichtGefundenMessage);
                }

                if (SelectedKalkulation.kalkulationstabelle_detail == null)
                {
                    throw new ArgumentOutOfRangeException("Die ausgewählte Kalulationstabelle enthält keine Positionen.", KalkulationstabelleOderArtikelnummerNichtGefundenMessage);
                }

                var ktp = SelectedKalkulation.kalkulationstabelle_detail.Where(n => n.artikelnr == ArtikelNr);
               
                if (!ktp.Any())
                {
                   throw new ArgumentOutOfRangeException("Die ausgewählte Artikelnummer ist in der Kalkulationstabelle nicht enthalten.", KalkulationstabelleOderArtikelnummerNichtGefundenMessage); 
                }
                if (ktp.Count()>1)
                {
                   throw new ArgumentOutOfRangeException("Die ausgewählte Artikelnummer ist in der Kalkulationstabelle mehrfach vorhanden.", ArtikelnummerMehrfachVorhandenMessage);  
                }

                
                if (ktp == null)
                {
                    throw new ArgumentOutOfRangeException(ArtikelNr, "Der Artikel ist nicht in der ausgewählten Kalulationstabelle enthalten.", KalkulationstabelleOderArtikelnummerNichtGefundenMessage);
                }

                if (ktp.SingleOrDefault().einzelpreis.HasValue)
                {
                    return Math.Round((decimal)ktp.SingleOrDefault().einzelpreis, 2);
                }
                else
                {
                    return 0m;
                }

            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw;
            }


            catch (Exception)
            {

                throw;
            }



        }

        public static decimal GetArtikelpriceArtikelstamm(string artikelNr)
        {

            try
            {
                using (var db = new SteinbachEntities())
                {
                    var art = db.lagerlisten.Where(n => n.artikelnr == artikelNr);
                    if (art == null)
                    {
                        throw new ArgumentOutOfRangeException("Artikelnummer", artikelNr, ArtikelnummerNichtVorhandenMessage);
                    }

                    if (art.Count() > 1)
                    {
                        throw new ArgumentOutOfRangeException("Artikelnummer", artikelNr, ArtikelnummerMehrfachVorhandenMessage);
                    }

                    if (art.SingleOrDefault().preiseuro.HasValue)
                    {
                        return Math.Round((decimal)art.SingleOrDefault().preiseuro, 2);
                    }
                    else
                    {
                        return 0m;
                    }
                }

            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }




        }


        public static decimal GetArticlePrice(kalkulationstabelle Kalkulationstabelle, StammBelegarten Belegart, string ArticleNr)
        {

            try
            {
                if (Belegart == null)
                {
                    throw new ArgumentOutOfRangeException("Es wurde keine Belegart ausgewählt - Preis kann nicht ermittelt werden", BelegartKannNichtNullSeinMessage);
                }

                if (!Belegart.Kalkulationstabellenpflicht.HasValue || Belegart.Kalkulationstabellenpflicht == 0)
                {
                    return DAL.WaWiTools.SI_BelegeHelper.GetArtikelpriceArtikelstamm(ArticleNr);
                }
                else
                {
                    return DAL.WaWiTools.SI_BelegeHelper.GetSelectedCalculationArticlePrice(Kalkulationstabelle, ArticleNr);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw;
            }

            catch (Exception)
            {

                throw;
            }

        }

        public static int? CreateArtikel(string artNr, double? preis, string bezeichnung, string einheit, int? hersteller,bool  Handelsware)
        {
            try
            {
                using (var db = new SteinbachEntities())
                {
                    if (artNr == string.Empty || artNr == null)
                    {
                        throw new ArgumentNullException("Artikelmnummer", ArtikelnummerKannNichtNullOderLeerSeinMessage);
                    }



                    var Art = new lagerliste();
                    Art.created = DateTime.Now;
                    Art.artikelnr = artNr;
                    Art.bezeichnung = bezeichnung;
                    Art.beschreibungeng = bezeichnung;
                    Art.beschreibung = bezeichnung;
                    Art.einheit = einheit;
                    Art.id_lieferant = hersteller;
                    Art.Handelsware = Handelsware == true ? (short)1 : (short)0;

                    db.AddTolagerlisten(Art);
                    db.SaveChanges();
                    return Art.id;

                }
                
            }
            catch (Exception)
            {
                throw;
               

            }
            
        }

    }
}
