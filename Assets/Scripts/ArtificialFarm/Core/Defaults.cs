using ArtificialFarm.FarmMap;
using UnityEngine;

namespace ArtificialFarm.Core
{
    internal static class Defaults
    {
        internal const uint GenomeLength = 8;
        internal const DisplayMode DisplayMode = FarmMap.DisplayMode.Default;
        internal static readonly WaitForSeconds WaitSeconds = new WaitForSeconds(0.0125f);


        /// <summary>
        /// To get default tile color (transparent => opacity is 0)
        /// </summary>
        /// <returns>Transparent color</returns>
        internal static Color Transparent => new Color(0, 0, 128, 0.1f);
    }
}