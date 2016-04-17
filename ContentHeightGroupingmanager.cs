using MVVMSidekick.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace EasyWaterfallStream
{
    public class ContentHeightGroupingManager : DependencyCollectionViewGroupingManagerBase
    {
        public ContentHeightGroupingManager(WaterfallStreamViewer viewer)
              : base(viewer.CollectionView)
        {

        }






        public static double GetContentHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(ContentHeightProperty);
        }

        public static void SetContentHeight(DependencyObject obj, double value)
        {
            obj.SetValue(ContentHeightProperty, value);
        }

        public static readonly DependencyProperty ContentHeightProperty =
            DependencyProperty.RegisterAttached(nameof(SetContentHeight).Remove(0, 3), typeof(double), typeof(ContentHeightGroupingManager), new PropertyMetadata(0d,
                (o, e)=>
                {
                    var ctxDO = (o as FrameworkElement)?.DataContext as DependencyCollectionViewGroup ;
                    if (ctxDO!=null)
                    {
                        ctxDO.SetValue(ContentHeightProperty,e.NewValue);
                    }
                }
                ));


        public static double GetContentWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(ContentWidthProperty);
        }

        public static void SetContentWidth(DependencyObject obj, double value)
        {
            obj.SetValue(ContentWidthProperty, value);
        }
        public static readonly DependencyProperty ContentWidthProperty =
            DependencyProperty.RegisterAttached(nameof(SetContentWidth).Remove(0, 3), typeof(double), typeof(ContentHeightGroupingManager), new PropertyMetadata(0d));



        protected override bool OnAddingItemToGroup(object item)
        {
            var min = CollectionView.CollectionGroups
                 .OfType<DependencyCollectionViewGroup>()
                 .Select(x => new { x, h = (double)x.GetValue(ContentHeightProperty), count = x.GroupItems.Count })
                   .OrderBy(x => x.h).ThenBy(x => x.count)
                 .FirstOrDefault();

            min.x.GroupItems.Add(item);
            return true;
        }

        protected override bool OnRemovingingItemFromGroup(object item)
        {
            return CollectionView.CollectionGroups
                    .OfType<DependencyCollectionViewGroup>()
                    .Select(x => x.GroupItems.Remove(item))
                    .Any();
        }
    }
}
