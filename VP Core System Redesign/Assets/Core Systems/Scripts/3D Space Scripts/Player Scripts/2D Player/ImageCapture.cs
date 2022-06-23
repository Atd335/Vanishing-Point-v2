using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCapture : MonoBehaviour
{   
    public Camera collisionCamera;
    public Texture2D texture;
    public RawImageCapture raw;

	public void _Start()
    {      
        collisionCamera = GameObject.FindGameObjectWithTag("Collision Camera").GetComponent<Camera>();
        print("Collision Camera Loaded...");
        raw = collisionCamera.gameObject.AddComponent<RawImageCapture>();
        raw.ic = this;
        raw._Start();
    }

    public void _Update()
    {
        
    }
}
