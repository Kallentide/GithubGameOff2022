using UnityEngine;

namespace GameOff2022.Station
{
    
    public enum ItemType
    {
        Beer,
        BeerMug,
        Tea,
        TeaCup
        
    }
    
    [CreateAssetMenu(fileName = "Item",menuName = "ScriptableObjects/Items/Item")]
    public class ItemSo : ScriptableObject
    {
        [Space] public ItemType itemType;
        [Space] public string itemName;
        [Space] public Sprite itemSprite;
        [Space] public GameObject item;
       
    }
}
