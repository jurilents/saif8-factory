using ArtificialFarm.Core;
using ArtificialFarm.FarmMap;
using UnityEngine;
using UnityEngine.UI;

namespace ArtificialFarm.UI
{
	public class FarmUI : MonoBehaviour
	{
		[Header("Start params")] public uint startPopulation = 20;

		[Header("Farm map")] public DisplayMode displayMode;

		[SerializeField] private Farm farm;
		[SerializeField] private SettingsUI settingsObject;
		[SerializeField] private Canvas settingsCanvas;
		[SerializeField] private Slider speedSlider;
		[SerializeField] private Text stepField;
		[SerializeField] private Text populationField;

		// public RectTransform graphicContainer;

		public void UpdateStepField(ulong step) => stepField.text = $"{step}";
		public void UpdatePopField(int population) => populationField.text = $"{population}";

		public void Run()
		{
			settingsObject.Apply();
			settingsCanvas.gameObject.SetActive(false);

			farm.Init();
			farm.Play();
		}

		private float[] stepDeltas =
		{
			0,
			0.1f,
			0.03f,
			0.0125f,
			0.005f,
			0.0025f,
		};

		public void OnSpeedChange()
		{
			int speed = (int) speedSlider.value;
			if (speed == 0)
			{
				farm.Pause();
			}
			else
			{
				farm.Play();
				FarmSettings.StepWaitingTime = new WaitForSeconds(stepDeltas[speed]);
			}
		}

		public void Pause() { }

		public void Stop()
		{
			settingsCanvas.gameObject.SetActive(true);
		}
	}
}