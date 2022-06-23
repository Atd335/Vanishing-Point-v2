using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Narrative Objects/EchoDialogueObject_3D", order = 1)]
public class EchoDialogueObject_3D : ScriptableObject
{
    public TextAsset textToSpeak;
    public string appearanceEffect = "offset";
    public string behaviorEffect = "wiggle a=0";
    public float spd = .75f;
    public float volume = .3f;
    public float pitch = 1.25f;
}