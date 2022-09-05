using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventOnOverlapWithPlayer : MonoBehaviour
{   

    public UnityEvent onOverlap;
    CharacterController2D cc2d;
    public bool fireOnce;
    private void Start()
    {
        cc2d = UpdateDriver.ud.GetComponent<CharacterController2D>();
    }

    void Update()
    {
        if (cc2d.colliderAtPlayerPosition().collider == null) { return; }

        if (cc2d.colliderAtPlayerPosition().collider.gameObject==this.gameObject)
        {
            onOverlap.Invoke();
            if (fireOnce) { Destroy(this); }
        }
    }

    public void DestroyThisScript()
    {
        Destroy(this);
    }

    public void testPrint(string str)
    {
        print(str);
    }

}


