namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a binding that can create another binding.
    /// </summary>
    /// <typeparam name="TSource">The binding's source.</typeparam>
    public partial interface ICanBind<TSource> :
        IBinding<TSource>
        where TSource : class
    {
        IBinding<TSource, TNewTarget> Bind<TNewTarget>(TNewTarget target)
            where TNewTarget : class;
    }
}
