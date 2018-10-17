using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace DAL
{
    public partial class Firmen_Telefon
    {

        BitmapImage[] Images;

        private bool _isDirty;
        public bool isDirty
        {
            get { return _isDirty; }
            set
            {
                if (value != _isDirty)
                {
                    _isDirty = value;
                    Background = value == true ? System.Windows.Media.Brushes.MintCream : System.Windows.Media.Brushes.Lavender;
                    OnPropertyChanged("isDirty");
                }
            }
        }

        public string Dialnumber
        {
            get { return Vorwahl + Rufnummer; }
           
        }

        private bool _ShowDialer;

        public bool ShowDialer
        {
            get { return _ShowDialer; }
            set { _ShowDialer = value; }
        }

        private BitmapImage _DialerImage;
        public BitmapImage DialerImage
        {
            get { return _DialerImage; }
            set
            {
                if (value != _DialerImage)
                {
                    _DialerImage = value;
                    //NotifyOfPropertyChange(() => DialerImage);
                    //isDirty = true;
                }

            }
        }

        private System.Windows.Media.Brush _Background;
        public System.Windows.Media.Brush Background
        {
            get { return _Background; }
            set
            {
                if (value != _Background)
                {
                    _Background = value;
                    OnPropertyChanged("Background");

                }
            }
        }


        partial void OnTypChanged()
        {
            ShowDialer = Typ == 16 ? false : true;
            //DialerImage = value == 16 ? Images[1] : Images[0];
            switch (Typ)
            {
                case 16:
                    {
                        DialerImage = Images[1];
                        break;
                    }
                case 17:
                    {
                        DialerImage = Images[2];
                        break;
                    }

                default:
                    {
                        DialerImage = Images[0];
                        break;
                    }

            }

            OnPropertyChanged("ShowDialer");
            OnPropertyChanged("DialerImage");
            isDirty = true;
           
        }


        partial void OnRufnummerChanged()
        {
            isDirty = true;
        }

        partial void OnVorwahlChanged()
        {
            isDirty = true;
        }

        partial void OnMemoChanged()
        {
            isDirty = true;
        }
        public Firmen_Telefon()
            :base()
        {
            BitmapImage[] _images = {new BitmapImage(new Uri("pack://application:,,,/SteinbachCRM;component/Images/Phone_thumb.png")), 
               new BitmapImage(new Uri("pack://application:,,,/SteinbachCRM;component/Images/Fax.png")),
                                 new BitmapImage(new Uri("pack://application:,,,/SteinbachCRM;component/Images/phone_mobile.png"))};


            Images = _images;
            isDirty = false;

        }



        protected override void OnPropertyChanged(string property)
        {
            base.OnPropertyChanged(property);
        }

    }
}
