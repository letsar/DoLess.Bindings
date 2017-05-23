namespace DoLess.Bindings
{
    public interface IDataTemplateSelector<T>
    {
        int GetLayoutId(T item);
    }
}