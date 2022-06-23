using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class App_Calculator : MonoBehaviour, IContentLoader
{

    public TextMeshProUGUI outputText;
    public string numbersString = "";
    public string operatorsString = "";

    public void loadContent(Object obj)
    {
        foreach (OSButtonScript button in GetComponentsInChildren<OSButtonScript>())
        {
            button.calculator = this;
        }
    }

	public void _Start()
    {
        
    }
    public void _Update()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
