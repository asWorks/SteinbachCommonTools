using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;





namespace DAL.ObjectCollections
{
    public class bvAuftragsBestand : ObservableCollection<vwBrunvollAuftragsbestand>
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


        public bvAuftragsBestand(IEnumerable<vwBrunvollAuftragsbestand> bvAutraege, SteinbachEntities context)
            : base(bvAutraege)
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

