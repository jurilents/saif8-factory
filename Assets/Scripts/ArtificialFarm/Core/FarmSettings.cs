using System;
using ArtificialFarm.FarmMap;
using ArtificialFarm.UI;
using UnityEngine;

namespace ArtificialFarm.Core
{
	public static class FarmSettings
	{
		public static string simulationId;
		public static int initialPopulation;
		public static Size size;
		public static uint genomeLength;
		public static float mutationChance;
		public static int mutationsCount;


		public static uint SummaryPopulation { get; set; }

		public static Farm Current { get; internal set; }

		public static DisplayMode DisplayMode { get; set; } = Defaults.DisplayMode;

		internal static sbyte TileNeighborsCount
		{
			set => Turn.TileNeighborsCount = value;
		}


		private static WaitForSeconds waitingTime;

		public static WaitForSeconds StepWaitingTime
		{
			get => waitingTime ??= Defaults.WaitSeconds;
			set => waitingTime = value;
		}

		public static Type DefaultGenes { get; set; }
	}
}