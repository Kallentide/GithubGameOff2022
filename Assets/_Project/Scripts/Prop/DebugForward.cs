using UnityEngine;

namespace GithubGameOff2022.Prop
{
    /// <summary>
    /// Display forward direction as Gizmo
    /// </summary>
    public class DebugForward : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        }
    }
}
