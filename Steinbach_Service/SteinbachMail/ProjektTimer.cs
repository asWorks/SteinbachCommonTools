using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace SteinbachMail
{
    public class ProjektTimer
    {
        private  DispatcherTimer dt;
     //   private bool bIsInit = false;
        //public  void init()
        //{
        //    if (!bIsInit)
        //    {
        //        dt.Tick += dt_Tick;


                
        //        var res = DAL.Session.MailTimerIntervalSMTP;
        //        dt.Interval = TimeSpan.FromSeconds(res);
        //        dt.Start();
        //        bIsInit = true;
        //        CallMailReminder();

        //    }
            
        //}

        public ProjektTimer()
        {
            dt = new DispatcherTimer();
            dt.Tick += dt_Tick;



            var res = DAL.Session.MailTimerIntervalSMTP;
            dt.Interval = TimeSpan.FromSeconds(res);
            dt.Start();
            CallMailReminder();
        }


        void dt_Tick(object sender, EventArgs e)
        {

            Eventlogging.log("Timer Tick", DateTime.Now.ToString());
              CallMailReminder();
              var res = DAL.Session.MailTimerIntervalSMTP;
              dt.Interval = TimeSpan.FromSeconds(res);


        }

        void CallMailReminder()
        {
            var mail = new CheckMailReminder();
            mail.checkMail();

        }

    }
}
