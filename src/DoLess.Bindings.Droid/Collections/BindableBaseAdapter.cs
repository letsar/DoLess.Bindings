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
using Java.Lang;

namespace DoLess.Bindings
{
    internal class BindableBaseAdapter<TItem> :
        BaseAdapter<TItem>,
        IBindableAdapter<TItem>,
        IExpandableListAdapter,
        INotifyDataSetChanged
        where TItem : class
    {
        private readonly CollectionViewAdapter<TItem> collectionViewAdapter;

        public BindableBaseAdapter()
        {
            this.collectionViewAdapter = new CollectionViewAdapter<TItem>(this);
        }

        public ICollectionViewAdapter<TItem> CollectionViewAdapter => this.collectionViewAdapter;

        public override int Count => this.collectionViewAdapter.ItemCount;

        public int GroupCount => this.Count;

        public override TItem this[int position] => this.collectionViewAdapter[position];

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            return this.collectionViewAdapter.GetView(position, convertView, parent);
        }

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
            return this.collectionViewAdapter.GetChildrenCount(groupPosition);
        }

        public View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            // TODO.
            throw new NotImplementedException();
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
            // TODO.
            throw new NotImplementedException();
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