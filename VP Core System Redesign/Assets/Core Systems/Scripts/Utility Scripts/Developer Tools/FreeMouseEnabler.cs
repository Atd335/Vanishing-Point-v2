using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMouseEnabler : MonoBehaviour
{
    public bool mouseLocked;

	public void _Start()
    {
        if (mouseLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void _Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            toggleMouse();
        }
    }
    public void toggleMouse(bool set)
    {
        mouseLocked = set;
        if (mouseLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void toggleMouse()
    {
        mouseLocked = !mouseLocked;
        if (mouseLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
