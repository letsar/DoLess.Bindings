namespace DoLess.Bindings
{
    internal partial interface INotifyItemChanged : INotifyDataChanged
    {        
        void NotifyItemChanged(int position);
        
        void NotifyItemChanged(int position, Java.Lang.Object payload);
        
        void NotifyItemInserted(int position);
        
        void NotifyItemMoved(int fromPosition, int toPosition);
        
        void NotifyItemRangeChanged(int positionStart, int itemCount);
        
        void NotifyItemRangeChanged(int positionStart, int itemCount, Java.Lang.Object payload);
        
        void NotifyItemRangeInserted(int positionStart, int itemCount);
        
        void NotifyItemRangeRemoved(int positionStart, int itemCount);
        
        void NotifyItemRemoved(int position);
    }
}