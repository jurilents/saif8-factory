using UnityEngine;

namespace ArtificialFarm.FarmMap
{
    public class Turn
    {
        private static sbyte _nbrsCount;
        private static sbyte _halfNbrsCount;

        public sbyte Side { get; private set; }


        public static sbyte TileNeighborsCount
        {
            set
            {
                _nbrsCount = value;
                _halfNbrsCount = (sbyte) (value / 2);
            }
        }

        public Turn Inverted
        {
            get
            {
                TakeTurnBySteps(_halfNbrsCount);
                var newTurn = new Turn(Side);
                TakeTurnBySteps(_halfNbrsCount);
                return newTurn;
            }
        }


        public Turn(sbyte basic = -1)
        {
            Side = basic == -1
                ? (sbyte) Random.Range(0, _nbrsCount - 1)
                : basic;
        }

        public void TurnLeft() => TakeTurnBySteps(-1);

        public void TurnRight() => TakeTurnBySteps(1);


        private void TakeTurnBySteps(sbyte delta)
        {
            Side += delta;
            if (Side >= _nbrsCount) Side -= _nbrsCount;
            else if (Side < 0) Side += _nbrsCount;
        }
    }
}