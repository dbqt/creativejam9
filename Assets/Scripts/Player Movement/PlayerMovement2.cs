﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour {

    public Transform player;
    public float translationSpeed = 1.0f;
    public bool allowMovement = false;

    private float stunDuration = 0;

    void FixedUpdate()
    {
        if (!allowMovement)
            return;

        float verticalValue = Input.GetAxis("Vertical_Player2");
        float horizontalValue = Input.GetAxis("Horizontal_Player2");
        Vector3 target = new Vector3(horizontalValue, 0, verticalValue);

        if (stunDuration > 0)
        {
            target *= -1;
            stunDuration -= Time.deltaTime;
        }

        player.LookAt(this.transform.position + target);
        player.Translate(target * translationSpeed, Space.World);
       
    }

    public void stun(float duration)
    {
        GetComponent<PlayerSoundManager>().playHitSoundEffect();
        stunDuration = duration;
    }

    public bool IsStunned()
    {
        return stunDuration > 0;
    }
}

/* Input Manager Settings
 * 
 * 1st Axis: 
 * Name: Horizontal_Player2
 * [..]
 * Type: Joystick Axis
 * Axis: X Axis
 * Joy Num: Joystick 2
 * 
 * 2nd Axis:
 * Name: Vertical_Player2
 * [..]
 * Type: Joystick Axis
 * Axis: Y Axis
 * Joy Num: Joystick 2
 */
