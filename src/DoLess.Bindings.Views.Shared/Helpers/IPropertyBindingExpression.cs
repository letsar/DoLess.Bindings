namespace DoLess.Bindings
{
    internal interface IPropertyBindingExpression
    {
        string RawExpression { get; }

        string Name { get; }
    }
}
