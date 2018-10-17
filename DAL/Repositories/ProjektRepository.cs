using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.ObjectCollections;




namespace DAL.Repositories
{
    public class ProjektRepository
    {

            public int RecordCount { get; private set; }
            private SteinbachEntities db;

            public ProjektRepository(SteinbachEntities  dbContext)
            {
                db = dbContext;

            }

         

            public bvAuftragsBestand GetLikeTest2(string filter, int toSkip, int toTake)
            {
                //string f = string.Empty;
                //StringBuilder sb = new StringBuilder();

                //foreach (var e in filter)
                //{
                //    sb.Append("it.");
                //    sb.Append(e.Key);
                //    sb.Append(" like ");
                //    sb.Append("'");
                //    sb.Append(e.Value);
                //    sb.Append("'");
                //    sb.Append(" and ");

                //}

                //f = sb.ToString();
                //if (f.EndsWith(" and "))
                //    f = f.Remove(f.LastIndexOf(" and "), 5);

                System.Data.Objects.ObjectQuery<vwBrunvollAuftragsbestand> p;
                if (filter == string.Empty)
                {
                    p = db.vwBrunvollAuftragsbestand
                          .OrderBy("it.projektnummer desc");
                }
                else
                {

                    p = db.vwBrunvollAuftragsbestand
                        .Where(filter)                
                       .OrderBy("it.projektnummer desc");
                }

                RecordCount = p.Count();

                var res = new bvAuftragsBestand(p.Skip(toSkip).Take(toTake), db);
                return res;
            }

            public ProjektCollection GetAlleProjekte()
            {
                var result = from p in db.projekte.Include("projekt_verlauf")
                             select p;
                var pc = new ProjektCollection(result, this.db);

                return pc;
            }

            public ProjektCollection GetProjekteListe()
            {
                var result = from p in db.projekte     
                             where p.datum >= new DateTime(2011,05,1) orderby p.projektnummer descending
                             select p;

                var pc = new ProjektCollection(result, this.db);
                return pc;
            }

            public ProjektCollection GetProjekteListe(int page,int rowsPerPage)
            {

                int toSkip = (page - 1) * rowsPerPage;

                var result = from p in db.projekte
                             where p.datum >= new DateTime(2004, 01, 1)
                             orderby p.projektnummer descending
                             select p;

                var pc = new ProjektCollection(result.Skip(toSkip).Take(rowsPerPage), this.db);
                return pc;
            }



            public ProjektCollection GetUntergeordneteProjekte(int ProjektID)
            {
            
              var buf = from n in db.projekte
                      where n.id == ProjektID
                      select n.projektnummer;

            string pNumber = buf.FirstOrDefault();
            
            var ur = from up in db.projekte
                     where up.ursprungsprojekt == pNumber
                     select up;

            var pc = new ProjektCollection(ur, this.db);
            return pc;
            
            }


            public ProjektCollection GetProjekteByID(int ProjektID)
            {
                var result = from p in db.projekte          //.Include("projekt_verlauf")
                             where p.id == ProjektID
                             select p;
               
                var pc = new ProjektCollection(result, this.db);

                return pc;
            }

            public ProjektCollection GetLeerprojekt()
            {
                var result = from p in db.projekte
                             where p.id == 0
                             select p;
                var pc = new ProjektCollection(result, this.db);

                return pc;
            }

            public int GetProjektIDFromNumber(string PNumber)
           {

               var query = from i in db.projekte
                           where i.projektnummer == PNumber
                           select i.id;
               
               return query.First(); 
           }

          /// <summary>
          /// Gibt eine Liste der ausführenden Firmen ohne Kunden zurück.
          /// </summary>
          /// <returns></returns>
           public IEnumerable<firma> GetKunden()
           {
               var query = from f in db.firmen where f.istFirma == 0
                           orderby f.name
                           select f;
               return query;
           
           }

        /// <summary>
        /// Gibt eine Liste der Kunden ohne die ausführenden Firman zurück.
        /// </summary>
        /// <returns></returns>
           public IEnumerable<firma> GetFirmen()
           {
               var query = from f in db.firmen
                           where f.istFirma == 1
                           orderby f.name
                           select f;
               return query;

           }

           public IEnumerable<schiff> GetSchiff()
           {
               var schiff = from s in db.schiffe
                            orderby s.name
                            select s;

               return schiff;
           
           
           }

           public IEnumerable<StammProjektTyp> GetProjektTyp()
           {
               var type = from t in db.StammProjektTypen
                            orderby t.Projekttyp
                            select t;

               return type;


           }

           public string GetNewProjektnummer()
           {
               var max = from p in db.projekte
                            select p.id;
               int num = max.Max();

               var number = from n in db.projekte
                            where n.id == num
                            select n.projektnummer;


                int newnum = int.Parse(number.First()) + 1;

               return newnum.ToString();

           }

           public string GetNewProjektnummerWithYear()
           {

               string year = DateTime.Now.ToString("yy");
               var number = from n in db.projekte
                            where  n.projektnummer.Substring(0,2)== (year.Trim())
                            select n.projektnummer;


               int newnum = number.Count() + 1;
               string pNum = year.Trim() + newnum; 
               return pNum;

           }

           public ProjektCollection GetLikeTest()
           {
               var test = from p in db.projekte
                          where p.projektnummer.Contains("111")
                          select p;
               var res = new ProjektCollection(test, db);
               return res;

           }

           public ProjektCollection GetLikeTest2()
           {
               var p = db.projekte 
                   .Where("it.type = 'anlage' and it.firmenname = 'Jets As'")
                   .OrderBy("it.projektnummer desc");

               var res = new ProjektCollection(p.Skip(4).Take(3) , db);
               return res;
           }


            //public TimeSpan GetProjectDurationByDuration(int ProjectID)
            //{
            //    var Duration = new TimeSpan(0);
            //    var result = from p in db.Logs
            //                 where p.ProjectID == ProjectID
            //                 select p;
            //    foreach (var p in result)
            //    {

            //        if (p.StopTime != null && p.Starttime != null)
            //        {
            //            p.Duration = p.StopTime - p.Starttime;
            //            if (p.Duration != null)
            //            {
            //                var ts = (TimeSpan)(p.Duration);
            //                Duration += ts;
            //            }
            //        }
            //    }


            //    return Duration;
            //}


        
            //public string GetProjectDuration(int ProjectID)
            //{
            //    var ts1 = GetProjectDurationByDuration(ProjectID);
            //    int hours = (ts1.Days * 24) + ts1.Hours;
            //    int days = (int)(hours / 8);
            //    int hoursLeft = hours % 8;
            //    string result = string.Format("{0}Tg {1} St {2} Min", days, hoursLeft, ts1.Minutes);
            //    return result;


            //}
        }
   
}
