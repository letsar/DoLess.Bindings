using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using DoLess.Bindings.Helpers;

namespace DoLess.Bindings.Observation
{
    [DebuggerDisplay("{Name}")]
    internal sealed class ObservedNode
    {
        private const string RootName = "vm";
        private static readonly TypeInfo INotifyPropertyChangedTypeInfo = typeof(INotifyPropertyChanged).GetTypeInfo();
        private readonly object propertyInfoLock = new object();
        private SortedDictionary<string, ObservedNode> nodes;
        private PropertyInfo propertyInfo;
        private INotifyPropertyChangedWeakEventHandler weakHandler;
        private Action whenChanged;

        private ObservedNode(string name, bool isObservable)
        {
            this.Name = name;
            this.IsObservable = isObservable;
            this.nodes = new SortedDictionary<string, ObservedNode>(StringComparer.Ordinal);
        }

        public bool IsObservable { get; }

        public string Name { get; }

        public IReadOnlyList<ObservedNode> Nodes => this.nodes.Values.ToList();

        public static bool CanBeObserved(Type type)
        {
            return INotifyPropertyChangedTypeInfo.IsAssignableFrom(type.GetTypeInfo());
        }

        public static bool CanBeObserved(MemberInfo member)
        {
            var propertyInfo = member as PropertyInfo;
            if (propertyInfo != null)
            {
                return CanBeObserved(propertyInfo.PropertyType);
            }

            return false;
        }

        public static ObservedNode CreateRoot()
        {
            return new ObservedNode(RootName, true);
        }

        public ObservedNode GetOrSet(MemberInfo member, string name = null)
        {
            Check.NotNull(member, nameof(member));

            if (this.IsObservable)
            {
                string memberName = name ?? member.Name;
                ObservedNode property;
                if (!this.nodes.TryGetValue(memberName, out property))
                {
                    property = new ObservedNode(memberName, CanBeObserved(member));
                    this.nodes[memberName] = property;
                }

                return property;
            }
            else
            {
                return this;
            }
        }

        public IReadOnlyList<INotifyPropertyChangedWeakEventHandler> GetWeakHandlers()
        {
            List<INotifyPropertyChangedWeakEventHandler> weakHandlers = new List<INotifyPropertyChangedWeakEventHandler>();
            weakHandlers.Add(this.weakHandler);
            weakHandlers.AddRange(this.Nodes.SelectMany(x => x.GetWeakHandlers()));
            weakHandlers = weakHandlers.Where(x => x != null).ToList();
            return weakHandlers;
        }

        public void Observe(INotifyPropertyChanged source, Action whenChanged)
        {
            Check.NotNull(whenChanged, nameof(whenChanged));

            if (this.IsObservable)
            {
                this.ObserveInternal(source, whenChanged);
            }
        }

        public bool Remove(string name)
        {
            Check.NotNull(name, nameof(name));

            return this.nodes.Remove(name);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            this.AppendToStringBuilder(stringBuilder);
            return stringBuilder.ToString();
        }

        public ObservedNode TryGet(string name)
        {
            ObservedNode property = null;
            this.nodes.TryGetValue(name, out property);
            return property;
        }

        private void AppendToStringBuilder(StringBuilder stringBuilder)
        {
            stringBuilder.Append(this.Name);
            if (this.nodes.Count > 0)
            {
                stringBuilder.Append("(");
                foreach (var property in this.nodes.Values)
                {
                    property.AppendToStringBuilder(stringBuilder);
                    stringBuilder.Append(", ");
                }
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
                stringBuilder.Append(")");
            }
        }

        private INotifyPropertyChanged GetNotiyfPropertyChangedProperty(object source)
        {
            lock (this.propertyInfoLock)
            {
                if (this.propertyInfo == null)
                {
                    this.propertyInfo = source.GetType().GetRuntimeProperty(this.Name);
                }
            }

            return this.propertyInfo.GetValue(source) as INotifyPropertyChanged;
        }

        private void ObserveInternal(INotifyPropertyChanged source, Action whenChanged)
        {
            this.whenChanged = whenChanged;

            if (source != null)
            {
                // Observes this one.
                this.weakHandler = source.AddWeakEventHandler(this.OnPropertyChanged);

                // Observes the children.
                foreach (var node in this.Nodes.Where(x => x.IsObservable))
                {
                    node.ObserveInternal(node.GetNotiyfPropertyChangedProperty(source), whenChanged);
                }
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ObservedNode node = null;
            if (this.nodes.TryGetValue(args.PropertyName, out node))
            {
                this.whenChanged();
                if (node.IsObservable)
                {
                    node.ResetWeakHandler(sender);
                }
            }
        }

        private void ResetWeakHandler(object source)
        {
            // Unsubscribes the previous subscription and create a new one.
            this.weakHandler?.Unsubscribe();
            this.weakHandler = null;

            // Gets the new value.
            var newValue = this.GetNotiyfPropertyChangedProperty(source);
            if (newValue != null)
            {
                this.weakHandler = new INotifyPropertyChangedWeakEventHandler(newValue, this.OnPropertyChanged);
            }
        }
    }
}
