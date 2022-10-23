using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    public static class RandMe
    {
        public static void SetRandomSeed(string seed) => Random.InitState(seed.GetHashCode());


        /// <summary>
        /// Generate random number between 0 -> 1
        /// </summary>
        public static float Rand() => Random.Range(0f, 1f);

        /// <summary>
        /// Generate random index to collection
        /// </summary>
        /// <param name="list">Countable collection</param>
        public static int RandIndex(this ICollection list) => Random.Range(0, list.Count - 1);


        /// <summary>
        /// Randomly picks one elements from the enumerable
        /// </summary>
        /// <typeparam name="T">The type of the item</typeparam>
        /// <param name="items">The items to random from</param>
        /// <returns> random item from collection</returns>
        public static T RandomItem<T>(this IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentException("Cannot randomly pick an item from the list, the list is null!");
            
            var enumerable = items as T[] ?? items.ToArray();
            
            if (!enumerable.Any())
                throw new ArgumentException(
                    "Cannot randomly pick an item from the list, there are no items in the list!");
            
            return enumerable.ElementAt(Random.Range(0, enumerable.Length));
        }

        /// <summary>
        /// Borrowed from http://stackoverflow.com/questions/56692/random-weighted-choice
        /// Randomly picks one element from the enumerable, taking into account a weight
        /// </summary>
        public static T WeightedRandomItem<T>(this IEnumerable<T> sequence, Func<T, float> weightSelector)
        {
            var enumerable = sequence as T[] ?? sequence.ToArray();
            float totalWeight = enumerable.Sum(weightSelector);
            // The weight we are after...
            float itemWeightIndex = Random.value * totalWeight;
            float currentWeightIndex = 0;

            foreach (var item in from witem in enumerable
                select new {Value = witem, Weight = weightSelector(witem)})
            {
                currentWeightIndex += item.Weight;

                // If we've hit or passed the weight we are after for this item then it's the one we want....
                if (currentWeightIndex >= itemWeightIndex)
                    return item.Value;
            }

            return default;
        }

        /// <summary>
        /// Generate random boolean with selected chance
        /// where 1 is 100% true and 0 is 100% false
        /// </summary>
        /// <param name="chance">Value between 0 -> 1</param>
        public static bool TryLuck(float chance) => Rand() <= chance;

        /// <summary>
        /// Generate full random color
        /// </summary>
        public static Color RandColor() => new Color(Rand(), Rand(), Rand());

        /// <summary>
        /// Generate random normalized direction
        /// </summary>
        public static Vector3 RandDirection() =>
            new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}