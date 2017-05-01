namespace DoLess.Bindings
{
    internal interface IHaveLinkedBinding:
        IBinding
    {
        IBinding LinkedBinding { get; }
    }
}
