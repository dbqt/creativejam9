using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public bool isPlayerOne ;
    public Grid GridRef;
    private float timeDelay = 0.3f;

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
                Debug.Log(isPlayerOne);
                Invoke("Dig", timeDelay);
            }
        }



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

    public void Dig() {

        GridCell cell = this.GridRef.GetCell(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.z));

        int goldObtained = 0;
        if (this.tools.shovel != null) {
            // using shovel
            if (cell.Type == GridCell.TerrainType.Soil) {
                goldObtained = cell.Dig(2);
            } else {
                goldObtained = cell.Dig(0);
            }
//            GetComponent<PlayerSoundManager>().PlayDiggingSoundEffect(true);

        } else {
            //using pickaxe
            if (cell.Type == GridCell.TerrainType.Soil) {
                goldObtained = cell.Dig(1);
            } else if (cell.Type == GridCell.TerrainType.Rock) {
                goldObtained = cell.Dig(2);
            } else {
                goldObtained = cell.Dig(0);
            }
        }

    /*    if (goldObtained > 0)
        GetComponent<PlayerSoundManager>().playGoldSoundEffect(); */   

        this.gold.AddGoldToPocket(goldObtained);
    }

    public void UseTNT() {

        if(this.tools.tnt == null) {
            return;
        }

        //putting tnt
        //GetComponent<PlayerSoundManager>().playDiggingSoundEffect(false, 0.5f);
        
        //exploding tnt
        //GetComponent<PlayerSoundManager>().playExplodingSoundEffect();

        //collecting gold
        //GetComponent<PlayerSoundManager>().playGoldSoundEffect();

        int goldObtained = this.GridRef.UseTNT( (int)this.transform.position.x, (int)this.transform.position.z, this.tools.tnt.radius);
        this.gold.AddGoldToPocket(goldObtained);
        this.tools.tnt = null;
    }

    public void GetStunned() {

    }
}
