using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CommonTools.Tools
{
  public static class ListViewHelper 
   { 
        /// <summary> 
        ///Finds an element that has the provided identifier name within the specified cell template identified by the cellColumn and cellRow 
       /// </summary> 
        /// <param name="listView">the referenced ListView control</param> 
        /// <param name="cellColumn">the index of column from which the CellTemplate will be retrieved</param> 
        /// <param name="cellRow">the index of the row from which the specified ListViewItem will be retrieved</param> 
        /// <param name="name">The name of the requested element.</param> 
        /// <returns>The requested element. This can be null reference if no matching element was found.</returns> 
       public static FrameworkElement FindNameFromCellTemplate(ListView listView, Int32 cellColumn, Int32 cellRow, String name) 
        { 
            if(listView == null) 
            { 
                throw new ArgumentNullException("listView"); 
            } 

            if(!listView.IsLoaded) 
            { 
                throw new InvalidOperationException("ListView is not yet loaded"); 
            } 

            if (cellRow >= listView.Items.Count || cellRow < 0) 
            { 
                throw new ArgumentOutOfRangeException("row"); 
            } 

            GridView gridView = listView.View as GridView; 
            if (gridView == null) 
            { 
                return null; 
            } 

            if (cellColumn >= gridView.Columns.Count || cellColumn < 0) 
            { 
                throw new ArgumentOutOfRangeException("column"); 
            } 

            ListViewItem item = listView.ItemContainerGenerator.ContainerFromItem(listView.Items[cellRow]) as ListViewItem; 
            if (item != null) 
            { 
                if (!item.IsLoaded) 
                { 
                    item.ApplyTemplate(); 
                } 
                GridViewRowPresenter rowPresenter = GetFrameworkElementByName<GridViewRowPresenter>(item); 

                if(rowPresenter != null) 
                { 
                    ContentPresenter templatedParent = VisualTreeHelper.GetChild(rowPresenter, cellColumn) as ContentPresenter; 
                    DataTemplate dataTemplate = gridView.Columns[cellColumn].CellTemplate; 
                    if(dataTemplate != null && templatedParent != null) 
                    { 
                        return dataTemplate.FindName(name, templatedParent) as FrameworkElement; 
                    } 
                } 
            } 

            return null; 
        } 

        private static T GetFrameworkElementByName<T>(FrameworkElement referenceElement) where T : FrameworkElement 
       { 
            FrameworkElement child = null; 
            for(Int32 i = 0; i < VisualTreeHelper.GetChildrenCount(referenceElement); i++) 
            { 
                child = VisualTreeHelper.GetChild(referenceElement, i) as FrameworkElement; 
                if(child != null && child.GetType() == typeof(T)) 
                { 
                    break; 
                } 
                else if(child != null) 
                { 
                    child = GetFrameworkElementByName<T>(child); 
                    if(child != null && child.GetType() == typeof(T)) 
                    { 
                        break; 
                    } 
                } 
            } 
            return child as T; 
        } 
    } 
} 

