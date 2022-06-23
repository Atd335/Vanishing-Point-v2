using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class App_Email : MonoBehaviour, IContentLoader
{
    public TextAsset[] emails;
    
    public Scrollbar emailButtonScrollbar;
    public Scrollbar emailContentScrollbar;

    public GameObject emailButton;
    public Transform ButtonParent;

    Vector2 buttonScrollPos;
    public TextMeshProUGUI textBox;

    //email button is 180x80

    public void loadContent(Object obj)
    { 
        emails = ((EmailContainer)obj).emails;
        emailButtonScrollbar.size = 1f / emails.Length;

        for (int i = 0; i < emails.Length; i++)
        {
            GameObject button = Instantiate(emailButton,ButtonParent);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-80*i);
            //apply name
            button.GetComponentInChildren<TextMeshProUGUI>().text = emails[i].text.Split('~')[0];
        }
    }


    private void Update()
    {
        buttonScrollPos.y = Mathf.Lerp(0,emails.Length*80, emailButtonScrollbar.value);
        buttonScrollPos.y = Mathf.Clamp(buttonScrollPos.y,0, (emails.Length - 1) * 80);
        ButtonParent.GetComponent<RectTransform>().anchoredPosition = buttonScrollPos;
    }

}
