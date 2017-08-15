using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoLess.Bindings.Sample.ViewModels;
using Foundation;
using UIKit;

namespace DoLess.Bindings.Sample.iOS.Views
{
    [Register(nameof(ButtonsViewController))]
    class ButtonsViewController : BaseViewController<ButtonsViewModel>
    {
        private UIButton buttonCommand;
        private UIButton buttonCancelCommand;

        public ButtonsViewController()
        {

        }

        protected override void Load()
        {
            this.Title = "Buttons";

            var stackView = new UIStackView();
            stackView.Axis = UILayoutConstraintAxis.Vertical;
            stackView.Distribution = UIStackViewDistribution.EqualSpacing;
            stackView.Alignment = UIStackViewAlignment.Center;
            stackView.Spacing = 30;

            this.buttonCommand = CreateButton("Command");
            this.buttonCancelCommand = CreateButton("CancelCommand");

            stackView.AddArrangedSubview(buttonCommand);
            stackView.AddArrangedSubview(buttonCancelCommand);

            stackView.TranslatesAutoresizingMaskIntoConstraints = false;

            this.View.AddSubview(stackView);

            stackView.CenterXAnchor.ConstraintEqualTo(this.View.CenterXAnchor).Active = true;
            stackView.CenterYAnchor.ConstraintEqualTo(this.View.CenterYAnchor).Active = true;
        }

        protected override void Bind()
        {
            this.ViewModel(new ButtonsViewModel())
                
                .Bind(this.buttonCommand)
                .ClickTo(x => x.CancellableCommand)
                
                .Bind(this.buttonCancelCommand)
                .ClickTo(x => x.CancellableCommand.CancelCommand);
        }
    }
}