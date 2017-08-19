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
}
