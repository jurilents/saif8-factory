using ArtificialFarm.BotAI;
using ArtificialFarm.Core;
using UnityEngine;

namespace ArtificialFarm.FarmMap
{
    /// <summary>
    /// Single farm map cell class
    /// </summary>
    public class Cell
    {
        public Vector3Int Pos { get; }

        public CellContentType ContentType { get; private set; }

        public FarmObject Content { get; private set; }


        public Cell(Vector3Int pos)
        {
            Pos = pos;
            ContentType = CellContentType.Void;
            Content = null;
        }


        /// <summary>
        /// Set cell content type and bot
        /// </summary>
        /// <param name="contentType">Cell content type enum value</param>
        /// <param name="farmObj">Instance of the bot (if you need)</param>
        public void SetContent(CellContentType contentType, FarmObject farmObj = null)
        {
            ContentType = contentType;
            Content = ContentType is CellContentType.Void ? null : farmObj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bot"></param>
        /// <returns></returns>
        public bool AdoptNewBot(Bot bot)
        {
            if (!(ContentType is CellContentType.Void)) return false;

            bot.Cell.SetContent(CellContentType.Void);
            bot.Cell = this;
            SetContent(CellContentType.Organism, bot);
            return true;
        }


        /// <summary>
        /// Get the color for a tile that matches the current cell
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public Color GetDisplayColor(DisplayMode mode)
        {
            if (Content is null) return Transparent();
            return Content.OnDisplay(mode);
        }

        /// <summary>
        /// To get default tile color (transparent => opacity is 0)
        /// </summary>
        /// <returns>Transparent color</returns>
        private static Color Transparent() => new Color(0, 0, 128, 0.25f);
    }
}