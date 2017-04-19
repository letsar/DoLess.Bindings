using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DoLess.Bindings.ObservableProperties
{
    internal class ObservablePropertiesBuilder : ExpressionVisitor
    {
        private const string IndexerMethodName = "get_Item";
        private const string IndexerName = "Item";

        private static readonly TypeInfo INotifyPropertyChangedTypeInfo = typeof(INotifyPropertyChanged).GetTypeInfo();
        private ObservableProperty current;

        public ObservablePropertiesBuilder()
        {
            this.Root = ObservableProperty.CreateRoot();
        }

        public ObservableProperty Root { get; }

        public override string ToString()
        {
            return this.Root.ToString();
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression != null)
            {
                this.Visit(node.Expression);
            }

            if (this.current != null)
            {
                this.current = this.current.GetOrSet(node.Member);
            }
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var expression = node.Object;
            if (expression != null)
            {
                this.Visit(expression);
            }

            if (node.Method.Name == IndexerMethodName)
            {
                if (this.current != null)
                {
                    this.current.GetOrSet(node.Method, IndexerName);
                }
                this.Visit(node.Arguments);
            }

            return node;
        }

        private void VisitExpressions(IReadOnlyList<Expression> expressions)
        {
            for (int i = 0; i < expressions.Count; i++)
            {
                this.Visit(expressions[i]);
            }
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            // The only parameter needs to be an INotifyPropertyChanged;
            this.current = this.Root;

            return base.VisitParameter(node);
        }
    }
}
