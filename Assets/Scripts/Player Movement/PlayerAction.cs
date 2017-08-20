﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public bool isPlayerOne ;
    public Grid GridRef;
    private float timeDelay = 0.3f;

    [Header("TNT")]
    public GameObject TNTPrefab;
    public float TNTDelay = 1f;

    [Header("Animations")]
    public Animator ToolAnimator;
    public GameObject Pickaxe, Shovel;

    PlayerTools tools;
    PlayerGold gold;

	// Use this for initialization
	void Start () {
		this.tools = this.gameObject.GetComponent<PlayerTools>();
        this.gold = this.gameObject.GetComponent<PlayerGold>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Dig"+ (isPlayerOne ? "1" : "2")))
        {
            if (this.gold.CanDig())
            {
                // use animation depending on tool
                if (this.tools.shovel != null) {
                    this.Shovel.SetActive(true);
                    this.Pickaxe.SetActive(false);
                    this.ToolAnimator.SetTrigger("UseShovel");
                } else {
                    this.Shovel.SetActive(false);
                    this.Pickaxe.SetActive(true);
                    this.ToolAnimator.SetTrigger("UsePickaxe");
                }

                Invoke("Dig", timeDelay);
            }
        }

        LimitPlayerBounds();

    }

    void LimitPlayerBounds()
    {
        Vector3 pos = this.gameObject.transform.position;

        if (pos.x < -0.5f)                  pos.x = -0.5f;
        if (pos.x > GridRef.SizeX - 0.5f)   pos.x = GridRef.SizeX - 0.5f;
        if (pos.z < -0.5f)                  pos.z = -0.5f;
        if (pos.z > GridRef.SizeY - 0.5f)   pos.z = GridRef.SizeY - 0.5f;

        this.gameObject.transform.position = pos;
    }

    public void TakeDamage()
    {
        GetComponent<PlayerSoundManager>().playHitSoundEffect();

        if (this.tools.shield != null) {
            this.tools.shield.nbHitsRemaining--;

            if(this.tools.shield.nbHitsRemaining <= 0) {
                this.tools.shield = null;
            }

            return;
        }

        this.gold.DropGold();
        GetStunned();
    }

    public void Dig()
    {
        if (GetComponent<PlayerMovement>().IsStunned()) return;

        GridCell cell = this.GridRef.GetCell(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.z));

        int goldObtained = 0;
        if (this.tools.shovel != null) {
            // using shovel
            if (cell.Type == GridCell.TerrainType.Soil) {
                goldObtained = cell.Dig(2);
            } else {
                goldObtained = cell.Dig(0);
            }
            GetComponent<PlayerSoundManager>().playDiggingSoundEffect(true,1);

        } else {
            //using pickaxe
            if (cell.Type == GridCell.TerrainType.Soil) {
                goldObtained = cell.Dig(1);
            } else if (cell.Type == GridCell.TerrainType.Rock) {
                goldObtained = cell.Dig(2);
            } else {
                goldObtained = cell.Dig(0);
            }
            GetComponent<PlayerSoundManager>().playDiggingSoundEffect(false, 1);
        }

        if (goldObtained > 0)
            GetComponent<PlayerSoundManager>().playGoldSoundEffect();

        this.gold.AddGoldToPocket(goldObtained);
    }

    public void UseTNT() {

        if(this.tools.tnt == null) {
            return;
        }

        //putting tnt
        GetComponent<PlayerSoundManager>().playDiggingSoundEffect(false, 0.5f);
        GameObject newTnt = Instantiate(TNTPrefab) as GameObject;
        newTnt.GetComponent<TNTBehavior>().SetTNT(this.TNTDelay);

        Invoke("AftermathTNT", this.TNTDelay);
        
        this.tools.tnt = null;
        
    }

    public void AftermathTNT() {
        int goldObtained = this.GridRef.UseTNT( Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.z), this.tools.tnt.radius);
        //exploding tnt
        //GetComponent<PlayerSoundManager>().playExplodingSoundEffect();
        //collecting gold
        //GetComponent<PlayerSoundManager>().playGoldSoundEffect();
        this.gold.AddGoldToPocket(goldObtained);
    }

    public void GetStunned() {

    }
}
