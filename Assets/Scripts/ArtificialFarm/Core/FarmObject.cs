using ArtificialFarm.FarmMap;

namespace ArtificialFarm.Core
{
    /// <summary>
    /// Basic class for any object on the farm
    /// </summary>
    public abstract class FarmObject
    {
        public uint Id { get; }

        protected internal Population Pop { get; }
        protected internal WorldMap Map { get; }
        protected internal Cell Cell { get; set; }
        protected internal Turn Turn { get; }


        public FarmObject()
        {
            Id = FarmSettings.SummaryPopulation++;
            Pop = FarmSettings.Current.Pop;
            Map = FarmSettings.Current.Map;
            Turn = new Turn(-1);
        }
    }
}