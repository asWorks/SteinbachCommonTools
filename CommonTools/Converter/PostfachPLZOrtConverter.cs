using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using DAL;

namespace CommonTools.Converter
{
    [ValueConversion(typeof(int), typeof(string))]
    public class PostfachPLZOrtConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {


            if (value != null)
            {
                int id = (int)value;
                if (id != 0)
                {

                    using (SteinbachEntities db = new SteinbachEntities())
                    {
                        //int id = (int)value;
                        var query = db.Firmen_Adressen.Where(k => k.id == id).SingleOrDefault();
                        var sb = new StringBuilder();
                        sb.Append(query.Postfach);
                        sb.Append(", ");
                        sb.Append(query.PostfachPLZ);
                        sb.Append(" ");
                        sb.Append(query.PostfachOrt);

                        return sb.ToString();

                    }
                }

                


            }

            return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {


            return "";
        }
    }
}
