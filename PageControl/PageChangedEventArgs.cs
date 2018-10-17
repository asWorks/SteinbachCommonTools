using System;

namespace PageControl
{
    public class PageChangedEventArgs : EventArgs
    {
        public readonly int toSkip;
        public readonly int toTake;

        public PageChangedEventArgs(int skip, int take)
        {
            toSkip = skip;
            toTake = take;
        }


    }
}
