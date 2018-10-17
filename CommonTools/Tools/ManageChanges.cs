using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Windows;


namespace CommonTools.Tools
{
    public class ManageChanges
    {
        #region "Enums"

        public enum SaveChangesEnum
        {
            Yes,
            No,
            Cancel,
            AllDone,
            noResult

        }
        #endregion

        #region "Declarations"

        SteinbachEntities db;

        #endregion

        #region "Constructors"
        public ManageChanges(SteinbachEntities db)
        {
            this.db = db;
            _ModifiedProperties = new List<string>();

        }

        #endregion

        #region "Static Functions"

        public static bool isDirty(SteinbachEntities db)
        {
            var mc = new ManageChanges(db);
            return mc.isDirty();

        }

        public static SaveChangesEnum SaveChanges(SteinbachEntities db, MessageBoxButton Button)
        {
            var mc = new ManageChanges(db);
            return mc.SaveChanges(Button);
        }
        public static SaveChangesEnum SaveChanges(SteinbachEntities db)
        {
            var mc = new ManageChanges(db);
            return mc.SaveChanges(MessageBoxButton.YesNoCancel);

        }

        public static List<string> GetModifiedProperties(SteinbachEntities db)
        {
            var mc = new ManageChanges(db);
            return mc.ModifiedProperties;

        }


        public static void SetUnmodified(SteinbachEntities db)
        {
            var mc = new ManageChanges(db);
            mc.SetUnmodified();
        }

        #endregion

        #region "Functions"

        public void WriteModifiedProperties()
        {
            if (db != null)
            {
                _ModifiedProperties.Clear();
                var osm = db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted);

                foreach (var ose in osm)
                {
                    var res = ose.GetModifiedProperties();
                    _ModifiedProperties = res.ToList();

                }
            }


        }



        public bool isDirty()
        {
            if (db != null)
            {
                var osm = db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted);

                return osm.Count() > 0;

            }
            else
            {
                return false;
            }

        }

        public void SetUnmodified()
        {
            try
            {
                if (db != null)
                {
                    var osm = db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted);

                    foreach (var ose in osm)
                    {
                        ose.ChangeState(System.Data.EntityState.Unchanged);


                    }

                }
            }
            catch (Exception)
            {

              
            }


        }





        public SaveChangesEnum SaveChanges(MessageBoxButton Button)
        {

            if (this.isDirty())
            {
                var res = (MessageBox.Show("Änderungen speichern ?", "Sicherheitsabfrage", Button));
                if (res == MessageBoxResult.Yes || res == MessageBoxResult.OK)
                {
                    db.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);


                    return SaveChangesEnum.AllDone;
                }
                else if (res == MessageBoxResult.No)
                {
                    return SaveChangesEnum.No;
                }
                else if (res == MessageBoxResult.Cancel)
                {

                    return SaveChangesEnum.Cancel;
                }


            }
            else
            {
                return SaveChangesEnum.AllDone;
            }

            return SaveChangesEnum.noResult;
        }

        public SaveChangesEnum SaveChanges()
        {
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            return SaveChanges(button);
        }

        #endregion


        #region "Properties"
        private List<string> _ModifiedProperties;

        public List<string> ModifiedProperties
        {
            get
            {
                WriteModifiedProperties();

                return _ModifiedProperties;

            }
            set { _ModifiedProperties = value; }
        }

        #endregion

    }
}
