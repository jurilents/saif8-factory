using ArtificialFarm.BotAI;
using UnityEngine;

namespace ArtificialFarm.FarmMap
{
    public class Cell
    {
        public Vector3Int Pos { get; }


        private CellContentType _content;
        private IBot _bot;


        public Cell(Vector3Int pos)
        {
            Pos = pos;
            _content = CellContentType.Void;
            _bot = null;
        }


        public void SetContent(CellContentType contentType, IBot bot = null)
        {
            _content = contentType;
            _bot = _content == CellContentType.Void ? null : bot;
        }


        public Color GetDisplayColor(DisplayMode mode)
        {
            if (_bot is null) return Transparent();

            switch (mode)
            {
                case DisplayMode.Default:
                    return Color.yellow;

                case DisplayMode.Energy:
                    if (!_bot.IsAlive) return Color.yellow;
                    return new Color((byte) (_bot.Energy * 2.55), 0, 128, 255);

                case DisplayMode.Diet:
                    break;
                case DisplayMode.Health:
                    break;

                default:
                    return Color.red;
            }
        }

        private static Color Transparent() => new Color(0, 0, 255, 255);
    }
}