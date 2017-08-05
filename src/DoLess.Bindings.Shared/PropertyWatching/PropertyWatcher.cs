using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DoLess.Bindings
{
    public delegate void PropertyChangedCallback(object source, string propertyName);

    /// <summary>
    /// Represents an object that observes the changes in the properties of an object defined by an expression.
    /// </summary>
    /// <remarks>
    /// This object needs to be disposed in order to remove the subscription of all the <see cref="INotifyPropertyChanged.PropertyChanged"/> events.
    /// </remarks>
    [DebuggerDisplay("{Name}")]
    public class PropertyWatcher : IDisposable
    {
        public const string DefaultName = "x";
        private static readonly TypeInfo INotifyPropertyChangedTypeInfo = typeof(INotifyPropertyChanged).GetTypeInfo();
        private readonly object propertyInfoLock = new object();


        private SortedDictionary<string, PropertyWatcher> watchers;
        private PropertyChangedCallback onPropertyChanged;
        private INotifyPropertyChanged source;
        private PropertyInfo propertyInfo;

        private PropertyWatcher(Type type, bool isWatchable, string name = DefaultName)
        {
            this.Name = name;
            this.PropertyType = type;
            this.IsWatchable = isWatchable;
            this.watchers = new SortedDictionary<string, PropertyWatcher>(StringComparer.Ordinal);
        }

        public string Name { get; }

        public Type PropertyType { get; set; }

        public bool IsWatchable { get; }

        /// <summary>
        /// Creates a new <see cref="PropertyWatcher"/> from the specified expression.
        /// </summary>
        /// <typeparam name="TViewModel">The view model type.</typeparam>
        /// <typeparam name="TProperty">The final property type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static PropertyWatcher Create<TViewModel, TProperty>(Expression<Func<TViewModel, TProperty>> expression)
        {
            PropertyWatcher propertyWatcher = new PropertyWatcher(typeof(TViewModel), true);
            PropertyWatcherBuilder builder = new PropertyWatcherBuilder(propertyWatcher);
            builder.Visit(expression);
            return propertyWatcher;
        }

        public void Dispose()
        {
            this.watchers.Values.ForEach(x => x.Dispose());
            this.watchers.Clear();
            this.watchers = null;
            this.onPropertyChanged = null;
            this.propertyInfo = null;
            this.ResetSource();
        }

        /// <summary>
        /// Watches the changes of the specified source.
        /// </summary>
        /// <param name="source">The object to watch.</param>
        /// <param name="onPropertyChanged">The method to call when any property changed.</param>
        public void Watch(object source, PropertyChangedCallback onPropertyChanged)
        {
            if (source != null &&
                source is INotifyPropertyChanged newSource &&
                onPropertyChanged != null)
            {
                this.onPropertyChanged = onPropertyChanged;
                this.ResetWatch(newSource);

                foreach (var watcher in this.watchers.Values.Where(x => x.IsWatchable))
                {
                    watcher.Watch(watcher.GetPropertyValue(newSource), onPropertyChanged);
                }
            }
        }

        internal PropertyWatcher AddOrGet(PropertyInfo propertyInfo)
        {
            return this.AddOrGet(propertyInfo.PropertyType, propertyInfo.Name);
        }

        internal PropertyWatcher AddOrGet(MethodInfo methodInfo, string propertyName)
        {
            return this.AddOrGet(methodInfo.ReturnType, propertyName);
        }

        private PropertyWatcher AddOrGet(Type type, string propertyName)
        {
            if (this.IsWatchable)
            {
                if (!this.watchers.TryGetValue(propertyName, out PropertyWatcher watcher))
                {
                    watcher = new PropertyWatcher(type, CanBeWatched(type), propertyName);
                    this.watchers[propertyName] = watcher;
                }
                return watcher;
            }
            else
            {
                return this;
            }
        }

        private static bool CanBeWatched(Type type)
        {
            return INotifyPropertyChangedTypeInfo.IsAssignableFrom(type.GetTypeInfo());
        }

        private void ResetWatch(INotifyPropertyChanged newSource)
        {
            this.ResetSource();

            if (newSource != null)
            {
                this.source = newSource;
                this.source.PropertyChanged += this.OnPropertyChanged;
            }
        }

        private void ResetSource()
        {
            if (this.source != null)
            {
                this.source.PropertyChanged -= this.OnPropertyChanged;
                this.source = null;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (this.watchers.TryGetValue(args.PropertyName, out PropertyWatcher watcher))
            {
                // We notify only if one the watched properties has changed.
                this.onPropertyChanged?.Invoke(sender, args.PropertyName);

                // If the changed property is an observable, we need to reset the PropertyChanged subscritpion in order to avoid memory leak.
                watcher.ResetWatch(watcher.GetPropertyValue(sender));
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            this.AppendToStringBuilder(stringBuilder);
            return stringBuilder.ToString();
        }

        private INotifyPropertyChanged GetPropertyValue(object source)
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

        private void AppendToStringBuilder(StringBuilder stringBuilder)
        {
            stringBuilder.Append(this.Name);
            if (this.watchers.Count > 0)
            {
                stringBuilder.Append("(");
                foreach (var property in this.watchers.Values)
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
