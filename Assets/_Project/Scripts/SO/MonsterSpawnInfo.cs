using UnityEngine;

namespace GithubGameOff2022.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/MonsterSpawnInfo", fileName = "MonsterSpawnInfo")]
    public class MonsterSpawnInfo : ScriptableObject
    {
        [Tooltip("Interval between 2 monster spawns")]
        public int SpawnInterval;

        [Tooltip("Possible monsters that can spawn")]
        public GameObject[] PossibleSpawns;
    }
}