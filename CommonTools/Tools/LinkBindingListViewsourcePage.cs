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
    public class LinkBindingListViewsourcePage
    {
        private BindingListCollectionView _view;
        private CollectionViewSource _ViewSource;
        public CollectionViewSource ViewSource
        {
            get { return _ViewSource; }
            set { _ViewSource = value; }
        }

        public BindingListCollectionView View
        {
            get { return _view; }
            set { _view = value; }
        }


        public LinkBindingListViewsourcePage(Page page, IEnumerable source, String Ressourcename)
        {
            ViewSource = ((CollectionViewSource)(page.FindResource(Ressourcename)));
            ViewSource.Source = source;
            View = (BindingListCollectionView)ViewSource.View;
        }

        public LinkBindingListViewsourcePage(Page page, String Ressourcename)
        {
            ViewSource = ((CollectionViewSource)(page.FindResource(Ressourcename)));
            View = (BindingListCollectionView)ViewSource.View;
        }


        public void LinkViews(Page page, IEnumerable source, String Ressourcename)
        {
            ViewSource = ((CollectionViewSource)(page.FindResource(Ressourcename)));
            ViewSource.Source = source;
            View = (BindingListCollectionView)ViewSource.View;

        }


    }
}
