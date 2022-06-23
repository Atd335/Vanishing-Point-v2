using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class OSCanvasRaycaster : MonoBehaviour
{
    public static OSCanvasRaycaster oscr;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    public GameObject hoveredOBJ;
    
    public UnityEvent onClick;
    public UnityEvent onHoveringElement;
    public UnityEvent onHold;
    public UnityEvent onRelease;

    public Transform appCanvas;

    void Awake()
    {
        oscr = this;

        m_Raycaster = GetComponent<GraphicRaycaster>();//Fetch the Raycaster from the GameObject (the Canvas)
        m_EventSystem = GetComponent<EventSystem>();//Fetch the Event System from the Scene
        onClick = new UnityEvent();
        onHold = new UnityEvent();
        onRelease = new UnityEvent();
        onHoveringElement = new UnityEvent();

        appCanvas = transform;
    }

    void Update()
    {

        m_PointerEventData = new PointerEventData(m_EventSystem);//Set up the new Pointer Event
        m_PointerEventData.position = Input.mousePosition;//Set the Pointer Event Position to that of the mouse position

        List<RaycastResult> results = new List<RaycastResult>();//Create a list of Raycast Results
        
        m_Raycaster.Raycast(m_PointerEventData, results); //Raycast using the Graphics Raycaster and mouse click position

        if (results.Count > 0)
        {
            hoveredOBJ = results[0].gameObject;//references only the topmost collider
        }
        else
        {
            hoveredOBJ = null;
        }

        if (Input.GetButtonDown("OS_CLICK"))
        {
            onClick.Invoke();
        }

        if (Input.GetButton("OS_CLICK"))
        {
            onHold.Invoke();
        }
        
        if (Input.GetButtonUp("OS_CLICK"))
        {
            onRelease.Invoke();
        }

        if (hoveredOBJ != null) { onHoveringElement.Invoke(); }
    }
}
