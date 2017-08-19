using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdownTimer : MonoBehaviour {

    public float timeLeft = 30.0f;
    public Text timerText;

	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
       
        timeLeft -= Time.deltaTime; 
        
        if (timeLeft > 0.0f)
        {
            int timeLeftInt = (int)Convert.ToInt32(timeLeft);
            timerText.text = timeLeftInt.ToString();
        }

        else
            Debug.Log("Time's up");
        
	}

}
