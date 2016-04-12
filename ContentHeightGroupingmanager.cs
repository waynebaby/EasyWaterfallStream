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

        //public WaterfallStreamViewer WaterfallStreamViewer
        //{h
        //    get { return (WaterfallStreamViewer)GetValue(WaterfallStreamViewerProperty); }
        //    set { SetValue(WaterfallStreamViewerProperty, value); }
        //}

        //public static readonly DependencyProperty WaterfallStreamViewerProperty =
        //    DependencyProperty.Register(nameof(WaterfallStreamViewer), typeof(WaterfallStreamViewer), typeof(ContentHeightGroupingManager), new PropertyMetadata(null,
        //             (o, e) =>mei's
        //             {
        //                 var gm = o as ContentHeightGroupingManager;
        //                 var count = (int)e.NewValue;
        //                 var view = gm.CollectionView;
        //                 InitCollectionViewGroup(view, count);
        //             }));





        public int GroupCount
        {
            get { return (int)GetValue(GroupCountProperty); }
            set { SetValue(GroupCountProperty, value); }
        }

        public static readonly DependencyProperty GroupCountProperty =
            DependencyProperty.Register(
                nameof(GroupCount),
                typeof(int),
                typeof(ContentHeightGroupingManager),
                new PropertyMetadata(3,
                    (o, e) =>
                    {
                        var gm = o as ContentHeightGroupingManager;
                        var count = (int)e.NewValue;
                        var view = gm.CollectionView;
                        InitCollectionViewGroup(view, count);
                    }
                ));

        private static void InitCollectionViewGroup(DependencyCollectionView view, int count)
        {
            if (view.CollectionGroups?.Count != count)
            {
                view.CollectionGroups.Clear();
                for (int i = 0; i < count; i++)
                {
                    var g = new DependencyCollectionViewGroup(i);
                    view.CollectionGroups.Add(g);
                }
            }
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
            DependencyProperty.RegisterAttached(nameof(SetContentHeight).Remove(0, 3), typeof(double), typeof(ContentHeightGroupingManager), new PropertyMetadata(0d));


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
                 .Select(x => new { x, value = (double)x.GetValue(ContentHeightProperty) })
                 .Aggregate((x, y) => x.value > y.value ? y : x);

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
