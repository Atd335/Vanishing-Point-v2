using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class App_Model : MonoBehaviour, IContentLoader
{
    BaseWindowController bwc;
    OSCanvasRaycaster oscr;

    public GameObject cameraViewer;
    GameObject model;

    public RawImage activeArea;

    bool highlighted;

    public RenderTexture RT;

    GameObject camViewerInst;
    Camera cam;
    public void loadContent(Object obj)
    {
        bwc = GetComponent<BaseWindowController>();

        oscr = OSCanvasRaycaster.oscr;
        oscr.onClick.AddListener(onClick);
        oscr.onHold.AddListener(onHold);
        oscr.onRelease.AddListener(onRelease);

        model = (GameObject)obj;

        //ensures that only one 3d model viewer is open at a time. 
        foreach (App_Model app in GameObject.FindObjectsOfType<App_Model>())
        {
            if (app != this)
            {
                Destroy(app.gameObject);
            }
        }

        Instantiate(model);
        camViewerInst = Instantiate(cameraViewer);
        cam = camViewerInst.GetComponentInChildren<Camera>();
        cam.targetTexture = RT;
    }


    void onClick()
    {
        if (oscr.hoveredOBJ == activeArea.gameObject)
        {
            highlighted = true;
        }
        else
        {
            highlighted = false;
        }
    }
    void onHold()
    {
        if (!highlighted) { return; }
        camControls();
    }

    void onRelease()
    {
        highlighted = false;
    }

    Vector3 camRot;
    float zPos;
    void camControls()
    {
        camRot += new Vector3(-Input.GetAxis("Mouse Y Model"), Input.GetAxis("Mouse X Model"), 0);
        camRot.x = Mathf.Clamp(camRot.x,0,90);
        camViewerInst.transform.rotation = Quaternion.Euler(camRot);

        
    }


    void Update()
    {
        //cam.aspect = activeArea.rectTransform.sizeDelta.x / activeArea.rectTransform.sizeDelta.y;
        if (oscr.hoveredOBJ == activeArea.gameObject)
        {
            zPos -= Input.GetAxis("Mouse ScrollWheel Model");
            zPos = Mathf.Clamp(zPos, -10, -2);
            cam.transform.localPosition = new Vector3(0, 0, zPos);
        }

        cam.aspect = (bwc.RT.sizeDelta.x-10)/((bwc.RT.sizeDelta.y - 55));
    }
}
