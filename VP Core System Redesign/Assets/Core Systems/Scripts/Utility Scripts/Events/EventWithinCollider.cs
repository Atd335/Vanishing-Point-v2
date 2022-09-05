using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using cakeslice;

public class EventWithinCollider : MonoBehaviour
{
    public Transform transformToCheckFor;

    public bool destroyOnTrigger;
    public UnityEvent colliderEvent;

    bool triggered;

    private void Start()
    {
        if (transformToCheckFor == null) { transformToCheckFor = GameObject.FindGameObjectWithTag("Player 3D").transform; }
    }

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

    public void enableHighlightGroup(GameObject obj)
    {
        foreach (Outline item in obj.GetComponentsInChildren<Outline>())
        {
            item.enabled = true;
        }
    }

    public void disableHighlightGroup(GameObject obj)
    {
        foreach (Outline item in obj.GetComponentsInChildren<Outline>())
        {
            item.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0,1,0,.6f);
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().size);
    }
}
