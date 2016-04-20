using MVVMSidekick.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace EasyWaterfallStream.Demo
{
    public class TestIntLoader : DependencyCollectionViewDelegateIncrementalLoader
    {
        uint value = 0;

        public TestIntLoader()
             : base(
                   (v, l) => 
                   true,
                  async (v, l, c) =>
                   {
                       c = 5;
                       var til = l as TestIntLoader;
                       for (int i = 0; i < c; i++)
                       {
                           v.Add(til.value);
                           til.value++;
                           await Task.Yield();
                       }

                       await Task.Yield();
                       await Task.Delay(100);
                       return new LoadMoreItemsResult { Count = (uint)c };
                   })
        {
            HasMoreItems = true;
        }
    }
}
