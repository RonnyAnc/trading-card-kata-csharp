using System;
using System.Collections.Generic;

namespace SharedKernel {
    public static class IListExtensions {
        public static IList<T> Shuffle<T>(this IList<T> list) {
            var shuffled = new List<T>();
            var random = new Random();
            while (list.Count > 0)
            {
                var randomIndex = random.Next(0, list.Count);
                shuffled.Add(list[randomIndex]);
                list.RemoveAt(randomIndex);
            }
            return shuffled;
        }
    }
}