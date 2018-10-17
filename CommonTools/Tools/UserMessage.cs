using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CommonTools.Tools
{
    public class UserMessage
    {
        public static void NotifyUser(string Message)
        {

            MessageBox.Show(Message);
        }
    }
}
