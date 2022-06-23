using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawImageCapture : MonoBehaviour
{
    public ImageCapture ic;

    public Vector2Int capturePos;
    public Vector2Int captureDimensions = new Vector2Int(150,150);


    Material renderMat;

    public void _Start()
    {
        renderMat = new Material(Shader.Find("Unlit/Color"));
        print($"RawImageCapture loaded onto Gameobject: '{this.gameObject.name}...'");
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest);
        grabImgData(src, capturePos, captureDimensions);
    }

    void grabImgData(Vector2Int position, Vector2Int scale)
    {
        Texture2D _tex = new Texture2D(scale.x, scale.y, TextureFormat.RGB24, false);

        Vector2Int pos = position;

        pos.x = Mathf.Clamp(pos.x, 0, Screen.width - scale.x);
        pos.y = Mathf.Clamp(pos.y, 0, Screen.height - scale.y);

        Rect rect = new Rect(pos.x, pos.y, scale.x, scale.y);

        _tex.ReadPixels(rect, 0, 0);
        _tex.Apply();

        ic.texture = _tex;
    }

    void grabImgData(RenderTexture src, Vector2Int position, Vector2Int scale)
    {
        Texture2D _tex = new Texture2D(scale.x, scale.y, TextureFormat.RGB24, false);

        Vector2Int pos = position;

        pos.x = Mathf.Clamp(pos.x, 0, Screen.width - scale.x);
        pos.y = Mathf.Clamp(pos.y, 0, Screen.height - scale.y);

        Rect rect = new Rect(pos.x, pos.y, scale.x, scale.y);

        _tex.ReadPixels(rect, 0, 0);
        _tex.Apply();

        ic.texture = _tex;
    }
}
