using ArtificialFarm.FarmMap;
using UnityEngine;

namespace ArtificialFarm.Core
{
	/// <summary>
	/// Basic class for any object on the farm
	/// </summary>
	public abstract class FarmObject : IFarmObject
	{
		private static uint summaryIdIter = 100;

		public uint Id { get; }

		public Population Pop { get; }
		public WorldMap Map { get; }
		public ICell Cell { get; set; }
		public Turn Turn { get; }


		/// <summary>
		/// Get the color for a tile that matches the current cell
		/// </summary>
		/// <param name="mode">Current display mode</param>
		/// <returns>Unity color</returns>
		public abstract Color OnDisplay(DisplayMode mode);


		protected FarmObject()
		{
			Id = summaryIdIter++;
			Pop = FarmSettings.Current.Pop;
			Map = FarmSettings.Current.Map;
			Turn = new Turn(-1);
		}
	}
}