using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.ComponentModel.Composition;

namespace CommonTools.ViewModels 
{
    [Export(typeof(AdressenViewModel))]
    public class AdressenViewModel :PropertyChangedBase
    {

        #region "UIProperties"
        private bool _isVisible;
        public bool isVisible
        {
            get { return _isVisible; }
            set
            {
                if (value != _isVisible)
                {
                    _isVisible = value;
                    NotifyOfPropertyChange(() => isVisible);
                    isDirty = true;
                }
            }
        }


        #endregion

        #region "Properties"
        public bool isDirty { get; set; }
        private Int32 _id;
        public Int32 id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyOfPropertyChange(() => id);
                    isDirty = true;
                }
            }
        }

        private Int32? _id_Firma;
        public Int32? id_Firma
        {
            get { return _id_Firma; }
            set
            {
                if (value != _id_Firma)
                {
                    _id_Firma = value;
                    NotifyOfPropertyChange(() => id_Firma);
                    isDirty = true;
                }
            }
        }

        private String _Straße;
        public String Straße
        {
            get { return _Straße; }
            set
            {
                if (value != _Straße)
                {
                    _Straße = value;
                    NotifyOfPropertyChange(() => Straße);
                    isDirty = true;
                }
            }
        }

        private String _PLZ;
        public String PLZ
        {
            get { return _PLZ; }
            set
            {
                if (value != _PLZ)
                {
                    _PLZ = value;
                    NotifyOfPropertyChange(() => PLZ);
                    isDirty = true;
                }
            }
        }

        private String _Ort;
        public String Ort
        {
            get { return _Ort; }
            set
            {
                if (value != _Ort)
                {
                    _Ort = value;
                    NotifyOfPropertyChange(() => Ort);
                    isDirty = true;
                }
            }
        }

        private String _Postfach;
        public String Postfach
        {
            get { return _Postfach; }
            set
            {
                if (value != _Postfach)
                {
                    _Postfach = value;
                    NotifyOfPropertyChange(() => Postfach);
                    isDirty = true;
                }
            }
        }

        private String _Bundesland;
        public String Bundesland
        {
            get { return _Bundesland; }
            set
            {
                if (value != _Bundesland)
                {
                    _Bundesland = value;
                    NotifyOfPropertyChange(() => Bundesland);
                    isDirty = true;
                }
            }
        }

        private String _Land;
        public String Land
        {
            get { return _Land; }
            set
            {
                if (value != _Land)
                {
                    _Land = value;
                    NotifyOfPropertyChange(() => Land);
                    isDirty = true;
                }
            }
        }

        private String _Bezeichnung;
        public String Bezeichnung
        {
            get { return _Bezeichnung; }
            set
            {
                if (value != _Bezeichnung)
                {
                    _Bezeichnung = value;
                    NotifyOfPropertyChange(() => Bezeichnung);
                    isDirty = true;
                }
            }
        }

        private Int32? _Typ;
        public Int32? Typ
        {
            get { return _Typ; }
            set
            {
                if (value != _Typ)
                {
                    _Typ = value;
                    if (value == 5 )
                    {
                        isVisible = true;
                    }
                    NotifyOfPropertyChange(() => Typ);
                    isDirty = true;
                }
            }
        }

        private String _PostfachPLZ;
        public String PostfachPLZ
        {
            get { return _PostfachPLZ; }
            set
            {
                if (value != _PostfachPLZ)
                {
                    _PostfachPLZ = value;
                    NotifyOfPropertyChange(() => PostfachPLZ);
                    isDirty = true;
                }
            }
        }

        private String _PostfachOrt;
        public String PostfachOrt
        {
            get { return _PostfachOrt; }
            set
            {
                if (value != _PostfachOrt)
                {
                    _PostfachOrt = value;
                    NotifyOfPropertyChange(() => PostfachOrt);
                    isDirty = true;
                }
            }
        }

        private Int16? _TypGeschaeftlich;
        public Int16? TypGeschaeftlich
        {
            get { return _TypGeschaeftlich; }
            set
            {
                if (value != _TypGeschaeftlich)
                {
                    _TypGeschaeftlich = value;
                    NotifyOfPropertyChange(() => TypGeschaeftlich);
                    isDirty = true;
                }
            }
        }

        private Int16? _TypRechnungsadresse;
        public Int16? TypRechnungsadresse
        {
            get { return _TypRechnungsadresse; }
            set
            {
                if (value != _TypRechnungsadresse)
                {
                    _TypRechnungsadresse = value;
                    NotifyOfPropertyChange(() => TypRechnungsadresse);
                    isDirty = true;
                }
            }
        }

        private Int16? _TypLieferadresse;
        public Int16? TypLieferadresse
        {
            get { return _TypLieferadresse; }
            set
            {
                if (value != _TypLieferadresse)
                {
                    _TypLieferadresse = value;
                    NotifyOfPropertyChange(() => TypLieferadresse);
                    isDirty = true;
                }
            }
        }

        private Int16? _Hauptadresse;
        public Int16? Hauptadresse
        {
            get { return _Hauptadresse; }
            set
            {
                if (value != _Hauptadresse)
                {
                    _Hauptadresse = value;
                    NotifyOfPropertyChange(() => Hauptadresse);
                    isDirty = true;
                }
            }
        }

        private String _Standort;
        public String Standort
        {
            get { return _Standort; }
            set
            {
                if (value != _Standort)
                {
                    _Standort = value;
                    NotifyOfPropertyChange(() => Standort);
                    isDirty = true;
                }
            }
        }

        private String _VAT;
        public String VAT
        {
            get { return _VAT; }
            set
            {
                if (value != _VAT)
                {
                    _VAT = value;
                    NotifyOfPropertyChange(() => VAT);
                    isDirty = true;
                }
            }
        }

        private String _ZusatzInfo;
        public String ZusatzInfo
        {
            get { return _ZusatzInfo; }
            set
            {
                if (value != _ZusatzInfo)
                {
                    _ZusatzInfo = value;
                    NotifyOfPropertyChange(() => ZusatzInfo);
                    isDirty = true;
                }
            }
        }

        private string _ZusatzInfo2;
        public string ZusatzInfo2
        {
            get { return _ZusatzInfo2; }
            set
            {
                if (value != _ZusatzInfo2)
                {
                    _ZusatzInfo2 = value;
                    NotifyOfPropertyChange(() => ZusatzInfo2);
                    isDirty = true;
                }
            }
        }


        private string _ZusatzInfo3;
        public string ZusatzInfo3
        {
            get { return _ZusatzInfo3; }
            set
            {
                if (value != _ZusatzInfo3)
                {
                    _ZusatzInfo3 = value;
                    NotifyOfPropertyChange(() => ZusatzInfo3);
                    isDirty = true;
                }
            }
        }







        #endregion


    }
}
