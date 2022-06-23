using UnityEngine;

[CreateAssetMenu(fileName = "file", menuName = "OS Objects/File Icon", order = 1)]
public class FileIcon : ScriptableObject
{
    public string name = "file";
    public string extension = "";
    public OsFileType fileType;

    public Sprite icon;
    public bool isLocked = false;
    public bool limitToOneInstance = true;
    public GameObject window;
    public Object content;

    public Vector2 positionOverride = new Vector2(-1,-1);
    public Vector2 windowPosition = new Vector2(100,100);
    public Vector2 windowScale = new Vector2(500,500);

    private void OnValidate()
    {
        switch (fileType)
        {
            case OsFileType.text:
                extension = ".txt";
                break;
            case OsFileType.audio:
                extension = ".aud";
                break;
            case OsFileType.image:
                extension = ".img";
                break;
            case OsFileType.video:
                extension = ".vid";
                break;
            case OsFileType.Object:
                extension = ".3dv";
                break;
            case OsFileType.calculator:
                extension = ".exe";
                break;
            case OsFileType.email:
                extension = ".exe";
                break;
            case OsFileType.folder:
                extension = "";
                break;
            case OsFileType.internet:
                extension = ".exe";
                break;
            case OsFileType.paint:
                extension = ".exe";
                break;
            case OsFileType.game1:
                extension = ".exe";
                break;
            default:
                break;
        }
    }

}

public enum OsFileType
{
    text,
    audio,
    image, 
    video,
    Object,
    calculator,
    email,
    folder,
    internet,
    game1,
    paint
}