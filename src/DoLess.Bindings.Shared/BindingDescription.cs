using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DoLess.Bindings
{
    internal class BindingDescription<TSource, TTarget> :
        IBindingDescription<TSource, TTarget>,
        IHaveBindingSet<TSource, TTarget>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
    {
        public BindingDescription(BindingSet<TSource, TTarget> bindingSet)
        {
            this.BindingSet = bindingSet;
        }

        public BindingDescription(TSource source, TTarget target) :
            this(new BindingSet<TSource, TTarget>(source, target))
        { }

        public BindingDescription(IHaveBindingSet<TSource, TTarget> bindingSetOwner) :
            this(bindingSetOwner?.BindingSet)
        { }

        public BindingSet<TSource, TTarget> BindingSet { get; }
    }
}
