using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace EasyWaterfallStream
{
    public class WaterLineDefinintion : DependencyObject, ICollectionViewGroup
    {
       
        public object Group
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IObservableVector<object> GroupItems
        {
            get
            {
                throw new NotImplementedException();
            }
        }

 

    }
}
