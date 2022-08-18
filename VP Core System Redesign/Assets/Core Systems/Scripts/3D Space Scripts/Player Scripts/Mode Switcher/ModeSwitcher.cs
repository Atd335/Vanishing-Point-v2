using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModeSwitcher : MonoBehaviour
{
    public bool firstPerson;
    public float waitTime = .2f;
    public bool validSwitch;

    public bool forceFPSMode;
    public bool force2DMode;

    CharacterController2D cc2d;
    CharacterController3D cc3d;

    public UnityEvent onFPS;
    public UnityEvent on2D;

	public void _Start()
    {
        firstPerson = true;
        cc2d = UpdateDriver.ud.GetComponent<CharacterController2D>();
        cc3d = UpdateDriver.ud.GetComponent<CharacterController3D>();
         
        on2D = new UnityEvent();
        onFPS = new UnityEvent();
        on2D.AddListener(setRespawnPoint);
    }

    public void _Update()
    {
        if (force2DMode && forceFPSMode) 
        {
            print("2D MODE and FPS MODE cannot be forced simultaneously!");
            print("Defaulting to FORCE FPS MODE");

            force2DMode = false;
        }

        validSwitch = cc2d.isOnScreen && !cc2d.checkForColliderBetween();

        if (Input.GetButtonDown("SwitchMode") && validSwitch && !forceFPSMode && !force2DMode)
        {
            toggleMode();
        }
    }

    public void forceMode2D(bool b)
    {
        force2DMode = b;
    }
    public void forceMode3D(bool b)
    {
        forceFPSMode = b;
    }


    public void toggleMode()
    {
        firstPerson = !firstPerson;
        triggerEvents();
    }

    public void toggleMode(bool set)
    {
        firstPerson = set;
        triggerEvents();
    }

    public void triggerEvents()
    {
        if (firstPerson)
        {
            onFPS.Invoke();
        }
        else
        {
            on2D.Invoke();
        }
    }

    public void setRespawnPoint()
    {
        cc2d.respawnPosition = cc2d.worldPosition;
    }

    public void setCheckpoint(Vector3 v)
    {

        if(v == new Vector3(-1,-1,-1))
        {
            Physics.Raycast(cc3d.head.position, cc3d.head.forward, out RaycastHit hit, Mathf.Infinity, UpdateDriver.layerMask);
            cc2d.checkPointPosition = hit.point;
            return;
        }

        cc2d.checkPointPosition = v;

    }
    public void respawnAtCheckpoint()
    {
        cc2d.respawnAtCheckPoint();
    }
}
