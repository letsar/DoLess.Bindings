namespace DoLess.Bindings
{
    internal partial interface INotifyItemChanged : INotifyDataChanged
    {
        void NotifyItemChanged(int position);        

        void NotifyItemInserted(int position);

        void NotifyItemMoved(int fromPosition, int toPosition);

        void NotifyItemRangeChanged(int positionStart, int itemCount);        

        void NotifyItemRangeInserted(int positionStart, int itemCount);

        void NotifyItemRangeRemoved(int positionStart, int itemCount);

        void NotifyItemRemoved(int position);
    }
}