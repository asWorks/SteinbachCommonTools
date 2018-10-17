using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Windows.Controls.Primitives;

namespace CommonTools.UserControls
{
    /// <summary>
    /// Interaction logic for FilteredComboBox_Extended.xaml
    /// </summary>
    public partial class FilteredComboBox_Extended : UserControl
    {
        public delegate void FiteredBoxChanged(object sender, DataTypes.FilteredComboBoxChangedEventArgs e);
        public delegate void FilteredBoxSelectionChanged(object sender, SelectionChangedEventArgs e);
        public event FiteredBoxChanged onfcbChanged;
        public event FilteredBoxSelectionChanged OnFcb_SelectionChanged;
        public event Func<DataTypes.FilteredComboBoxChangedEventArgs, IEnumerable> OnFCBChangedFunc;
        


        CollectionViewSource ViewSource;

        IEnumerable ComboBoxDataSource;

        bool bUser = false;

        public FilteredComboBox_Extended()
        {
            InitializeComponent();


        }


        public IEnumerable CBoxItemssource
        {
            get { return (IEnumerable)GetValue(CBoxItemssourceProperty); }
            set { SetValue(CBoxItemssourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxItemssource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxItemssourceProperty =
            DependencyProperty.Register("CBoxItemssource", typeof(IEnumerable), typeof(FilteredComboBox_Extended), null);






        public IEnumerable FilteredItemsSource
        {
            get { return (IEnumerable)GetValue(FilteredItemsSourceProperty); }
            set { SetValue(FilteredItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilteredItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilteredItemsSourceProperty =
           DependencyProperty.Register("FilteredItemsSource", typeof(IEnumerable), typeof(FilteredComboBox_Extended), null);










        public object CBoxSelectedValue
        {
            get { return (object)GetValue(CBoxSelectedValueProperty); }
            set { SetValue(CBoxSelectedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxSelectedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxSelectedValueProperty =
            DependencyProperty.Register("CBoxSelectedValue", typeof(object), typeof(FilteredComboBox_Extended), null);




        public object CBoxSelectedItem
        {
            get { return (object)GetValue(CBoxSelectedItemProperty); }
            set { SetValue(CBoxSelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxSelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxSelectedItemProperty =
            DependencyProperty.Register("CBoxSelectedItem", typeof(object), typeof(FilteredComboBox_Extended), null);




        public string CBoxSelectedValuePath
        {
            get { return (string)GetValue(CBoxSelectedValuePathProperty); }
            set { SetValue(CBoxSelectedValuePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxSelectedValuePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxSelectedValuePathProperty =
            DependencyProperty.Register("CBoxSelectedValuePath", typeof(string), typeof(FilteredComboBox_Extended), null);




        public string CBoxDisplayMemberPath
        {
            get { return (string)GetValue(CBoxDisplayMemberPathProperty); }
            set { SetValue(CBoxDisplayMemberPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxDisplayMemberPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxDisplayMemberPathProperty =
            DependencyProperty.Register("CBoxDisplayMemberPath", typeof(string), typeof(FilteredComboBox_Extended), null);




        public string DisplayedMemberpath
        {
            get { return (string)GetValue(DisplayedMemberpathProperty); }
            set { SetValue(DisplayedMemberpathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayedMemberpath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayedMemberpathProperty =
            DependencyProperty.Register("DisplayedMemberpath", typeof(string), typeof(FilteredComboBox_Extended), null);







        public Thickness CBoxMargin
        {
            get { return (Thickness)GetValue(CBoxMarginProperty); }
            set { SetValue(CBoxMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxMarginProperty =
            DependencyProperty.Register("CBoxMargin", typeof(Thickness), typeof(FilteredComboBox_Extended), null);



        public double CBoxWidth
        {
            get { return (double)GetValue(CBoxWidthProperty); }
            set { SetValue(CBoxWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxWidthProperty =
            DependencyProperty.Register("CBoxWidth", typeof(double), typeof(FilteredComboBox_Extended), null);
        

        public double CBoxHeight
        {
            get { return (double)GetValue(CBoxHeightProperty); }
            set { SetValue(CBoxHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxHeightProperty =
            DependencyProperty.Register("CBoxHeight", typeof(double), typeof(FilteredComboBox_Extended), null);




        public DataTemplate cBoxItemTemplate
        {
            get { return (DataTemplate)GetValue(cBoxItemTemplateProperty); }
            set { SetValue(cBoxItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cBoxItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cBoxItemTemplateProperty =
            DependencyProperty.Register("cBoxItemTemplate", typeof(DataTemplate), typeof(FilteredComboBox_Extended), null);




        public DataTemplate StoredItemTemplate
        {
            get { return (DataTemplate)GetValue(StoredItemTemplateProperty); }
            set { SetValue(StoredItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StoredItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StoredItemTemplateProperty =
            DependencyProperty.Register("StoredItemTemplate", typeof(DataTemplate), typeof(FilteredComboBox_Extended), null);







        public bool cBoxIsSynchronizedWithCurrentItem
        {
            get { return (bool)GetValue(cBoxIsSynchronizedWithCurrentItemProperty); }
            set { SetValue(cBoxIsSynchronizedWithCurrentItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cBoxIsSynchronizedWithCurrentItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cBoxIsSynchronizedWithCurrentItemProperty =
            DependencyProperty.Register("cBoxIsSynchronizedWithCurrentItem", typeof(bool), typeof(FilteredComboBox_Extended), null);


        

        //public DataTemplate cBoxItemTemplate
        //{
        //    get
        //    {
        //        return cBox.ItemTemplate;
        //    }

        //    set
        //    {
        //        this.cBox.ItemTemplate = value;
        //    }
        //}


        public Thickness ControlsMargin
        {
            get
            {
                return cBox.Margin;
            }
            set
            {
                cBox.Margin = value;
                tBox.Margin = value;
            }
        }

        public double ControlsHeight
        {
            get
            {
                return cBox.Height;
            }
            set
            {
                cBox.Height = value;
                tBox.Height = value;
            }
        }




        public IEnumerable ComboBoxViewSource
        {
            get { return ComboBoxDataSource; }
            set
            {
                ComboBoxDataSource = value;
                if (ViewSource != null)
                {
                    SetUser(false);
                    // ViewSource.Source = ComboBoxDataSource;
                    cBox.ItemsSource = value;
                }

            }
        }

        public double ControlsWidth
        {
            get { return cBox.Width; }
            set
            {
                cBox.Width = value;
                tBox.Width = value;


            }
        }




        public int MaxTextLength
        {
            get { return tBox.MaxLength; }
            set { tBox.MaxLength = value; }
        }


        public Brush ControlsBackground
        {
            get { return cBox.Background; }
            set
            {
                cBox.Background = value;
                tBox.Background = value;

            }
        }

        public Binding This_SelectedValue
        {
            get { return null; }
            set
            {
                cBox.SetBinding(Selector.SelectedValueProperty, value);

            }
        }

        public void SetSimpleBinding(string vPath, string vSelectedValuePath, string vDisplayedMemberPath, BindingMode vMode, UpdateSourceTrigger vTrigger)
        {
            Binding b = new Binding(vPath);
            b.Mode = vMode;
            b.UpdateSourceTrigger = vTrigger;
            cBox.SelectedValuePath = vSelectedValuePath;
            cBox.DisplayMemberPath = vDisplayedMemberPath;
            cBox.SetBinding(Selector.SelectedValueProperty, b);


        }

        public void SetSimpleBinding(string vPath, string vSelectedValuePath, DataTemplate vTemplate, BindingMode vMode, UpdateSourceTrigger vTrigger)
        {
            Binding b = new Binding(vPath);
            b.Mode = vMode;
            b.UpdateSourceTrigger = vTrigger;
            cBox.SelectedValuePath = vSelectedValuePath;
            cBox.ItemTemplate = vTemplate;
            cBox.SetBinding(Selector.SelectedValueProperty, b);

        }

        public string ComboBoxText
        {
            get
            {
                return cBox.Text;
            }

            set { ;}
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cBoxViewSource")));
            if (ComboBoxDataSource != null)
            {
                SetUser(false);
                //ViewSource.Source = ComboBoxDataSource;
            }
            //ControlsWidth = 80;
            //ControlsHeight = 25;
            //ControlsMargin = new Thickness(0,0,0,0);
            //ControlsBackground = Brushes.White;


        }

        private void SetUser(bool State)
        {
            bUser = State;

            if (State)
            {
                tBox.Background = Brushes.Yellow;
                tBox.Foreground = Brushes.Red;
            }
            else
            {
                tBox.Background = Brushes.White;
                tBox.Foreground = Brushes.Black;
            }
        }

        private void cBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SetUser(true);
            if (e.Text != "\r")
            {

                tBox.Visibility = System.Windows.Visibility.Visible;
                cBox.Visibility = Visibility.Collapsed;

                tBox.SelectionStart = tBox.Text.Length;
                tBox.Focus();
            }
        }

        private void tBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {

                SetUser(true);
                tBox.Visibility = System.Windows.Visibility.Collapsed;
                cBox.Visibility = Visibility.Visible;
               
                if (StoredItemTemplate != null)
                {
                    cBoxItemTemplate = StoredItemTemplate;
                    CBoxDisplayMemberPath = string.Empty;
                }


                if (OnFCBChangedFunc != null)
                {

                    cBox.ItemsSource = OnFCBChangedFunc(new DataTypes.FilteredComboBoxChangedEventArgs(tBox.Text));

                }

                if (onfcbChanged != null)
                {
                    onfcbChanged(this, new DataTypes.FilteredComboBoxChangedEventArgs(tBox.Text));

                    if (FilteredItemsSource != null)
                    {
                        cBox.ItemsSource = FilteredItemsSource;

                    }




                    //cBox.IsDropDownOpen = true;
                }



                cBox.Focus();
                cBox.Text = tBox.Text;
                tBox.Text = "";
                cBox.IsDropDownOpen = true;
            }
        }

     

        private void cBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            cBox.ItemsSource = CBoxItemssource;
            if (DisplayedMemberpath != null)
            {
                CBoxDisplayMemberPath = DisplayedMemberpath;
                cBoxItemTemplate = null;
            }


            if (OnFcb_SelectionChanged != null)
            {
                if (bUser)
                {

                    OnFcb_SelectionChanged(this.cBox, e);
                }

            }


        }

        private void cBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetUser(true);
            if (StoredItemTemplate != null)
            {
                cBoxItemTemplate = StoredItemTemplate;
                CBoxDisplayMemberPath = string.Empty;
            }
        }

        private void cBox_KeyDown(object sender, KeyEventArgs e)
        {
            SetUser(true);
        }

        public void Reset()
        {
            tBox.Text = "";
            cBox.Text = "";
            tBox.Visibility = System.Windows.Visibility.Collapsed;
            cBox.Visibility = Visibility.Visible;
            // cBox.ItemsSource = null;


        }

        private void cBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

    }
}
