using System;

namespace DAL
{
    public class CompareSI_InventurenPositionen : System.Collections.Generic.IEqualityComparer<SI_InventurenPositionen>
    {

        public bool Equals(SI_InventurenPositionen p1, SI_InventurenPositionen p2)
        {
            if (p1.id_artikel == p2.id_artikel)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public int GetHashCode(SI_InventurenPositionen bx)
        {
            int hCode = bx.id;
            return hCode.GetHashCode();
        }


    }
}
