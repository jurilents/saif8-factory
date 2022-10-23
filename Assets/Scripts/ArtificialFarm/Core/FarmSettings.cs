using System;
using ArtificialFarm.FarmMap;
using ArtificialFarm.UI;
using UnityEngine;

namespace ArtificialFarm.Core
{
	public static class FarmSettings
	{
		public static uint SummaryPopulation { get; set; }

		public static Farm Current { get; internal set; }


		// TODO: plz, review it...
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