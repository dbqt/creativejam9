﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [Header("Grid")]
    public GameObject CellPrefab;
    public GameObject RockPrefab;
    public GameObject CactusPrefab;
    public Material DugMaterial;

    public int SizeX = 10, SizeY = 10;
    public int TotalGold = 50;

    public int NbRocks = 2;
    public int NbCactus = 2;

    private float internalTimer = 0f;
    private bool roundStarted = false;

    [Header("Map")]

    public Transform Map;
    public GameObject RedXPrefab;
    public Transform MapTexture;
    public Animator MapAnimator;

    public GridCell[,] internalGrid;

    public float TimeBeforeHidingMap = 5f;

    private HashSet<Coordinates> mapMarks;
    private HashSet<Coordinates> obstacles;

	// Use this for initialization
	void Start () {
		this.internalGrid = new GridCell[SizeX, SizeY];
        this.mapMarks = new HashSet<Coordinates>();
        this.obstacles = new HashSet<Coordinates>();

        this.roundStarted = false;

        // Create grid
        for(int i = 0; i < SizeX; ++i) {
            for(int j = 0; j < SizeY; ++j) {
                this.internalGrid[i, j] = new GridCell();
                this.internalGrid[i, j].DugMaterial = this.DugMaterial;
                // TODO: Random terrain.
                this.internalGrid[i, j].Cell = Instantiate(CellPrefab, new Vector3(i, 0f, j), Quaternion.identity) as GameObject;
                this.internalGrid[i, j].Cell.transform.SetParent(this.gameObject.transform); // Attach each cell to the grid.
                this.internalGrid[i, j].Initialize(GridCell.TerrainType.Soil, 0, 6);
            }
        }

        // Distribute gold
        for(int gold = this.TotalGold; gold > 0; gold--) {
            var coord = new Coordinates{ x = Random.Range(0, this.SizeX), y = Random.Range(0, this.SizeY) };
            this.mapMarks.Add(coord);
            this.internalGrid[coord.x, coord.y].Gold++;
        }

        // Add Obstacles
        for(int rock = 0; rock < this.NbRocks; rock++) {
            var coord = new Coordinates{ x = Random.Range(0, this.SizeX), y = Random.Range(0, this.SizeY) };
            // Make sure we get unique random coords for obstacles.
            while (this.obstacles.Contains(coord)) {
                coord.x = Random.Range(0, this.SizeX);
                coord.y = Random.Range(0, this.SizeY);
            }
            obstacles.Add(coord);
            var newObstacle = Instantiate(this.RockPrefab);
            newObstacle.transform.SetParent(this.gameObject.transform);
            newObstacle.transform.localPosition = new Vector3(coord.x, 0f, coord.y);
            newObstacle.transform.Rotate(Vector3.up * Random.Range(0f, 360f));
            this.internalGrid[coord.x, coord.y].Type = GridCell.TerrainType.Rock;
        }

        for(int cactus = 0; cactus < this.NbRocks; cactus++) {
            var coord = new Coordinates{ x = Random.Range(0, this.SizeX), y = Random.Range(0, this.SizeY) };
            // Make sure we get unique random coords for obstacles.
            while (this.obstacles.Contains(coord)) {
                coord.x = Random.Range(0, this.SizeX);
                coord.y = Random.Range(0, this.SizeY);
            }
            obstacles.Add(coord);
            var newObstacle = Instantiate(this.CactusPrefab);
            newObstacle.transform.SetParent(this.gameObject.transform);
            newObstacle.transform.localPosition = new Vector3(coord.x, 0f, coord.y);
            newObstacle.transform.Rotate(Vector3.up * Random.Range(0f, 360f));
            this.internalGrid[coord.x, coord.y].Type = GridCell.TerrainType.Cactus;
        }

        // Populate map
        // TODO: Place/align Map in the middle of the grid
        //float middleX = ((float)SizeX) /2f;
        //float middleY = ((float)SizeY) /2f;

        foreach(Coordinates c in this.mapMarks) {
            var mark = Instantiate(RedXPrefab) as GameObject;
            mark.transform.SetParent(this.Map);
            mark.transform.localPosition = new Vector3(c.x, 0f, c.y);
            mark.transform.localRotation = Quaternion.identity;
        }

        ShowHideMap();
	}

    void Update() {
        if (this.roundStarted) {
            this.internalTimer -= Time.deltaTime;
            if(this.internalTimer <= 0f) {
                EndRound();
            }
        }
    }

    public void ShowHideMap() {
        ShowMap();
        Invoke("HideMap", this.TimeBeforeHidingMap);
    }

    // Safely get a cell from the grid using coordinates (x, y).
    public GridCell GetCell(int x, int y) {
        if (x < 0 || x >= this.SizeX || y < 0 || y >= this.SizeY) {
            Debug.LogWarning("GetCell : Invalid coordinates");
            return null;
        } 
        return this.internalGrid[x, y];
    }

    public int UseTNT(int x, int y, int radius = 3) {
        return 0;
    }

    public void ShowMap() {
        MapAnimator.SetTrigger("ShowMap");
        GameLogic.Instance.StartTimer(this.TimeBeforeHidingMap);
    }

    public void HideMap() {
        MapAnimator.SetTrigger("HideMap");
        Invoke("StartRound", 1f);
    }

    public void StartRound() {
        GameLogic.Instance.StartTimer(GameLogic.Instance.RoundDuration);
        this.internalTimer = GameLogic.Instance.RoundDuration;
        this.roundStarted = true;
    }

    public void EndRound() {
        this.roundStarted = false;
        GameLogic.Instance.EndRound();
    }

    private class Coordinates {
        public int x = 0;
        public int y = 0;
    }
}
