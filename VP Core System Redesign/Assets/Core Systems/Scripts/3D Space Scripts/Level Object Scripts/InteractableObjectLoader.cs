using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectLoader : MonoBehaviour
{

    public Shader shader;

    public void _Start()
    {
        //for each object with the object properties component...
        foreach (ObjectProperties op in GameObject.FindObjectsOfType<ObjectProperties>())
        {
            GameObject g = op.gameObject;
            GameObject gg = new GameObject($"{g.name}_2D");//create a new object

            gg.layer = 6; //assign it to the 2d rendering layer

            gg.transform.SetParent(g.transform);//make it a child of its 3d counterpart
            gg.transform.localScale = Vector3.one;
            gg.transform.localPosition = Vector3.zero;
            gg.transform.localRotation = Quaternion.Euler(Vector3.zero);

            //duplicate its mesh
            var mf = gg.AddComponent<MeshFilter>();
            var mr = gg.AddComponent<MeshRenderer>();
            mf.mesh = g.GetComponent<MeshFilter>().sharedMesh;

            //set it to a flat material
            Material mat = new Material(shader);
            mat.color = op.color_2D;
            mr.material = mat;
        }

        print("2D object materials loaded...");
    }
}
