using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;


namespace DAL.DataTypes
{

     
    public class SI_BelegePositionChangedEventArgs
    {

        public SI_BelegePositionen CurrentPosition;

        public SI_BelegePositionChangedEventArgs(SI_BelegePositionen Position)
        {
            CurrentPosition = Position;
        }

    }
}
