using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameScore : MonoBehaviour {

    public Grid GameGrid;

    [Header("Player1")]
    public Text P1Score, P1Gold, P1Pocket;

    [Header("Player2")]
    public Text P2Score, P2Gold, P2Pocket;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//p1
        P1Score.text = "Score: " + GameGrid.Player1.GetComponent<PlayerGold>().TotalGold;
        P1Gold.text = "Gold: " + GameGrid.Player1.GetComponent<PlayerGold>().UsableGold;
        if(GameGrid.Player1.GetComponent<PlayerGold>().PocketedGold < GameGrid.Player1.GetComponent<PlayerGold>().InventorySize) {
            P1Pocket.text = "Pocket: " + GameGrid.Player1.GetComponent<PlayerGold>().PocketedGold + "/" + GameGrid.Player1.GetComponent<PlayerGold>().InventorySize;
        } else {
            P1Pocket.text = "Pocket filled!";
        }

        //p2
        P2Score.text = "Score: " + GameGrid.Player2.GetComponent<PlayerGold>().TotalGold;
        P2Gold.text = "Gold: " + GameGrid.Player2.GetComponent<PlayerGold>().UsableGold;
        if(GameGrid.Player2.GetComponent<PlayerGold>().PocketedGold < GameGrid.Player2.GetComponent<PlayerGold>().InventorySize) {
            P2Pocket.text = "Pocket: " + GameGrid.Player2.GetComponent<PlayerGold>().PocketedGold + "/" + GameGrid.Player2.GetComponent<PlayerGold>().InventorySize;
        } else {
            P2Pocket.text = "Pocket filled!";
        }
	}
}
