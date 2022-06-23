using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App_Folder : MonoBehaviour, IContentLoader
{
    FileList files;
    public GameObject iconObj;
    public RectTransform contentArea;

    int fileCount;

    public void loadContent(Object obj)
    {
        files = (FileList)obj;
        foreach (FileIcon file in files.files)
        {
            GameObject f = Instantiate(iconObj,contentArea.transform);
            OSIconObject ff = f.GetComponent<OSIconObject>();
            ff.file = file;
            ff.updateIcon();
            fileCount++;
        }
    }
}
