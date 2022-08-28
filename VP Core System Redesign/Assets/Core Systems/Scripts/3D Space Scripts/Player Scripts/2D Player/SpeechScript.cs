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
    public Image playerIcon;
    
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


    float textboxTimer = 1;
    public void _Update()
    {
        if (showingBox){txtBox.rectTransform.sizeDelta = Vector2.Lerp(txtBox.rectTransform.sizeDelta, boxSize, Time.deltaTime * 15f);}
        else{txtBox.rectTransform.sizeDelta = Vector2.Lerp(txtBox.rectTransform.sizeDelta, new Vector2(boxSize.x/2, 0), Time.deltaTime * 15f);}

        Vector2 v = cc2d.playerRectTransform.anchoredPosition;
        
        bool characterOffScreen = !cc2d.isOnScreen || (v.x < 0) || (v.x > Screen.width) || (v.y < 0) || (v.y > Screen.height);

        if (!characterOffScreen)
        {
            //boxPosition = cc2d.playerRectTransform.anchoredPosition+new Vector2(0,60);
            textboxTimer += Time.deltaTime * 5;
            textboxTimer = Mathf.Clamp(textboxTimer,0,1);
            boxPosition = Vector2.Lerp(new Vector2((boxSize.x / 2) + 5, 0 + 5), cc2d.playerRectTransform.anchoredPosition + new Vector2(0, 60), CreateEaseInEaseOutCurve().Evaluate(textboxTimer));
            
            boxPosition.x = Mathf.Clamp(boxPosition.x,boxSize.x/2,Screen.width-(boxSize.x/2));
            boxPosition.y = Mathf.Clamp(boxPosition.y,0,Screen.height-(boxSize.y));
            txtBox.rectTransform.anchoredPosition = boxPosition;
        }
        else
        {
            textboxTimer = 0;
            boxPosition = new Vector2((boxSize.x / 2) + 5, 0 + 5);
            txtBox.rectTransform.anchoredPosition = Vector2.Lerp(txtBox.rectTransform.anchoredPosition, boxPosition, Time.deltaTime * 10f);
        }

        playerIcon.color = Color.Lerp(playerIcon.color, new Color(1, 1, 1, System.Convert.ToInt32(characterOffScreen && showingBox)), Time.deltaTime * 20);

        playerIcon.rectTransform.anchoredPosition = new Vector2(boxSize.x + 10, 0 + 5);
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

    public static AnimationCurve CreateEaseInEaseOutCurve()
    {
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(new Keyframe(0, 0, 0, 0));
        curve.AddKey(new Keyframe(1, 1, 0, 0));
        return curve;
    }
}
