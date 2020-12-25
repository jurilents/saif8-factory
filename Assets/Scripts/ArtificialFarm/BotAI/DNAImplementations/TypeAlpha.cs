using System.Linq;
using ArtificialFarm.BotAI.Genetic;
using UnityEngine;
using Utilities;

namespace ArtificialFarm.BotAI.DNAImplementations
{
    public static class TypeAlpha
    {
        [Gene("movement", "mvf")]
        static void MoveForward(Bot bot)
        {
            var newCell = bot.Map.Move(bot.Cell, bot.Turn);
            if (newCell.Content is null) bot.Cell = newCell;
            bot.Energy -= 10;
        }

        [Gene("movement", "mvb")]
        static void MoveBackward(Bot bot)
        {
            bot.Turn.Inverse();
            var newCell = bot.Map.Move(bot.Cell, bot.Turn);
            if (newCell.Content is null) bot.Cell = newCell;
            bot.Turn.Inverse();
            bot.Energy -= 15;
        }

        [Gene("movement", "tnr")]
        static void TurnRight(Bot bot)
        {
            bot.Turn.TurnRight();
            bot.Energy -= 5;
        }

        [Gene("movement", "tnl")]
        static void TurnLeft(Bot bot)
        {
            bot.Turn.TurnLeft();
            bot.Energy -= 5;
        }


        [Gene("eating", "sun")]
        static void Photosynthesis(Bot bot)
        {
            bot.Energy += 20;
        }

        [Gene("eating", "eat")]
        static void EatNeighbor(Bot bot)
        {
            var forwardCell = bot.Map.Move(bot.Cell, bot.Turn);
            var neighbor = (Bot) forwardCell.Content;
            if (neighbor is null) return;

            byte deltaEaten = (byte) Mathf.Min(neighbor.Energy, 33);
            neighbor.Energy -= deltaEaten;
            bot.Energy += deltaEaten;
        }


        [Gene("reproduction", "div")]
        static void DivisionReproduction(Bot bot)
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