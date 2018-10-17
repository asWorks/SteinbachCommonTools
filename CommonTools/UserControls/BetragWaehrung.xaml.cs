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

namespace CommonTools.UserControls
{
    /// <summary>
    /// Interaction logic for BetragWaehrung.xaml
    /// </summary>
    public partial class BetragWaehrung : UserControl
    {
        public BetragWaehrung()
        {
            InitializeComponent();
           
        }

        //static BetragWaehrung()
 
        //{
 
        //    //This OverrideMetadata call tells the system that this element wants 

        //    //to provide a style that is different than its base class.
 
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(BetragWaehrung), 

        //        new FrameworkPropertyMetadata(typeof(BetragWaehrung)));
 
        //}

        //public BetragWaehrung()

        //    : base()
        //{

        //}




        public string TextBoxText
        {
            get
            {
                return (string)GetValue(TextBoxTextProperty);
            }
            set
            {
                SetValue(TextBoxTextProperty, value);
            }
        }

        public static readonly DependencyProperty TextBoxTextProperty = DependencyProperty.Register("TextBoxText", typeof(string), typeof(BetragWaehrung), null);





        public double TextBoxHeight
        {
            get { return (double)GetValue(TextBoxHeightProperty); }
            set { SetValue(TextBoxHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxHeightProperty =
            DependencyProperty.Register("TextBoxHeight", typeof(double), typeof(BetragWaehrung), null);



        public double MainStackPanelHeight
        {
            get { return (double)GetValue(MainStackPanelHeightProperty); }
            set { SetValue(MainStackPanelHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MainStackPanelHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainStackPanelHeightProperty =
            DependencyProperty.Register("MainStackPanelHeight", typeof(double), typeof(BetragWaehrung), null);



        public Thickness MainGridMargin
        {
            get { return (Thickness)GetValue(MainGridMarginProperty); }
            set { SetValue(MainGridMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MainGridMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainGridMarginProperty =
            DependencyProperty.Register("MainGridMargin", typeof(Thickness), typeof(BetragWaehrung), null);




        public Thickness StackPanelMargin
        {
            get { return (Thickness)GetValue(StackPanelMarginProperty); }
            set { SetValue(StackPanelMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StackPanelMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StackPanelMarginProperty =
            DependencyProperty.Register("StackPanelMargin", typeof(Thickness), typeof(BetragWaehrung), null);

        



        public TextAlignment TextBoxTextAlignment
        {
            get { return (TextAlignment)GetValue(TextBoxTextAlignmentProperty); }
            set { SetValue(TextBoxTextAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxTextAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxTextAlignmentProperty =
            DependencyProperty.Register("TextBoxTextAlignment", typeof(TextAlignment), typeof(BetragWaehrung), null);





        public Thickness TextBlockMargin
        {
            get { return (Thickness)GetValue(TextBlockMarginProperty); }
            set { SetValue(TextBlockMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockMarginProperty =
            DependencyProperty.Register("TextBlockMargin", typeof(Thickness), typeof(BetragWaehrung), null);





        public VerticalAlignment TextBoxVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(TextBoxVerticalAlignmentProperty); }
            set { SetValue(TextBoxVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxVerticalAlignmentProperty =
            DependencyProperty.Register("TextBoxVerticalAlignment", typeof(VerticalAlignment), typeof(BetragWaehrung), null);

        

        
        





        public string TextBlockText
        {
            get { return (string)GetValue(TextBlockTextProperty); }
            set { SetValue(TextBlockTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for  TextBlockText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockTextProperty =
            DependencyProperty.Register("TextBlockText", typeof(string), typeof(BetragWaehrung), null);




        public VerticalAlignment TextBlockVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(TExtBlockVerticalAlignmentProperty); }
            set { SetValue(TExtBlockVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TExtBlockVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TExtBlockVerticalAlignmentProperty =
            DependencyProperty.Register("TExtBlockVerticalAlignment", typeof(VerticalAlignment), typeof(BetragWaehrung), null);





        public double TextBlockWidth
        {
            get { return (double)GetValue(TextBlockWidthProperty); }
            set { SetValue(TextBlockWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockWidthProperty =
            DependencyProperty.Register("TextBlockWidth", typeof(double), typeof(BetragWaehrung), null);

        


        public IEnumerable CBoxItemssource
        {
            get { return (IEnumerable)GetValue(CBoxItemssourceProperty); }
            set { SetValue(CBoxItemssourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxItemssource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxItemssourceProperty =
            DependencyProperty.Register("CBoxItemssource", typeof(IEnumerable), typeof(BetragWaehrung),null);





        public string CBoxSelectedValue
        {
            get { return (string)GetValue(CBoxSelectedValueProperty); }
            set { SetValue(CBoxSelectedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxSelectedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxSelectedValueProperty =
            DependencyProperty.Register("CBoxSelectedValue", typeof(string), typeof(BetragWaehrung), null);




        public double CBoxHeight
        {
            get { return (double)GetValue(CBoxHeightProperty); }
            set { SetValue(CBoxHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxHeightProperty =
            DependencyProperty.Register("CBoxHeight", typeof(double), typeof(BetragWaehrung), null);

        





        public HorizontalAlignment CBoxHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(CBoxHorizontalAlignmentProperty); }
            set { SetValue(CBoxHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxHorizontalAlignmentProperty =
            DependencyProperty.Register("CBoxHorizontalAlignment", typeof(HorizontalAlignment), typeof(BetragWaehrung), null);



        public VerticalAlignment CBoxVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(CBoxVerticalAlignmentProperty); }
            set { SetValue(CBoxVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxVerticalAlignmentProperty =
            DependencyProperty.Register("CBoxVerticalAlignment", typeof(VerticalAlignment), typeof(BetragWaehrung), null);





        public double CBoxWidth
        {
            get { return (double)GetValue(CBoxWidthProperty); }
            set { SetValue(CBoxWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxWidthProperty =
            DependencyProperty.Register("CBoxWidth", typeof(double), typeof(BetragWaehrung), null);





        public Brush CBoxBackground
        {
            get { return (Brush)GetValue(CBoxBackgroundProperty); }
            set { SetValue(CBoxBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxBackgroundProperty =
            DependencyProperty.Register("CBoxBackground", typeof(Brush), typeof(BetragWaehrung), null);





        public Thickness CBoxMargin
        {
            get { return (Thickness)GetValue(CBoxMarginProperty); }
            set { SetValue(CBoxMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxMargin.  This enables animation, styling, binding, etc...TextBoxHeight
        public static readonly DependencyProperty CBoxMarginProperty =
            DependencyProperty.Register("CBoxMargin", typeof(Thickness), typeof(BetragWaehrung), null);




        public string CBoxSelectedValuePath
        {
            get { return (string)GetValue(CBoxSelectedValuePathProperty); }
            set { SetValue(CBoxSelectedValuePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxSelectedValuePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxSelectedValuePathProperty =
            DependencyProperty.Register("CBoxSelectedValuePath", typeof(string), typeof(BetragWaehrung), null);






        public string CBoxDisplayMemberPath
        {
            get { return (string)GetValue(CBoxDisplayMemberPathProperty); }
            set { SetValue(CBoxDisplayMemberPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxDisplayMemberPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxDisplayMemberPathProperty =
            DependencyProperty.Register("CBoxDisplayMemberPath", typeof(string), typeof(BetragWaehrung), null);

        

        



        
                      
        public FontFamily TextBlockFontFamily
        {
            get { return (FontFamily)GetValue(TextBlockFontFamilyProperty); }
            set { SetValue(TextBlockFontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockFontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockFontFamilyProperty =
            DependencyProperty.Register("TextBlockFontFamily", typeof(FontFamily), typeof(BetragWaehrung), null);



        public double TextBlockFontSize
        {
            get { return (double)GetValue(TextBlockFontSizeProperty); }
            set { SetValue(TextBlockFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockFontSizeProperty =
            DependencyProperty.Register("TextBlockFontSize", typeof(double), typeof(BetragWaehrung), null);





        public FontFamily ValueFontFamily
        {
            get { return (FontFamily)GetValue(ValueFontFamilyProperty); }
            set { SetValue(ValueFontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueFontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueFontFamilyProperty =
            DependencyProperty.Register("ValueFontFamily", typeof(FontFamily), typeof(BetragWaehrung), null);




        public double ValueFontSize
        {
            get { return (double)GetValue(ValueFontSizeProperty); }
            set { SetValue(ValueFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueFontSizeProperty =
            DependencyProperty.Register("ValueFontSize", typeof(double), typeof(BetragWaehrung), null);

        public Thickness TextBoxMargin
        {
            get { return (Thickness)GetValue(TextBoxMarginProperty); }
            set { SetValue(TextBoxMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxMarginProperty =
            DependencyProperty.Register("TextBoxMargin", typeof(Thickness), typeof(BetragWaehrung), null);




        public Thickness WaehrungBoxMargin
        {
            get { return (Thickness)GetValue(WaehrungBoxMarginProperty); }
            set { SetValue(WaehrungBoxMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WaehrungBoxMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaehrungBoxMarginProperty =
            DependencyProperty.Register("WaehrungBoxMargin", typeof(Thickness), typeof(BetragWaehrung), null);

        public Orientation StackPanelOrientation
        {
            get { return (Orientation)GetValue(StackPanelOrientationProperty); }
            set { SetValue(StackPanelOrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StackPanelOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StackPanelOrientationProperty =
            DependencyProperty.Register("StackPanelOrientation", typeof(Orientation), typeof(BetragWaehrung), null);


        public TextAlignment TextBlockTextAlignment
        {
            get { return (TextAlignment)GetValue(TextBlockTextAlignmentProperty); }
            set { SetValue(TextBlockTextAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockTextAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockTextAlignmentProperty =
            DependencyProperty.Register("TextBlockTextAlignment", typeof(TextAlignment), typeof(BetragWaehrung), null);
    }
   
}
