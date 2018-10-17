using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonTools.Tools
{
    public class Convert
    {

        public static bool Short2BoolN(Int16? value)
        {
            if (value == null)
            {
                return false;
            }

            return value != 0 ? true:false;
        }

    }
}
