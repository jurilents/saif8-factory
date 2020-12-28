using ArtificialFarm.BotAI.Genetic;
using ArtificialFarm.FarmMap;
using UnityEngine;

namespace ArtificialFarm.BotAI.DNAImplementations
{
    public abstract class TypeAlpha : IDNA
    {
        [Gene("movement", "MOVE_FORWARD")]
        public static void MoveForward(Bot bot)
        {
            var newCell = bot.Map.GetCellByMove(bot.Cell, bot.Turn);
            newCell.AdoptNewBot(bot);

            bot.Energy -= 8;
        }

        [Gene("movement", "MOVE_BACKWARD")]
        public static void MoveBackward(Bot bot)
        {
            var newCell = bot.Map.GetCellByMove(bot.Cell, bot.Turn.Inverted);
            newCell.AdoptNewBot(bot);
            bot.Energy -= 12;
        }

        [Gene("movement", "TURN_RIGHT")]
        public static void TurnRight(Bot bot)
        {
            bot.Turn.TurnRight();
            bot.Energy -= 3;
        }

        [Gene("movement", "TURN_LEFT")]
        public static void TurnLeft(Bot bot)
        {
            bot.Turn.TurnLeft();
            bot.Energy -= 3;
        }


        [Gene("eating", "PHOTOSYNTHESIS")]
        public static void Photosynthesis(Bot bot)
        {
            bot.Energy += 50;
        }

        [Gene("eating", "EAT_OTHER")]
        public static void EatNeighbor(Bot bot)
        {
            var forwardCell = bot.Map.GetCellByMove(bot.Cell, bot.Turn);
            var neighbor = (Bot) forwardCell.Content;
            if (neighbor is null) return;

            byte deltaEaten = (byte) Mathf.Min(neighbor.Energy, 33);
            neighbor.Energy -= deltaEaten;
            bot.Energy += deltaEaten;
        }


        [Gene("reproduction", "DIVISION_REPR")]
        public static void DivisionReproduction(Bot bot)
        {
            if (bot.Energy < 25 || bot.Age < 10) return;

            bot.Pop.BirthChildFor(bot);
            bot.Energy -= 25;
        }
    }
}