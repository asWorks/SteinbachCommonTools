using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DAL.Tools
{
    public class LoggingTool
    {
        public enum LogState
        {
            high = 4,
            medium = 2,
            low = 1,
            critical = 8,
            none=0
        }


        #region "Log Exception"


        public static void LogExeption(Exception ex, string ClassName, string procedure)
        {
            LoggingTool.LogExeption(ex, ClassName, procedure, LogState.medium);
        }

        public static void LogExeption(Exception ex)
        {

            LoggingTool.LogExeption(ex, string.Empty, string.Empty, LogState.medium);


        }

        public static void LogExeption(Exception ex, LogState ls)
        {

            LoggingTool.LogExeption(ex, string.Empty, string.Empty, ls);


        }


        public static void LogExeption(Exception ex, string ClassName, string procedure, LogState state)
        {
            try
            {
                  int s = (int)state;

                  if (s >= DAL.Session.LogState)
                  {
                      System.Text.StringBuilder sb = new StringBuilder();
                      sb.AppendLine(ex.Message);

                      if (ex.InnerException != null)
                      {
                          sb.AppendLine(ex.InnerException.Message);
                      }

                      if (ClassName != string.Empty)
                      {
                          sb.Append("Class : ");
                          sb.AppendLine(ClassName);
                      }

                      if (procedure != string.Empty)
                      {
                          sb.Append("Prozedur : ");
                          sb.AppendLine(procedure);
                      }
                      sb.AppendLine(DateTime.Now.ToLongDateString());
                      Trace.WriteLine(sb.ToString());
                      Trace.Flush();
                  }
            }
            catch (Exception)
            {
                
               
            }
          

           
        }

#endregion

        #region "Log Message"

        public static void LogMessage(string Message,LogState state)
        {
            LogMessage(Message, string.Empty, string.Empty, state);
        }



        public static void LogMessage(string Message)
        {
            LogMessage(Message, string.Empty, string.Empty, LogState.high);
        }



        public static void LogMessage(string Message, string ClassName, string procedure, LogState state)
        {
            int s = (int)state;

            if (s >= DAL.Session.LogState)
            {
                System.Text.StringBuilder sb = new StringBuilder();
                sb.AppendLine(Message);
                if (ClassName != string.Empty)
                {
                    sb.Append("Class : ");
                     sb.AppendLine(ClassName);
                }

                if (procedure != string.Empty)
                {
                    sb.Append("Prozedur : ");
                     sb.AppendLine(procedure);
                }
               
                sb.AppendLine(DateTime.Now.ToString());
                Trace.WriteLine(sb.ToString());
                Trace.Flush();

            }
        }

        #endregion

        #region "Add Database Logging"

        public static bool addDatabaseLogging(string level, int priority, string Text ,LogState State)
        {
          
              int s = (int)State;

              if (s >= DAL.Session.LogState)
              {
                  try
                  {
                      var log = new logging();
                      log.created = DateTime.Now;
                      log.level = level;
                      log.priority = priority;
                      string username = DAL.Session.GetValidUser.benutzername;

                      //if (DAL.Session.User == null)
                      //{
                      //    username = "Kein User abgemeldet";
                      //}
                      //else
                      //{
                      //    username = DAL.Session.User.benutzername;
                      //}

                      
                      //Text += " : User : " + DAL.Session.User != null ? DAL.Session.User.benutzername : "Kein User abgemeldet";
                      Text += " : User : " + username;
                      log.text = Text;

                      using (var db = new SteinbachEntities())
                      {
                          db.AddTologging(log);
                          db.SaveChanges();
                          return true;

                      }
                  }
                  catch (Exception ex)
                  {
                      DAL.Tools.LoggingTool.LogExeption(ex, "addDatabaseLogging", "addLogin", DAL.Tools.LoggingTool.LogState.low);
                      return false;
                  }
              }

              return true;

        }


        public static bool addDatabaseLogging(string level, int priority, string Text)
        {

            return addDatabaseLogging(level, priority, Text, LogState.low);


        }

       public static bool addDatabaseLogging(string level, int priority, params string[] TextItems)
        {

            return addDatabaseLogging(level, priority, LogState.low, TextItems);

        }

       public static bool addDatabaseLogging(string level, int priority, LogState state,params string[] TextItems)
       {
           var sb = new StringBuilder();
           foreach (var item in TextItems)
           {
               sb.Append(item);
               sb.Append(" | ");
           }
           return addDatabaseLogging(level, priority, sb.ToString(),state);
       }
    }
        #endregion

}
