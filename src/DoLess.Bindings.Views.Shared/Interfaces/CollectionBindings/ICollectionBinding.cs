using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    public partial interface ICollectionBinding<TSource, TTarget, TSourceItem>
        where TSource : class
        where TTarget : class
    {
    }
}
