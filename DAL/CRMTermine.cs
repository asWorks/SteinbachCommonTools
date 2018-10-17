using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class CRMTermine
    {

        private C1.WPF.DateTimeEditors.C1DateTimePickerEditMode _DateTimePickerFormat;

        public C1.WPF.DateTimeEditors.C1DateTimePickerEditMode DateTimePickerFormat
        {
            get { return _DateTimePickerFormat; }
            set
            {
                _DateTimePickerFormat = value;
                OnPropertyChanged("DateTimePickerFormat");
                
            }
        }
        


        partial void OnistGanztaegigChanging(short? value)
        {
            if (value.HasValue)
            {
                var newVal = (short)value;
                if (newVal == 1)
                {
                    DateTimePickerFormat = C1.WPF.DateTimeEditors.C1DateTimePickerEditMode.Date;

                    if (TerminVon.HasValue)
                    {
                        DateTime tv = (DateTime)TerminVon;
                        tv = new DateTime(tv.Year, tv.Month, tv.Day, 0, 0, 0);
                        TerminVon = tv;
                    }

                    if (TerminVon.HasValue)
                    {
                        DateTime tv = (DateTime)TerminBis;
                        tv = new DateTime(tv.Year, tv.Month, tv.Day, 23, 59, 59);
                        TerminBis = tv;
                    }

                }
                else
                {
                    DateTimePickerFormat = C1.WPF.DateTimeEditors.C1DateTimePickerEditMode.DateTime;
                }

                
            }

        }

        public CRMTermine()
            :base()
        {
            istGanztaegig = 0;

        }



        protected override void OnPropertyChanged(string property)
        {
            base.OnPropertyChanged(property);
        }
    }
}
