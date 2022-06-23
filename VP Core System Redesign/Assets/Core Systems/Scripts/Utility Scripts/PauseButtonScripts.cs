using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PauseButtonScripts : MonoBehaviour
{

    Image img;

    public Color defaultColor;
    public Color hoveredColor;
    public Color clickedColor;

    OSCanvasRaycaster oscr;
    public UnityEvent clickEvent;

    private void Start()
    {
        img = GetComponent<Image>();
        oscr = OSCanvasRaycaster.oscr;
        oscr.onClick.AddListener(onClick);
    }

    private void Update()
    {
        if (this.gameObject == oscr.hoveredOBJ) { img.color = Color.Lerp(img.color, hoveredColor, Time.deltaTime * 10f); }
        else { img.color = Color.Lerp(img.color, defaultColor, Time.deltaTime * 10f); }
    }

    void onClick()
    {
        if (oscr.hoveredOBJ != this.gameObject) { return; }
        img.color = clickedColor;
        clickEvent.Invoke();
    }

    public void returnToDesktop()
    {
        //use scenemanager to load Dektop
        SceneManagerScript.setIndex(UpdateDriver.ud.returnScene);
        FadeControllerScript.fade.onOpaque.AddListener(SceneManagerScript.loadSceneImmediate);
        FadeControllerScript.fade.fadeToOpaque();
    }

}
