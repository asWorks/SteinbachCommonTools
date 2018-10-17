using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using DAL;

namespace CommonTools.Converter
{
    [ValueConversion(typeof(long), typeof(string))]
    public class ReminderTimespanStringConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
          

            if (value != null)
            {
                long id = (long)value;
                if (id != 0)
                {

                    using (SteinbachEntities db = new SteinbachEntities())
                    {
                        TimeSpan ts= TimeSpan.FromTicks(id);
                        long Duration = (long)ts.TotalMinutes;
                        var query = db.AuswahlEintraege.Where(e => e.Gruppe == "TypErinnerungDauer" && e.ai_int == Duration).SingleOrDefault();
                       
                        if (query != null)
                        {
                              return query.Eintrag;
                        }
                      

                    }
                }




            }


            return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {

                {
                    using (SteinbachEntities db = new SteinbachEntities())
                    {
                        var query = db.AuswahlEintraege.Where(e => e.Gruppe == "TypErinnerungDauer" && e.Eintrag == value).SingleOrDefault();
                        if (query != null)
                        {
                             TimeSpan ts = new TimeSpan(0, (int)query.ai_int, 0);
                        return ts.Ticks;
                        }
                       
                    }

                }
            }
            return null;

        }
    }
}
