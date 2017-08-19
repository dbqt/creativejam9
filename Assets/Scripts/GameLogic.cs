﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour {

    static public GameLogic Instance = null;

    public enum GameState { Menu, Intro, Round, EndRound, Shop, EndGame, Transition }

    public GameState State;

    public int TotalRound = 3;
    public int Round = 1;

    public float RoundDuration = 60f;

    public bool confirmation1 = false;
    public bool confirmation2 = false;


    [Header("UI")]

    public countdownTimer Timer;
    public Text TimerText;

    private bool showTimer;

	// Use this for initialization
	void Start () {
		if (GameLogic.Instance != null) {
            Destroy(this.gameObject);
            return;
        }

        GameLogic.Instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.showTimer = false;
        this.TimerText.text = "";
        this.State = GameState.Menu;
	}

    void Update() {

        if(this.showTimer) {
            this.TimerText.text = this.Timer.timerText;
        }

        // State Machine whlie waiting
        switch (this.State) {
            case GameState.Menu:
            //Debug.Log("case menu");
            // Nothing
            break;
            case GameState.Intro:
            //Debug.Log("case intro");

            /* if(Input.GetButton("????")) {
                this.confirmation1 = true;
            }

            if(Input.GetButton("????")) {
                this.confirmation2 = true;
            }

            */

            if(this.confirmation1 && this.confirmation2) {
                StartRound();
            }

            break;
            case GameState.Round:
            //Debug.Log("case round");
            // Pause logic
            break;
            case GameState.EndRound:
            //Debug.Log("case end round");
            break;
            case GameState.Shop:
            //Debug.Log("case shop");
            /* if(Input.GetButton("????")) {
                this.confirmation1 = true;
            }

            if(Input.GetButton("????")) {
                this.confirmation2 = true;
            }

            */

            if(this.confirmation1 && this.confirmation2) {
                CloseShop();
            }
            break;
            case GameState.EndGame:
            /* if(Input.GetButton("????")) {
                this.confirmation1 = true;
            }

            if(Input.GetButton("????")) {
                this.confirmation2 = true;
            }

            */

            if(this.confirmation1 && this.confirmation2) {
                LoadMenu();
            }
            break;
            default:
            break;
        }
    }
	
	public void LoadIntro() {
        // show rules , character selection
        this.Round = 1;
        this.State = GameState.Intro;
        this.confirmation1 = false;
        this.confirmation2 = false;
    }

    public void StartRound() {
        Debug.Log("StartRound");
        // load level
        // start round timer
        // show map

        this.State = GameState.Round;
        SceneManager.LoadScene("main");
    }

    public void EndRound() {
        // show results
        this.State = GameState.EndRound;
        if(this.Round < this.TotalRound) {
            Invoke("ShowShop", 1f);
        } else {
            EndGame();
        }
    }

    public void ShowShop() {
        // show shop

        this.State = GameState.Shop;
        SceneManager.LoadScene("shop");

        this.confirmation1 = false;
        this.confirmation2 = false;
    }

    public void CloseShop() {
        this.Round++;
        this.State = GameState.Transition;
        Invoke("StartRound", 1f);
    }

    public void EndGame() {
        this.State = GameState.EndGame;
        this.confirmation1 = false;
        this.confirmation2 = false;
        Debug.Log(" end game");
    }

    public void LoadMenu() {

        this.State = GameState.Menu;
        SceneManager.LoadScene("mainmenu");
    }

    public void StartTimer(float time) {
        this.showTimer = true;
        this.Timer.StartTimer(time);
    }

    public void HideTimer() {
        this.showTimer = false;
        this.TimerText.text = "";
    }
}