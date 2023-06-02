using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace I2MS2.UserControls
{

    public class TextHelper : DependencyObject
    {
        //------------------------------------
        // 숫자 (0~9, 컴마, 마이너스, 쩜)
        //------------------------------------
        public static bool GetIsNumeric(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsNumericProperty);
        }

        public static void SetIsNumeric(DependencyObject obj, bool value)
        {
            obj.SetValue(IsNumericProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsNumeric.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsNumericProperty =
             DependencyProperty.RegisterAttached("IsNumeric", typeof(bool), typeof(TextHelper), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
             {
                 TextBox targetTextbox = s as TextBox;
                 if (targetTextbox != null)
                 {
                     if ((bool)e.OldValue && !((bool)e.NewValue))
                     {
                         targetTextbox.PreviewTextInput -= targetTextbox_PreviewTextInput;

                     }
                     if ((bool)e.NewValue)
                     {
                         targetTextbox.PreviewTextInput += targetTextbox_PreviewTextInput;
                         targetTextbox.PreviewKeyDown += targetTextbox_PreviewKeyDown;
                     }
                 }
             })));

        static void targetTextbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        static void targetTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Char newChar = e.Text.ToString()[0];
            e.Handled = true;

            TextBox targetTextbox = sender as TextBox;
            int max_length = targetTextbox.MaxLength;
            if (max_length == 0)
                max_length = 10;
            string t1 = targetTextbox.Text;

            if (t1.Length < max_length)
            {
                if (Char.IsDigit(newChar) || Char.IsControl(newChar) || (newChar == '.') || (newChar == '-'))
                {
                    e.Handled = false;
                }

            }
        }

        //------------------------------------
        // 한글포함 텍스트(모든 글자)
        //------------------------------------
        public static bool GetIsText(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsTextProperty);
        }

        public static void SetIsText(DependencyObject obj, bool value)
        {
            obj.SetValue(IsTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsNumeric.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTextProperty =
             DependencyProperty.RegisterAttached("IsText", typeof(bool), typeof(TextHelper), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
             {
                 TextBox targetTextbox = s as TextBox;
                 if (targetTextbox != null)
                 {
                     if ((bool)e.OldValue && !((bool)e.NewValue))
                     {
                         targetTextbox.PreviewTextInput -= targetTextbox_PreviewTextInput2;

                     }
                     if ((bool)e.NewValue)
                     {
                         targetTextbox.PreviewTextInput += targetTextbox_PreviewTextInput2;
                         targetTextbox.PreviewKeyDown += targetTextbox_PreviewKeyDown2;
                     }
                 }
             })));

        static void targetTextbox_PreviewKeyDown2(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        static void targetTextbox_PreviewTextInput2(object sender, TextCompositionEventArgs e)
        {
            //Char newChar = e.Text.ToString()[0];
            e.Handled = true;

            TextBox targetTextbox = sender as TextBox;
            string t1 = targetTextbox.Text;

            int byteCount = Encoding.Default.GetByteCount(t1.ToString());
            if (byteCount < targetTextbox.MaxLength)
                e.Handled = false;
        }
    }
}