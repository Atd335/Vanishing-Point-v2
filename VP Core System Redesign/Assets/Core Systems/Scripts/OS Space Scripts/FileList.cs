using UnityEngine;

[CreateAssetMenu(fileName = "fileList", menuName = "OS Objects/File List", order = 2)]
public class FileList : ScriptableObject
{
   public FileIcon[] files;
}
