using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PageControl
{
    public class PagerHelper
    {
        public string RecordInfo { get; set; }
        public int recordsPP { get; set; }
        protected internal int CurrentPage { get; set; }
        protected internal int RecordsCount { get; set; }
        protected internal int fromPage { get; set; }
        protected internal int toPage { get; set; }
        protected internal int _toSkip;
        public int toSkip
        {
            get
            {
                return _toSkip;
            }

            set
            {
                if (value < 0)
                    _toSkip = 0;
                else
                    _toSkip = value;
            }

        }
        public int toTake { get; set; }

        public void Reset(int Page, int RecordCount, int RecordsPerPage)
        {
            CurrentPage = Page;
            fromPage = CurrentPage;

            RecordsCount = RecordCount;
            recordsPP = RecordsPerPage;
            toPage = RecordsCount < recordsPP ? RecordsCount : CurrentPage + (recordsPP-1);
            toTake = recordsPP;
            toSkip = 0;
            RecordInfo = String.Format("Datensatz {0} bis {1} von {2}", CurrentPage,
                                        toPage,
                                        RecordsCount);



        }

        public void goLast()
        {
            
            //CurrentPage = RecordsCount - recordsPP;

            toPage = RecordsCount;
            CurrentPage = RecordsCount <= recordsPP ? 1 : toPage - (recordsPP -1);
            fromPage = CurrentPage;
            toSkip = fromPage -1;
            RecordInfo = String.Format("Datensatz {0} bis {1} von {2}", CurrentPage,
                                        toPage,
                                        RecordsCount);

        }
        public void goFirst()
        {
            toSkip = 0;
            CurrentPage = 1;
            fromPage = CurrentPage;
            toPage = RecordsCount < recordsPP ? RecordsCount : CurrentPage + (recordsPP -1);
            RecordInfo = String.Format("Datensatz {0} bis {1} von {2}", CurrentPage,
                                        toPage,
                                        RecordsCount);

        }

        public void next()
        {
            if (CurrentPage + recordsPP < RecordsCount)
            {
                CurrentPage += recordsPP;
                fromPage = CurrentPage;
                //toPage = RecordsCount < recordsPP ? RecordsCount : CurrentPage + recordsPP;
                toPage = toPage < (RecordsCount - recordsPP) ? CurrentPage + (recordsPP-1) : RecordsCount ;

                toSkip = fromPage - 1;     //CurrentPage - recordsPP;
            }
            else
            {

                            
                
                
               // goLast();
            }
            RecordInfo = String.Format("Datensatz {0} bis {1} von {2}", CurrentPage,
                                        toPage,
                                        RecordsCount);



        }

        public void previous()
        {
            if (CurrentPage - recordsPP < 0)
                goFirst();
            else
            {
                CurrentPage -= recordsPP;
                fromPage = CurrentPage;
                toPage = RecordsCount < recordsPP ? RecordsCount : CurrentPage + (recordsPP -1);
                toSkip = fromPage - 1; // CurrentPage - recordsPP;
            }

            RecordInfo = String.Format("Datensatz {0} bis {1} von {2}", CurrentPage,
                                        toPage,
                                        RecordsCount);

        }








    }
}
