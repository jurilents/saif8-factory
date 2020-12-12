using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class RandMe
    {
        public static float Rand() => Random.Range(0f, 1f);
        public static int RandIndex(ICollection list) => Random.Range(0, list.Count - 1);
        public static T RandItem<T>(IList<T> list) => list[Random.Range(0, list.Count - 1)];

        
        public static bool TryLuck(float chance) => Rand() <= chance;
        public static Color GenColor() => new Color(Rand(), Rand(), Rand());
    }
}