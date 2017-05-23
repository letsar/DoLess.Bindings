namespace DoLess.Bindings
{
    internal class SingleDataTemplateSelector<T> : IDataTemplateSelector<T>
    {
        private readonly int resourceId;

        public SingleDataTemplateSelector(int resourceId)
        {
            this.resourceId = resourceId;
        }

        public int GetLayoutId(T item)
        {
            return this.resourceId;
        }
    }
}