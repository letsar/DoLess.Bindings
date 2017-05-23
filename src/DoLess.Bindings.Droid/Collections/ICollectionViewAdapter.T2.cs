namespace DoLess.Bindings
{
    public interface ICollectionViewAdapter<TItem, TSubItem> :
        ICollectionViewAdapter<TItem>
        where TItem : class
        where TSubItem : class
    {
        IViewBinder<TSubItem> SubItemBinder { get; }
    }
}