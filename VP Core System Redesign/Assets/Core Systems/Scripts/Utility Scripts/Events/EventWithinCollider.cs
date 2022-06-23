using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventWithinCollider : MonoBehaviour
{
    public Transform transformToCheckFor;

    public bool destroyOnTrigger;
    public UnityEvent colliderEvent;

    bool triggered;

    void Update()
    {
        if (IsInsideBoxCollider(GetComponent<BoxCollider>(), transformToCheckFor.position)&&!triggered)
        {
            colliderEvent.Invoke();
            if (destroyOnTrigger) { Destroy(this.gameObject); }
            triggered = true;
        }
        else
        {
            triggered = false;
        }
    }

    public static bool IsInsideBoxCollider(BoxCollider aCol, Vector3 aPoint)
    {
        var b = new Bounds(aCol.center, aCol.size * 0.5f);
        var p = aCol.transform.InverseTransformPoint(aPoint);
        return b.Contains(p);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0,1,0,.6f);
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().size);
    }
}
