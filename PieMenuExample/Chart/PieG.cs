using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PieMenuExample
{
    public class PieG : ItemsControl
    {
        public static readonly DependencyProperty RadiusProperty;
        public static readonly DependencyProperty InnerRadiusProperty;
        public static readonly DependencyProperty SectorGapProperty;
        public static readonly DependencyProperty GapProperty;
        public static readonly DependencyProperty MenuSectorProperty;
        public static readonly DependencyProperty SelectedBackgroundProperty;
        public static readonly DependencyProperty RotationProperty;
        public static readonly DependencyProperty RotateTextProperty;
        public static readonly DependencyProperty SetvalueProperty;
        public static readonly DependencyProperty BorderBrush2Property;

        [Bindable(true)]
        public double Setvalue
        {
            get { return (double)GetValue(PieG.SetvalueProperty); }
            set { base.SetValue(PieG.SetvalueProperty, value); }
        }
        
        [Bindable(true)]
        public double Radius
        { 
            get
            {
                return (double)base.GetValue(PieG.RadiusProperty);
            }
            set
            {
                base.SetValue(PieG.RadiusProperty, value);
            }
        }

        [Bindable(true)]
        public double InnerRadius
        {
            get
            {
                return (double)base.GetValue(PieG.InnerRadiusProperty);
            }
            set
            {
                base.SetValue(PieG.InnerRadiusProperty, value);
            }
        }

        [Bindable(true)]
        public double SectorGap
        {
            get
            {
                return (double)base.GetValue(PieG.SectorGapProperty);
            }
            set
            {
                base.SetValue(PieG.SectorGapProperty, value);
            }
        }

        [Bindable(true)]
        public double Gap
        {
            get
            {
                return (double)base.GetValue(PieG.GapProperty);
            }
            set
            {
                base.SetValue(PieG.GapProperty, value);
            }
        }

        [Bindable(true)]
        public double MenuSector
        {
            get
            {
                return (double)base.GetValue(PieG.MenuSectorProperty);
            }
            set
            {
                base.SetValue(PieG.MenuSectorProperty, value);
            }
        }

        [Bindable(true)]
        public Brush SelectedBackground
        {
            get
            {
                return (Brush)base.GetValue(PieG.SelectedBackgroundProperty);
            }
            set
            {
                base.SetValue(PieG.SelectedBackgroundProperty, value);
            }
        }

        [Bindable(true)]
        public Brush BorderBrush2
        {
            get
            {
                return (Brush)base.GetValue(PieG.BorderBrush2Property);
            }
            set
            {
                base.SetValue(PieG.BorderBrush2Property, value);
            }
        }

        [Bindable(true)]
        public double Rotation
        {
            get
            {
                return (double)base.GetValue(PieG.RotationProperty);
            }
            set
            {
                base.SetValue(PieG.RotationProperty, value);
            }
        }

        [Bindable(true)]
        public bool RotateText
        {
            get
            {
                return (bool)base.GetValue(PieG.RotateTextProperty);
            }
            set
            {
                base.SetValue(PieG.RotateTextProperty, value);
            }
        }

        private List<List<Tuple<double, double>>> _sectors;
        private Selection _selection = new Selection();

        private int _current_level = -1;
        private int _current_selection = -1;
        private bool _was_selected = false;
        private double _size;

        static List<Brush> bl1;

        static PieG()
        {

            bl1 = new List<Brush>();
            
//            PropertyInfo[] props = typeof(Brushes).GetProperties();
//            for (int i = 0; i < props.Length; i++)
//                bl1.Add((Brush)props[i].GetValue(null, null));

            for (int i = 1; i < 60; i++)
            {
                string s = String.Format("_color{0:00}", i);
                SolidColorBrush a = new SolidColorBrush((Color)Application.Current.Resources[s]);
                bl1.Add(a);
            }

            PieG.RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(PieG), new FrameworkPropertyMetadata(50.0));
            PieG.InnerRadiusProperty = DependencyProperty.Register("InnerRadius", typeof(double), typeof(PieG), new FrameworkPropertyMetadata(10.0));
            PieG.SectorGapProperty = DependencyProperty.Register("SectorGap", typeof(double), typeof(PieG), new FrameworkPropertyMetadata(5.0));
            PieG.GapProperty = DependencyProperty.Register("Gap", typeof(double), typeof(PieG), new FrameworkPropertyMetadata(5.0));
            PieG.MenuSectorProperty = DependencyProperty.Register("MenuSector", typeof(double), typeof(PieG), new FrameworkPropertyMetadata(360.0));
            PieG.SelectedBackgroundProperty = DependencyProperty.Register("SelectedBackground", typeof(Brush), typeof(PieG), new FrameworkPropertyMetadata(Brushes.Gray));
            PieG.RotationProperty = DependencyProperty.Register("Rotation", typeof(double), typeof(PieG), new FrameworkPropertyMetadata(0.0));
            PieG.RotateTextProperty = DependencyProperty.Register("RotateText", typeof(bool), typeof(PieG), new FrameworkPropertyMetadata(true));
            PieG.SetvalueProperty = DependencyProperty.Register("Setvalue", typeof(double), typeof(PieG), new FrameworkPropertyMetadata(5.0, OnValue));
            PieG.BorderBrush2Property = DependencyProperty.Register("BorderBrush2", typeof(Brush), typeof(PieG), new FrameworkPropertyMetadata(Brushes.Gray, OnValue2));
        }

        private static void OnValue2(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var action = (PieG)d;
            if (action != null)
            {
                action.InvalidateVisual();
                //action.DoubleAnimation_Start();
            }
        }

        // 값이 변경되면 화면을 갱신처리 
        private static void OnValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var action = (PieG)d;
            if (action != null)
            { 
                action.InvalidateVisual();
                //action.DoubleAnimation_Start();
            }
        }

        
        
        protected override Size MeasureOverride(Size availablesize)
        {
            // The "thickness" of each "layer" of menu
            double d = 2.0 * (Radius - InnerRadius + Gap);

            // fictious size of "empty" menu
            double s = 2.0 * (InnerRadius - Gap);

            // find size as maximum size of menu items
            double ss = 0;

            foreach (UIElement i in Items)
            {
                ss = Math.Max(ss, (i as PieGItem).CalculateSize(s, d));
            }
            
            foreach (UIElement i in Items)
            {
                i.Measure(availablesize);
            }

            _size = ss;

            return new Size(ss, ss);
        }

        protected override Size ArrangeOverride(Size finalsize)
        {
            return finalsize;
        }

        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            Point click_point = e.MouseDevice.GetPosition(this);
           
            Down(click_point);

            e.MouseDevice.Capture(this);

            InvalidateVisual();
        }

        protected override void OnMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            Point point = e.MouseDevice.GetPosition(this);

            Up(point);

            // release mouse device
            e.MouseDevice.Capture(null);

            InvalidateVisual();
        }

        protected override void OnTouchDown(System.Windows.Input.TouchEventArgs e)
        {
            base.OnTouchDown(e);

            Point touch_point = e.TouchDevice.GetTouchPoint(this).Position;

            Down(touch_point);

            e.TouchDevice.Capture(this);

            e.Handled = true;

            InvalidateVisual();
        }

        protected override void OnTouchUp(System.Windows.Input.TouchEventArgs e)
        {
            base.OnTouchUp(e);

            Point point = e.TouchDevice.GetTouchPoint(this).Position;
            
            Up(point);
            
            // release touch device
            e.TouchDevice.Capture(null);

            e.Handled = true;

            InvalidateVisual();
        }

        private void Down(Point point)
        {
            // find the menu item on which the point is
            Tuple<int, int> t = FindMenuItem(point);
            int level = t.Item1;
            int selection = t.Item2;

            if (selection != -1)
            {
                // remeber the selection
                _current_level = level;
                _current_selection = selection;

                if (selection != _selection.GetSelection(level))
                {
                    // select if it is not already selected
                    _selection.SetSelection(level, selection);
                    _was_selected = false;
                }
                else
                {
                    // remember that is was already selected
                    _was_selected = true;
                }
            }
        }

        private void Up(Point point)
        {
            if (_current_level == -1 || _current_selection == -1) return;

            // find the menu item on which the (release) point is
            Tuple<int, int> t = FindMenuItem(point);
            int level = t.Item1;
            int selection = t.Item2;

            if (level == _current_level && selection == _current_selection)
            {
                // release happens in same menu item as the current selection

                // Find menu item and trigger actions
                ItemsControl items_control = this;
                for (int i = 0; i <= level; i++)
                {
                    int sel = _selection.GetSelection(i);
                   
                    items_control = (ItemsControl)items_control.Items[sel];
                }
                (items_control as PieGItem).OnClick();

                if (items_control.Items.Count == 0)
                {
                    // If no children (means it is a leaf and we have come to our end), then deselect whole menu
                    _selection.SetSelection(0, -1);
                }
                else if (_was_selected)
                {
                    // deselect if it was already selected
                    _selection.SetSelection(level, -1);
                }
            }
            else
            {
                // release happens somewhere else than pressdown

                // Deselect and else do nothing
                _selection.SetSelection(_current_level, -1);
            }

            // reset current selection
            _current_level = -1;
            _current_selection = -1;
            _was_selected = false;
        }

        private Tuple<int, int> FindMenuItem(Point point)
        {
            // coordinates of point relative to center
            double x = point.X - _size / 2.0;
            double y = point.Y - _size / 2.0;

            // distance from center
            double m = Math.Sqrt(x * x + y * y);

            // find the level in the menu of the part clicked 
            int level = 0;
            double l = Radius;

            // a small buffer just to avoid getting a level that is not there
            double g = (Gap > 0 ? Gap / 2.0 : 1);
            while (m >= l + g)
            {
                level++;
                l += Gap + Radius - InnerRadius;
            }

            if (level >= _sectors.Count)
            {
                // point is outside higest level of visible menu
                return new Tuple<int, int>(-1, -1);
            }

            // angle in radians
            double theta = Math.Acos(Math.Abs(x / m));

            // convert angle to degrees
            double angle = 0;
            if (x >= 0 && y >= 0) angle = theta * (360.0 / (2.0 * Math.PI));
            else if (x >= 0 && y < 0) angle = 360.0 - theta * (360.0 / (2.0 * Math.PI));
            else if (x < 0 && y >= 0) angle = 180.0 - theta * (360.0 / (2.0 * Math.PI));
            else if (x < 0 && y < 0) angle = 180 + theta * (360.0 / (2.0 * Math.PI));

            // run through the visible sectors at present level to see in which sector the angle belongs
            // must consider the possibility that angles defining sectors might be less that 0 and greater than 360
            // if rotation is less than -180 or greater than 360 there may be some problems, but this is handled in initial call to DrawMenu

            int selection = -1;

            for (int i = 0; i < _sectors[level].Count; i++)
            {
                double a1 = _sectors[level][i].Item1;
                double a2 = _sectors[level][i].Item2;

                // we have as invariant a1 < a2

                if (a1 < 0.0 && a2 >= 0.0)
                {
                    if (a1 + 360.0 <= angle || angle <= a2)
                    {
                        selection = i;
                        break;
                    }
                }
                else if (a1 < 0.0 && a2 < 0.0)
                {
                    if (a1 + 360.0 <= angle && angle <= a2 + 360.0)
                    {
                        selection = i;
                        break;
                    }
                }
                else if (a1 > 360.0)
                {
                    if (a1 - 360.0 <= angle && angle <= a2 - 360.0)
                    {
                        selection = i;
                        break;
                    }
                }
                else if (a2 > 360.0)  // Must have: 0.0 <= a1 <= 360.0
                {
                    if (a1 <= angle || angle <= a2 - 360.0)
                    {
                        selection = i;
                        break;
                    }
                }
                else // 0.0 <= a1 <= 360.0 && 0.0 <= a2 <= 360.0
                {
                    if (a1 <= angle && angle <= a2)
                    {
                        selection = i;
                        break;
                    }
                }
            }

            return new Tuple<int, int>(level, selection);
        }

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            // "normalize" rotation to avoid problems elsewhere
            double rotation = this.Rotation;
            while (rotation < 0.0) rotation += 360.0;
            while (rotation > 360.0) rotation -= 360.0;

            // initialize data structure for remembering sectors at each level
            _sectors = new List<List<Tuple<double, double>>>();

            // Draw the menu (level 0)
            DrawMenu(this, 0, rotation, this.MenuSector, drawingContext);
            //DoubleAnimation_Start();
        }

        // 화면에 그려주는 부분 
        private void DrawMenu(ItemsControl items_control, int level, double angle, double sector, DrawingContext drawingContext)
        {
            // Make sure sector is no more than 360 degrees
            double full_sector = Math.Min(sector, 360.0);

            // find number of menu items at current level
            // return if none
            int count = items_control.Items.Count;
            if (count == 0) return;

            // Add list to remember sectors at this level
            _sectors.Add(new List<Tuple<double, double>>());

            // Coordinates of center of (full) menu
            double x = _size / 2.0;
            double y = _size / 2.0;

            // calculate inner and outer radius for this level
            double inner_radius = InnerRadius + (Radius - InnerRadius + Gap) * level;
            if (inner_radius < SectorGap) inner_radius = SectorGap;
            double outer_radius = Radius + (Radius - InnerRadius + Gap) * level;

            // Sector gap is given as a length. Find the angle giving this gap on inner and outer radius
            double inner_gap_angle;
            if (inner_radius == 0) inner_gap_angle = 0;
            else inner_gap_angle = 2.0 * Math.Asin(SectorGap / (2.0 * inner_radius)) * 360.0 / (2.0 * Math.PI);
            double outer_gap_angle = 2.0 * Math.Asin(SectorGap / (2.0 * outer_radius)) * 360.0 / (2.0 * Math.PI);

            // numbers of sector gaps (one more if we are using full circle)
            int c = (full_sector < 360.0 ? count - 1 : count);

            // Calculate the inner and outer arc of each menu item
            double inner_angle = (full_sector - inner_gap_angle * c) / count;
            double outer_angle = (full_sector - outer_gap_angle * c) / count;

            // draw each menu item 
            for (int i = 0; i < count; i++)
            {
                // calculate the boundaries of menu item as angle of the inner and outer arcs
                double start_inner_angle = angle + i * (full_sector / count) + inner_gap_angle / 2.0;
                double end_inner_angle = start_inner_angle + (full_sector / count) - inner_gap_angle;
                double start_outer_angle = angle + i * (full_sector / count) + outer_gap_angle / 2.0;
                double end_outer_angle = start_outer_angle + (full_sector / count) - outer_gap_angle;

                // remeber the boundaries (as sector angles) of the menu item
                _sectors[level].Add(new Tuple<double, double>(start_outer_angle, end_outer_angle));
               
                // Calculate the corners of the sector
                Point p1 = CalculatePoint(x, y, start_inner_angle, inner_radius-1);
                Point p2 = CalculatePoint(x, y, end_inner_angle, inner_radius-1);
                Point p3 = CalculatePoint(x, y, end_outer_angle, outer_radius -1);
                Point p4 = CalculatePoint(x, y, start_outer_angle, outer_radius-1);

                // find center of the corners 
                Point center = CalculatePoint(x, y, (start_inner_angle + end_inner_angle) / 2.0, (inner_radius + outer_radius) / 2.0);

                // specify the figure representing the menu item
                PathFigure pathFigure = new PathFigure();
                pathFigure.Segments = new PathSegmentCollection();
                pathFigure.StartPoint = p1;
                pathFigure.Segments.Add(new ArcSegment(p2, new Size(inner_radius, inner_radius), end_inner_angle - start_inner_angle, end_inner_angle - start_inner_angle > 180.0, SweepDirection.Clockwise, true));
                pathFigure.Segments.Add(new LineSegment(p3, true));
                pathFigure.Segments.Add(new ArcSegment(p4, new Size(outer_radius, outer_radius), end_outer_angle - start_outer_angle, end_outer_angle - start_outer_angle > 180.0, SweepDirection.Counterclockwise, true));
                pathFigure.IsClosed = true;
                pathFigure.IsFilled = true;

                // Create geometry object and add the figure
                PathGeometry geometry = new PathGeometry();
                geometry.Figures.Add(pathFigure);

                // Get the menu item to extract properties
                PieGItem menu_item = items_control.Items[i] as PieGItem;

                // find color for backgound and border 
                Brush background_brush = Brushes.Transparent; // menu_item.Background;
                if (background_brush == null) background_brush = this.Background;
                if (background_brush == null) background_brush = Brushes.White;

                if (_selection.GetSelection(level) == i)
                {
                    background_brush = this.SelectedBackground;
                }
                else
                {
                    if(i < Setvalue)
                        background_brush = bl1[i];
                }

                Brush border_brush = this.BorderBrush2;

                // 보더를 그려주려면 고정으로 필요 
                // Draw the geometry representing the menu item
                drawingContext.DrawGeometry(background_brush, new Pen(border_brush, 1), geometry);
//                drawingContext.DrawGeometry(background_brush, new Pen(border_brush, menu_item.BorderThickness.Left), geometry);

                // Get header of menu item as string and make a formatted text based on properties of menu item
                String header = (String)menu_item.Header;

                FormattedText text = new FormattedText(header,
                                CultureInfo.CurrentCulture,
                                FlowDirection.LeftToRight,
                                new Typeface(menu_item.FontFamily, menu_item.FontStyle, menu_item.FontWeight, menu_item.FontStretch),
                                menu_item.FontSize,
                                menu_item.Foreground);

                // Calculate placement of text
                Point text_point = new Point(center.X - text.Width / 2.0, center.Y - text.Height / 2.0);

                // Draw the text as name of menu item
                if (this.RotateText) drawingContext.PushTransform(new RotateTransform((start_inner_angle + end_inner_angle) / 2.0 + 90.0, center.X, center.Y));
                // drawingContext.DrawText(text, text_point);    // romee
                if (this.RotateText) drawingContext.Pop();

                // if this menu item is selected, draw the next level
                if (_selection.GetSelection(level) == i) 
                {
                    // start angle of sub menu is center angle of menu item minus half the sector of the sub menu
                    double new_angle = (start_inner_angle + end_inner_angle) / 2.0 - menu_item.SubMenuSector / 2.0;
                    DrawMenu(menu_item, level + 1, new_angle, menu_item.SubMenuSector, drawingContext);
                }
            }
        }

        private Point CalculatePoint(double centerX, double centerY, double angle, double radius)
        {
            double x = centerX + Math.Cos((Math.PI / 180.0) * angle) * radius;
            double y = centerY + Math.Sin((Math.PI / 180.0) * angle) * radius;
            return new Point(x, y);
        }

        public void DoubleAnimation_Start()
        {
            //더블 애니메이션 하나 설정 했다. 
            DoubleAnimation MyDoubleAnimation = new DoubleAnimation();

            MyDoubleAnimation.From = 0.0;
            MyDoubleAnimation.To = 50;
            //가속도값 설정하기 
            MyDoubleAnimation.AccelerationRatio = 0.3;

            MyDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            //애니메이션 효과를 적용한 후에는 속성 값을 변경하기 
            MyDoubleAnimation.FillBehavior = FillBehavior.Stop;

            MyDoubleAnimation.Completed += new EventHandler(MyDoubleAnimation_Completed);

            this.BeginAnimation(PieG.SetvalueProperty, MyDoubleAnimation);
            //_t1.BeginAnimation(TextBlock.TextProperty, MyDoubleAnimation);

        }

        void MyDoubleAnimation_Completed(object sender, EventArgs e)
        {

            //더블 애니메이션 하나 설정 했다. 
            DoubleAnimation MyDoubleAnimation = new DoubleAnimation();

            MyDoubleAnimation.From = 50;
            MyDoubleAnimation.To = Setvalue;
            //가속도값 설정하기 
            MyDoubleAnimation.AccelerationRatio = 0.7;

            MyDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            //애니메이션 효과를 적용한 후에는 속성 값을 변경하기 
            MyDoubleAnimation.FillBehavior = FillBehavior.Stop;

            this.BeginAnimation(PieG.SetvalueProperty, MyDoubleAnimation);
            //_t1.BeginAnimation(TextBlock.TextProperty, MyDoubleAnimation);
        }



    }

    // 선택 처리 
    class Selection
    {
        private List<int> _selection = new List<int>();

        public void SetSelection(int level, int selection)
        {
            // make sure internal list is long enough
            while (_selection.Count < level + 1)
            {
                _selection.Add(-1);
            }

            // set selection or deselect if same item is already selected
            if (_selection[level] == selection) _selection[level] = -1;
            else _selection[level] = selection;

            // deselect items at higher level
            for (int i = level + 1; i < _selection.Count; i++)
            {
                _selection[i] = -1;
            }
        }

        public int GetSelection(int level)
        {
            if (level >= _selection.Count) return -1;

            return _selection[level];
        }

        public int GetLevel()
        {
            for (int i = _selection.Count - 1; i >= 0; i--)
            {
                if (_selection[i] != -1) return i;
            }
            return -1;
        }
    }
}
