using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSoundManager : MonoBehaviour
{
    public AudioClip bow;
    public AudioClip menuShopSoundtrack, gameSoundtrack;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlayMenuSoundtrack()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(MenuSoundtrack);
    }

    void PlayGameSoundtrack()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(gameSoundtrack);
    }

    void PlayBowSoundEffect()
    {
        GetComponent<AudioSource>().PlayOneShot(bow);
    }
}
