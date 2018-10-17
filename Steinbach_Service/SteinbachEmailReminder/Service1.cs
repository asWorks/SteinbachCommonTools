using System;
using System.ServiceProcess;
using System.Windows.Threading;

namespace SteinbachEmailReminder
{
    public partial class Service1 : ServiceBase
    {
     //   private DispatcherTimer dt;
        System.Timers.Timer ti;
        System.Timers.Timer tiTermin;

    
        public Service1()
        {
            InitializeComponent();
           
            //if (!System.Diagnostics.EventLog.SourceExists("SteinbachEmailReminderSource"))
            //{
            //    System.Diagnostics.EventLog.CreateEventSource(
            //        "SteinbachEmailReminderSource", "SteinbachEmailReminderLog");
            //}
            //eventLog1.Source = "SteinbachEmailReminderSource";
            //eventLog1.Log = "SteinbachEmailReminderLog";

            ti = new System.Timers.Timer();
            ti.Elapsed+=ti_Elapsed;
            ti.Interval = DAL.Session.MailTimerIntervalSMTP * 1000;
            tiTermin = new System.Timers.Timer();
            tiTermin.Elapsed += new System.Timers.ElapsedEventHandler(tiTermin_Elapsed);
            tiTermin.Interval = DAL.Session.TerminTimerIntervalSMTP * 1000;

          
        }

        void tiTermin_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            var termin = new SteinbachMail.CheckApointmetReminder();
            termin.CheckAppointments();
            SteinbachMail.Eventlogging.log("Termin Tick", DateTime.Now.ToString());
        }

        void ti_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var mail = new SteinbachMail.CheckMailReminder();
            mail.checkMail();
            SteinbachMail.Eventlogging.log("Timer Tick", DateTime.Now.ToString());
        }

       

        protected override void OnStart(string[] args)
        {
            try
            {
               
              
                ti.Enabled = true;
                ti.AutoReset = true;
                tiTermin.Enabled = true;
                tiTermin.AutoReset = true;

                SteinbachMail.Eventlogging.log("in OnStart");
                SteinbachMail.Eventlogging.log(SteinbachMail.CheckMailReminder.GetEnvironment());
              
            }
            catch (Exception ex)
            {
                SteinbachMail.Eventlogging.log(ex.Message);
              
                if (ex.InnerException != null)
                {
                    SteinbachMail.Eventlogging.log(ex.InnerException.Message);
                   
                }

            }
        }

        protected override void OnStop()
        {
            SteinbachMail.Eventlogging.log("In onStop.");
          

        }

        protected override void OnContinue()
        {
            SteinbachMail.Eventlogging.log("In OnContinue.");
          
        }

    
    }
}
