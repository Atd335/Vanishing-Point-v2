using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;



public class PauseSettingsScript : MonoBehaviour
{
    MusicScript music;
    SFXScript sfx;
    SpeechScript speech;
    
    public bool gamePaused;
    public bool muteMusic;

    public float mouseSensitiviy = 1;
    public float masterVolume = 1;
    public float musicVolume = 1;
    public float SFXVolume = 1;
    public float speechVolume = 1;
    float pauseLerpTimer;

    RawImage pauseDim;

	public void _Start()
    {
        music = GetComponent<MusicScript>();
        sfx = GetComponent<SFXScript>();
        speech = GetComponent<SpeechScript>();

        pauseDim = GameObject.Find("Pause Dim").GetComponent<RawImage>();
    }

    public void _Update()
    {
        if (Input.GetButtonDown("Pause")) 
        {
            if (menuAnimating) { return; }
            gamePaused = !gamePaused;
            toggleMenu(gamePaused);
        }



        mouseSensitiviy = Mathf.Clamp(mouseSensitiviy,0.05f,4f);
        masterVolume = Mathf.Clamp(masterVolume,0,1);
        musicVolume = Mathf.Clamp(musicVolume, 0,1);
        SFXVolume = Mathf.Clamp(SFXVolume, 0,1);
        speechVolume = Mathf.Clamp(speechVolume, 0,1);

        music.AS.volume = musicVolume * masterVolume * System.Convert.ToInt32(!muteMusic); 
        sfx.AS.volume = SFXVolume * masterVolume;
        speech.AS.volume = speechVolume * masterVolume;
        
        if (gamePaused)
        {
            music.AS.pitch = Mathf.Lerp(music.AS.pitch, .5f, Time.deltaTime * 10);
            music.AS.volume = (musicVolume * masterVolume * System.Convert.ToInt32(!muteMusic)) / 3f;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            music.AS.pitch = Mathf.Lerp(music.AS.pitch, 1, Time.deltaTime * 10);
            Cursor.lockState = CursorLockMode.Locked;
        }       
    }

    public void toggleMenu(bool isUp)
    {
        StartCoroutine(moveMenu(isUp));
    }

    bool menuAnimating;
    float pauseSpeed = 10f;
    IEnumerator moveMenu(bool isUp)
    {
        menuAnimating = true;
        
        float timer = 0;

        while (timer!=1)
        {
            timer += Time.deltaTime * pauseSpeed;
            timer = Mathf.Clamp(timer,0,1);
            
            if (isUp)
            {
                pauseDim.color = new Color(0,0,0,Mathf.Lerp(0,0.5f,timer));
            }
            else
            {
                pauseDim.color = new Color(0, 0, 0, Mathf.Lerp(.5f, 0f, timer));
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
     
        menuAnimating = false;
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
