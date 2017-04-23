// Types de binding.

// Propriété <=> Propriété
// Propriété <=> Event (target) [Selected, SelectionChanged] [Command, Click]

// La source (vm) est toujours lié via une collection de propriété qui retournent une valeur. Func<TSource, TSourceProperty> (ex: vm => vm.FirstName + vm.LastName)
// La target (ct) est soit lié directement à une propriété, soit à un événement (et dans ce cas, c'est la target qui est à l'origine de la source de données)
// (ex : ct => ct.Text ou ct => ct.)



vm.Bind(control)
  .From(vm => vm.FirstName + vm.LastName)
  .With(ct => ct.Text)

Binder.Bind(vm, x => x.FirstName + x.LastName)
      .To(ct, x => x.Text);

vm.Bind(control)
  .TextTo(vm => vm.FirstName + vm.LastName)

vm.Bind(ct)
  .TargetProperty(ct => ct.Text)
  .SourceExpression(vm => vm.FirstName + vm.LastName) // Expression possible.

vm.Bind(ct)
  .TargetEvent(ct => ct.Click)
  .SourceCommand(vm => vm.Command) // Expression impossible.

Bind
Property
To
