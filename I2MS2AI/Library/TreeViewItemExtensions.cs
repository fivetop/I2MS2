using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace I2MS2.Library
{
    // 트리뷰 댑스 사용안함 
    public static class TreeViewItemExtensions
    {
        public static int GetDepth(this TreeViewItem item)
        {
            DependencyObject target = item;
            var depth = 0;
            while (target != null)
            {
                if (target is TreeView)
                    return depth;
                if (target is TreeViewItem)
                    depth++;

                target = VisualTreeHelper.GetParent(target);
            }
            return 0;
        }
    }
}
