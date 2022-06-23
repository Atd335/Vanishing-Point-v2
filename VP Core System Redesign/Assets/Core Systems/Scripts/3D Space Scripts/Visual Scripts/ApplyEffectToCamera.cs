using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class ApplyEffectToCamera : MonoBehaviour
{
    public Material material;
    public static RenderTexture RT_OVERLAY;

    Vector2 screenDimensions;

    private void Start()
    {
        screenDimensions = new Vector2(Screen.width, Screen.height);
        RT_OVERLAY = new RenderTexture(Screen.width, Screen.height, 0);
        //GetComponent<Camera>().targetTexture = RT_OVERLAY;
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, RT_OVERLAY, material);
        Graphics.Blit(source, destination);

        UpdateDriver.ud.GetComponent<OverlayController>().raw.texture = RT_OVERLAY;
    }
}
