using Android.App;
using Android.Widget;
using Android.OS;
using DoLess.Bindings.Sample.ViewModels;
using DoLess.Bindings;
using Android.Support.V7.Widget;
using System.Linq.Expressions;
using System;

namespace DoLess.Bindings.Sample.Droid
{
    [Activity(Label = "DoLess.Bindings.Sample.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);

            LinearLayout layout = new LinearLayout(this);
            layout.Orientation = Orientation.Vertical;
            TextView textView = new TextView(this);
            Button button = new Button(this);
            button.Text = "Change";
            button.Click += this.Button_Click;
            layout.AddView(textView);
            layout.AddView(button);
            SetContentView(layout);

            textView.Text = "Hello";

            this.ViewModel = new MainViewModel();
            this.ViewModel.Person = new PersonViewModel();
            this.ViewModel.Person.FirstName = "Bill";
            this.ViewModel.Person.LastName = "Gates";

            this.ViewModel.Bind(textView)
                          .Property(x => x.Text)
                          .To(x => $"{x.Person.FirstName} {x.Person.LastName}");
            //this.OnEvent(t => t.Click);
            //Bindings.WeakEventManager<TextView, EventArgs>.Current.AddHandler(textView, textView.Click);
            //this.ViewModel.Bind(textView, x => x.Text)
            //              .To(vm => vm.Name);                          
        }

        private void Button_Click(object sender, EventArgs e)
        {
            this.ViewModel.Person.LastName = this.ViewModel.Person.LastName + "e";
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

