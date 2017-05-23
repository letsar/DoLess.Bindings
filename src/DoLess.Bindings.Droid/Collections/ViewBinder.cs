using Android.Views;
using System;

namespace DoLess.Bindings
{
    internal class ViewBinder<TData> :
        IViewBinder<TData>
        where TData : class
    {
        public event EventHandler<EventArgs<TData>> Click;
        public event EventHandler<EventArgs<TData>> LongClick;

        public Func<IBindableView<TData>, IBinding> DataBinder { get; private set; }

        public IDataTemplateSelector<TData> DataTemplateSelector { get; private set; }

        public IViewBinder<TData> BindTo(Func<IBindableView<TData>, IBinding> dataBinder)
        {
            this.DataBinder = dataBinder;
            return this;
        }

        public void BindViewHolder(BindableViewHolder<TData> viewHolder, TData data)
        {
            if (this.DataBinder == null)
            {
                Bindings.LogError("you should set the data binder for this subview");
            }
            else
            {
                if (viewHolder != null)
                {
                    // Unbinds the previous bindings before setting the new one.
                    viewHolder.Unbind();

                    viewHolder.ViewModel = data;
                    viewHolder.BindEvents();
                    viewHolder.Binding = this.DataBinder(viewHolder);
                }
            }
        }

        public BindableViewHolder<TData> CreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(viewType, parent, false);
            var viewHolder = new BindableViewHolder<TData>(view);
            viewHolder.Click += this.OnViewHolderClick;
            viewHolder.LongClick += this.OnViewHolderLongClick;
            return viewHolder;
        }

        public int GetLayoutId(TData data)
        {
            return this.DataTemplateSelector.GetLayoutId(data);
        }

        public View GetView(TData data, View convertView, ViewGroup parent)
        {
            BindableViewHolder<TData> viewHolder = null;

            if (convertView != null)
            {
                viewHolder = convertView.Tag as BindableViewHolder<TData>;
            }

            if (viewHolder == null)
            {
                viewHolder = this.CreateViewHolder(parent, this.GetLayoutId(data));
                convertView.Tag = viewHolder;
            }

            this.BindViewHolder(viewHolder, data);
            return convertView;
        }

        public void RecycleViewHolder(Java.Lang.Object holder)
        {
            var viewHolder = holder as BindableViewHolder<TData>;
            if (viewHolder != null)
            {
                viewHolder.UnbindEvents();
            }
        }

        public IViewBinder<TData> WithDataTemplate(int resourceId)
        {
            this.DataTemplateSelector = new SingleDataTemplateSelector<TData>(resourceId);
            return this;
        }

        public IViewBinder<TData> WithDataTemplateSelector<T>()
            where T : IDataTemplateSelector<TData>, new()
        {
            this.DataTemplateSelector = Cache<T>.Instance;
            return this;
        }

        private void OnViewHolderClick(object sender, EventArgs<TData> e)
        {
            this.Click?.Invoke(this, e);
        }

        private void OnViewHolderLongClick(object sender, EventArgs<TData> e)
        {
            this.LongClick?.Invoke(this, e);
        }
    }
}