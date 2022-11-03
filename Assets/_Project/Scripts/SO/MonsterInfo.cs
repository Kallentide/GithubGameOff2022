using UnityEngine;
using System.Collections.Generic;
using GithubGameOff2022.NPC;

namespace GithubGameOff2022.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/MonsterInfo", fileName = "MonsterInfo")]
    public class MonsterInfo : ScriptableObject
    {
        [Tooltip("Monster prefab")]
        public GameObject Prefab;

        [Tooltip("List of needs that will be fulfilled in the defined order")]
        public List<NPCController.Need> Needs;
    }
}