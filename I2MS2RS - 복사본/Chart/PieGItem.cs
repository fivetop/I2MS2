using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace I2MS2.Chart
{
    public class PieGItem : HeaderedItemsControl
    {
        public event RoutedEventHandler Click;
            
        public static readonly DependencyProperty SubMenuSectorProperty;
        public static readonly DependencyProperty CommandProperty;

        [Bindable(true)]
        public double SubMenuSector
        {
            get
            {
                return (double)base.GetValue(PieGItem.SubMenuSectorProperty);
            }
            set
            {
                base.SetValue(PieGItem.SubMenuSectorProperty, value);
            }
        }

        [Bindable(true)]
        public ICommand Command
        {
            get
            {
                return (ICommand)base.GetValue(PieGItem.CommandProperty);
            }
            set
            {
                base.SetValue(PieGItem.CommandProperty, value);
            }
        }

        double _size;

        static PieGItem()
        {
            PieGItem.SubMenuSectorProperty = DependencyProperty.Register("SubMenuSector", typeof(double), typeof(PieGItem), new FrameworkPropertyMetadata(120.0));
            PieGItem.CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(PieGItem), new FrameworkPropertyMetadata(null));
        }

        public double CalculateSize(double s, double d)
        {
            // size of current level 
            double ss = s + d;

            foreach (UIElement i in Items)
            {
               ss = Math.Max(ss, (i as PieGItem).CalculateSize(s + d, d));
            }

            _size = ss;
    
            return _size;
        }

        protected override Size MeasureOverride(Size availablesize)
        {
            foreach (UIElement i in Items) 
            {
                i.Measure(availablesize);
            }

            return new Size(_size, _size);
        }

        protected override Size ArrangeOverride(Size finalsize)
        {
            return finalsize;
        }

        public void OnClick()
        {
            if (Command != null && Command.CanExecute(null))
            {
                Command.Execute(Header);
            }

            if (Click != null)
            {
                Click(this, new RoutedEventArgs());
            }
        }
    }
}
