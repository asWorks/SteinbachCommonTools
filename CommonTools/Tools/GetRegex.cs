using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonTools.Tools
{
    public class GetRegex
    {
        public static string GetPhoneNumber(string number)
        {
           
            var regEx =  new Regex(@"/^((\+[0-9]{2,4}([ -][0-9]+?[ -]| ?\([0-9]+?\) ?))   |(\(0[0-9 ]+?\) ?)|(0[0-9]+? ?( |-|\/) ?))([0-9]+?[ \/-]?)+?[0-9]$/");
            if (regEx.IsMatch (number))
            {
                return number;
            }

            return false.ToString(); 

        }
    }
}
