<<<<<<< HEAD
﻿using ArtificialFarm.UI;
=======
﻿using ArtificialFarm;
using ArtificialFarm.BotAI;
using ArtificialFarm.BotIntelligences;
>>>>>>> parent of 7347170... v0.1.0
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