using UnityEngine;

public class Tracker : MonoBehaviour
{

    [Required]
    public Transform tracked;

    public float stiffness = 0.5f;
    public Vector3 offset = Vector3.zero;
    
    void LateUpdate() {
        var targetPosition = tracked.position;
        targetPosition.z = -10;
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition + offset, stiffness);
    }
}
