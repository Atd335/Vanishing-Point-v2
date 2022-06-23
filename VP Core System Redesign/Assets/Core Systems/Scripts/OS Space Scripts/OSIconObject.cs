using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OSIconObject : MonoBehaviour
{
    OSCanvasRaycaster oscr;

    RectTransform rt;

    public FileIcon file;
    public Image iconIMG;
    public Image highlightIMG;
    public TextMeshProUGUI tmp;
    public GameObject window;
    public GameObject lastWindowSpawned;
    public Object content;

    public bool highlighted;
    public bool onlySpawnOne;


    private void OnValidate()
    {
        if (file != null)
        {
            updateIcon();
        }
    }

    private void Start()
    {
        rt = GetComponent<RectTransform>();

        oscr = OSCanvasRaycaster.oscr;
        oscr.onClick.AddListener(onClick);
        oscr.onHold.AddListener(onHold);
    }

    void onClick()
    {
        if (oscr.hoveredOBJ == this.gameObject)
        {
            if (highlighted)
            {
                print($"Opening {this.gameObject.name}'s window");
                openWindow();
                return;
            }
            highlighted = true;
            highlightIMG.enabled = (highlighted);
        }
        else
        {
            highlighted = false;
            highlightIMG.enabled = (highlighted);
        }
    }

    void onHold()
    {
        //if (highlighted) { rt.anchoredPosition = Input.mousePosition; }
    }

    void openWindow()
    {
        if (lastWindowSpawned != null && onlySpawnOne) { return; }

        GameObject win = Instantiate(window,oscr.appCanvas);
        BaseWindowController bwc = win.GetComponent<BaseWindowController>();
        bwc.RT.anchoredPosition = file.windowPosition;
        bwc.RT.sizeDelta = file.windowScale;
        bwc.lockedRatioSize = rt.sizeDelta;
        lastWindowSpawned = win;
        win.GetComponent<IContentLoader>().loadContent(content);

        highlighted = false;
        highlightIMG.enabled = (highlighted);
    }

    public void updateIcon()
    {
        if (file != null)
        {
            tmp.text = file.name + file.extension;
            iconIMG.sprite = file.icon;
            content = file.content;
            this.gameObject.name = "ICON_" + tmp.text;
            GetComponent<RectTransform>().anchoredPosition = file.positionOverride;
            window = file.window;
            onlySpawnOne = file.limitToOneInstance;
        }
    }


}
