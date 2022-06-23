using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class App_Notepad : MonoBehaviour, IContentLoader
{

    string NotePadText;
    public TMP_InputField tmp;

    public void loadContent(Object obj)
    {
        NotePadText = ((TextAsset)obj).text;
        tmp = GetComponentInChildren<TMP_InputField>();
        tmp.text = NotePadText;
    }
}
