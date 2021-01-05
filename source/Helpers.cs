using System;
using System.Collections.Generic;
using System.Linq;

namespace Dennysoft.Core.JsonDoc
{
    /// <summary>
    /// This class contains some helper functions for the sake of ease.
    /// </summary>
    internal static class Helpers
    {
        /// <summary>
        /// Checks if the string has no meaningful value.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// Checks if the string has some meaningful value.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool HasValue(this string s)
        {
            return !s.IsNullOrWhiteSpace();
        }

        /// <summary>
        /// Checks if the given set is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="set"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> set)
        {
            return set == null || !set.Any();
        }

        /// <summary>
        /// Gets the first element where the predicate matches.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int FirstIndexOf<T>(this IEnumerable<T> array, Predicate<T> predicate)
        {
            var res = array.Select((s, i) => new {i, s}).Where(t => predicate.Invoke(t.s)).Select(t => t.i).ToList();

            if (res.IsNullOrEmpty()) return -1;
            else
            {
                return res[0];
            }
        }

        /// <summary>
        /// Checks if a value is in an array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool IsIn<T>(this T value, params T[] array)
        {
            return array.Contains(value);
        }
    }
}
