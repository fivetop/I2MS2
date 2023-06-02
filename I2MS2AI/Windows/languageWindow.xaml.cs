using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using System.Threading;
using I2MS2.Translation;
using I2MS2.Models;

namespace I2MS2.Windows
{
    /// <summary>
    /// language.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
//using I2MS2.Properties;

    public partial class languageWindow : Window
    {
        private CultureInfo backup_ci = CultureInfo.CurrentUICulture;

        public languageWindow()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CurrentUICulture;

            if (ci.Name == "ko-KR")
                _rdoKorean.IsChecked = true;
            else
                _rdoEnglish.IsChecked = true;
        }

        private void _btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (_rdoKorean.IsChecked ?? false)
                g.lang_id = 1080001;
            else if (_rdoEnglish.IsChecked ?? false)
                g.lang_id = 1080002;
            Close();
        }

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (backup_ci.Name == "ko-KR")
                TranslationManager.Instance.CurrentLanguage = new CultureInfo("ko-KR");
            else
                TranslationManager.Instance.CurrentLanguage = new CultureInfo("en-US");

            Close();
        }

        private void _rdoKorean_Click(object sender, RoutedEventArgs e)
        {
            
            TranslationManager.Instance.CurrentLanguage = new CultureInfo("ko-KR");
        }

        private void _rdoEnglish_Checked(object sender, RoutedEventArgs e)
        {
            TranslationManager.Instance.CurrentLanguage = new CultureInfo("en-US");
        }
    }
}
