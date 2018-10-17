using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;





namespace DAL.ObjectCollections
{
    public class viewProjektAnlagentyp : ObservableCollection<vwProjektAnlagentyp>
    {

        private readonly SteinbachEntities _context;
        //private GridView   TestGridView;
        public SteinbachEntities Context
        {
            get { return _context; }
        }

        // public bvAuftragsBestand(IEnumerable<vwBrunvollAuftragsbestand> bvAutraege, SteinbachEntities context, GridView gridView)
        //    : base(bvAutraege)
        //{
        //    try
        //    {
        //        _context = context;
        //        TestGridView = gridView;
        //        BuildGrid();

        //    }
        //    catch (Exception ex)
        //    {

        //        //LoggingTool.LogExeption(ex, "TemplateCollection", "Constructor");
        //        System.Windows.MessageBox.Show(ex.Message);
        //    }



        //}


        public viewProjektAnlagentyp(IEnumerable<vwProjektAnlagentyp> viewProAblagentyp, SteinbachEntities context)
            : base(viewProAblagentyp)
        {
            try
            {
                _context = context;
            }
            catch (Exception ex)
            {

                //LoggingTool.LogExeption(ex, "TemplateCollection", "Constructor");
                //System.Windows.MessageBox.Show("Beim Initialisieren von vwBrunvollAuftragsbestand ist ein Fehler aufgetreten.");
            }



        }


    }
}

