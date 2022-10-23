using ArtificialFarm.FarmMap;
using UnityEngine;

namespace ArtificialFarm.UI
{
	public class SettingsUI : MonoBehaviour
	{
		[SerializeField] private Farm farm;

		[SerializeField] private string simulationId;
		[SerializeField] private int mapSizeIndex;

		private Vector2Int[] _sizes = new[]
		{
			new Vector2Int(16, 32),
			new Vector2Int(64, 180),
		};

		public void Start() { }
	}
}