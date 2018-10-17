using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//using ProjektDB.Tools;
using DAL;
using CommonTools.Views;
using CommonTools.Tools;
namespace CommonTools.Views
{
    /// <summary>
    /// Interaction logic for Logon.xaml
    /// </summary>
    public partial class Logon : Window
    {
        public Logon()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            try
            {
              
                if (Session.Login(txtUser.Text, pwbPassword.Password) == false)
                {
                    MessageBox.Show("Anmeldung war nicht erfolgreich");

                }
                else
                {
                    SaveCreds((bool)chkSaveLogon.IsChecked);
                    this.Close();
                }
            }

            catch (Exception ex)
            {

                ErrorMethods.HandleStandardError(ex);
            }



        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (Session.User == null)
            {
                Environment.Exit(1);
                //Application.Current.Shutdown();
            }
            base.OnClosing(e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var x = new Properties.Settings();
            if (x.SaveCredentials)
            {
                chkSaveLogon.IsChecked = true;
                txtUser.Text = x.Login;
                pwbPassword.Password = x.Password;
            }
        }

        private void SaveCreds(bool state)
        {
            try
            {
                var x = new Properties.Settings();
                if (state)
                {
                    x.Login = txtUser.Text;
                    x.Password = pwbPassword.Password;
                    x.SaveCredentials = true;


                }
                else
                {
                    x.Login = string.Empty;
                    x.Password = string.Empty;
                    x.SaveCredentials = false;
                }
                x.Save();
            }
            catch (Exception ex)
            {

                ErrorMethods.HandleStandardError(ex);
            }


        }
    }
}
