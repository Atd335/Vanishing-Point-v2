using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GoInvisibleWithPlayer : MonoBehaviour
{

    public Color invisibleColor;
    public Color visibleColor;

    Image img;

	public void _Start()
    {
        
    }
    public void _Update()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (UpdateDriver.ud.GetComponent<CharacterController2D>().isOnScreen)
        {
            img.color = visibleColor;
        }
        else
        {
            img.color = invisibleColor;
        }
    }
}
