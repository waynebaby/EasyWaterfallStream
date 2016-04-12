﻿using MVVMSidekick.Collections;
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
            CollectionView = new DependencyCollectionView();
        }

        public DataTemplate WaterLineDataTemplate
        {
            get { return (DataTemplate)GetValue(WaterLineDataTemplateProperty); }
            set { SetValue(WaterLineDataTemplateProperty, value); }
        }

        public static readonly DependencyProperty WaterLineDataTemplateProperty =
            DependencyProperty.Register(nameof(WaterLineDataTemplate), typeof(DataTemplate), typeof(WaterfallStreamViewer), new PropertyMetadata(null));


        public DataTemplate WaterfallItemTemplate
        {
            get { return (DataTemplate)GetValue(WaterfallItemTemplateProperty); }
            set { SetValue(WaterfallItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty WaterfallItemTemplateProperty =
            DependencyProperty.Register(nameof(WaterfallItemTemplate), typeof(DataTemplate), typeof(WaterfallStreamViewer), new PropertyMetadata(null));




        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(WaterfallStreamViewer), new PropertyMetadata(null));



        public DependencyCollectionView CollectionView
        {
            get { return (DependencyCollectionView)GetValue(CollectionViewProperty); }
            private set { SetValue(CollectionViewProperty, value); }
        }

        public static readonly DependencyProperty CollectionViewProperty =
            DependencyProperty.Register(nameof(CollectionView), typeof(DependencyCollectionView), typeof(WaterfallStreamViewer), new PropertyMetadata(null));




    }
}