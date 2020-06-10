using UnityEngine;

namespace Part2
{
    public class Tracker : MonoBehaviour
    {

        [Required] public Transform tracked;

        public Vector3 offset = Vector3.zero;
        public float stiffness = 0.5f;

        void LateUpdate()
        {
            var targetPosition = tracked.position + offset;
            targetPosition.z = -10;
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, stiffness);
        }
    }
}