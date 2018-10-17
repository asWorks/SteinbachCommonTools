using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PageControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Pager : UserControl
    {
       public delegate void DelegatePageChanged(object sender, PageChangedEventArgs e ); 
       private PagerHelper ph;
       public event DelegatePageChanged eventPageChanged;

       

        public Pager()
        {
            InitializeComponent();
            ph = new PagerHelper();
        }

        public void Reset(int CurrentPage,int RecordsPerPage,int RecordCount)
        {
            ph.Reset(CurrentPage, RecordCount, RecordsPerPage);
            tbRecordsShown.Text = ph.RecordInfo;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            string op = b.Content.ToString();
            //var temp = new ProjektRepository(db);
            switch (op)
            {
                case "++":
                    {
                        ph.goLast();
                        break;
                    }
                case "+":
                    {
                        ph.next();
                        break;
                    }
                case "-":
                    {
                        ph.previous();
                        break;
                    }
                case "--":
                    {
                        ph.goFirst();
                        break;
                    }
                default:
                    break;
            }

            tbRecordsShown.Text = ph.RecordInfo;
            eventPageChanged(this, new PageChangedEventArgs(ph.toSkip, ph.toTake));

        }
    }
}