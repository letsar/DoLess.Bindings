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

namespace DoLess.Bindings.Sample.Droid
{
    [Activity(Label = "DoLess.Bindings.Sample.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
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

            

            //LinearLayout layout = new LinearLayout(this);
            //layout.Orientation = Orientation.Vertical;
            //TextView textView = new TextView(this);
            //Button button = new Button(this);
            //button.Text = "Change";
            //button.Click += this.Button_Click;
            //Button commandButton = new Button(this);
            //commandButton.Text = "CommandButton";
            //commandButton.Click += this.CommandButton_Click;
            //Button cancelCommandButton = new Button(this);
            //cancelCommandButton.Text = "CancelCommandButton";
            //EditText editText = new EditText(this);
            //RecyclerView recyclerView = new RecyclerView(this);
            this.FindViewById<RecyclerView>(Resource.Id.recyclerView1).SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

            //layout.AddView(textView);
            //layout.AddView(button);
            //layout.AddView(commandButton);
            //layout.AddView(cancelCommandButton);
            //layout.AddView(editText);
            //layout.AddView(recyclerView);

            //SetContentView(layout);

            //textView.Text = "Hello";
            //editText.Text = "bouh";

            this.ViewModel = new MainViewModel();
            this.ViewModel.Person = new PersonViewModel();
            this.ViewModel.Person.FirstName = "Bill";
            this.ViewModel.Person.LastName = "Gates";

            this.ViewModel.Persons = new ObservableCollection<PersonViewModel>(Enumerable.Range(1, 1000).Select(x => new PersonViewModel(x.ToString(), (x + 1).ToString())));

            //recyclerView.SetAdapter(new Adapter1(Enumerable.Range(1, 50).Select(x => x.ToString()).ToArray()));

            Bindings.Trace += this.Bindings_Failed;

            this.CreateBindableView(this.ViewModel)
                .Bind<TextView>(Resource.Id.textView1)
                .Property(x => x.Text)
                .To(x => $"{x.Person.FirstName} {x.Person.LastName}")
                
                .Bind<Button>(Resource.Id.button2)
                .ClickTo(x => x.CancellableCommand)

                .Bind<Button>(Resource.Id.button3)
                .ClickTo(x => x.CancellableCommand.CancelCommand)

                .Bind<EditText>(Resource.Id.editText1)
                .Property(x => x.Text)
                .To(x => x.Person.FirstName)
                .TwoWay()

                .Bind<RecyclerView>(Resource.Id.recyclerView1)
                .ItemsSourceTo(x => x.Persons)
                .Configure(a => a.WithItemTemplate(Resource.Layout.item_person)
                                 .BindItemTo(v => v.Bind<TextView>(Resource.Id.item_person_firstname)
                                                   .Property(x => x.Text)
                                                   .To(x => x.FirstName)

                                                   .Bind(v.View)
                                                   .ClickTo(x => x.ChangeFirstNameCommand)

                                                   .Bind<TextView>(Resource.Id.item_person_lastname)
                                                   .Property(x => x.Text)
                                                   .To(x => x.LastName)))
                .ItemLongClickTo(x => x.SelectPersonCommand);

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

        private void Button_Click(object sender, EventArgs e)
        {
            Bindings.Purge();
            //this.ViewModel.Persons.Add(new PersonViewModel("FirstName", "LastName"));
            //this.ViewModel.Persons = new ObservableCollection<PersonViewModel>
            //{
            //    new PersonViewModel("Bill", "Gates"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Satya", "Nadella"),
            //    new PersonViewModel("Steve", "Jobs"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook"),
            //    new PersonViewModel("Tim", "Cook")
            //};
        }

        private string GetString(string s)
        {
            return s;
        }

        //public void OnEvent<TextView>(Expression<Func<TextView, EventHandler>> expr)
        //{

        //}

        public void OnEvent<TEventArgs>(Expression<Func<TextView, EventHandler<TEventArgs>>> expr) where TEventArgs : EventArgs
        {

        }

        public void OnProperty<TextView, TProperty>(Expression<Func<TextView, TProperty>> expr)
        {

        }

        public MainViewModel ViewModel { get; set; }
    }
}

