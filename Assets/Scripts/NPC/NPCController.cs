using UnityEngine;
using UnityEngine.AI;

namespace GithubGameOff2022.NPC
{
    public class NPCController : MonoBehaviour
    {
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.SetDestination(Vector3.zero);
        }
    }
}
