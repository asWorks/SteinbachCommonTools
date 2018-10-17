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
    public class LinkViewsourcePage
    {
  private ListCollectionView _view;
        private CollectionViewSource _ViewSource;
        public CollectionViewSource ViewSource
        {
            get { return _ViewSource; }
            set { _ViewSource = value; }
        }
          
        public ListCollectionView View
        {
            get { return _view; }
            set { _view = value; }
        }


        public LinkViewsourcePage(Page page, IEnumerable source, String Ressourcename)
        {
            ViewSource = ((CollectionViewSource)(page.FindResource(Ressourcename)));
            ViewSource.Source = source;
            View = (ListCollectionView)ViewSource.View;
        }

        public LinkViewsourcePage(Page page, String Ressourcename)
        {
            ViewSource = ((CollectionViewSource)(page.FindResource(Ressourcename)));
            View = (ListCollectionView)ViewSource.View;
        }


        public void LinkViews(Page page, IEnumerable source, String Ressourcename)
        {
            ViewSource = ((CollectionViewSource)(page.FindResource(Ressourcename)));
            ViewSource.Source = source;
            View = (ListCollectionView)ViewSource.View;

        }

    
    }
}
