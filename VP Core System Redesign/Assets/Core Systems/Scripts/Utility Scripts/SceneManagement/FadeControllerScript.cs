using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeControllerScript : MonoBehaviour
{

    public static FadeControllerScript fade;

    public Image fadeIMG;

    public Color transparentColor = new Color(0,0,0,0);
    public Color opaqueColor = new Color(0,0,0,1);

    public UnityEvent onOpaque;
    public UnityEvent onTransparent;

    private void Awake()
    {
        fade = this;

        fadeIMG = GetComponent<Image>();
        fadeIMG.color = transparentColor;//color is trasnparent by default. must be set to opque in start function. 

        onOpaque = new UnityEvent();
        onTransparent = new UnityEvent();
    }

    public void setImageOpaque()
    {
        fadeIMG.color = opaqueColor;   
    }

    public void setImageTransparent()
    {
        fadeIMG.color = transparentColor;
    }

    public void setTransparentColor(Color color)
    {
        transparentColor = color;
    }

    public void setOpaqueColor(Color color)
    {
        opaqueColor = color;
    }

    public void fadeToOpaque(float speed = .1f)
    {
        StartCoroutine(colorFade(transparentColor,opaqueColor,speed));
    }

    public void fadeToTransparent(float speed = .1f)
    {
        StartCoroutine(colorFade(opaqueColor, transparentColor, speed));
    }

    IEnumerator colorFade(Color col1, Color col2, float spd)
    {
        
        float timer = 0;
        while (timer!=1)
        {
            timer += Time.deltaTime / spd;
            timer = Mathf.Clamp(timer,0,1);
            fadeIMG.color = Color.Lerp(col1,col2,timer);

            yield return new WaitForSeconds(Time.deltaTime);
        }

        if (col2 == transparentColor) { onTransparent.Invoke(); }
        else{ onOpaque.Invoke(); }
    }
}
