namespace DoLess.Bindings
{
    internal class SingleItemTemplateSelector<TItem> : IItemTemplateSelector<TItem>
    {
        private readonly int resourceId;

        public SingleItemTemplateSelector(int resourceId)
        {
            this.resourceId = resourceId;
        }

        public int GetLayoutId(TItem item)
        {
            return this.resourceId;
        }
    }
}