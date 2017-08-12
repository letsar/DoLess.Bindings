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
    public abstract class BaseActivity<T> : 
        AppCompatActivity, 
        IBindableView<T> 
        where T : class
    {
        public IBinder<T> Binder { get; set; }

        protected void SetToolbarTitle(string title, bool canGoBack = true)
        {
            this.SetSupportActionBar(this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar));
            this.SupportActionBar.Title = $"DoLess.Bindings - {title}";
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(canGoBack);
            this.SupportActionBar.SetHomeButtonEnabled(canGoBack);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                this.Finish();
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnStop()
        {
            base.OnStop();
            this.Binder?.Dispose();
            this.Binder = null;
        }
    }
}