using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(ObjectProperties)), CanEditMultipleObjects]

public class ObjectPropertiesInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ObjectProperties myScript = (ObjectProperties)target;

        if (GUILayout.Button("Set Custom 3D Color"))
        {
            foreach (ObjectProperties obj in targets)
            {
                obj.setColor3D(obj.customColor);
            }
        }

        if (GUILayout.Button("Set Custom 2D Color"))
        {
            foreach (ObjectProperties obj in targets)
            {
                obj.setColor2D(obj.customColor);
            }
        }


        if (GUILayout.Button("Make Platform"))
        {
            foreach (ObjectProperties obj in targets)
            {
                obj.setColor2D(ColorInfo.platformColor_2D);
                obj.setColor3D(ColorInfo.platformColor_3D);
            }
        }

        if (GUILayout.Button("Make Cutout"))
        {
            foreach (ObjectProperties obj in targets)
            {
                obj.setColor2D(ColorInfo.cutoutColor_2D);
                obj.setColor3D(ColorInfo.cutoutColor_3D);
            }
        }

        if (GUILayout.Button("Make Damage"))
        {
            foreach (ObjectProperties obj in targets)
            {
                obj.setColor2D(ColorInfo.damageColor_2D);
                obj.setColor3D(ColorInfo.damageColor_3D);
            }
        }
    }
}