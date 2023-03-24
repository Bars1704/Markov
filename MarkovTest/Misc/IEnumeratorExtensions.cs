using System;
using System.Collections.Generic;

namespace MarkovTest.Misc
{
    public static class IEnumeratorExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var x in collection)
                action.Invoke(x);
        }
    }
}