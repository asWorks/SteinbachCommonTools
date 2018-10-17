using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("SteinbachEmailReminderSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "SteinbachEmailReminderSource", "SteinbachEmailReminderLog");
            }
            eventLog1.Source = "SteinbachEmailReminderSource";
            eventLog1.Log = "SteinbachEmailReminderLog";

        }

        protected override void OnStart(string[] args)
        {
            try
            {
            SteinbachMail.ProjektTimer.init();
            eventLog1.WriteEntry("in OnStart");
            eventLog1.WriteEntry( SteinbachMail.CheckMailReminder.GetEnvironment());
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message);
                if (ex.InnerException != null)
                {
                    eventLog1.WriteEntry(ex.InnerException.Message);
                }
               
            }
           

        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In onStop.");

        }

        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }  

    }
}
