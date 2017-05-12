using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DoLess.Bindings
{
    internal interface INotifyItemChanged : INotifyDataSetChanged
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