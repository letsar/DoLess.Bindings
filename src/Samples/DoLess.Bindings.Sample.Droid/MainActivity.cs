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

namespace DoLess.Bindings.Sample.Droid
{
    [Activity(Label = "DoLess.Bindings.Sample.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IView<MainViewModel>
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

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);

            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser += this.AndroidEnvironment_UnhandledExceptionRaiser;
            Java.Lang.Thread.DefaultUncaughtExceptionHandler = new ExceptHandler();
            TaskScheduler.UnobservedTaskException += this.TaskScheduler_UnobservedTaskException;

            LinearLayout layout = new LinearLayout(this);
            layout.Orientation = Orientation.Vertical;
            TextView textView = new TextView(this);
            Button button = new Button(this);
            button.Text = "Change";
            button.Click += this.Button_Click;
            Button commandButton = new Button(this);
            commandButton.Text = "CommandButton";
            commandButton.Click += this.CommandButton_Click;
            Button cancelCommandButton = new Button(this);
            cancelCommandButton.Text = "CancelCommandButton";
            EditText editText = new EditText(this);
            RecyclerView recyclerView = new RecyclerView(this);

            layout.AddView(textView);
            layout.AddView(button);
            layout.AddView(commandButton);
            layout.AddView(cancelCommandButton);
            layout.AddView(editText);
            layout.AddView(recyclerView);

            SetContentView(layout);

            textView.Text = "Hello";
            editText.Text = "bouh";

            this.ViewModel = new MainViewModel();
            this.ViewModel.Person = new PersonViewModel();
            this.ViewModel.Person.FirstName = "Bill";
            this.ViewModel.Person.LastName = "Gates";

            this.ViewModel.Persons = new System.Collections.Generic.List<PersonViewModel>
            {
                new PersonViewModel("Bill", "Gates"),
                new PersonViewModel("Steve", "Ballmer"),
                new PersonViewModel("Satya", "Nadella"),
                new PersonViewModel("Steve", "Jobs"),
                new PersonViewModel("Tim", "Cook")
            };

            this.Bind(textView)
                .Property(x => x.Text)
                .To(x => $"{x.Person.FirstName} {x.Person.LastName}")
                .Bind(commandButton)
                .ClickTo(x => x.CancellableCommand)
                .Bind(cancelCommandButton)
                .ClickTo(x => x.CancellableCommand.CancelCommand)
                .Bind(editText)
                .Property(x => x.Text)
                .To(x => x.Person.FirstName)
                .TwoWay()
                .Bind(recyclerView)
                .ItemsSourceTo(x => x.Persons)
                .WithItemTemplate(Resource.Layout.item_person)
                .BindItemTo((vm, vh) => vm.BindFromViewModel(vh.GetView<TextView>(Resource.Id.item_person_firstname))
                                          .Property(x => x.Text)
                                          .To(x => x.FirstName)
                                          .Bind(vh.GetView<TextView>(Resource.Id.item_person_lastname))
                                          .Property(x => x.Text)
                                          .To(x => x.LastName));

            //this.OnEvent(t => t.Click);
            //Bindings.WeakEventManager<TextView, EventArgs>.Current.AddHandler(textView, textView.Click);
            //this.ViewModel.Bind(textView, x => x.Text)
            //              .To(vm => vm.Name);   

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
            this.ViewModel.Person.FirstName = this.ViewModel.Person.LastName + "e";
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

