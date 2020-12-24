using ArtificialFarm.BotAI;
<<<<<<< HEAD
=======
using ArtificialFarm.BotIntelligences;
>>>>>>> parent of 7347170... v0.1.0
using UnityEngine;

namespace ArtificialFarm.FarmMap
{
    public class Cell
    {
        public Vector3Int Pos { get; }

<<<<<<< HEAD

        private CellContentType _content;
        private IBot _bot;
=======
        public IBot Bot { get; set; }
>>>>>>> parent of 7347170... v0.1.0


        public Cell(Vector3Int pos)
        {
            Pos = pos;
<<<<<<< HEAD
            _content = CellContentType.Void;
            _bot = null;
        }


        public void SetContent(CellContentType contentType, IBot bot = null)
        {
            _content = contentType;
            _bot = _content == CellContentType.Void ? null : bot;
=======
            Bot = null;
>>>>>>> parent of 7347170... v0.1.0
        }


        public Color GetDisplayColor(DisplayMode mode)
        {
<<<<<<< HEAD
            if (_bot is null) return Transparent();
=======
            if (Bot == null) return Transparent();
>>>>>>> parent of 7347170... v0.1.0

            switch (mode)
            {
                case DisplayMode.Default:
                    return Color.yellow;

                case DisplayMode.Energy:
<<<<<<< HEAD
                    if (!_bot.IsAlive) return Color.yellow;
                    return new Color((byte) (_bot.Energy * 2.55), 0, 128, 255);

                case DisplayMode.Diet:
                    break;
                case DisplayMode.Health:
                    break;
=======
                    if (!Bot.IsAlive) return Color.yellow;
                    return new Color((byte) (Bot.Energy * 2.55), 0, 128, 255);
>>>>>>> parent of 7347170... v0.1.0

                default:
                    return Color.red;
            }
        }

        private static Color Transparent() => new Color(0, 0, 255, 255);
    }
}