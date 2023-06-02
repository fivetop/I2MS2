using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using I2MS2.Animation;
using I2MS2.Pages;

namespace I2MS2.UserControls
{
	/// <summary>
	/// regionFlagControl.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class regionFlagControl : UserControl
	{
        Boolean is_have_parent_event = false;

        public delegate void CustomMouseEnterHandler(string name);
        public event CustomMouseEnterHandler customMouseEnterEvent;


        public delegate void CustomMouseLeaveHandler(string name);
        public event CustomMouseLeaveHandler customMouseLeaveEvent;

        public int id;

        // 기본 초기화 함수
        public regionFlagControl()
		{
            this.InitializeComponent();

            is_have_parent_event = false;
            SimpleAnimation anim = new SimpleAnimation();
            anim.gridBlinkingAnimationStart(flagGrid, 1, 0.8, 0.8, 500);
        }

        // flag 에 관한 이벤트를 parent로 부터 추가로 받는 초기화 함수
        public regionFlagControl(int _id)
        {
            this.InitializeComponent();

            id = _id;
            is_have_parent_event = true;
            SimpleAnimation anim = new SimpleAnimation();
            anim.gridBlinkingAnimationStart(flagGrid, 1, 0.8, 0.8, 500);
            //this.MouseEnter += select_event;
            //this.MouseLeave += un_select_event;
        }

        private void selectArea_MouseEnter(object sender, MouseEventArgs e)
        {
            if (is_have_parent_event == true)
            {
                SimpleAnimation anim = new SimpleAnimation();

                anim.gridBlinkingAnimationStop(flagGrid);
                Point tempCenter = new Point(25, 25);
                Vector tempFromV = new Vector(1, 1);
                Vector tempToV = new Vector(1.5, 1.5);
                anim.gridScaleAnimation(flagGrid, tempFromV, tempToV, tempCenter, 0.8, 100);
     

                Color ToC = Color.FromArgb(0xFF, 0xF2, 0x59, 0x12);
                anim.pathColorAnimation(flag, Color.FromArgb(0xFF, 0xBD, 0xBB, 0xBA), ToC, 0.8, 100);
                anim.pathColorAnimation(flagPole, Color.FromArgb(0xFF, 0xBD, 0xBB, 0xBA), ToC, 0.8, 100);

                customMouseEnterEvent(this.Name);
            }

        }

        private void selectArea_MouseLeave(object sender, MouseEventArgs e)
        {
            if (is_have_parent_event == true)
            {
                SimpleAnimation anim = new SimpleAnimation();

                anim.gridBlinkingAnimationStop(flagGrid);

                Point tempCenter = new Point(25, 25);
                Vector tempFromV = new Vector(1.5, 1.5);
                Vector tempToV = new Vector(1, 1);
                anim.gridScaleAnimation(flagGrid, tempFromV, tempToV, tempCenter, 0.8, 100);

                Color ToC = Color.FromArgb(0xFF, 0xF2, 0x59, 0x12);
                anim.pathColorAnimation(flag, ToC, Color.FromArgb(0xFF, 0xBD, 0xBB, 0xBA), 0.8, 100);
                anim.pathColorAnimation(flagPole, ToC, Color.FromArgb(0xFF, 0xBD, 0xBB, 0xBA), 0.8, 100);

                customMouseLeaveEvent(this.Name);
            } 
       }

	}
}