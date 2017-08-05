using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    internal static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            if (action != null)
            {
                foreach (var item in self)
                {
                    action(item);
                }
            }
        }
    }
}
