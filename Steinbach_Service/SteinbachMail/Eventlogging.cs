using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SteinbachMail
{
    public class Eventlogging
    {

        public static void log(params string[] TextItems)
        {
            Eventlogging.log(EventLogEntryType.Information, TextItems);
        }


        public static void log(EventLogEntryType type, params string[] TextItems)
        {
          
            Eventlogging.log(0,type,TextItems);
        }

        public static void log(int eventID, params string[] TextItems)
        {

            Eventlogging.log(eventID, EventLogEntryType.Information, TextItems);
        }


        public static void log(int eventID,EventLogEntryType type, params string[] TextItems)
        {
            const string sSource = "SteinbachEmailReminderSource";
            const string sLog = "SteinbachEmailReminderLog";

            var sb = new StringBuilder();
            foreach (var item in TextItems)
            {
                sb.Append(item);
                sb.Append(" | ");
            }

            try
            {
                if (!EventLog.SourceExists(sSource))
                {
                    EventLog.CreateEventSource(
                        sSource, sLog);
                }
            }
           
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }


            EventLog.WriteEntry(sSource, sb.ToString(), type,eventID);


        }
        

       

    }
}
