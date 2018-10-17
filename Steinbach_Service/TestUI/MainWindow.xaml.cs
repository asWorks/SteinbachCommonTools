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
using System.Data;
using System.Globalization;

namespace TestUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SteinbachMail.ProjektTimer pt;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //pt = new SteinbachMail.ProjektTimer();
                ////eventLog1.WriteEntry("in OnStart");
                //textBox1.Text = (SteinbachMail.CheckMailReminder.GetEnvironment());
              //  var ap = new SteinbachMail.CheckApointmetReminder();
                var ap = new SteinbachMail.CheckMailReminder();
                ap.checkMail();

            }
            catch (Exception ex)
            {
                textBox1.Text = (ex.Message);
                if (ex.InnerException != null)
                {
                    textBox1.Text+= (ex.InnerException.Message);
                }

            }

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //string [] test = {"123456789","987654321","Apfel","Birne"};
            //textBox1.Text = test.FirstOrDefault().ToString();

            var ntd = new DateTime(2012, 06, 01);
            System.Globalization.Calendar objCal = CultureInfo.CurrentCulture.Calendar;
            int weekofyear = objCal.GetWeekOfYear(ntd, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            MessageBox.Show(weekofyear.ToString());
        }
    }
}
