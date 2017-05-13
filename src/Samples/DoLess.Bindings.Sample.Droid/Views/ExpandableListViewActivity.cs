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
using Android.Support.V7.Widget;

namespace DoLess.Bindings.Sample.Droid.Views
{
    [Activity(Label = "ExpandableListViewActivity")]
    public class ExpandableListViewActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.activity_expandablelistview);           

            this.SetToolbarTitle("ExpandableListView");
            this.ViewModel = new ExpandableViewModel();
            this.ViewModel.InitializePersones();
            

            //this.CreateBindableView(this.ViewModel)
            //     .Bind<RecyclerView>(Resource.Id.activity_recyclerview_recyclerView)
            //     .ItemsSourceTo(x => x.Persons)
            //     .Configure(a => a.WithItemTemplate(Resource.Layout.item_person)
            //                      .BindItemTo(v => v.Bind<TextView>(Resource.Id.item_person_firstname)
            //                                        .Property(x => x.Text)
            //                                        .To(x => x.FirstName)

            //                                        .Bind(v.View)
            //                                        .ClickTo(x => x.ChangeFirstNameCommand)

            //                                        .Bind<TextView>(Resource.Id.item_person_lastname)
            //                                        .Property(x => x.Text)
            //                                        .To(x => x.LastName)))
            //     .ItemLongClickTo(x => x.SelectPersonCommand);
        }

        public ExpandableViewModel ViewModel { get; set; }
    }
}