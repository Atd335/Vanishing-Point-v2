using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventOnOverlapWithPlayer : MonoBehaviour
{   

    public UnityEvent onOverlap;
    CharacterController2D cc2d;

    private void Start()
    {
        cc2d = UpdateDriver.ud.GetComponent<CharacterController2D>();
    }

    void Update()
    {
        if (cc2d.colliderAtPlayerPosition().collider.gameObject==this.gameObject)
        {
            onOverlap.Invoke();
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


