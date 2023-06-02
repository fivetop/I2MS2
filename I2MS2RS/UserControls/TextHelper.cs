using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace I2MS2.UserControls
{

    public class TextHelper : DependencyObject
    {
        #region // 스페셜 특수문자 적용   숫자 + '.' + '-' , IP, TEL

        public static Char GetSpecial(DependencyObject obj)
        {
            return (Char)obj.GetValue(SpecialProperty);
        }

        public static void SetSpecial(DependencyObject obj, Char value)
        {
            obj.SetValue(SpecialProperty, value);
        }

        public static readonly DependencyProperty SpecialProperty =
            DependencyProperty.RegisterAttached("Special", typeof(Char), typeof(TextHelper), new PropertyMetadata('0', onSpecial));

        private static void onSpecial(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextHelper t1 = d as TextHelper;
        }

        public static int GetMin(DependencyObject obj)
        {
            return (int)obj.GetValue(MinProperty);
        }

        public static void SetMin(DependencyObject obj, int value)
        {
            obj.SetValue(MinProperty, value);
        }

        public static int GetMax(DependencyObject obj)
        {
            return (int)obj.GetValue(MaxProperty);
        }

        public static void SetMax(DependencyObject obj, int value)
        {
            obj.SetValue(MaxProperty, value);
        }

        // Using a DependencyProperty as the backing store for Max.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.RegisterAttached("Max", typeof(int), typeof(TextHelper), new PropertyMetadata(0));

        
        // Using a DependencyProperty as the backing store for Min.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinProperty =
            DependencyProperty.RegisterAttached("Min", typeof(int), typeof(TextHelper), new PropertyMetadata(0));

        
        //------------------------------------
        // 숫자 (0~9, 컴마, 마이너스, 쩜)         숫자/ 특수 컴머, 마이너스 , 점
        //------------------------------------
        public static bool GetIsNumeric(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsNumericProperty);
        }

        public static void SetIsNumeric(DependencyObject obj, bool value)
        {
            obj.SetValue(IsNumericProperty, value);
        }

        // 숫자 + '.' + '-' , IP, TEL
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

        // 2016.04.22 romee special charecter 

        static void targetTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Char newChar;

            try
            {
                newChar = e.Text.ToString()[0];
            }
            catch
            {
                e.Handled = true;
                return;
            }
            e.Handled = true;

            TextBox targetTextbox = sender as TextBox;
            Char aa = (Char) targetTextbox.GetValue(TextHelper.SpecialProperty);

            int max_length = targetTextbox.MaxLength;
            if (max_length == 0)
                max_length = 10;
            string t1 = targetTextbox.Text;

            if (t1.Length < max_length)
            {
                if (Char.IsDigit(newChar) || Char.IsControl(newChar))
                {
                    e.Handled = false;
                }
                else if ((aa != '0') && (newChar == aa))
                {
                    e.Handled = false;
                }
            }
        }
        #endregion

        #region //  한글/영문 대소/ 숫자 특수 문자 제외 2016.04.22 romee
        public static bool GetIsAlphaNumeric(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAlphaNumericProperty);
        }

        public static void SetIsAlphaNumeric(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAlphaNumericProperty, value);
        }

        // 한글/영문 대소/ 숫자 특수 문자 제외 2016.04.22 romee 
        public static readonly DependencyProperty IsAlphaNumericProperty =
             DependencyProperty.RegisterAttached("IsAlphaNumeric", typeof(bool), typeof(TextHelper), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
             {
                 TextBox targetTextbox = s as TextBox;
                 if (targetTextbox != null)
                 {
                     if ((bool)e.OldValue && !((bool)e.NewValue))
                     {
                         targetTextbox.PreviewTextInput -= targetTextbox_PreviewTextInput4;

                     }
                     if ((bool)e.NewValue)
                     {
                         targetTextbox.PreviewTextInput += targetTextbox_PreviewTextInput4;
                         targetTextbox.PreviewKeyDown += targetTextbox_PreviewKeyDown4;
                     }
                 }
             })));

        static void targetTextbox_PreviewKeyDown4(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;

            //            Console.WriteLine("{0}", e.Key.ToString());
            if (e.Key == Key.ImeProcessed)
                e.Handled = false;
            //var char_ASCII = event.keyCode;

            TextBox targetTextbox = sender as TextBox;
            string t1 = targetTextbox.Text;

            int byteCount = Encoding.Default.GetByteCount(t1.ToString());    // 한글 카운트 용 

            if (byteCount > targetTextbox.MaxLength)
            {
                e.Handled = true;
                if (e.Key == Key.Back)
                    e.Handled = false;

            }
        }

        static void targetTextbox_PreviewTextInput4(object sender, TextCompositionEventArgs e)
        {
            Char newChar;

            try
            {
                newChar = e.Text.ToString()[0];
            }
            catch
            {
                e.Handled = false;
                return;
            }
            e.Handled = true;

            TextBox targetTextbox = sender as TextBox;
            string t1 = targetTextbox.Text;

            int byteCount = Encoding.Default.GetByteCount(t1.ToString());
            if (byteCount < targetTextbox.MaxLength)
            {
                if ((newChar >= 33 && newChar <= 44) || (newChar >= 58 && newChar <= 64) || (newChar >= 91 && newChar <= 96) || (newChar >= 123 && newChar <= 126))
                {
                    // (45 -) (46 .) (47 /) 제외 
                    e.Handled = true;
                    //                    if (newChar == '@' || newChar == '#' || newChar == '(' || newChar == ')' || newChar == '-' || newChar == '_')
                    //                        e.Handled = false;
                }
                else
                    e.Handled = false;
            }
            else
            {
                Console.WriteLine("총 글자수는 {0}자를 초과할 수 없습니다.", byteCount);
            }
        }
        #endregion

        #region //  한글/영문 대소/ 숫자 특수 문자 제외 2016.04.22 romee
        public static bool GetIsHan(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsHanProperty);
        }

        public static void SetIsHan(DependencyObject obj, bool value)
        {
            obj.SetValue(IsHanProperty, value);
        }

        // 한글/영문 대소/ 숫자 특수 문자 제외 2016.04.22 romee 
        public static readonly DependencyProperty IsHanProperty =
             DependencyProperty.RegisterAttached("IsHan", typeof(bool), typeof(TextHelper), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
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
            // GS_DEL 
            // 스페이스 입력 필드와 아닌필드 분리 필요 , 우선 스페이스 허가 처리 romee 2016.08.10
            //e.Handled = e.Key == Key.Space;

            //            Console.WriteLine("{0}", e.Key.ToString());
            if (e.Key == Key.ImeProcessed)
                e.Handled = false;
            //var char_ASCII = event.keyCode;

            TextBox targetTextbox = sender as TextBox;
            string t1 = targetTextbox.Text;

            int byteCount = Encoding.Default.GetByteCount(t1.ToString());    // 한글 카운트 용 

            if ((byteCount) > (targetTextbox.MaxLength))
            {
                e.Handled = true;
                if (e.Key == Key.Back)
                    e.Handled = false;

            }
        }

        static void targetTextbox_PreviewTextInput2(object sender, TextCompositionEventArgs e)
        {
            Char newChar;

            try
            {
                newChar = e.Text.ToString()[0];
            }
            catch
            {
                e.Handled = false;
                return;
            }
            e.Handled = true;

            TextBox targetTextbox = sender as TextBox;
            string t1 = targetTextbox.Text;

            int byteCount = Encoding.Default.GetByteCount(t1.ToString());
            if (byteCount < targetTextbox.MaxLength)
            {
                e.Handled = false;
            }
            else
            {
                Console.WriteLine("총 글자수는 {0}자를 초과할 수 없습니다.", byteCount);
            }
        }
        #endregion

        #region //  정규식 검사 IP, EMAIL, TEL

        //------------------------------------
        // 한글포함 텍스트(모든 글자)       한글/영문 대소/ 숫자/ 특수
        // 사용 안함 
        //------------------------------------
        public static bool GetIsVal(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsValProperty);
        }

        public static void SetIsVal(DependencyObject obj, bool value)
        {
            obj.SetValue(IsValProperty, value);
        }

        // 한글포함 텍스트(모든 글자)       한글/영문 대소/ 숫자/ 특수
        public static readonly DependencyProperty IsValProperty =
             DependencyProperty.RegisterAttached("IsVal", typeof(bool), typeof(TextHelper), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
             {
                 TextBox targetTextbox = s as TextBox;
                 if (targetTextbox != null)
                 {
                     if ((bool)e.OldValue && !((bool)e.NewValue))
                     {
                         targetTextbox.PreviewTextInput -= targetTextbox_PreviewTextInput6;
                     }
                     if ((bool)e.NewValue)
                     {
                        targetTextbox.PreviewTextInput += targetTextbox_PreviewTextInput6;
                        targetTextbox.LostFocus += targetTextbox_LostFocus;
                        targetTextbox.PreviewKeyDown += targetTextbox_PreviewKeyDown6;
                     }
                 }
             })));


        static void targetTextbox_PreviewKeyDown6(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private static void targetTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            Hashtable previewRegex = new Hashtable();
            Hashtable completionRegex = new Hashtable();
            Hashtable errorMessage = new Hashtable();
            Hashtable validationState = new Hashtable();

            completionRegex["IP"] = @"^(((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))\.){3}((1[0-9]{2})|(2[0-4]\d)|(25[0-5])|(\d{1,2}))";
            completionRegex["EMAIL"] = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
            completionRegex["TEL"] = @"^(0\d{1,3}\-\d{2,4}\-\d{3,4})$";

            e.Handled = false;
            TextBox tbox = (TextBox)sender;
            try
            {
                string tag = tbox.Tag.ToString();
                if (tag == "")
                    return;
            }
            catch (Exception e1)
            {
                return;
            }

            Regex regex = new Regex((string)completionRegex[(string)tbox.Tag]);
            if (regex.IsMatch(tbox.Text))
                tbox.Foreground = Brushes.LightGray;
            else
                tbox.Foreground = Brushes.LightSkyBlue;
            e.Handled = false;
        }

        static void targetTextbox_PreviewTextInput6(object sender, TextCompositionEventArgs e)
        {
            Hashtable previewRegex = new Hashtable();
            Hashtable completionRegex = new Hashtable();
            Hashtable errorMessage = new Hashtable();
            Hashtable validationState = new Hashtable();

            completionRegex["IP"] = @"^(((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))\.){3}((1[0-9]{2})|(2[0-4]\d)|(25[0-5])|(\d{1,2}))";
            completionRegex["EMAIL"] = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
            completionRegex["TEL"] = @"^(0\d{1,3}\-\d{2,4}\-\d{3,4})$";

            e.Handled = false;
            TextBox tbox = (TextBox)sender;
            try 
            { 
                string tag = tbox.Tag.ToString();
                if (tag == "")
                    return;
            }
            catch(Exception e1)
            {
                    return;
            }

            Char newChar;

            try
            {
                newChar = e.Text.ToString()[0];
            }
            catch
            {
                e.Handled = true;
                return;
            }
            e.Handled = true;

            Regex regex = new Regex((string)completionRegex[(string)tbox.Tag]);
            if (regex.IsMatch(tbox.Text))
                tbox.Foreground = Brushes.LightGray;
            else
                tbox.Foreground = Brushes.LightSkyBlue;
            e.Handled = false;

            switch ((string)tbox.Tag)
            {
                case "IP":
                    if (Char.IsDigit(newChar) || Char.IsControl(newChar))
                    {
                        e.Handled = false;
                    }
                    else if (newChar == '.')
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    break;
                case "TEL":
                    if (Char.IsDigit(newChar) || Char.IsControl(newChar))
                    {
                        e.Handled = false;
                    }
                    else if (newChar == '-')
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    break;
                case "EMAIL":
                    if (Char.IsDigit(newChar) || Char.IsControl(newChar) || Char.IsLetterOrDigit(newChar))
                    {
                        e.Handled = false;
                    }
                    else if ((newChar == '.') || (newChar == '@'))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }
        #endregion

        #region // 사용안함


        #endregion

        #region //  숫자/ 최대/ 최소
        // 특수 문자 제외 2016.04.22 romee 
        // 최대 / 최소 렌지 
        //------------------------------------
        public static bool GetIsMinMax(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMinMaxProperty);
        }

        public static void SetIsMinMax(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMinMaxProperty, value);
        }

        // 숫자/ 최대/ 최소
        public static readonly DependencyProperty IsMinMaxProperty =
             DependencyProperty.RegisterAttached("IsMinMax", typeof(bool), typeof(TextHelper), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
             {
                 TextBox targetTextbox = s as TextBox;
                 if (targetTextbox != null)
                 {
                     if ((bool)e.OldValue && !((bool)e.NewValue))
                     {
                         targetTextbox.PreviewTextInput -= targetTextbox_PreviewTextInput3;

                     }
                     if ((bool)e.NewValue)
                     {
                         targetTextbox.PreviewTextInput += targetTextbox_PreviewTextInput3;
                         targetTextbox.PreviewKeyDown += targetTextbox_PreviewKeyDown3;
                     }
                 }
             })));

        static void targetTextbox_PreviewKeyDown3(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        static void targetTextbox_PreviewTextInput3(object sender, TextCompositionEventArgs e)
        {
            Char newChar;

            try
            {
                newChar = e.Text.ToString()[0];
            }
            catch
            {
                e.Handled = true;
                return;
            }
            e.Handled = true;

            TextBox targetTextbox = sender as TextBox;

            int tmin = (int)targetTextbox.GetValue(TextHelper.MinProperty);
            int tmax = (int)targetTextbox.GetValue(TextHelper.MaxProperty);

            int max_length = targetTextbox.MaxLength;
            if (max_length == 0)
                max_length = 10;
            string t1 = targetTextbox.Text;

            if (t1.Length < max_length)
            {
                if (Char.IsDigit(newChar) || Char.IsControl(newChar))
                {
                    e.Handled = false;
                }
                else if (tmin > tmin && tmax < tmin)
                {
                    e.Handled = false;
                }
            }
        }
        #endregion


    }
}