using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DoLess.Bindings
{
    internal class PropertyWatcherBuilder : ExpressionVisitor
    {
        private const string IndexerMethodName = "get_Item";
        private const string IndexerName = "Item";

        private PropertyWatcher currentWatcher;

        public PropertyWatcherBuilder(PropertyWatcher propertyWatcher)
        {            
            this.PropertyWatcher = propertyWatcher;
        }

        public PropertyWatcher PropertyWatcher { get; }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression != null)
            {
                this.Visit(node.Expression);
            }

            if (node.Member is PropertyInfo propertyInfo)
            {
                this.currentWatcher = this.currentWatcher?.AddOrGet(propertyInfo);
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
                this.currentWatcher = this.currentWatcher?.AddOrGet(node.Method, IndexerName);
            }

            this.Visit(node.Arguments);

            return node;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            // Reset the current watcher to the root one be cause we revisit the only parameter.
            this.currentWatcher = this.PropertyWatcher;

            return base.VisitParameter(node);
        }

        private void VisitExpressions(IReadOnlyList<Expression> expressions)
        {
            for (int i = 0; i < expressions.Count; i++)
            {
                this.Visit(expressions[i]);
            }
        }        
    }
}
