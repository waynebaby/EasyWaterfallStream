using MVVMSidekick.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace EasyWaterfallStream
{
    public sealed class WaterfallStreamViewer : Control
    {
        public WaterfallStreamViewer()
        {
            this.DefaultStyleKey = typeof(WaterfallStreamViewer);
            CollectionView = new DependencyCollectionView() { };
            CollectionView.GroupingManager = new ContentHeightGroupingManager(this);

            var b = new Binding()
            {
                Source = this,
                Path = new PropertyPath(nameof(Loader)),
                Mode = BindingMode.TwoWay
            };

            BindingOperations.SetBinding(CollectionView, DependencyCollectionView.IncrementalLoaderProperty, b);

            this.Loaded += (o, e) => { CollectionView.GroupingManager.GroupCount = 5; };
        }

        public DataTemplate WaterLineDataTemplate
        {
            get { return (DataTemplate)GetValue(WaterLineDataTemplateProperty); }
            set { SetValue(WaterLineDataTemplateProperty, value); }
        }

        public static readonly DependencyProperty WaterLineDataTemplateProperty =
            DependencyProperty.Register(nameof(WaterLineDataTemplate), typeof(DataTemplate), typeof(WaterfallStreamViewer), new PropertyMetadata(null));


       



        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(WaterfallStreamViewer), new PropertyMetadata(null));



        internal DependencyCollectionView CollectionView
        {
            get { return (DependencyCollectionView)GetValue(CollectionViewProperty); }
            set { SetValue(CollectionViewProperty, value); }
        }

        internal static readonly DependencyProperty CollectionViewProperty =
            DependencyProperty.Register(nameof(CollectionView), typeof(DependencyCollectionView), typeof(WaterfallStreamViewer), new PropertyMetadata(null));


        public DependencyCollectionViewIncrementalLoaderBase Loader
        {
            get { return (DependencyCollectionViewIncrementalLoaderBase)GetValue(LoaderProperty); }
            set { SetValue(LoaderProperty, value); }
        }

        public static readonly DependencyProperty LoaderProperty =
            DependencyProperty.Register(nameof(Loader), typeof(DependencyCollectionViewIncrementalLoaderBase), typeof(WaterfallStreamViewer), new PropertyMetadata(null));


        public double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register(nameof(VerticalOffset), typeof(double), typeof(WaterfallStreamViewer), new PropertyMetadata(0));


        public double ScrollableHeight
        {
            get { return (double)GetValue(ScrollableHeightProperty); }
            set { SetValue(ScrollableHeightProperty, value); }
        }

        public static readonly DependencyProperty ScrollableHeightProperty =
            DependencyProperty.Register(nameof(ScrollableHeight), typeof(double), typeof(WaterfallStreamViewer), new PropertyMetadata(0));

        public double HorizontalOffset
        {
            get { return (double)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.Register(nameof(HorizontalOffset), typeof(double), typeof(WaterfallStreamViewer), new PropertyMetadata(0));


        public double ScrollableWidth
        {
            get { return (double)GetValue(ScrollableWidthProperty); }
            set { SetValue(ScrollableWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScrollableWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollableWidthProperty =
            DependencyProperty.Register("ScrollableWidth", typeof(double), typeof(WaterfallStreamViewer), new PropertyMetadata(0));



    }
}
