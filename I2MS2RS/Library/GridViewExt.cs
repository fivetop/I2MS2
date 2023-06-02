using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace I2MS2.Library
{

    // 그리드 확장 
    public static class GridViewExtensions
    {
        #region IsContentCentered

        [Category("Common")]
        [AttachedPropertyBrowsableForType(typeof(GridViewColumn))]
        public static bool GetIsContentCentered(GridViewColumn gridViewColumn)
        {
            return (bool)gridViewColumn.GetValue(IsContentCenteredProperty);
        }
        public static void SetIsContentCentered(GridViewColumn gridViewColumn, bool value)
        {
            gridViewColumn.SetValue(IsContentCenteredProperty, value);
        }

        public static readonly DependencyProperty IsContentCenteredProperty =
            DependencyProperty.RegisterAttached(
                "IsContentCentered",
                typeof(bool), // type
                typeof(GridViewExtensions), // containing type
                new PropertyMetadata(default(bool), OnIsContentCenteredChanged)
                );

        private static void OnIsContentCenteredChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OnIsContentCenteredChanged((GridViewColumn)d, (bool)e.NewValue);
        }
        private static void OnIsContentCenteredChanged(GridViewColumn gridViewColumn, bool isContentCentered)
        {
            if (isContentCentered == false) { return; }
            // must wait a bit otherwise GridViewColumn.DisplayMemberBinding will not yet be initialized, 
            new DispatcherTimer(TimeSpan.FromMilliseconds(100), DispatcherPriority.Normal, OnColumnLoaded, gridViewColumn.Dispatcher)
            {
                Tag = gridViewColumn
            }.Start();
        }

        static void OnColumnLoaded(object sender, EventArgs e)
        {
            var timer = (DispatcherTimer)sender;
            timer.Stop();

            var gridViewColumn = (GridViewColumn)timer.Tag;
            if (gridViewColumn.DisplayMemberBinding == null)
            {
                throw new Exception("Only allowed with DisplayMemberBinding.");
            }
            var textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            textBlockFactory.SetBinding(TextBlock.TextProperty, gridViewColumn.DisplayMemberBinding);
            textBlockFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
            var cellTemplate = new DataTemplate { VisualTree = textBlockFactory };
            gridViewColumn.DisplayMemberBinding = null; // must null, otherwise CellTemplate won't be recognized
            gridViewColumn.CellTemplate = cellTemplate;
        }

        #endregion IsContentCentered
    }
}
