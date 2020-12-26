using ArtificialFarm.BotAI.Genetic;
using UnityEngine;

namespace ArtificialFarm.BotAI.DNAImplementations
{
    public abstract class TypeAlpha : IDNA
    {
        [Gene("movement", "mvf")]
        public static void MoveForward(Bot bot)
        {
            var newCell = bot.Map.Move(bot.Cell, bot.Turn);
            if (newCell.Content is null) bot.Cell = newCell;
            bot.Energy -= 10;
        }

        [Gene("movement", "mvb")]
        public static void MoveBackward(Bot bot)
        {
            bot.Turn.Inverse();
            var newCell = bot.Map.Move(bot.Cell, bot.Turn);
            if (newCell.Content is null) bot.Cell = newCell;
            bot.Turn.Inverse();
            bot.Energy -= 15;
        }

        [Gene("movement", "tnr")]
        public static void TurnRight(Bot bot)
        {
            bot.Turn.TurnRight();
            bot.Energy -= 5;
        }

        [Gene("movement", "tnl")]
        public static void TurnLeft(Bot bot)
        {
            bot.Turn.TurnLeft();
            bot.Energy -= 5;
        }


        [Gene("eating", "sun")]
        public static void Photosynthesis(Bot bot)
        {
            bot.Energy += 20;
        }

        [Gene("eating", "eat")]
        public static void EatNeighbor(Bot bot)
        {
            var forwardCell = bot.Map.Move(bot.Cell, bot.Turn);
            var neighbor = (Bot) forwardCell.Content;
            if (neighbor is null) return;

            byte deltaEaten = (byte) Mathf.Min(neighbor.Energy, 33);
            neighbor.Energy -= deltaEaten;
            bot.Energy += deltaEaten;
        }


        [Gene("reproduction", "div")]
        public static void DivisionReproduction(Bot bot)
        {
            if (bot.Energy < 25 || bot.Age < 10) return;

            bot.Pop.BirthChildFor(bot);
            bot.Energy -= 50;
        }
    }
}