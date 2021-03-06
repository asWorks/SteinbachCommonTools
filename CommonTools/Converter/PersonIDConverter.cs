﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections;
using DAL;


namespace CommonTools.Converter
{
[ValueConversion(typeof(int),typeof(string ))]
    public class PersonIDConverter:IValueConverter
    {



        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
                    
            using (var db = new SteinbachEntities())
            {

                if (value == null || (int)value == 0)
                {
                    return "-";
                }

                


                int v = (int)value;
                var query = from p in db.personen where p.id == v select p.benutzername;
                //string res = "";
                //foreach (string s in query)
                //{
                //     res = s;
                
                //}
                
                
                //string buf =res ;
                return query.FirstOrDefault();
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

     
    }
}
