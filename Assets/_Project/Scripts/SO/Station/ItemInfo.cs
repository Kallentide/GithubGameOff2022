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

        public override bool Equals(object other)
        {
            if (other is ItemInfo item)
            {
                return item == this;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ItemName.GetHashCode();
        }

        public static bool operator==(ItemInfo a, ItemInfo b)
        {
            if (a is null) return b is null;
            if (b is null) return false;
            return a.ItemName == b.ItemName;
        }

        public static bool operator !=(ItemInfo a, ItemInfo b)
            => !(a == b);
    }
}
