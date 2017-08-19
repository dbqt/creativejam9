using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour {

    public Transform player;
    public float translationSpeed = 1.0f;
	
    void FixedUpdate()
    {
        float verticalValue2 = Input.GetAxis("Vertical_Player2");
        float horizontalValue2 = Input.GetAxis("Horizontal_Player2");
        Vector3 target = new Vector3(horizontalValue2, 0, verticalValue2);
        player.Translate(target * translationSpeed, Space.World);
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