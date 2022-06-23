using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayController : MonoBehaviour
{

    ModeSwitcher switcher;
    public RawImage raw;
    public RawImage rawBG;

	public void _Start()
    {
        switcher = UpdateDriver.ud.GetComponent<ModeSwitcher>();
        raw = GameObject.FindGameObjectWithTag("OverlayTextures").GetComponent<RawImage>();
        rawBG = GameObject.FindGameObjectWithTag("OverlayTextureBG").GetComponent<RawImage>();
    }
    public void _Update()
    {
        if (switcher.firstPerson)
        {
            raw.color = Color.Lerp(raw.color,new Color(1,1,1,.4f), Time.deltaTime * 10);
            rawBG.color = Color.Lerp(rawBG.color,new Color(1,1,1,0), Time.deltaTime * 10);
        }
        else
        {
            raw.color = Color.Lerp(raw.color, new Color(1, 1, 1, 1), Time.deltaTime * 10);
            rawBG.color = Color.Lerp(rawBG.color, new Color(1, 1, 1, .8f), Time.deltaTime * 10);
        }
    }
}
