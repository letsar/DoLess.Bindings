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
            this.ViewModel.InitializePersons();


            this.CreateBindableView(this.ViewModel)
                .Bind<ExpandableListView>(Resource.Id.activity_expandablelistview_expandablelistview)
                .ItemsSourceTo(x => x.Persons)
                .ConfigureSubItem(a => a.WithDataTemplate(Resource.Layout.item_person)
                                        .BindTo(v => v.Bind<TextView>(Resource.Id.item_person_firstname)
                                                      .Property(x => x.Text)
                                                      .To(x => x.FirstName)

                                                      .Bind<TextView>(Resource.Id.item_person_lastname)
                                                      .Property(x => x.Text)
                                                      .To(x => x.LastName)))
                .ConfigureItem(a => a.WithDataTemplate(Resource.Layout.header_person)
                                     .BindTo(v => v.Bind<TextView>(Resource.Id.header_person_text)
                                                   .Property(x => x.Text)
                                                   .To(x => ((IGrouping<string, PersonViewModel>)x).Key)));

        }

        public ExpandableViewModel ViewModel { get; set; }
    }
}