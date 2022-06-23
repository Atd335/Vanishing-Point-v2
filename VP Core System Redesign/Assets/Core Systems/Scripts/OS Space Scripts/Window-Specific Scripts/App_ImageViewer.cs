using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class App_ImageViewer : MonoBehaviour, IContentLoader
{


    Texture img;
    public RawImage imgHolder;
    BaseWindowController bwc;
    public void loadContent(Object obj)
    {
        bwc = GetComponent<BaseWindowController>();

        img = (Texture)obj;
        imgHolder.texture = img;

        //bwc.RT.sizeDelta = new Vector2(img.width + 10,img.height + 55)/2;
    }

}
