using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [Header("Grid")]
    public GameObject CellPrefab;

    public int SizeX = 10, SizeY = 10;
    public int TotalGold = 50;

    [Header("Map")]

    public Transform Map;
    public GameObject RedXPrefab;
    public Transform MapTexture;
    public Animator MapAnimator;

    public GridCell[,] internalGrid;

    private HashSet<Coordinates> mapMarks;

	// Use this for initialization
	void Start () {
		this.internalGrid = new GridCell[SizeX, SizeY];
        this.mapMarks = new HashSet<Coordinates>();

        // Create grid
        for(int i = 0; i < SizeX; ++i) {
            for(int j = 0; j < SizeY; ++j) {
                this.internalGrid[i, j] = new GridCell();
                // TODO: Random this.
                this.internalGrid[i, j].Cell = Instantiate(CellPrefab, new Vector3(i, 0f, j), Quaternion.identity) as GameObject;
                this.internalGrid[i, j].Cell.transform.SetParent(this.gameObject.transform); // Attach each cell to the grid.
                this.internalGrid[i, j].Initialize(GridCell.TerrainType.Soil, 0, 3);
            }
        }

        // Distribute gold
        for(int gold = this.TotalGold; gold > 0; gold--) {
            var coord = new Coordinates{ x = Random.Range(0, this.SizeX), y = Random.Range(0, this.SizeY) };
            this.mapMarks.Add(coord);
            this.internalGrid[coord.x, coord.y].Gold++;
        }

        // Populate map
        // TODO: Place/align Map in the middle of the grid
        //float middleX = ((float)SizeX) /2f;
        //float middleY = ((float)SizeY) /2f;

        foreach(Coordinates c in mapMarks) {
            var mark = Instantiate(RedXPrefab) as GameObject;
            mark.transform.SetParent(Map);
            mark.transform.localPosition = new Vector3(c.x, 0f, c.y);
            mark.transform.localRotation = Quaternion.identity;
        }

        ShowMap();

	}

    // Safely get a cell from the grid using coordinates (x, y).
    public GridCell GetCell(int x, int y) {
        if (x < 0 || x >= this.SizeX || y < 0 || y >= this.SizeY) {
            Debug.LogWarning("GetCell : Invalid coordinates");
            return null;
        } 
        return this.internalGrid[x, y];
    }

    public void ShowMap() {
        MapAnimator.SetTrigger("ShowMap");
    }

    public void HideMap() {
        MapAnimator.SetTrigger("HideMap");
    }

    private class Coordinates {
        public int x = 0;
        public int y = 0;
    }
}
