using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Febucci.UI;
using Febucci.UI.Examples;

public class SpeechScript : MonoBehaviour
{

    CharacterController2D cc2d;

    bool showingBox;
    Vector2 boxSize;
    Vector2 boxPosition;

    public AudioSource AS;
    public AudioClip[] phones;

    Image txtBox;
    TextMeshProUGUI tmp;
    TextAnimator txtAnim;
    TextAnimatorPlayer txtAnimPlr;

    public void _Start()
    {
        cc2d = UpdateDriver.ud.GetComponent<CharacterController2D>();

        AS = this.gameObject.AddComponent<AudioSource>() as AudioSource;
        phones = UpdateDriver.ud.phones;

        txtBox = GameObject.FindGameObjectWithTag("EchoTextBox").GetComponent<Image>();
        boxSize = txtBox.rectTransform.sizeDelta;
        txtBox.rectTransform.sizeDelta = new Vector2(boxSize.x/2, 0);

        var g = GameObject.FindGameObjectWithTag("EchoText");
        tmp = g.GetComponent<TextMeshProUGUI>();
        txtAnim = g.GetComponent<TextAnimator>();
        txtAnimPlr = g.GetComponent<TextAnimatorPlayer>();

        txtAnimPlr.onCharacterVisible.AddListener(playSound);// the playsound method will play every time a character is added. 
        txtAnimPlr.onTextShowed.AddListener(disableText);

        //TEST
        //Speak(UpdateDriver.ud.dialogueObjs[0]);
    }

    public void _Update()
    {
        if (showingBox){txtBox.rectTransform.sizeDelta = Vector2.Lerp(txtBox.rectTransform.sizeDelta, boxSize, Time.deltaTime * 15f);}
        else{txtBox.rectTransform.sizeDelta = Vector2.Lerp(txtBox.rectTransform.sizeDelta, new Vector2(boxSize.x/2, 0), Time.deltaTime * 15f);}

        Vector2 v = cc2d.playerRectTransform.anchoredPosition;
        
        bool b = !cc2d.isOnScreen || (v.x < 0) || (v.x > Screen.width) || (v.y < 0) || (v.y > Screen.height);

        if (!b)
        {
            boxPosition = cc2d.playerRectTransform.anchoredPosition+new Vector2(0,60);
            boxPosition.x = Mathf.Clamp(boxPosition.x,boxSize.x/2,Screen.width-(boxSize.x/2));
            boxPosition.y = Mathf.Clamp(boxPosition.y,0,Screen.height-(boxSize.y));
            txtBox.rectTransform.anchoredPosition = boxPosition;
            
        }
        else
        {
            boxPosition = new Vector2((boxSize.x / 2) + 5, 0 + 5);
            txtBox.rectTransform.anchoredPosition = Vector2.Lerp(txtBox.rectTransform.anchoredPosition, boxPosition, Time.deltaTime * 10f);
        }
    }

    void playSound(char c)
    {
        int id = (int)$"{c}".ToUpper()[0];
        id -= 65;
        try {AS.PlayOneShot(phones[id]);}
        catch (System.Exception){}
    }

    public void Speak(string text, string app="offset", string eff= "wiggle a=0", float spd = .75f, float volume = .3f, float pitch = 1.25f)
    {
        AS.volume = volume;
        AS.pitch = pitch;
        string constructedString = "{"+app+"}"+"<"+eff+">"+text+ "</"+eff.Split(' ')[0]+">"+"{/"+app.Split(' ')[0]+ "}";
        txtAnim.SetText("", true);
        txtAnimPlr.SetTypewriterSpeed(spd);
        txtAnimPlr.ShowText(constructedString);
        txtAnimPlr.StartShowingText();
        showingBox = true;
    }

    public void Speak(EchoDialogueObject_3D obj)
    {
        AS.volume = obj.volume;
        AS.pitch = obj.pitch;
        string constructedString = "{" + obj.appearanceEffect + "}" + "<" + obj.behaviorEffect + ">" + obj.textToSpeak.text + "</" + obj.behaviorEffect.Split(' ')[0] + ">" + "{/" + obj.appearanceEffect.Split(' ')[0] + "}";
        txtAnim.SetText("", true);
        txtAnimPlr.SetTypewriterSpeed(obj.spd);
        txtAnimPlr.ShowText(constructedString);
        txtAnimPlr.StartShowingText();
        showingBox = true;
    }

    void disableText()
    {
        float time = txtAnim.text.Length * .1f;
        StartCoroutine(disableTextEnum(time));
    }

    IEnumerator disableTextEnum(float time)
    {
        yield return new WaitForSeconds(1);//WAIT TIME IS STANDARD 1 SECOND RN!
        hide();   
    }

    void hide()
    {
        showingBox = false;
        txtAnim.SetText("", true);
    }
}
