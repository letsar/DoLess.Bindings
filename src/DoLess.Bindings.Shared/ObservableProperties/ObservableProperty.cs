using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using DoLess.Bindings.Helpers;

namespace DoLess.Bindings.ObservableProperties
{
    [DebuggerDisplay("{Name}")]
    internal sealed class ObservableProperty
    {
        private static readonly TypeInfo INotifyPropertyChangedTypeInfo = typeof(INotifyPropertyChanged).GetTypeInfo();
        private const string RootName = "vm";

        private Dictionary<string, ObservableProperty> properties;

        private ObservableProperty(string name, bool isObservable)
        {
            this.Name = name;
            this.IsObservable = isObservable;
            this.properties = new Dictionary<string, ObservableProperty>();
        }

        public static ObservableProperty CreateRoot()
        {
            return new ObservableProperty(RootName, true);
        }

        public string Name { get; }

        public bool IsObservable { get; }

        public IReadOnlyList<ObservableProperty> Properties => this.properties.Values.ToList();

        //public ObservableProperty GetOrSet(string name)
        //{
        //    Check.NotNull(name, nameof(name));

        //    ObservableProperty property;
        //    if (!this.properties.TryGetValue(name, out property))
        //    {
        //        property = new ObservableProperty(name);
        //        this.properties[name] = property;
        //    }

        //    return property;
        //}

        public ObservableProperty GetOrSet(MemberInfo member, string name = null)
        {
            Check.NotNull(member, nameof(member));

            if (this.IsObservable)
            {
                string memberName = name ?? member.Name;
                ObservableProperty property;
                if (!this.properties.TryGetValue(memberName, out property))
                {
                    property = new ObservableProperty(memberName, CanBeObserved(member));
                    this.properties[memberName] = property;
                }

                return property;
            }
            else
            {
                return this;
            }
        }

        public bool Remove(string name)
        {
            Check.NotNull(name, nameof(name));

            return this.properties.Remove(name);
        }

        public ObservableProperty TryGet(string name)
        {
            ObservableProperty property = null;
            this.properties.TryGetValue(name, out property);
            return property;
        }

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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            this.AppendToStringBuilder(stringBuilder);
            return stringBuilder.ToString();
        }

        private void AppendToStringBuilder(StringBuilder stringBuilder)
        {
            stringBuilder.Append(this.Name);
            if (this.properties.Count > 0)
            {
                stringBuilder.Append("(");
                foreach (var property in this.properties.Values.OrderBy(x => x.Name))
                {
                    property.AppendToStringBuilder(stringBuilder);
                    stringBuilder.Append(", ");
                }
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
                stringBuilder.Append(")");
            }
        }
    }
}
