using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class App_Audio : MonoBehaviour, IContentLoader
{
    OSCanvasRaycaster oscr;

    public AudioSource AS;
    AudioClip clip;

    float clipLength;
    float currentTime;

    public Scrollbar scroll;
    public Scrollbar volume;
    public bool paused;
    bool highlighted;

    public TextMeshProUGUI title;

    public void loadContent(Object obj)
    {
        title.text = obj.name;
        
        oscr = OSCanvasRaycaster.oscr;
        oscr.onClick.AddListener(onClick);
        oscr.onHold.AddListener(onHold);
        oscr.onRelease.AddListener(onRelease);

        AS = GetComponent<AudioSource>();
        //scroll = GetComponentInChildren<Scrollbar>();

        clip = (AudioClip)obj;
        clipLength = clip.length;


        AS.clip = clip;
        AS.Play();
    }

    void onClick()
    {
        if (oscr.hoveredOBJ == scroll.handleRect.gameObject)
        {
            highlighted = true;
            AS.Pause();
        }
        else
        {
            highlighted = false;
        }
    }
    void onHold()
    {
        if (!highlighted) { return; }
        AS.time = (scroll.value / 1f) * clipLength;
    }

    void onRelease()
    {
        highlighted = false;
        if (!paused) { AS.UnPause(); }
    }

    private void Update()
    {
        currentTime = AS.time;
        scroll.value = currentTime / clipLength;
        AS.volume = volume.value;
    }
}
