using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFinalScore : MonoBehaviour {


    public Text Player1_Score;
    public Text Player2_Score;
	// Use this for initialization
	void Start () {
        int score1 = PlayerPrefs.GetInt("TotalGold1"); //p1
        int score2 = PlayerPrefs.GetInt("TotalGold2"); //p2

        Player1_Score.text = score1.ToString();
        Player2_Score.text = score2.ToString();

	}
}
