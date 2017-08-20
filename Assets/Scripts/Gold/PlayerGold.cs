using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour {

    public int TotalGold = 0;
    public int UsableGold = 0;
    public int InventorySize = 3;
    public int PocketedGold = 0;

    public bool CanDig() {
        return this.PocketedGold < this.InventorySize;
    }

    // Returns amount of gold dropped.
    public int DropGold() {
        int amount = this.PocketedGold;
        this.PocketedGold = 0;
        this.TotalGold -= amount;
        this.UsableGold -= amount;
        return amount;
    }

    // Frees up the pocket.
    public void DepositGold() {
        this.PocketedGold = 0;
    }

    // Add gold to pocket, usable and total;
    public void AddGoldToPocket(int amount) {
        this.PocketedGold += amount;
        this.UsableGold += amount;
        this.TotalGold += amount;
    }

    // Substracts gold from player. Note: This will always deposit the gold before the transaction.
    public void SpendGold(int amount) {
        DepositGold();
        this.UsableGold -= amount;
    }

    public void LoadData() {
        this.PocketedGold = 0;
        var tools = this.gameObject.GetComponent<PlayerTools>();
        if (this.gameObject.GetComponent<PlayerAction>().isPlayerOne) {
            this.TotalGold = PlayerPrefs.GetInt("TotalGold1");
            this.UsableGold = PlayerPrefs.GetInt("UsableGold1");
            tools.shield = PlayerPrefs.GetInt("Shield1") == 1 ? new Shield() : null;
            tools.pickAxe = PlayerPrefs.GetInt("Pickaxe1") == 1 ? new PickAxe() : null;
            tools.shovel = PlayerPrefs.GetInt("Shovel1") == 1 ? new Shovel() : null;
            tools.bag = PlayerPrefs.GetInt("Bag1") == 1 ? new Bag() : null;
            tools.tnt = PlayerPrefs.GetInt("Tnt1") == 1 ? new TNT() : null;
        } else {
            this.TotalGold = PlayerPrefs.GetInt("TotalGold2");
            this.UsableGold = PlayerPrefs.GetInt("UsableGold2");
            tools.shield = PlayerPrefs.GetInt("Shield2") == 1 ? new Shield() : null;
            tools.pickAxe = PlayerPrefs.GetInt("Pickaxe2") == 1 ? new PickAxe() : null;
            tools.shovel = PlayerPrefs.GetInt("Shovel2") == 1 ? new Shovel() : null;
            tools.bag = PlayerPrefs.GetInt("Bag2") == 1 ? new Bag() : null;
            tools.tnt = PlayerPrefs.GetInt("Tnt2") == 1 ? new TNT() : null;
        }
    }

    // Deposit then save data to persist.
    public void SaveData() {
        DepositGold();
        var tools = this.gameObject.GetComponent<PlayerTools>();
        if (this.gameObject.GetComponent<PlayerAction>().isPlayerOne) {
            PlayerPrefs.SetInt("TotalGold1", this.TotalGold );
            PlayerPrefs.SetInt("UsableGold1", this.UsableGold);
            PlayerPrefs.SetInt("Shield1", tools.shield != null ? 1 : 0);
            PlayerPrefs.SetInt("Pickaxe1", tools.pickAxe != null ? 1 : 0);
            PlayerPrefs.SetInt("Shovel1", tools.shovel != null ? 1 : 0);
            PlayerPrefs.SetInt("Bag1", tools.bag != null ? 1 : 0);
            PlayerPrefs.SetInt("Tnt1", tools.tnt != null ? 1 : 0);
        } else {
            PlayerPrefs.SetInt("TotalGold2", this.TotalGold );
            PlayerPrefs.SetInt("UsableGold2", this.UsableGold);
            PlayerPrefs.SetInt("Shield2", tools.shield != null ? 1 : 0);
            PlayerPrefs.SetInt("Pickaxe2", tools.pickAxe != null ? 1 : 0);
            PlayerPrefs.SetInt("Shovel2", tools.shovel != null ? 1 : 0);
            PlayerPrefs.SetInt("Bag2", tools.bag != null ? 1 : 0);
            PlayerPrefs.SetInt("Tnt2", tools.tnt != null ? 1 : 0);
        }
    }
}
