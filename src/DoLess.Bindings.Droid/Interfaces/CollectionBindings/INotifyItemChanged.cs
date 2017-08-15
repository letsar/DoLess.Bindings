namespace DoLess.Bindings
{
    internal partial interface INotifyItemChanged : INotifyDataChanged
    {                
        void NotifyItemChanged(int position, Java.Lang.Object payload);     
        
        void NotifyItemRangeChanged(int positionStart, int itemCount, Java.Lang.Object payload);       
    
    }
}