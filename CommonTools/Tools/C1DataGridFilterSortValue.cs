using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C1.WPF.DataGrid;

namespace CommonTools.Tools
{
    public class C1DataGridFilterSortValue
    {

     

        public static DataGridColumnValue<DataGridFilterState>[] FilterState { get; set; }
        public static DataGridColumnValue<DataGridSortDirection>[] SortState { get; set; }
        public static string DataTypeString { get; set; }
      


        public static string GetCurrentFilter()
        {
            DataTypeString = "5-s,6-s,7-s,8-d,9-n";
            
         
            string buf = "";
           
                    
            return ReadDictionary(true,"");
        }





        private static string ReadDictionary(bool bIncludeLike, string preFilter)
        {
              Dictionary<int, string> DataTypes = new Dictionary<int, string>();
              string[] types = DataTypeString.Split(',');
           
            foreach (var item in types)
            {
                string[] res = item.Split('-');
                DataTypes.Add(int.Parse(res[0]),res[1]);
            }
            
            string f = string.Empty;
            StringBuilder sb = new StringBuilder();

            foreach (var e in FilterState)
            {
              
                    string buf;
                    var suc = DataTypes.TryGetValue(e.Column.Index, out buf);


                    if (buf == "d")
                    {
                        

                        sb.Append("it.");
                        sb.Append(e.Column.Name);
                        sb.Append(" >= ");
                        sb.Append(String.Format("DATETIME'{0} 00:00:00'", GetDateFormatted(e.Value.Tag.ToString())));
                        sb.Append(" and ");
                        sb.Append("it.");
                        sb.Append(e.Column.Name);
                        sb.Append(" <= ");
                        sb.Append(String.Format("DATETIME'{0} 23:59:59'", GetDateFormatted(e.Value.Tag.ToString())));



                    }
                    else if (buf == "n")
                    {
                        sb.Append("it.");
                        sb.Append(e.Column.Name);
                        sb.Append(" = ");
                        sb.Append(e.Value.Tag.ToString());
                        sb.Append(" and ");
                    }
                    else
                    {
                        sb.Append("it.");
                        sb.Append(e.Column.Name);
                        sb.Append(" like ");
                        sb.Append("'");
                        if (bIncludeLike)
                            sb.Append("%");
                        sb.Append(e.Value.Tag.ToString());
                        if (bIncludeLike)
                            sb.Append("%");
                        sb.Append("'");
                        sb.Append(" and ");
                    }
               
            }

            f = sb.ToString();
            if (f.EndsWith(" and "))
                f = f.Remove(f.LastIndexOf(" and "), 5);

            //filterString = f;

            if (preFilter != string.Empty && f != string.Empty)
            {
                f = String.Format("{0} {1} {2}", preFilter, " and ", f);
               // filterString = f;
            }
            else if (preFilter != string.Empty && f == string.Empty)
            {
                f = preFilter;
               // filterString = preFilter;
            }
            return f;

        }



    

        static string  GetDateFormatted(string date)
        {
            int yy = DateTime.Now.Year;
            string y = yy.ToString().Substring(0, 2);
            if (date.Length == 8)
            {
                return String.Format("{4}{0}-{1}-{2}", date.Substring(6, 2), date.Substring(3, 2), date.Substring(0, 2), y);
            }
            else if (date.Length == 10)
            {
                return String.Format("{0}-{1}-{2}", date.Substring(6, 4), date.Substring(3, 2), date.Substring(0, 2));
            }
            else
            {
                return "";
            }


        }

    }
}
