using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseWindowController : MonoBehaviour
{

    OSCanvasRaycaster oscr;
    
    public GameObject topCollider;
    public GameObject resizeCollider;
    public GameObject resizeGraphic;

    public RectTransform RT;

    public Vector2 minimumScale = new Vector2(200,200);
    public Vector3 lockedRatioSize;
    Vector3 relativePosition;
    Vector3 initSize;

    public bool resizable = false;
    public bool consistentRatio = false;
    bool TopHighlighted;
    bool ResizeHighlighted;



    // Start is called before the first frame update
    void Start()
    {
        RT = GetComponent<RectTransform>();
        oscr = OSCanvasRaycaster.oscr;
        oscr.onClick.AddListener(onClick);
        oscr.onHold.AddListener(onHold);

        lockedRatioSize = RT.sizeDelta;

        resizeCollider.SetActive(resizable);
        resizeGraphic.SetActive(resizable);
        setToTop();
    }

    void onClick()
    {
        if (topCollider == oscr.hoveredOBJ)
        {
            TopHighlighted = true;
            relativePosition = RT.anchoredPosition3D - Input.mousePosition;
            setToTop();
        }
        else
        {
            TopHighlighted = false;
        }

        if (resizeCollider == oscr.hoveredOBJ)
        {
            ResizeHighlighted = true;
            relativePosition = Input.mousePosition;
            initSize = RT.sizeDelta;
            setToTop();
        }
        else
        {
            ResizeHighlighted = false;
        }
    }

    void onHold()
    {
        if (TopHighlighted)
        {
            Vector3 v = Input.mousePosition + relativePosition;

            v.x = Mathf.Clamp(v.x,0,Screen.width - RT.sizeDelta.x);
           
            v.y = Mathf.Clamp(v.y,50,Screen.height);

            RT.anchoredPosition = v;
        }

        if (ResizeHighlighted)
        {
            if (!consistentRatio)
            {
                Vector3 v = (Input.mousePosition - new Vector3(relativePosition.x, relativePosition.y, 0));
                v.y *= -1;
                Vector3 vv = v + initSize;

                vv.x = Mathf.Clamp(vv.x, minimumScale.x, Mathf.Infinity);
                vv.y = Mathf.Clamp(vv.y, minimumScale.y, Mathf.Infinity);

                RT.sizeDelta = vv;
            }
            else
            {
                Vector3 v = (Input.mousePosition - new Vector3(relativePosition.x, relativePosition.y, 0));
                v.y *= -1;
                Vector3 vv = v + initSize;

                vv.x = Mathf.Clamp(vv.x, minimumScale.x, Mathf.Infinity);
                vv.y = Mathf.Clamp(vv.y, minimumScale.y, Mathf.Infinity);

                float scaleRatio = lockedRatioSize.x / lockedRatioSize.y;
                vv.x = vv.y * scaleRatio;

                RT.sizeDelta = vv;
            }
        }
    }

    public void setToTop()
    {
        transform.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
