using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionCart : MonoBehaviour {

    private bool hasCollided;
	// Update is called once per frame
	void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player1")
        {
            collider.gameObject.GetComponent<PlayerGold>().DepositGold();//cash money stashed
            Debug.Log("Player 1 has stashed his gold!");
        }
	}
}
