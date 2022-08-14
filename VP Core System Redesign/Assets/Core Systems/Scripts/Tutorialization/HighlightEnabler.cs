using UnityEngine;
using cakeslice;

[CreateAssetMenu(fileName = "Data", menuName = "Tutorial Commands/HighlightEnabler", order = 1)]
public class HighlightEnabler : ScriptableObject, ITutorialCommand
{
    public bool enableObject = true;
    public string objectName;

    public void Toggle()
    {
        GameObject.Find(objectName).GetComponent<Outline>().enabled = enableObject;
    }

    public void RunCommand()
    {
        Toggle();
    }
}