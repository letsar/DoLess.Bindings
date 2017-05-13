using Android.App;
using Android.Widget;
using Android.OS;
using DoLess.Bindings.Sample.ViewModels;
using DoLess.Bindings;
using Android.Support.V7.Widget;
using System.Linq.Expressions;
using System;
using Android.Runtime;
using Java.Lang;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using Android.Support.V7.App;
using DoLess.Bindings.Sample.Droid.Views;

namespace DoLess.Bindings.Sample.Droid
{
    [Activity(Label = "DoLess.Bindings.Sample.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : BaseActivity
    {
        private class ExceptHandler : Java.Lang.Object, Java.Lang.Thread.IUncaughtExceptionHandler
        {
            public void UncaughtException(Thread t, Throwable e)
            {
                throw new NotImplementedException();
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser += this.AndroidEnvironment_UnhandledExceptionRaiser;
            Java.Lang.Thread.DefaultUncaughtExceptionHandler = new ExceptHandler();
            TaskScheduler.UnobservedTaskException += this.TaskScheduler_UnobservedTaskException;

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            this.SetToolbarTitle("Home", false);

            this.FindViewById<Button>(Resource.Id.main_buttons).Click += (s, e) => this.StartActivity(typeof(ButtonsActivity));
            this.FindViewById<Button>(Resource.Id.main_texts).Click += (s, e) => this.StartActivity(typeof(TextsActivity));
            this.FindViewById<Button>(Resource.Id.main_recyclerview).Click += (s, e) => this.StartActivity(typeof(RecyclerViewActivity));
            this.FindViewById<Button>(Resource.Id.main_expandablelistview).Click += (s, e) => this.StartActivity(typeof(ExpandableListViewActivity));

            Bindings.Trace += this.Bindings_Failed;           

            //this.CreateBindableView(this.ViewModel)
            //    .Bind(textView)
            //    .Property(x => x.Text)
            //    .To(x => $"{x.Person.FirstName} {x.Person.LastName}")

            //    .Bind(commandButton)
            //    .ClickTo(x => x.CancellableCommand)

            //    .Bind(cancelCommandButton)
            //    .ClickTo(x => x.CancellableCommand.CancelCommand)

            //    .Bind(editText)
            //    .Property(x => x.Text)
            //    .To(x => x.Person.FirstName)
            //    .TwoWay()

            //    .Bind(recyclerView)
            //    .ItemsSourceTo(x => x.Persons)
            //    .Configure(a => a.WithItemTemplate(Resource.Layout.item_person)
            //                     .BindItemTo(v => v.Bind<TextView>(Resource.Id.item_person_firstname)
            //                                       .Property(x => x.Text)
            //                                       .To(x => x.FirstName)

            //                                       .Bind(v.View)
            //                                       .ClickTo(x => x.ChangeFirstNameCommand)

            //                                       .Bind<TextView>(Resource.Id.item_person_lastname)
            //                                       .Property(x => x.Text)
            //                                       .To(x => x.LastName)))
            //    .ItemLongClickTo(x => x.SelectPersonCommand);           
        }

        private void MainActivity_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Bindings_Failed(object sender, BindingTraceEventArgs obj)
        {
            if (obj.EventType == BindingTraceEventType.Error)
            {
                throw new NotImplementedException();
            }
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CommandButton_Click(object sender, EventArgs e)
        {

        }     


        public void OnEvent<TEventArgs>(Expression<Func<TextView, EventHandler<TEventArgs>>> expr) where TEventArgs : EventArgs
        {

        }

        public void OnProperty<TextView, TProperty>(Expression<Func<TextView, TProperty>> expr)
        {

        }

        public MainViewModel ViewModel { get; set; }
    }
}

