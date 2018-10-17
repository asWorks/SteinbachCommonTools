﻿using System;
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

namespace CommonTools.UserControls
{
    /// <summary>
    /// Interaction logic for LabelAndText.xaml
    /// </summary>
    public partial class LabelAndText : UserControl
    {
        public delegate void TextBoxText_Changed(object sender, TextChangedEventArgs e);
        public event TextBoxText_Changed onTextBox_TextChanged;

        public event Action<object, MouseButtonEventArgs>  onMouseDoubleClick;
        public event Action<object, KeyboardFocusChangedEventArgs> onGotKeyboardFocus;
        public event Action<object, MouseButtonEventArgs> onPreviewMouseLeftButtonDown;


       //  private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
     //   private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        public LabelAndText()
        {
            InitializeComponent();
            tBox.Height = 21;

        }




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

        public static readonly DependencyProperty TextBoxTextProperty = DependencyProperty.Register("TextBoxText", typeof(string), typeof(LabelAndText), null);




        public bool TextBoxAcceptsReturn
        {
            get { return (bool)GetValue(TextBoxAcceptsReturnProperty); }
            set { SetValue(TextBoxAcceptsReturnProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxAcceptsReturn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxAcceptsReturnProperty =
            DependencyProperty.Register("TextBoxAcceptsReturn", typeof(bool), typeof(LabelAndText),null );

        



        public double TextBoxMaxLength
        {
            get { return (double)GetValue(TextBoxMaxLengthProperty); }
            set { SetValue(TextBoxMaxLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxMaxLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxMaxLengthProperty =
            DependencyProperty.Register("TextBoxMaxLength", typeof(double), typeof(LabelAndText), null);

        



        public string TextBlockText
        {
            get { return (string)GetValue(TextBlockTextProperty); }
            set { SetValue(TextBlockTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for  TextBlockText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockTextProperty =
            DependencyProperty.Register("TextBlockText", typeof(string), typeof(LabelAndText), null);



        public double TextBoxWidth
        {
            get { return (double)GetValue(TextBoxWidthProperty); }
            set { SetValue(TextBoxWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxWidthProperty =
            DependencyProperty.Register("TextBoxWidth", typeof(double), typeof(LabelAndText), null);



        public double TextBlockWidth
        {
            get { return (double)GetValue(TextBlockWidthProperty); }
            set { SetValue(TextBlockWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockWidthProperty =
            DependencyProperty.Register("TextBlockWidth", typeof(double), typeof(LabelAndText), null);

        



        public Orientation StackPanelOrientation
        {
            get { return (Orientation)GetValue(StackPanelOrientationProperty); }
            set { SetValue(StackPanelOrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StackPanelOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StackPanelOrientationProperty =
            DependencyProperty.Register("StackPanelOrientation", typeof(Orientation), typeof(LabelAndText), null);




        public TextAlignment TextBoxTextAlignment
        {
            get { return (TextAlignment)GetValue(TextBoxTextAlignmentProperty); }
            set { SetValue(TextBoxTextAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxTextAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxTextAlignmentProperty =
            DependencyProperty.Register("TextBoxTextAlignment", typeof(TextAlignment), typeof(LabelAndText), null);



        public HorizontalAlignment TextBlockHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(TextBlockHorizontalAlignmentProperty); }
            set { SetValue(TextBlockHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockHorizontalAlignmentProperty =
            DependencyProperty.Register("TextBlockHorizontalAlignment", typeof(HorizontalAlignment), typeof(LabelAndText), null);

        


        public TextAlignment TextBlockTextAlignment
        {
            get { return (TextAlignment)GetValue(TextBlockTextAlignmentProperty); }
            set { SetValue(TextBlockTextAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockTextAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockTextAlignmentProperty =
            DependencyProperty.Register("TextBlockTextAlignment", typeof(TextAlignment), typeof(LabelAndText), null);





        public Thickness TextBlockMargin
        {
            get { return (Thickness)GetValue(TextBlockMarginProperty); }
            set { SetValue(TextBlockMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockMarginProperty =
            DependencyProperty.Register("TextBlockMargin", typeof(Thickness), typeof(LabelAndText), null);

        


        public Thickness TextBoxMargin
        {
            get { return (Thickness)GetValue(TextBoxMarginProperty); }
            set { SetValue(TextBoxMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxMarginProperty =
            DependencyProperty.Register("TextBoxMargin", typeof(Thickness), typeof(LabelAndText), null);




        public FontFamily TextBlockFontFamily
        {
            get { return (FontFamily)GetValue(TextBlockFontFamilyProperty); }
            set { SetValue(TextBlockFontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockFontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockFontFamilyProperty =
            DependencyProperty.Register("TextBlockFontFamily", typeof(FontFamily), typeof(LabelAndText), null);



        public double TextBlockFontSize
        {
            get { return (double)GetValue(TextBlockFontSizeProperty); }
            set { SetValue(TextBlockFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockFontSizeProperty =
            DependencyProperty.Register("TextBlockFontSize", typeof(double), typeof(LabelAndText), null);


        public FontFamily ValueFontFamily
        {
            get { return (FontFamily)GetValue(ValueFontFamilyProperty); }
            set { SetValue(ValueFontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueFontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueFontFamilyProperty =
            DependencyProperty.Register("ValueFontFamily", typeof(FontFamily), typeof(LabelAndText), null);




        public double ValueFontSize
        {
            get { return (double)GetValue(ValueFontSizeProperty); }
            set { SetValue(ValueFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueFontSizeProperty =
            DependencyProperty.Register("ValueFontSize", typeof(double), typeof(LabelAndText), null);

        private void tBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (onTextBox_TextChanged != null)
            {
                onTextBox_TextChanged(this.tBox, e);
            }

        }

        private void tBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (onMouseDoubleClick != null)
            {
                onMouseDoubleClick(this.tBox, e);
            }

        }

        private void tBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (onGotKeyboardFocus != null)
            {
                onGotKeyboardFocus(this.tBox, e);
            }
        }

        private void tBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (onPreviewMouseLeftButtonDown != null)
            {
                onPreviewMouseLeftButtonDown(this.tBox, e);
            }
        }

    }

}

