using System;
using ArtificialFarm.BotAI.Genetic;
using ArtificialFarm.Core;
using UnityEngine;

namespace ArtificialFarm.BotAI.DNAImplementations
{
	public abstract class TypeAlpha : IDNA
	{
		[Gene("movement", "MOVE_FORWARD")]
		public static void MoveForward(IBot bot)
		{
			var newCell = bot.Map.GetCellByMove(bot.Cell, bot.Turn);
			newCell.AdoptNewBot(bot);

			bot.Energy -= 8;
		}


		// [Gene("movement", "MOVE_BACKWARD")]
		// public static void MoveBackward(IBot bot)
		// {
		//     var newCell = bot.Map.GetCellByMove(bot.Cell, bot.Turn.Inverted);
		//     newCell.AdoptNewBot(bot);
		//     bot.Energy -= 12;
		// }


		[Gene("movement", "TURN_RIGHT")]
		public static void TurnRight(IBot bot)
		{
			bot.Turn.TurnRight();
			bot.Energy -= 3;
		}


		[Gene("movement", "TURN_LEFT")]
		public static void TurnLeft(IBot bot)
		{
			bot.Turn.TurnLeft();
			bot.Energy -= 3;
		}


		[Gene("eating", "EATING")]
		public static void Eating(IBot bot)
		{
			switch (bot.Genome.Diet)
			{
				case Diet.PhotoPlant:
					bot.Energy += 20 * bot.Cell.SunModifier;
					break;

				case Diet.MineralsPlant:
					bot.Energy += 20 * bot.Cell.MineralsModifier;
					break;

				case Diet.HerbivAnimal:
				{
					var nbr = GetForwardNeighbor(bot);
					var nbrDiet = nbr?.Genome.Diet ?? Diet.None;

					if (nbrDiet is Diet.PhotoPlant or Diet.MineralsPlant)
					{
						float delta = nbr.Energy / 2;
						bot.Energy += delta;
						nbr.Energy -= delta;
					}

					break;
				}

				case Diet.PredatorAnimal:
				{
					var nbr = GetForwardNeighbor(bot);
					var nbrDiet = nbr?.Genome.Diet ?? Diet.None;

					if (nbrDiet is Diet.HerbivAnimal or Diet.PredatorAnimal)
					{
						float delta = Mathf.Max(nbr.Energy / 2, 50);
						bot.Energy += delta;
						nbr.Energy -= delta;
					}

					break;
				}

				/*
				case Diet.OmnivorAnimal:
				{
				    var nbr = GetForwardNeighbor(bot);
				    var nbrDiet = nbr?.Genome.Diet ?? Diet.None;

				    if (nbrDiet != Diet.None)
				    {
				        float delta = nbr.Energy / 3;
				        bot.Energy += delta;
				        nbr.Energy -= delta;
				    }

				    break;
				}
				*/

				case Diet.None:
					Debug.LogWarning("Diet is none for " + bot);
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			// Debug.Log($"{bot} => {bot.Genome.Diet}");
		}


		private static IBot GetForwardNeighbor(IFarmObject bot)
		{
			var forwardCell = bot.Map.GetCellByMove(bot.Cell, bot.Turn);
			return (IBot) forwardCell.Content;
		}


		[Gene("reproduction", "DIVISION_REPR")]
		public static void DivisionReproduction(IBot bot)
		{
			if (bot.Energy < 25 || bot.Age < 10) return;

			bot.Pop.BirthChildFor(bot);
			bot.Energy -= 25;
		}
	}
}