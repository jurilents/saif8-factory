using ArtificialFarm.BotAI;
using ArtificialFarm.Core;
using UnityEngine;
using Utilities;

namespace ArtificialFarm.FarmMap
{
    /// <summary>
    /// Single farm map cell class
    /// </summary>
    public class Cell : ICell
    {
        public Vector3Int Pos { get; }

        public CellContentType ContentType { get; private set; }

        public IFarmObject Content { get; private set; }

        public float SunModifier { get; }
        public float MineralsModifier { get; }


        public Cell(Vector3Int pos, Size? mapSize = null)
        {
            Pos = pos;
            ContentType = CellContentType.Void;
            Content = null;

            if (mapSize is null)
            {
                SunModifier = 1;
                MineralsModifier = 0;
            }
            else
            {
                var ms = mapSize.Value;
                SunModifier = ms.HF.ReLU(pos.y, 0.333f);
                MineralsModifier = ms.HF.InverseReLU(pos.y, 0.333f);
            }
        }


        /// <summary>
        /// Set cell content type and bot
        /// </summary>
        /// <param name="contentType">Cell content type enum value</param>
        /// <param name="farmObj">Instance of the bot (if you need)</param>
        public void SetContent(CellContentType contentType, IFarmObject farmObj = null)
        {
            ContentType = contentType;
            Content = farmObj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bot"></param>
        /// <returns></returns>
        public bool AdoptNewBot(IBot bot)
        {
            if (ContentType != CellContentType.Void) return false;

            bot.Cell.SetContent(CellContentType.Void);
            bot.Cell = this;
            SetContent(CellContentType.Organism, bot);
            return true;
        }


        public override string ToString() => $"Cell[x:{Pos.x}, y={Pos.y}] ({Reflection.GetTypeName(ContentType)})";
    }
}