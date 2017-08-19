using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell {

    public enum TerrainType { Soil, Rock, Cactus }

    public GameObject Cell;
    public Material DugMaterial;

    public bool IsHidden = true;
    public TerrainType Type = TerrainType.Soil;
    public int Gold = 0;

    private int HP = 3;

    // Initialize with terrain, starting amount of gold, starting hp and whether it starts hidden.
	public void Initialize(TerrainType terrain, int startingGold, int startingHP, bool startHidden = true) {
        this.Type = terrain;
        this.Gold = startingGold;
        this.HP = startingHP;
        this.IsHidden = startHidden;
    }

    // Returns the number of gold dug.
    public int Dig() {
        this.HP--;

        if (this.HP <= 0) {
            int goldDug = this.Gold;
            this.Gold = 0;
            this.IsHidden = false;
            UseDugMaterial();
            return goldDug;
        }

        return 0;
    }

    private void UseDugMaterial(){
        this.Cell.GetComponent<Renderer>().material = DugMaterial;
    }
}
