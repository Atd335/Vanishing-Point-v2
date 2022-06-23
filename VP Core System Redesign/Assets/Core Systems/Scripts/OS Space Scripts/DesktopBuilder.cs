using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopBuilder : MonoBehaviour
{
    public GameObject iconPrefab;
    [HideInInspector]
    public Transform iconCanvas;

    public FileList iconList;
    int iconCount = 0;

    private void Start()
    {
        PlaceIcons();
    }

    void PlaceIcons()
    {
        iconCanvas = GameObject.FindGameObjectWithTag("Icon Canvas").transform;

        foreach (FileIcon icon in iconList.files)
        {
            placeIndividualIcon(icon);
        }
    }

    void placeIndividualIcon(FileIcon icon)
    {

        GameObject i = Instantiate(iconPrefab,iconCanvas);
        OSIconObject oi = i.GetComponent<OSIconObject>();
        oi.file = icon;
        oi.updateIcon();
        //spawn it
        iconCount++;
    }

    void placeOpenExecutables()
    { 
        
    }

	public void _Start()
    {
        
    }
    public void _Update()
    {
        
    }
}
