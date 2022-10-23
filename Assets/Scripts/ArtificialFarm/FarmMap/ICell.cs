using ArtificialFarm.BotAI;
using ArtificialFarm.Core;
using UnityEngine;

namespace ArtificialFarm.FarmMap
{
    public interface ICell
    {
        Vector3Int Pos { get; }
        CellContentType ContentType { get; }
        IFarmObject Content { get; }

        void SetContent(CellContentType contentType, IFarmObject farmObj = null);

        bool AdoptNewBot(IBot bot);

        float SunModifier { get; }
        float MineralsModifier { get; }
    }
}