using System;
using UnityEngine;

namespace GameOff2022.Station
{

    public enum ResourceType
    {
        BeerGlass,
        BloodGlass
    }
    
    [CreateAssetMenu(menuName = "ScriptableObjects/Resource/Resource",fileName = "Resource")]
    public class ResourceSo: ScriptableObject
    {

        public ResourceInfo resourceInfo;

    }


    [Serializable]
    public class ResourceInfo
    {
        public ResourceType beerGlass;
        public int amount;

    }
    
    
}
