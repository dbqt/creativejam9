using System.Collections;
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

    public GameObject IntroCanvas, EndRoundCanvas;

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

        // State Machine while waiting
        switch (this.State) {
            case GameState.Menu:
            //Debug.Log("case menu");
            // Nothing
            break;
            case GameState.Intro:
            //Debug.Log("case intro");

            if(Input.GetButtonDown("Start_Player1")) {
                Debug.Log("Start Player ONE");
                this.confirmation1 = true;
            }

            if(Input.GetButtonDown("Start_Player2")) {
                Debug.Log("Start Player TWO");
                this.confirmation2 = true;
            }

            if(this.confirmation1 && this.confirmation2) {
                HideIntroCanvas();
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
            if(Input.GetButton("Start_Player1")) {
                this.confirmation1 = true;
            }

            if(Input.GetButton("Start_Player2")) {
                this.confirmation2 = true;
            }

           

            if(this.confirmation1 && this.confirmation2) {
                CloseShop();
            }
            break;
            case GameState.EndGame:
            if(Input.GetButton("Start_Player1")) {
                this.confirmation1 = true;
            }

            if(Input.GetButton("Start_Player2")) {
                this.confirmation2 = true;
            }

           

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
        ResetData();
        this.Round = 1;
        this.State = GameState.Intro;
        this.confirmation1 = false;
        this.confirmation2 = false;
        ShowIntroCanvas();
    }

    void ShowIntroCanvas() {
        this.IntroCanvas.SetActive(true);
    }

    void HideIntroCanvas() {
        this.IntroCanvas.SetActive(false);
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
            ShowEndRoundCanvas();
            Invoke("ShowShop", 1f);
        } else {
            EndGame();
        }
    }

    void ShowEndRoundCanvas() {
        this.EndRoundCanvas.SetActive(true);
    }

    void HideEndRoundCanvas() {
        this.EndRoundCanvas.SetActive(false);
    }

    public void ShowShop() {
        // show shop
        HideEndRoundCanvas();
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

    void ResetData() {

        PlayerPrefs.SetInt("TotalGold1", 0 );
        PlayerPrefs.SetInt("UsableGold1", 0);
        PlayerPrefs.SetInt("Shield1", 0);
        PlayerPrefs.SetInt("Pickaxe1", 0);
        PlayerPrefs.SetInt("Shovel1", 0);
        PlayerPrefs.SetInt("Bag1", 0);
        PlayerPrefs.SetInt("Tnt1", 0);
    
        PlayerPrefs.SetInt("TotalGold2", 0 );
        PlayerPrefs.SetInt("UsableGold2", 0);
        PlayerPrefs.SetInt("Shield2", 0);
        PlayerPrefs.SetInt("Pickaxe2", 0);
        PlayerPrefs.SetInt("Shovel2", 0);
        PlayerPrefs.SetInt("Bag2", 0);
        PlayerPrefs.SetInt("Tnt2", 0);
        
    }
}
