using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    internal static class IEnumerableExtensions
    {
        public static int InternalCount(this IEnumerable self)
        {
            if (self == null)
            {
                return 0;
            }

            var collection = self as ICollection;
            if (collection != null)
            {
                return collection.Count;
            }

            int count = 0;
            IEnumerator enumerator = self.GetEnumerator();
            while (enumerator.MoveNext())
            {
                count++;
            }
            return count;
        }

        public static System.Object InternalElementAt(this IEnumerable self, int position)
        {
            if (self == null)
            {
                return null;
            }

            var itemsList = self as IList;
            if (itemsList != null)
            {
                return itemsList[position];
            }

            var enumerator = self.GetEnumerator();
            for (var i = 0; i <= position; i++)
            {
                enumerator.MoveNext();
            }

            return enumerator.Current;
        }
    }
}
