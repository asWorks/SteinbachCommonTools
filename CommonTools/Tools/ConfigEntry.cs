using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonTools.Tools
{
    public class ConfigEntry<T>
    {

        public static T GetConfigEntry(string key, string Default, string Explanation = "")
        {

            string result = string.Empty;
            try
            {
                using (var db = new DAL.SteinbachEntities())
                {
                    var res = db.config.Where(s => s.mkey == key).FirstOrDefault();

                    if (res != null)
                    {
                        result = res.value;

                    }
                    else
                    {
                        var c = new DAL.config { mkey = key, value = Default, Description = Explanation };
                        db.AddToconfig(c);
                        db.SaveChanges();
                        result = Default;

                    }
                }





                object ReturnVal = result;


                if (typeof(T) == typeof(string))
                {

                    return (T)ReturnVal;
                }

                else if (typeof(T) == typeof(int))
                {
                    int iOut = 0;
                    bool check = int.TryParse(result, out iOut);

                    if (check)
                    {
                        object ret = iOut;
                        return (T)ret;
                    }


                }

                return default(T);

            }
            catch (Exception)
            {

                return default(T);
            }


             


         
        }
    }
}
