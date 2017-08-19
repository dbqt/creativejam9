using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionCart2 : MonoBehaviour
{
    private bool hasCollided;
    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player2")
        {
            collider.gameObject.GetComponent<PlayerGold>().DepositGold();//cash money stashed
            Debug.Log("Player 2 has stashed his gold!");
        }
    }
}
