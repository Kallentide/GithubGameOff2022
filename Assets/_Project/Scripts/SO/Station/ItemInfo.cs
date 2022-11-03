using UnityEngine;

namespace GameOff2022.SO.Station
{
    public enum ItemType
    {
        Alcohol,
        Glass
    }
    
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items/Item")]
    public class ItemInfo : ScriptableObject
    {
        [Space] public ItemType ItemType;
        [Space] public string ItemName;
        [Space] public Sprite ItemSprite;
        [Space] public GameObject Item;
       
    }
}
