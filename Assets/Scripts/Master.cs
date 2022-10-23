using ArtificialFarm.UI;
using UnityEngine;

public class Master : MonoBehaviour
{
	[SerializeField] private Farm farm;

	private void Start()
	{
		Screen.SetResolution(640, 480, false);

		farm.Init();
		farm.Play();
	}
}