using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCapDebug : MonoBehaviour
{
    ImageCapture ic;

	public void _Start()
    {
        ic = UpdateDriver.ud.GetComponent<ImageCapture>();
    }
    public void _Update()
    {
        GameObject.FindGameObjectWithTag("Collision Debug").GetComponent<RawImage>().texture = ic.texture;
    }
}
