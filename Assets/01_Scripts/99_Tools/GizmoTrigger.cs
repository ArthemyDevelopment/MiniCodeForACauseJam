#if UNITY_EDITOR
using UnityEngine;

public class GizmoTrigger : MonoBehaviour
{

    public Color GizmoColor;
    public bool useScale = false;
    public SphereCollider col;

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;

            Gizmos.matrix = transform.localToWorldMatrix;

        if(useScale)
            Gizmos.DrawSphere(Vector3.zero, transform.localScale.x);
        else
            Gizmos.DrawSphere(col.center, col.radius);
    }
}

#endif
