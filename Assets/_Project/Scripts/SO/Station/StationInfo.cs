using UnityEngine;

namespace GameOff2022.SO.Station
{
    [CreateAssetMenu(fileName = "Station", menuName = "ScriptableObject/Station")]
    public class StationInfo : ScriptableObject
    {
        [Tooltip("Crafting Duration (in seconds)")] [Space] public float CraftingDuration;
        [Tooltip("Text displayed when you can use the station")][Space] public string CommandAvailable;
        [Tooltip("Add input here")]
        [Space] public ItemInfo InputSo;
        [Tooltip("Add output here")]
        [Space] public ItemInfo OutputSo;
    }
    
}
