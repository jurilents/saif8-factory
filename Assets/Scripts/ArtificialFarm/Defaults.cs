using ArtificialFarm.FarmMap;
using UnityEngine;

namespace ArtificialFarm
{
    internal static class Defaults
    {
        internal const uint GenomeLength = 8;
        internal const DisplayMode DisplayMode = FarmMap.DisplayMode.Default;
        internal static readonly WaitForSeconds WaitSeconds = new WaitForSeconds(0.1f);
    }
}