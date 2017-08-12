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
    public class TextsActivity : BaseActivity<TextsViewModel>
    {
        private static List<WeakReference<TextView>> Weaks = new List<WeakReference<TextView>>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.activity_texts);

            this.SetToolbarTitle("Texts");
            
            var vm = new TextsViewModel();
            vm.Person = new PersonViewModel("Dark", "Vador");            

            this.ViewModel(vm)
                .Bind<TextView>(Resource.Id.activity_texts_textview)                
                .Property(x => x.Text)
                .To(x => $"{x.Person.FirstName} {x.Person.LastName}")

                .Bind<EditText>(Resource.Id.activity_texts_edittext)                
                .Property(x => x.Text)
                .To(x => x.Person.FirstName, BindingMode.TwoWay);          
        }
    }
}