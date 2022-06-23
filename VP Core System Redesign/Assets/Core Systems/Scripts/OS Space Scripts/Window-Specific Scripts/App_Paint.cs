using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class App_Paint : MonoBehaviour, IContentLoader
{

    OSCanvasRaycaster oscr;

    public Transform canvasMaskTransform;
    public Transform masterCanvas;

    public GameObject penTip;
    public GameObject penStroke;

    public float tipSize = 10;

    public Color tipColor = Color.black;

    Vector3 currentPos;
    Vector3 lastPos;

    bool highlighted;

    public void loadContent(Object obj)
    {
        oscr = OSCanvasRaycaster.oscr;
        oscr.onClick.AddListener(onClick);
        oscr.onHold.AddListener(onHold);
        oscr.onRelease.AddListener(onRelease);

        masterCanvas = oscr.appCanvas;
    }

    void onClick()
    {
        if (oscr.hoveredOBJ != canvasMaskTransform.gameObject) { highlighted = false; return; }
        currentPos = Input.mousePosition;
        lastPos = Input.mousePosition;
        highlighted = true;
    }

    void onHold()
    {
        if (!highlighted) { return; }

        currentPos = Input.mousePosition;

        GameObject pt = Instantiate(penTip,masterCanvas);
        GameObject ps = Instantiate(penStroke,masterCanvas);

        pt.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;
        ps.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;

        ps.transform.up = lastPos - currentPos;

        pt.GetComponent<Image>().color = tipColor;
        ps.GetComponent<Image>().color = tipColor;

        pt.GetComponent<RectTransform>().sizeDelta = Vector2.one * tipSize;
        ps.GetComponent<RectTransform>().sizeDelta = new Vector2(tipSize,Vector2.Distance(lastPos,currentPos));



        pt.transform.SetParent(canvasMaskTransform);
        ps.transform.SetParent(canvasMaskTransform);
        pt.transform.SetAsLastSibling();
        ps.transform.SetAsLastSibling();

        lastPos = Input.mousePosition;
    }

    void onRelease()
    {

        highlighted = false;

    }

}
