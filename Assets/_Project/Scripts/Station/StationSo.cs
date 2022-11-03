using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameOff2022.Station
{
    [CreateAssetMenu(fileName = "Station",menuName = "ScriptableObjects/Station")]
    public class StationSo : ScriptableObject
    {

        [Tooltip("Crafting Duration")] [Space] public float craftingDuration;
        [Tooltip("Crafting Duration")] [Space] public string stationName;
        [Tooltip("Add input here")]
        [Space] public ItemSo inputSo;
        [Tooltip("Add output here")]
        [Space] public ItemSo outputSo;

    }
    
}
