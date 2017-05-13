namespace DoLess.Bindings
{
    internal class SingleItemTemplateSelector : IItemTemplateSelector
    {
        private readonly int resourceId;

        public SingleItemTemplateSelector(int resourceId)
        {
            this.resourceId = resourceId;
        }

        public int GetLayoutId(object item)
        {
            return this.resourceId;
        }
    }
}