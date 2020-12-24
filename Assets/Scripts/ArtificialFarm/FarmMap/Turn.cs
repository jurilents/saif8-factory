using UnityEngine;

namespace ArtificialFarm.FarmMap
{
    public struct Turn
    {
        public static sbyte TileNeighborsCount
        {
            set
            {
                _nbrsCount = value;
                _halfNbrsCount = (sbyte) (value / 2);
            }
        }

        private static sbyte _nbrsCount;
        private static sbyte _halfNbrsCount;
        
        public sbyte side { get; private set; }

        public Turn(sbyte basic = -1)
        {
            side = basic == -1 ? (sbyte) Random.Range(0, _nbrsCount - 1) : basic;
        }

        public void Inverse() => TakeTurnBySteps(_halfNbrsCount);

        public void TurnLeft() => TakeTurnBySteps(-1);

        public void TurnRight() => TakeTurnBySteps(1);

        private void TakeTurnBySteps(sbyte delta)
        {
            side += delta;
            if (side >= _nbrsCount) side -= _nbrsCount;
            else if (side < 0) side += _nbrsCount;
        }
    }
}