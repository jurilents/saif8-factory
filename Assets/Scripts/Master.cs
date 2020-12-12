using ArtificialFarm;
using ArtificialFarm.BotAI;
using ArtificialFarm.BotIntelligences;
using UnityEngine;

public class Master : MonoBehaviour
{
    [SerializeField] private Farm _farm;
    
    private void Start()
    {
        _farm.Init();
        _farm.Play();
    }
}