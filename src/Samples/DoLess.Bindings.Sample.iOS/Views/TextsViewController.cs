using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoLess.Bindings.Sample.ViewModels;
using Foundation;
using UIKit;

namespace DoLess.Bindings.Sample.iOS.Views
{
    [Register(nameof(TextsViewController))]
    class TextsViewController : BaseViewController<TextsViewModel>
    {
        private UILabel label;
        private UITextField textField;

        public TextsViewController()
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

            this.label = new UILabel();
            this.textField = new UITextField();

            stackView.AddArrangedSubview(textField);
            stackView.AddArrangedSubview(label);

            stackView.TranslatesAutoresizingMaskIntoConstraints = false;

            this.View.AddSubview(stackView);

            stackView.CenterXAnchor.ConstraintEqualTo(this.View.CenterXAnchor).Active = true;
            stackView.CenterYAnchor.ConstraintEqualTo(this.View.CenterYAnchor).Active = true;
        }

        protected override void Bind()
        {
            var vm = new TextsViewModel();
            vm.Person = new PersonViewModel("Dark", "Vador");

            this.ViewModel(vm)
                
                .Bind(this.label)
                .Property(x => x.Text)
                .To(x => $"{x.Person.FirstName} {x.Person.LastName}")
                
                .Bind(this.textField)
                .Property(x => x.Text)
                .To(x => x.Person.FirstName, BindingMode.TwoWay);
        }
    }
}