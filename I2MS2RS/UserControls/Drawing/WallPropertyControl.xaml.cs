using I2MS2.Library;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace I2MS2.UserControls.Drawing
{
    // 벽 속성 선택을 위한 컨트롤 , 벽두께, 투명도, 높이 설정 , 기존 설정값 출력
    /// <summary>
    /// WallPropertyControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WallPropertyControl : UserControl
    {
        public delegate void WallPropChangedEventHandler(string type, object Value);
        public event WallPropChangedEventHandler wallPropChangedEvent;

        private Double wall_alpha;
        
        public WallPropertyControl()
        {
            wall_alpha = 1;
            InitializeComponent();
        }

        // 두께
        public Double setWallThiness(Double thin)
        {
            Double result_thin;
            switch((int)thin)
            {
                case 1:
                    _radioThin1.IsChecked = true;
                    result_thin = 1;
                    break;
                case 2:
                    _radioThin2.IsChecked = true;
                    result_thin = 2;
                    break;
                case 3:
                    _radioThin3.IsChecked = true;
                    result_thin = 3;
                    break;
                case 5:
                    _radioThin5.IsChecked = true;
                    result_thin = 5;
                    break;
                case 30:
                    _radioThin30.IsChecked = true;
                    result_thin = 30;
                    break;

                default:
                    _radioThin3.IsChecked = true;
                    result_thin = 3;
                    break;
            }
            return result_thin;
        }
        // 높이
        public Double setWallHeight(Double h)
        {
            Double result_h;
            switch ((int)h)
            {
                case 5:
                    _radioHeight5.IsChecked = true;
                    result_h = Etc.get_int(_radioHeight5.Content.ToString());
                    break;
                case 10:
                    _radioHeight10.IsChecked = true;
                    result_h = Etc.get_int(_radioHeight10.Content.ToString());
                    break;
                case 15:
                    _radioHeight15.IsChecked = true;
                    result_h = Etc.get_int(_radioHeight15.Content.ToString()); 
                    break;
                case 25:
                    _radioHeight25.IsChecked = true;
                    result_h = Etc.get_int(_radioHeight25.Content.ToString()); 
                    break;
                case 150:
                    _radioHeight150.IsChecked = true;
                    result_h = Etc.get_int(_radioHeight150.Content.ToString()); 
                    break;
                default:
                    _radioHeight10.IsChecked = true;
                    result_h = Etc.get_int(_radioHeight10.Content.ToString());
                    break;
            }
            return result_h;
        }
        // 투명도
        public void setAlpha(Double v)
        {
            AlphaSlider.Value = v;
        }

        private void _radioThin_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton rd = (RadioButton)sender;
                Double th = getIntFromRdContent(rd);

                if ( (th != -200) && (wallPropChangedEvent!=null))
                    wallPropChangedEvent("THICKNESS", th);
            }
        }

        private void AlphaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.IsLoaded == true)
            {
                if ((Math.Abs(wall_alpha - AlphaSlider.Value)) > 0.1) 
                {
                    wall_alpha = AlphaSlider.Value;
                    wallPropChangedEvent("ALPHA", wall_alpha);
                }
            }
        }

        private void _radioHeight_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton rd = (RadioButton)sender;
                Double h = getIntFromRdContent(rd);

                if ( (h != -200) && (wallPropChangedEvent!=null))
                    wallPropChangedEvent("HEIGHT", h);
            }
        }

        private Double getIntFromRdContent(RadioButton rd) // 레디오 버튼의 더블 값 변경 
        {
            if (rd.Content is string)
            {
                string v = (string)rd.Content;
                Double h = Convert.ToDouble(v);
                return h;
            }
            else
                return -200;
        }

       
    }
}
