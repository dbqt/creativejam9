using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioClip[] dirtDigging, stoneDigging, hit, explode;
    public AudioClip money;

    private bool isDirt = true;
    private int elapsedDiggingTime=0 ;
    private int totalDiggingTime = 0;


	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (elapsedDiggingTime <= totalDiggingTime)
        {
            elapsedDiggingTime++;

            if (elapsedDiggingTime % 30 == 0)
            {
                if (isDirt)
                    GetComponent<AudioSource>().PlayOneShot(dirtDigging[Random.Range(0, 3)]);
                else
                    GetComponent<AudioSource>().PlayOneShot(stoneDigging[Random.Range(0, 3)]);
            }
        }
    }

    public void playDiggingSoundEffect(bool IsDirt, float time)
    {
        isDirt = IsDirt;
        elapsedDiggingTime = 0;
        totalDiggingTime = (int)(time*60);

    }

    public void playHitSoundEffect()
    {
        GetComponent<AudioSource>().PlayOneShot(hit[Random.Range(0,2)]);
    }

    public void playGoldSoundEffect(){
        GetComponent<AudioSource>().PlayOneShot(money);
    }

    public void playExplodingSoundEffect()
    {
        GetComponent<AudioSource>().PlayOneShot(explode[Random.Range(0, 2)]);
    }
}
