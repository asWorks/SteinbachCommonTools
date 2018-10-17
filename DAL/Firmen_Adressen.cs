using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace DAL
{
    public partial class Firmen_Adressen
    {
        private string _StrasseCaption;

        public Firmen_Adressen()
        {

            SetStrasseCaption();
        }

        public string StrasseCaption
        {
            get { return _StrasseCaption; }
            set
            {
                if (value != _StrasseCaption)
                {
                    _StrasseCaption = value;
                    OnPropertyChanged("StrasseCaption");
                }


            }
        }

        private bool _isVisible;

        public bool isVisible
        {
            get { return _isVisible; }
            set
            {
                if (value != _isVisible)
                {
                    _isVisible = value;
                    OnPropertyChanged("isVisible");
                }
            }
        }

        private Brush _BackGround;

        public Brush BackGround
        {
            get { return _BackGround; }
            set
            {
                if (_BackGround != value)
                {
                    _BackGround = value;
                    OnPropertyChanged("BackGround");
                }
            }
        }
        



        
        partial void OnTypChanged()
        {
            SetStrasseCaption();
        }
        

        void SetStrasseCaption()
        {
            if (Typ.HasValue)
            {
                switch (Typ)
                {
                    case 1:
                        {
                            StrasseCaption = "Straße";
                            BackGround = Brushes.Lavender;
                            isVisible = false;
                            break;
                        }

                    case 2:
                        {
                            StrasseCaption = "Postfach";
                            BackGround = Brushes.LightBlue;
                            isVisible = false;
                            break;
                        }
                    case 3:
                        {
                            StrasseCaption = "Straße";
                            BackGround = Brushes.LightGreen;
                            isVisible = true;
                            break;
                        }

                    default:
                        {
                            StrasseCaption = "Straße";
                            BackGround = Brushes.Lavender;
                            isVisible = false;
                            break;
                        }
                }
            }
            else
            {
                StrasseCaption = "Straße";
                BackGround = Brushes.Lavender;
                isVisible = false;
            }

        }

    }
}
