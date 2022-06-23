
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
public class App_Video : MonoBehaviour, IContentLoader
{
    OSCanvasRaycaster oscr;

    VideoClip vid;
    public VideoPlayer player;
    BaseWindowController bwc;

    double clipDuration;

    Scrollbar scroll;

    public TextMeshProUGUI timestamp;
    public TextMeshProUGUI timestamp2;

    public void loadContent(UnityEngine.Object obj)
    {
        oscr = OSCanvasRaycaster.oscr;
        bwc = GetComponent<BaseWindowController>();
        scroll = GetComponentInChildren<Scrollbar>();
        player = GetComponent<VideoPlayer>();

        vid = (VideoClip)obj;
        clipDuration = vid.length;

        player.clip = vid;
        player.Play();

        oscr.onClick.AddListener(onClick);
        oscr.onHold.AddListener(onHold);
        oscr.onRelease.AddListener(onRelease);
    }

    bool highlighted;
    public bool paused;
    void onClick()
    {
        if (oscr.hoveredOBJ == scroll.handleRect.gameObject)
        {
            highlighted = true;
            player.Pause();
        }
    }

    void onHold()
    {
        if (!highlighted) { return; }
        player.time = (scroll.value/1f) * clipDuration;
    }

    void onRelease()
    {
        highlighted = false;
        if (!paused)
        {
            player.Play();
        }
    }

    private void Update()
    {
        if (!highlighted)
        {
            scroll.value = (float)(player.time / clipDuration);
        }

        TimeSpan playTime = TimeSpan.FromSeconds(player.time);
        TimeSpan totalTime = TimeSpan.FromSeconds(clipDuration);
    }


}
