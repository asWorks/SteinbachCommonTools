using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.IO;

namespace CommonTools.ObjectFactories
{
    public class EmailFactory
    {
        public static int DeleteEmail(int id)
        {
            try
            {
                using (var db = new SteinbachEntities())
                {
                    var mail = db.CRMEmails.Where(m => m.id == id).SingleOrDefault();
                    if (mail != null)
                    {
                        if (mail.EmailAttachments.Count() > 0)
                        {
                            var att = mail.EmailAttachments.ElementAt(0);
                            FileInfo fi = new FileInfo(att.Path);
                            DirectoryInfo di = new DirectoryInfo(fi.DirectoryName);
                            try
                            {
                                di.Delete(true);
                            }
                            catch (Exception)
                            {

                                
                            }



                            foreach (var item in mail.EmailAttachments.ToList())
                            {
                                db.DeleteObject(item);
                            }
                        }

                        db.DeleteObject(mail);
                        return db.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);
                    }
                    else
                    {
                        return 0;
                    }



                }
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.ShowErrorMessage(ex);
                return 0;
            }



        }

    }
}

