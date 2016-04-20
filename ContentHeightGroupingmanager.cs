using MVVMSidekick.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace EasyWaterfallStream
{
    public class ContentHeightGroupingManager : DependencyCollectionViewGroupingManagerBase
    {
        public ContentHeightGroupingManager(WaterfallStreamViewer viewer)
              : base(viewer.CollectionView)
        {

        }

        public double GlobalVerticalOffset
        {
            get { return (double)GetValue(GlobalVerticalOffsetProperty); }
            set { SetValue(GlobalVerticalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GlobalVerticalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GlobalVerticalOffsetProperty =
            DependencyProperty.Register(nameof(GlobalVerticalOffset), typeof(double), typeof(ContentHeightGroupingManager), new PropertyMetadata(0d,
                (o, e) =>
                {
                    var cm = o as ContentHeightGroupingManager;
                    var items = cm.CollectionView.CollectionGroups.OfType<DependencyCollectionViewGroup>();
                    foreach (var item in items)
                    {
                        var sv = GetBindedScrollViewer(item);
                        var nv = (double)e.NewValue;
                        if (sv.VerticalOffset!=nv)
                        {
                            sv.ScrollToVerticalOffset(nv);
                        }
                    }

                }

                ));




        public static ScrollViewer GetBindedScrollViewer(DependencyObject obj)
        {
            return (ScrollViewer)obj.GetValue(BindedScrollViewerProperty);
        }

        public static void SetBindedScrollViewer(DependencyObject obj, ScrollViewer value)
        {
            obj.SetValue(BindedScrollViewerProperty, value);
        }

        // Using a DependencyProperty as the backing store for BindedScrollViewer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindedScrollViewerProperty =
            DependencyProperty.RegisterAttached(nameof(SetBindedScrollViewer).Remove(0, 3), typeof(ScrollViewer), typeof(ContentHeightGroupingManager), new PropertyMetadata(
                null,
                (o, e) =>
                {
                    var fe = (o as FrameworkElement);
                    if (fe!=null)
                    {

                        fe.Loaded += (ob, ev) =>
                        {
                            var ctxDO = fe.DataContext as DependencyCollectionViewGroup;
                            if (ctxDO != null)
                            {
                                var sv = e.NewValue as ScrollViewer;
                                SetBindedScrollViewer(ctxDO, sv);
                                var cm = ctxDO.ParentView.GroupingManager as ContentHeightGroupingManager;

                                var binding = new Binding()
                                {
                                    Source = sv,
                                    Path = new PropertyPath(nameof(ScrollViewer.VerticalOffset)),
                                };

                                BindingOperations.SetBinding(sv, VerticalOffsetProperty, binding);

                            }

                        };
                    }
                }
                ));


        public static double GetVerticalOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(VerticalOffsetProperty);
        }

        public static void SetVerticalOffset(DependencyObject obj, double value)
        {
            obj.SetValue(VerticalOffsetProperty, value);
        }

        // Using a DependencyProperty as the backing store for VerticalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.RegisterAttached("VerticalOffset", typeof(double), typeof(ContentHeightGroupingManager), new PropertyMetadata(0d,
            (o, e) =>
            {
                var sv = o as ScrollViewer;
                var ctxDO = (o as FrameworkElement)?.DataContext as DependencyCollectionViewGroup;
                if (ctxDO != null)
                {
                    var cm = ctxDO.ParentView.GroupingManager as ContentHeightGroupingManager;
                    cm.GlobalVerticalOffset = sv.VerticalOffset;
                }
            }));




        protected override bool OnAddingItemToGroup(object item)
        {
            var min = CollectionView.CollectionGroups
                 .OfType<DependencyCollectionViewGroup>()
                 .Select(x => new { x, h = GetBindedScrollViewer(x)?.ScrollableHeight ?? 0, count = x.GroupItems.Count })
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
