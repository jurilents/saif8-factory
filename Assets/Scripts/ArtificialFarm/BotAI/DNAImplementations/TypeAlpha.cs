using ArtificialFarm.BotAI.Genetic;
using UnityEngine;
using Utilities;

namespace ArtificialFarm.BotAI.DNAImplementations
{
    public sealed class TypeAlpha : Bot
    {
        [Gene("type-alpha", "mvf")]
        public static void MoveForward(Bot bot)
        {
            var newCell = bot.Map.Move(bot.Cell, bot.Turn);
            if (newCell.Bot is null) bot.Cell = newCell;
            bot.Energy -= 10;
        }

        [Gene("type-alpha", "mvb")]
        public static void MoveBackward(Bot bot)
        {
            bot.Turn.Inverse();
            var newCell = bot.Map.Move(bot.Cell, bot.Turn);
            if (newCell.Bot is null) bot.Cell = newCell;
            bot.Turn.Inverse();
            bot.Energy -= 15;
        }

        [Gene("type-alpha", "tnr")]
        public static void TurnRight(Bot bot)
        {
            bot.Turn.TurnRight();
            bot.Energy -= 5;
        }

        [Gene("type-alpha", "tnl")]
        public static void TurnLeft(Bot bot)
        {
            bot.Turn.TurnLeft();
            bot.Energy -= 5;
        }


        [Gene("type-alpha", "sun")]
        public static void Photosynthesis(Bot bot)
        {
            bot.Energy += 20;
        }

        [Gene("type-alpha", "eat")]
        public static void EatNeighbor(Bot bot)
        {
            var forwardCell = bot.Map.Move(bot.Cell, bot.Turn);
            var neighbor = (TypeAlpha) forwardCell.Bot;
            if (neighbor is null) return;

            byte deltaEaten = (byte) Mathf.Min(neighbor.Energy, 33);
            neighbor.Energy -= deltaEaten;
            bot.Energy += deltaEaten;
        }


        [Gene("type-alpha", "div")]
        public static void DivisionReproduction(Bot bot)
        {
            if (bot.Energy < 25 || bot.Age < 10) return;

            var emptyNeighboured = bot.Map.GetNeighbors(bot.Cell.Pos)
                .Select(n => bot.Map.GetCell(n)).Where(c => c.Bot is null).ToList();
            if (emptyNeighboured.Count == 0) return;
            var nbrCell = RandMe.RandItem(emptyNeighboured);

            var child = new TypeAlpha(bot);
            nbrCell.Bot = child;
            bot.Pop.Birth(child);
            bot.Energy -= 50;
        }
    }
}