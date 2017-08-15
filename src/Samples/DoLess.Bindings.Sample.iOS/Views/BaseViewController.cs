using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace DoLess.Bindings.Sample.iOS.Views
{
    abstract class BaseViewController<T> :
        UIViewController,
        IBindableView<T>
        where T : class
    {
        public IBinder<T> Binder { get; set; }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.White;

            this.Load();
            this.Bind();
        }

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
            this.Binder?.Dispose();
            this.Binder = null;
        }

        abstract protected void Bind();

        abstract protected void Load();

        protected static UIButton CreateButton(string title)
        {
            var button = UIButton.FromType(UIButtonType.System);
            button.SetTitle(title, UIControlState.Normal);
            return button;
        }
    }
}