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
    /// Interaction logic for LabelAndFilteredComboBox.xaml
    /// </summary>
    public partial class LabelAndFilteredComboBox : UserControl
    {
        public delegate void FiteredBoxChanged(object sender, FilteredComboBoxChangedEventArgs e);
        public delegate void FilteredBoxSelectionChanged(object sender, SelectionChangedEventArgs e);
        public event FiteredBoxChanged onfcbChanged;
        public event FilteredBoxSelectionChanged OnFcb_SelectionChanged;

        CollectionViewSource ViewSource;

        IEnumerable ComboBoxDataSource;
        //   System.Collections.IEnumerable ComboBoxDataSource;
        bool bUser = false;

        public LabelAndFilteredComboBox()
        {
            InitializeComponent();

        }


        public string TextBlockText
        {
            get { return (string)GetValue(TextBlockTextProperty); }
            set { SetValue(TextBlockTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for  TextBlockText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockTextProperty =
            DependencyProperty.Register("TextBlockText", typeof(string), typeof(LabelAndFilteredComboBox), null);

        public double TextBlockWidth
        {
            get { return (double)GetValue(TextBlockWidthProperty); }
            set { SetValue(TextBlockWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockWidthProperty =
            DependencyProperty.Register("TextBlockWidth", typeof(double), typeof(LabelAndFilteredComboBox), null);


        public HorizontalAlignment TextBlockHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(TextBlockHorizontalAlignmentProperty); }
            set { SetValue(TextBlockHorizontalAlignmentProperty, value); }
        }



        // Using a DependencyProperty as the backing store for TextBlockHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockHorizontalAlignmentProperty =
            DependencyProperty.Register("TextBlockHorizontalAlignment", typeof(HorizontalAlignment), typeof(LabelAndFilteredComboBox), null);


        public TextAlignment TextBlockTextAlignment
        {
            get { return (TextAlignment)GetValue(TextBlockTextAlignmentProperty); }
            set { SetValue(TextBlockTextAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockTextAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockTextAlignmentProperty =
            DependencyProperty.Register("TextBlockTextAlignment", typeof(TextAlignment), typeof(LabelAndFilteredComboBox), null);

        public FontFamily TextBlockFontFamily
        {
            get { return (FontFamily)GetValue(TextBlockFontFamilyProperty); }
            set { SetValue(TextBlockFontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockFontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockFontFamilyProperty =
            DependencyProperty.Register("TextBlockFontFamily", typeof(FontFamily), typeof(LabelAndFilteredComboBox), null);



        public double TextBlockFontSize
        {
            get { return (double)GetValue(TextBlockFontSizeProperty); }
            set { SetValue(TextBlockFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockFontSizeProperty =
            DependencyProperty.Register("TextBlockFontSize", typeof(double), typeof(LabelAndFilteredComboBox), null);

        public Orientation StackPanelOrientation
        {
            get { return (Orientation)GetValue(StackPanelOrientationProperty); }
            set { SetValue(StackPanelOrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StackPanelOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StackPanelOrientationProperty =
            DependencyProperty.Register("StackPanelOrientation", typeof(Orientation), typeof(LabelAndFilteredComboBox), null);



        public object CBoxIsSynchronizedWithCurrentItem
        {
            get { return (object)GetValue(CBoxIsSynchronizedWithCurrentItemProperty); }
            set { SetValue(CBoxIsSynchronizedWithCurrentItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxIsSynchronizedWithCurrentItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxIsSynchronizedWithCurrentItemProperty =
            DependencyProperty.Register("CBoxIsSynchronizedWithCurrentItem", typeof(object), typeof(LabelAndFilteredComboBox), null);

        



        public IEnumerable CBoxItemssource
        {
            get { return (IEnumerable)GetValue(CBoxItemssourceProperty); }
            set { SetValue(CBoxItemssourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxItemssource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxItemssourceProperty =
            DependencyProperty.Register("CBoxItemssource", typeof(IEnumerable), typeof(LabelAndFilteredComboBox), null);




        public object CBoxSelectedItem
        {
            get { return (object)GetValue(CBoxSelectedItemProperty); }
            set { SetValue(CBoxSelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxSelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxSelectedItemProperty =
            DependencyProperty.Register("CBoxSelectedItem", typeof(object), typeof(LabelAndFilteredComboBox), null);





        //public Selector CBoxSelectedItem
        //{
        //    get { return (Selector)GetValue(CBoxSelectedItemProperty); }
        //    set { SetValue(CBoxSelectedItemProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for CBoxSelectedItem.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CBoxSelectedItemProperty =
        //    DependencyProperty.Register("CBoxSelectedItem", typeof(Selector), typeof(LabelAndFilteredComboBox), null);

        



        public Brush CBoxBackground     
        {
            get { return (Brush)GetValue(CBoxBackgroundProperty); }
            set { SetValue(CBoxBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxBackgroundProperty =
            DependencyProperty.Register("CBoxBackground", typeof(Brush), typeof(LabelAndFilteredComboBox), null);

        


        public object CBoxSelectedValue
        {
            get { return (object)GetValue(CBoxSelectedValueProperty); }
            set { SetValue(CBoxSelectedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxSelectedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxSelectedValueProperty =
            DependencyProperty.Register("CBoxSelectedValue", typeof(object), typeof(LabelAndFilteredComboBox), null);




        public string CBoxSelectedValuePath
        {
            get { return (string)GetValue(CBoxSelectedValuePathProperty); }
            set { SetValue(CBoxSelectedValuePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxSelectedValuePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxSelectedValuePathProperty =
            DependencyProperty.Register("CBoxSelectedValuePath", typeof(string), typeof(LabelAndFilteredComboBox), null);




        public string CBoxDisplayMemberPath
        {
            get { return (string)GetValue(CBoxDisplayMemberPathProperty); }
            set { SetValue(CBoxDisplayMemberPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxDisplayMemberPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxDisplayMemberPathProperty =
            DependencyProperty.Register("CBoxDisplayMemberPath", typeof(string), typeof(LabelAndFilteredComboBox), null);



        public Thickness TextBlockMargin
        {
            get { return (Thickness)GetValue(TextBlockMarginProperty); }
            set { SetValue(TextBlockMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockMarginProperty =
            DependencyProperty.Register("TextBlockMargin", typeof(Thickness), typeof(LabelAndFilteredComboBox), null);




        public Thickness TextBoxMargin
        {
            get { return (Thickness)GetValue(TextBoxMarginProperty); }
            set { SetValue(TextBoxMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxMarginProperty =
            DependencyProperty.Register("TextBoxMargin", typeof(Thickness), typeof(LabelAndFilteredComboBox), null);




        public Thickness ComboBoxMargin
        {
            get { return (Thickness)GetValue(ComboBoxMarginProperty); }
            set { SetValue(ComboBoxMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComboBoxMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComboBoxMarginProperty =
            DependencyProperty.Register("ComboBoxMargin", typeof(Thickness), typeof(LabelAndFilteredComboBox), null);

    

        public double CBoxWidth
        {
            get { return (double)GetValue(CBoxWidthProperty); }
            set { SetValue(CBoxWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxWidthProperty =
            DependencyProperty.Register("CBoxWidth", typeof(double), typeof(LabelAndFilteredComboBox), null);




        public double CBoxHeight
        {
            get { return (double)GetValue(CBoxHeightProperty); }
            set { SetValue(CBoxHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxHeightProperty =
            DependencyProperty.Register("CBoxHeight", typeof(double), typeof(LabelAndFilteredComboBox), null);





        public DataTemplate cBoxItemTemplate
        {
            get
            {
                return cBox.ItemTemplate;
            }

            set
            {
                this.cBox.ItemTemplate = value;
            }
        }
        //public Thickness ControlsMargin
        //{
        //    get
        //    {
        //        return cBox.Margin;
        //    }
        //    set
        //    {
        //        cBox.Margin = value;
        //        tBox.Margin = value;
        //    }
        //}

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

                cBox.IsDropDownOpen = true;

                if (onfcbChanged != null)
                {
                    onfcbChanged(this, new FilteredComboBoxChangedEventArgs(tBox.Text));
                }


                tBox.Text = "";
                cBox.Focus();
                cBox.IsDropDownOpen = true;
            }
        }

        private void cBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
        }

        private void cBox_KeyDown(object sender, KeyEventArgs e)
        {
            SetUser(true);
        }
    }
}
