using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class ObjectProperties : MonoBehaviour
{
    
    public Color customColor;
    public Color color_2D = Color.white;
    public Color color_3D = Color.white;

    //the 2d and 3d colors are distinct to allow for different materials / colors underneath. I want to keep the 2d colors standard in order to make detection easier. they really should not be changed.

    public void setColor2D(Color col)
    {
        color_2D = col;
    }

    public void setColor3D(Color col)
    {
        color_3D = col;
        Material m = new Material(Shader.Find("Standard"));
        m.color = color_3D;
        GetComponent<Renderer>().material = m;
    }

    private void Reset()//assigns the colors to white when added
    {
        setColor2D(color_2D);
        setColor3D(color_3D);
    }

    public void enableHighlight(bool b)
    {
        GetComponent<Outline>().enabled = b;   
    }


    //private void OnDrawGizmos()//debugs view in editor
    //{
    //    if (!GameObject.FindObjectOfType<UpdateDriver>() || !GameObject.FindObjectOfType<UpdateDriver>().displayGizmos) { return; }
    //    Color c = color_2D;
    //    c.a = .5f;
    //    Gizmos.color = c;
    //    Gizmos.DrawWireMesh(GetComponent<MeshFilter>().sharedMesh,transform.position,transform.rotation,transform.lossyScale);
    //}
}
