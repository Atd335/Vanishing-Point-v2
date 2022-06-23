using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{

    public AudioSource AS;

	public void _Start()
    {
        AS = this.gameObject.AddComponent<AudioSource>() as AudioSource;
    }

    public void playSFX(AudioClip clip, float volume = .1f)
    {
        AS.PlayOneShot(clip, volume);
    }

    public void _Update()
    {
        
    }

}
