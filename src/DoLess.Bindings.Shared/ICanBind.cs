namespace DoLess.Bindings
{
    public partial interface ICanBind<TSource> : IBinding<TSource>
        where TSource : class
    {
        IBinding<TSource, TNewTarget> Bind<TNewTarget>(TNewTarget target)
            where TNewTarget : class;
    }
}
