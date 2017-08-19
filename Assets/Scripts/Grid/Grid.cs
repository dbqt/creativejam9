using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public GameObject CellPrefab;
    public int SizeX = 10, SizeY = 10;

    public GridCell[,] internalGrid;

	// Use this for initialization
	void Start () {
		this.internalGrid = new GridCell[SizeX, SizeY];

        for(int i = 0; i < SizeX; ++i) {
            for(int j = 0; j < SizeY; ++j) {
                this.internalGrid[i, j] = new GridCell();
                // TODO: Random this.
                this.internalGrid[i, j].Cell = Instantiate(CellPrefab, new Vector3(i, 0f, j), Quaternion.identity) as GameObject;
                this.internalGrid[i, j].Initialize(GridCell.TerrainType.Soil, 1, 3);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
