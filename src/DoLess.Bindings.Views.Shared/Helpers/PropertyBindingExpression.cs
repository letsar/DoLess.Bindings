using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace DoLess.Bindings
{
    [DebuggerDisplay("{RawExpression}")]
    internal class PropertyBindingExpression<TSource, TProperty> :
        IPropertyBindingExpression
    {
        private readonly TSource source;
        private readonly Func<TSource, TProperty> getter;
        private readonly Action<TSource, TProperty> setter;

        public PropertyBindingExpression(TSource source, Expression<Func<TSource, TProperty>> expression)
        {
            this.RawExpression = expression.ToString();
            this.source = source;

            // The expression is already the getter.
            this.getter = expression.Compile();

            if (expression.Body is MemberExpression body)
            {
                try
                {
                    this.Name = body.Member.Name;

                    // The setter is a little more complicated.
                    // We have to create a second argument of type TProperty (the value), and the expression must be the assignment of this value to the body.
                    var valueParameter = Expression.Parameter(typeof(TProperty), "value");
                    var setterExpression = Expression.Assign(body, valueParameter);
                    this.setter = Expression.Lambda<Action<TSource, TProperty>>(setterExpression, expression.Parameters[0], valueParameter)
                                            .Compile();

                }
                catch (Exception ex)
                {
                    Bindings.LogError($"'{this.RawExpression}' is not a property expression", ex);
                }
            }
        }

        public string RawExpression { get; }

        public string Name { get; }

        public TProperty Value
        {
            get { return this.getter(this.source); }
            set { this.setter?.Invoke(this.source, value); }
        }
    }
}
