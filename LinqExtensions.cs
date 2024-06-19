using System;
using System.Collections.Generic;
using System.Linq;

namespace Andromeda.Collections
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Enumerates generic <see cref="Array"/>
        /// </summary>
        public static IEnumerable<object> Enumerate(this Array array)
        {
            var en = array.GetEnumerator();

            while (en.MoveNext())
            {
                yield return en.Current;
            }
        }

        /// <summary>
        /// Groups elements of the collection into
        /// the groups of size <paramref name="groupSize"/>.
        /// Last group might be not full
        /// </summary>
        ///
        /// <typeparam name="T">Any type</typeparam>
        ///
        /// <param name="source">Source enumeration</param>
        ///
        /// <param name="groupSize">Size of the group</param>
        ///
        /// <returns>Enumeration of groups of elements</returns>
        public static IEnumerable<IEnumerable<T>> BreakIntoGroups<T>(
            this IEnumerable<T> source,
            int groupSize
        ) => source
            .Select((x, i) => new { Key = i / groupSize, Value = x })
            .GroupBy(x => x.Key, x => x.Value);

        /// <summary>
        /// If element exists in the collection - gives that element,
        /// if element doesnt exist - gives provided <paramref name="defaultValue"/>
        /// </summary>
        ///
        /// <typeparam name="T">Value type</typeparam>
        ///
        /// <param name="collection">Collection to look for element</param>
        ///
        /// <param name="index">Index of the element</param>
        ///
        /// <param name="defaultValue">Default value if the element is not found</param>
        ///
        /// <returns>
        /// <see cref="Nullable{T}"/> element or <paramref name="defaultValue"/>
        /// </returns>
        public static T? ElementAtOrDefault<T>(
            this IReadOnlyCollection<T> collection,
            int index,
            T? defaultValue = null
        ) where T : struct
            => index >= 0 && index < collection.Count
                ? collection.ElementAt(index)
                : defaultValue;
    }
}
