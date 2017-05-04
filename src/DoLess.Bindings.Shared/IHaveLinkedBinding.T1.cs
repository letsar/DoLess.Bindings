namespace DoLess.Bindings
{
    internal interface IHaveLinkedBinding:
        IBinding
    {
        IBinding LinkedBinding { get; }

        long Id { get; }

        object Creator { get; }
    }
}
