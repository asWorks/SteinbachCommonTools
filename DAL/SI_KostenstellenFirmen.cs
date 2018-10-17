using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class SI_KostenstellenFirmen
    {
        partial void OnSchiffChanged()
        {
            if (Schiff == 1)
            {
                Land = 0;
            }
        }

        partial void OnLandChanged()
        {
            if (Land == 1)
            {
                Schiff = 0;
            }
        }
    }
}
