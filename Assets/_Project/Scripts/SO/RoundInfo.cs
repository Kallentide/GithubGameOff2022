using UnityEngine;

namespace GithubGameOff2022.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/RoundInfo", fileName = "RoundInfo")]
    public class RoundInfo : ScriptableObject
    {
        public float RoundDuration;
    }
}