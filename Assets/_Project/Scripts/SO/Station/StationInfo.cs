using UnityEngine;

namespace GameOff2022.SO.Station
{
    [CreateAssetMenu(fileName = "Station", menuName = "ScriptableObjects/Station")]
    public class StationInfo : ScriptableObject
    {
        [Tooltip("Crafting Duration (in seconds)")] [Space] public float CraftingDuration;
        [Tooltip("Station name, make sure they are in the translation files!")][Space] public string StationName;
        [Tooltip("Add input here")]
        [Space] public ItemInfo InputSo;
        [Tooltip("Add output here")]
        [Space] public ItemInfo OutputSo;
    }
    
}
