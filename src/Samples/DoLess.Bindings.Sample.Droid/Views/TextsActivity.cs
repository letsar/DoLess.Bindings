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
using DoLess.Bindings;
using DoLess.Bindings.Sample.ViewModels;
using Android.Support.V7.App;
using Java.Lang;

namespace DoLess.Bindings.Sample.Droid.Views
{
    [Activity(Label = "TextsActivity")]
    public class TextsActivity : BaseActivity, IBindableView<TextsViewModel>
    {
        private static List<WeakReference<TextView>> Weaks = new List<WeakReference<TextView>>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.activity_texts);

            this.SetToolbarTitle("Texts");
            
            var vm = new TextsViewModel();
            vm.Person = new PersonViewModel("Dark", "Vador");

            var activity_texts_textview = this.FindViewById<TextView>(Resource.Id.activity_texts_textview);
            var activity_texts_edittext = this.FindViewById<EditText>(Resource.Id.activity_texts_edittext);

            this.Setup(vm)
                .Bind<TextView>(Resource.Id.activity_texts_textview)                
                .Property(x => x.Text)
                .To(x => $"{x.Person.FirstName} {x.Person.LastName}")

                .Bind<EditText>(Resource.Id.activity_texts_edittext)                
                .Property(x => x.Text)
                .To(x => x.Person.FirstName, BindingMode.TwoWay);

            //this.CreateBindableView(this.ViewModel)

            //    .Bind<TextView>(Resource.Id.activity_texts_textview)
            //    .Property(x => x.Text)
            //    .To(x => $"{x.Person.FirstName} {x.Person.LastName}")

            //    .Bind<EditText>(Resource.Id.activity_texts_edittext)
            //    .Property(x => x.Text)
            //    .To(x => x.Person.FirstName)
            //    .TwoWay();
        }

        public IBinder<TextsViewModel> Binder { get; set; }

        protected override void OnStop()
        {
            base.OnStop();
            this.Binder?.Dispose();
            this.Binder = null;
        }
    }
}