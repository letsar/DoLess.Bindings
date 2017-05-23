using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;

namespace DoLess.Bindings
{
    internal class BindableBaseExpandableListAdapter<TItem, TSubItem> :
        BindableBaseAdapter<TItem>,
        ICollectionViewAdapter<TItem, TSubItem>,
        IExpandableListAdapter
        where TItem : class, IEnumerable<TSubItem>
        where TSubItem : class
    {        
        private readonly ViewBinder<TSubItem> subItemBinder;

        public BindableBaseExpandableListAdapter()
        {
            this.subItemBinder = new ViewBinder<TSubItem>();
        }

        public int GroupCount => this.Count;

        public IViewBinder<TSubItem> SubItemBinder => this.subItemBinder;

        public Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return null;
        }

        public long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public int GetChildrenCount(int groupPosition)
        {
            return this[groupPosition].Count();
        }

        public View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            return this.subItemBinder.GetView(this[groupPosition].ElementAt(childPosition), convertView, parent);
        }

        // Base implementation returns a long:
        // bit 0: Whether this ID points to a child (unset) or group (set), so for this method
        //        this bit will be 0.
        // bit 1-31: Lower 31 bits of the groupId
        // bit 32-63: Lower 32 bits of the childId.
        public long GetCombinedChildId(long groupId, long childId)
        {
            return (long)(0x8000000000000000UL | (ulong)((groupId & 0x7FFFFFFF) << 32) | (ulong)(childId & 0xFFFFFFFF));
        }

        // Base implementation returns a long (from BaseExpandableListAdapter.java):
        // bit 0: Whether this ID points to a child (unset) or group (set), so for this method
        //        this bit will be 1.
        // bit 1-31: Lower 31 bits of the groupId
        // bit 32-63: Lower 32 bits of the childId.
        public long GetCombinedGroupId(long groupId)
        {
            return (groupId & 0x7FFFFFFF) << 32;
        }

        public Java.Lang.Object GetGroup(int groupPosition)
        {
            return null;
        }

        public long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            return this.GetView(groupPosition, convertView, parent);
        }

        public bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }

        public void OnGroupCollapsed(int groupPosition)
        {
            // Do nothing.
        }

        public void OnGroupExpanded(int groupPosition)
        {
            // Do nothing.
        }
    }
}
