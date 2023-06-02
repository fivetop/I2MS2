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
    // 컽러 선택, 컬러 픽커
    public partial class brush_vm
    {
        public SolidColorBrush brush { get; set; }
        public string name { get; set; }
    }

    /// <summary>
    /// ColorSelector.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ColorSelector : UserControl
    {
        Boolean pick_bt_canexcute = true;
        public static RoutedCommand Cmd_btnColorPick = new RoutedCommand();       
        public delegate void ColorChangedEventHandler(SolidColorBrush brush);
        public event ColorChangedEventHandler colorChangedEvent;

        public delegate void ColorPickEventHandler(Object obj);
        public event ColorPickEventHandler colorPickEvent;

        List<brush_vm> brush_list = new List<brush_vm>();
        public SolidColorBrush brush;

        public ColorSelector()
        {
            InitializeComponent();
            initColorList();
        }

        #region Init Color List
        private void initColorList()
        {

            addBrushFromRgb("FF0000");
            addBrushFromRgb("FF5E00");
            addBrushFromRgb("FFBB00");
            addBrushFromRgb("FFE400");
            addBrushFromRgb("ABF200");
            addBrushFromRgb("1DDB16");
            addBrushFromRgb("00D8FF");
            addBrushFromRgb("0054FF");
            addBrushFromRgb("0100FF");
            addBrushFromRgb("5F00FF");
            addBrushFromRgb("FF00DD");
            addBrushFromRgb("FF007F");
            addBrushFromRgb("000000");

            addBrushFromRgb("FFA7A7");
            addBrushFromRgb("FFC19E");
            addBrushFromRgb("FFE08C");
            addBrushFromRgb("FAED7D");
            addBrushFromRgb("CEF279");
            addBrushFromRgb("B7F0B1");
            addBrushFromRgb("B2EBF4");
            addBrushFromRgb("B2CCFF");
            addBrushFromRgb("B5B2FF");
            addBrushFromRgb("D1B2FF");
            addBrushFromRgb("FFB2F5");
            addBrushFromRgb("FFB2D9");
            addBrushFromRgb("D5D5D5");


            addBrushFromRgb("F15F5F");
            addBrushFromRgb("F29661");
            addBrushFromRgb("F2CB61");
            addBrushFromRgb("E5D85C");
            addBrushFromRgb("BCE55C");
            addBrushFromRgb("86E57F");
            addBrushFromRgb("5CD1E5");
            addBrushFromRgb("6799FF");
            addBrushFromRgb("6B66FF");
            addBrushFromRgb("A566FF");
            addBrushFromRgb("F361DC");
            addBrushFromRgb("F361A6");
            addBrushFromRgb("A6A6A6");


            addBrushFromRgb("CC3D3D");
            addBrushFromRgb("CC723D");
            addBrushFromRgb("CCA63D");
            addBrushFromRgb("C4B73B");
            addBrushFromRgb("9FC93C");
            addBrushFromRgb("47C83E");
            addBrushFromRgb("3DB7CC");
            addBrushFromRgb("4374D9");
            addBrushFromRgb("4641D9");
            addBrushFromRgb("8041D9");
            addBrushFromRgb("D941C5");
            addBrushFromRgb("D9418C");
            addBrushFromRgb("747474");

            addBrushFromRgb("980000");
            addBrushFromRgb("993800");
            addBrushFromRgb("997000");
            addBrushFromRgb("998A00");
            addBrushFromRgb("6B9900");
            addBrushFromRgb("2F9D27");
            addBrushFromRgb("008299");
            addBrushFromRgb("003399");
            addBrushFromRgb("050099");
            addBrushFromRgb("3F0099");
            addBrushFromRgb("990085");
            addBrushFromRgb("99004C");
            addBrushFromRgb("4C4C4C");

            addBrushFromRgb("670000");
            addBrushFromRgb("662500");
            addBrushFromRgb("664B00");
            addBrushFromRgb("665C00");
            addBrushFromRgb("476600");
            addBrushFromRgb("22741C");
            addBrushFromRgb("005766");
            addBrushFromRgb("002266");
            addBrushFromRgb("030066");
            addBrushFromRgb("2A0066");
            addBrushFromRgb("660058");
            addBrushFromRgb("660033");
            addBrushFromRgb("212121");

            _lvColor.ItemsSource = brush_list;
        }
        // 브러시 처리 
        private void addBrushFromRgb(string rgb)
        {
            int r1 = (int)rgb[0] - getAsciiCalValue(rgb[0]);
            int r2 = (int)rgb[1] - getAsciiCalValue(rgb[1]);
            byte r = (byte)((r1 << 4) + r2);

            int g1 = (int)rgb[2] - getAsciiCalValue(rgb[2]);
            int g2 = (int)rgb[3] - getAsciiCalValue(rgb[3]);

            byte g = (byte)(((g1 << 4) & 0xF0) + g2);

            int b1 = (int)rgb[4] - getAsciiCalValue(rgb[4]);
            int b2 = (int)rgb[5] - getAsciiCalValue(rgb[5]);
            byte b = (byte)((b1 << 4) + b2); ;

            SolidColorBrush br = new SolidColorBrush(Color.FromArgb(0xFF, r, g, b));
            brush_vm b_vm = new brush_vm()
            {
                brush = br,
                name = string.Format("{0}{1}{2}{3}",br.Color.A, br.Color.R, br.Color.G, br.Color.B)
            };
            brush_list.Add(b_vm);
        }
        // 색상값 조정  , 그라데이션 처리 ??
        public static int getAsciiCalValue(char ch)
        {
            if (ch > 47)
            {
                if (ch > 64)
                {
                    if (ch > 96)
                        return 87;
                    else
                        return 55;
                }
                else
                {
                    return 48;
                }
            }
            else
                return 0;
        } 
        #endregion

        //private void Palette_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    int imageX = (int)Mouse.GetPosition(_imgPalette).X;
        //    int imageY = (int)Mouse.GetPosition(_imgPalette).Y;

        //    CroppedBitmap cb = new CroppedBitmap(_imgPalette.Source as BitmapSource, new Int32Rect(imageX, imageY, 1, 1));
        //    byte[] pixels = new byte[4];
        //    cb.CopyPixels(pixels, 4, 0);


        //    Color color = Color.FromArgb(255, pixels[2], pixels[1], pixels[0]);
        //    brush = new SolidColorBrush(color);
            
        //    _rectSelectColorShow.Fill = brush;


        //    colorChangedEvent(brush);

        //}
        // 피커 모드 설정 
        public void set_wallPropPick_mode(Boolean mode)
        {
            pick_bt_canexcute = mode;
        }
        // 색상 선택 
        public void changeColor(SolidColorBrush _brush)
        {
            _lvColor.SelectedItem = null;
            brush = _brush;
            _rectSelectColorShow.Fill = brush;
            String br_str = string.Format("{0}{1}{2}{3}", _brush.Color.A, _brush.Color.R, _brush.Color.G, _brush.Color.B);

            brush_vm br = brush_list.Find(at => at.name == br_str);
            if (br == null) return;


            _lvColor.SelectedItem = br;

            //brush_vm br = new brush_vm()
            //{
            //    brush = _brush,
            //    name = _brush.ToString()
            //};
            //brush_list.Add(br);
            //_lvColor.ItemsSource = null;
            //_lvColor.ItemsSource = brush_list;

            //colorChangedEvent(brush);
        }
        // 색상 변경
        private void _lvColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_lvColor.SelectedItem == null) return;
            brush_vm br_vm = (brush_vm)_lvColor.SelectedItem;
            brush = br_vm.brush;
            _rectSelectColorShow.Fill = brush;

            colorChangedEvent(brush);
        }

        //private void _btnColorPick_Click(object sender, RoutedEventArgs e)
        //{
        //    colorPickEvent(1);
        //}

        private void _btnColorPick_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = pick_bt_canexcute;
        }
        // 피커 시작 
        private void _btnColorPick_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            colorPickEvent(1);
        }
    }
}
