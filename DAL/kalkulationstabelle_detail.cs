using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using DAL.Tools;


namespace DAL
{
    public partial class kalkulationstabelle_detail
    {
        private double? prozent;
        private double? KursNOK;
        private double? transport;



        public kalkulationstabelle_detail()
        {


            Recalc();

        }


        public void Recalc()
        {
            Recalc(false);
        }


        public void Recalc(bool reload)
        {

            LoadData(reload);
            if (this.umrechnungeuro.HasValue)
            {
                this.summeposition = this.menge * umrechnungeuro;

                try
                {
                    if (prozent.HasValue)
                    {
                        this.zuschlag = this.umrechnungeuro * (prozent / 100);
                    }
                    if (zuschlag.HasValue)
                    {
                        this.zuschlagpreis = umrechnungeuro + zuschlag;
                    }
                    if (this.zuschlagpreis.HasValue)
                    {
                        if (this.menge.HasValue)
                        {
                            this.gesamtpositionen = this.zuschlagpreis * this.menge;

                        }


                    }



                }
                catch (Exception ex)
                {

                    // LoggingTool.LogExeption(ex, "kalkulationstabelle_detail", "OnumrechnungeuroChanged");
                }


            }

        }


       

        partial void OnumrechnungeuroChanged()
        {
            //using (SteinbachEntities db = new SteinbachEntities())
            //{
            //    var query = from d in db.lagerlisten where d.artikelnr == this.artikelnr select d;
            //    if (query.Count() == 0)
            //    {
            //        if (this.artikelnr != "" && this.artikelnr != string.Empty && this.artikelnr != null)
            //        {


            //            var ll = new lagerliste();
            //            ll.artikelnr = this.artikelnr;
            //            ll.bezeichnung = this.beschreibung;
            //            ll.preiseuro = this.einzelpreis;
            //            db.AddTolagerlisten(ll);
                        
            //        }

            //    }
            //    else
            //    {
            //        foreach (var item in query)
            //        {
            //            if (item.preiseuro < this.einzelpreis)
            //            {
            //                item.preiseuro = this.einzelpreis;
            //            }
                        
            //        }
            //    }

            //    db.SaveChanges();
            //}


            Recalc();


        }

        partial void OnepnokChanged()
        {

            LoadData();

            if (this.epnok.HasValue)
            {
                if (this.epnok != 0)
                {
                    this.umrechnungeuro = this.epnok * KursNOK;
                }
            }
        }


        private void LoadData()
        {
            LoadData(false);

        }
        private void LoadData(bool reload)
        {
            if (prozent == null || transport == null || KursNOK == null || reload == true)
            {


                using (SteinbachEntities db = new SteinbachEntities())
                {
                    var query = from c in db.kalkulationstabellen where c.id == this.id_kalkulationstabelle select c;
                    kalkulationstabelle t = query.FirstOrDefault();
                    if (t != null)
                    {
                        prozent = t.procent;
                        KursNOK = t.euroumrechnung;
                        transport = t.transportverpackung;
                    }
                }

            }

        }


        private void CheckArtikelExists()
        {
            if (artikelnr != null )
            {
                
            }

        }


        partial void OnartikelnrChanged()
        {
            CheckArtikelExists();
        }

        partial void OnbeschreibungChanged()
        {
           CheckArtikelExists();
        }

        partial void Onid_LieferantChanged()
        {
            CheckArtikelExists();
        }

    }
}
