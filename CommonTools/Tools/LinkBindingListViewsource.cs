using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections;

namespace CommonTools.Tools
{
    public class LinkBindingListViewsource<T,C> where T:Control
    {
     //   private BindingListCollectionView _view;
        private C _view;
        private CollectionViewSource _ViewSource;
        public CollectionViewSource ViewSource
        {
            get { return _ViewSource; }
            set { _ViewSource = value; }
        }

        public C View
        {
            get { return _view; }
            set { _view = value; }
        }


        public LinkBindingListViewsource(T page, IEnumerable source, String Ressourcename)
        {
            ViewSource = ((CollectionViewSource)(page.FindResource(Ressourcename)));
            ViewSource.Source = source;
            View = (C)ViewSource.View;
        }

        public LinkBindingListViewsource(T page, String Ressourcename)
        {
            ViewSource = ((CollectionViewSource)(page.FindResource(Ressourcename)));
            View = (C)ViewSource.View;
        }


        public void LinkViews(T page, IEnumerable source, String Ressourcename)
        {
            ViewSource = ((CollectionViewSource)(page.FindResource(Ressourcename)));
            ViewSource.Source = source;
            View = (C)ViewSource.View;

        }


    }
}
