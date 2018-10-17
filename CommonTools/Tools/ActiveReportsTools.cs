using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Expressions.ExpressionObjectModel;
using DAL.Tools;

namespace CommonTools.Tools
{
    public class ActiveReportsTools<T>
    {

        public static void SetParameter(string ParamName, T Value, ref GrapeCity.ActiveReports.Document.PageDocument pageDocument)
        {

            ParameterValue value = new ParameterValue();
            value.Value = Value;
            value.Label = "neu";


            pageDocument.Parameters[ParamName].ValidValues.Add(value);
            pageDocument.Parameters[ParamName].Values.Add(value);
        }

    
        
        public static PageDocument GetDocument(string path)
        {
            try
            {
               // LoggingTool.LogMessage("Entering GetDocument", "Dal.Tools.ActiveReportTools", "LocateDataSource", LoggingTool.LogState.low);
                PageReport pageReport = new PageReport(new System.IO.FileInfo(path));
                var pageDocument = new PageDocument(pageReport);
                return pageDocument;
              //  LoggingTool.LogMessage("Done GetDocument", "Dal.Tools.ActiveReportTools", "LocateDataSource", LoggingTool.LogState.low);
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }

        }

    }
}
