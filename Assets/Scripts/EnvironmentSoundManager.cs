using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSoundManager : MonoBehaviour
{
    public AudioClip menuShopSoundtrack, gameSoundtrack;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayMenuSoundtrack()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(menuShopSoundtrack);
    }

    public void PlayGameSoundtrack()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(gameSoundtrack);
    }
}
