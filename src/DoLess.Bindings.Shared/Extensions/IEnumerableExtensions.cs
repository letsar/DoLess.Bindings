using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    internal static class IEnumerableExtensions
    {
        public static int Count(this IEnumerable self)
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
    }
}
