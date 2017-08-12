
using Android.App;
using Android.OS;
using Android.Widget;
using DoLess.Bindings.Sample.ViewModels;

namespace DoLess.Bindings.Sample.Droid.Views
{
    [Activity(Label = "ButtonsActivity")]
    public class ButtonsActivity : BaseActivity<ButtonsViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.activity_buttons);

            this.SetToolbarTitle("Buttons");

            this.ViewModel(new ButtonsViewModel())

                .Bind<Button>(Resource.Id.activity_buttons_command)
                .ClickTo(x => x.CancellableCommand)
                
                .Bind<Button>(Resource.Id.activity_buttons_cancelcommand)
                .ClickTo(x => x.CancellableCommand.CancelCommand);          
        }
    }
}