using ArtificialFarm.BotAI;
using ArtificialFarm.BotIntelligences;
using UnityEngine;

namespace ArtificialFarm.FarmMap
{
    public class Cell
    {
        public Vector3Int Pos { get; }

        public IBot Bot { get; set; }


        public Cell(Vector3Int pos)
        {
            Pos = pos;
            Bot = null;
        }


        public Color GetDisplayColor(DisplayMode mode)
        {
            if (Bot == null) return Transparent();

            switch (mode)
            {
                case DisplayMode.Default:
                    return Color.yellow;

                case DisplayMode.Energy:
                    if (!Bot.IsAlive) return Color.yellow;
                    return new Color((byte) (Bot.Energy * 2.55), 0, 128, 255);

                default:
                    return Color.red;
            }
        }

        private static Color Transparent() => new Color(0, 0, 255, 255);
    }
}