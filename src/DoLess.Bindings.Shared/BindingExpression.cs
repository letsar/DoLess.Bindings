using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DoLess.Bindings
{
    internal sealed class BindingExpression<TSource, TProperty>
        where TSource : class
    {
        private readonly WeakReference<TSource> weakSource;
        private readonly PropertyInfo propertyInfo;
        private readonly FieldInfo fieldInfo;
        private readonly Func<TSource, object> getPropertyOwner;
        private readonly Func<TProperty> get;
        private readonly Action<TProperty> set;

        public BindingExpression(TSource source, Expression<Func<TSource, TProperty>> expression)
        {
            this.weakSource = new WeakReference<TSource>(source);

            try
            {
                var memberExpression = (MemberExpression)expression.Body;

                this.propertyInfo = memberExpression.Member as PropertyInfo;
                this.fieldInfo = memberExpression.Member as FieldInfo;

                this.Name = memberExpression.Member.Name;

                if (this.propertyInfo != null)
                {
                    this.get = this.GetFromPropertyInfo;
                    this.set = this.SetFromPropertyInfo;                    
                }
                else if (this.fieldInfo != null)
                {
                    this.get = this.GetFromFieldInfo;
                    this.set = this.SetFromFieldInfo;
                }

                this.getPropertyOwner = Expression.Lambda<Func<TSource, object>>(memberExpression.Expression, expression.Parameters).Compile();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("This is not a property expression", ex);
            }
        }

        public string Name { get; }

        public TProperty Value
        {
            get { return this.get(); }
            set { this.set(value); }
        }

        private TProperty GetFromPropertyInfo()
        {
            return (TProperty)this.propertyInfo.GetValue(this.GetPropertyOwner());
        }

        private void SetFromPropertyInfo(TProperty value)
        {
            this.propertyInfo.SetValue(this.GetPropertyOwner(), value);
        }

        private TProperty GetFromFieldInfo()
        {
            return (TProperty)this.fieldInfo.GetValue(this.GetPropertyOwner());
        }

        private void SetFromFieldInfo(TProperty value)
        {
            this.fieldInfo.SetValue(this.GetPropertyOwner(), value);
        }

        private object GetPropertyOwner()
        {
            return this.getPropertyOwner(this.weakSource.GetOrDefault());
        }
    }
}
