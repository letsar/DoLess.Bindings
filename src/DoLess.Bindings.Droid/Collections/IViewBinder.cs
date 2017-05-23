using System;

namespace DoLess.Bindings
{
    public interface IViewBinder<TData>       
        where TData : class
    {
        event EventHandler<EventArgs<TData>> Click;
        event EventHandler<EventArgs<TData>> LongClick;

        IViewBinder<TData> BindTo(Func<IBindableView<TData>, IBinding> dataBinder);

        IViewBinder<TData> WithDataTemplate(int resourceId);

        IViewBinder<TData> WithDataTemplateSelector<T>()
            where T : IDataTemplateSelector<TData>, new();
    }
}