﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTypes
{
    public class FilteredComboBoxChangedEventArgs : EventArgs
    {

        public readonly string filter;

        public FilteredComboBoxChangedEventArgs(string NewFilter)
        {

            filter = NewFilter;
        }
    }
}
