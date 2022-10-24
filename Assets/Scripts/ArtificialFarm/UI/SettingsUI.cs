using ArtificialFarm.Core;
using ArtificialFarm.FarmMap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArtificialFarm.UI
{
	public class SettingsUI : MonoBehaviour
	{
		[SerializeField] private TMP_InputField simulationIdField;
		[SerializeField] public Slider initialPopField;
		[SerializeField] private TMP_Dropdown mapSizeIndexField;
		[SerializeField] private Toggle loopByXField;
		[SerializeField] private Toggle loopByYField;
		[SerializeField] public TMP_InputField genomeLengthField;
		[SerializeField] public Slider mutationChanceField;
		[SerializeField] public Slider mutationsCountField;

		private Vector2Int[] _sizes =
		{
			new Vector2Int(20, 20),
			new Vector2Int(80, 60),
		};

		public void Apply()
		{
			FarmSettings.simulationId = simulationIdField.text;
			FarmSettings.initialPopulation = (int) initialPopField.value;
			FarmSettings.size = new Size(_sizes[mapSizeIndexField.value], loopByXField.isOn, loopByYField.isOn);
			FarmSettings.genomeLength = uint.Parse(genomeLengthField.text);
			FarmSettings.mutationChance = mutationChanceField.value / 100f;
			FarmSettings.mutationsCount = (int) mutationsCountField.value;
		}

		public void SetDisplayMode(int modeIndex)
		{
			FarmSettings.DisplayMode = (DisplayMode) modeIndex;
		}
	}
}