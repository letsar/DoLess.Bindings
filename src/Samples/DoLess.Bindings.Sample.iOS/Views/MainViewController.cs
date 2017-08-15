using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;

namespace DoLess.Bindings.Sample.iOS.Views
{
    [Register("MainViewController")]
    class MainViewController : UIViewController
    {
        public MainViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "Home";
            this.View.BackgroundColor = UIColor.White;

            var stackView = new UIStackView();
            stackView.Axis = UILayoutConstraintAxis.Vertical;
            stackView.Distribution = UIStackViewDistribution.EqualSpacing;
            stackView.Alignment = UIStackViewAlignment.Center;
            stackView.Spacing = 30;
            
            stackView.AddArrangedSubview(this.CreateButton("Buttons", () => new ButtonsViewController()));
            stackView.AddArrangedSubview(this.CreateButton("Texts", () => new TextsViewController()));

            stackView.TranslatesAutoresizingMaskIntoConstraints = false;
            
            this.View.AddSubview(stackView);

            stackView.CenterXAnchor.ConstraintEqualTo(this.View.CenterXAnchor).Active = true;
            stackView.CenterYAnchor.ConstraintEqualTo(this.View.CenterYAnchor).Active = true;
        }

        private UIButton CreateButton(string title, Func<UIViewController> createViewController)
        {
            var button = UIButton.FromType(UIButtonType.System);
            button.SetTitle(title, UIControlState.Normal);
            button.TouchUpInside += (s, e) => this.NavigationController.PushViewController(createViewController(), true);
            return button;
        }
    }
}