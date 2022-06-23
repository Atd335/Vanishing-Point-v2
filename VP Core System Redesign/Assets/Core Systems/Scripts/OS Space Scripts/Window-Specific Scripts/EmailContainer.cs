using UnityEngine;

[CreateAssetMenu(fileName = "emailList", menuName = "OS Objects/Email List", order = 3)]
public class EmailContainer : ScriptableObject
{
    public TextAsset[] emails;
}
