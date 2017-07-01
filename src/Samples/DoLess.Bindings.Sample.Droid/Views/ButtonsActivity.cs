using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DoLess.Bindings.Sample.ViewModels;
using Android.Support.V7.App;

namespace DoLess.Bindings.Sample.Droid.Views
{
    [Activity(Label = "ButtonsActivity")]
    public class ButtonsActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.activity_buttons);

            this.SetToolbarTitle("Buttons");
            this.ViewModel = new ButtonsViewModel();

            this.CreateBindableView(this.ViewModel)

                .Bind<Button>(Resource.Id.activity_buttons_command)
                .ClickTo(x => x.CancellableCommand)

                .Bind<Button>(Resource.Id.activity_buttons_cancelcommand)
                .ClickTo(x => x.CancellableCommand.CancelCommand);
        }

        public ButtonsViewModel ViewModel { get; set; }
    }
}