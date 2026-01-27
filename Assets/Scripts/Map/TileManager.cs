using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour {
    private List<List<MapTile>> mapTiles = new();
    public RectTransform scrollViewPanel;
    public RewardManager rewardManager;
    public GameObject rewardPanel;
    public MapTile currentTile;
    public GameObject mapTilePrefab;
    public GameObject mapConnectorPrefab;
    float spawnDelay = 0.2f;

    private void Start() {
        StartCoroutine(CreateMapTiles());
    }

    private void UpdateTileAccess() {
        bool isTileActive = PlayerPrefs.HasKey(PlayerPrefsKeys.tileActive);

        for (int y = 0; y < mapTiles.Count; y++) {
            for (int x = 0; x < mapTiles[y].Count; x++) {

                bool isCompleted = PlayerPrefs.GetInt($"TileCompleted_{y}-{x}", 0) == 1;
                bool isLastCompleted = PlayerPrefs.GetInt($"LastCompleted_{y}-{x}", 0) == 1;

                mapTiles[y][x].MarkAsCompleted(isCompleted);

                if (!isTileActive && isLastCompleted) {
                    mapTiles[y][x].UnlockNextTiles();

                    foreach (var mapTile in mapTiles[y]) {
                        mapTile.SetUnlocked(false);
                    }

                    //Scroll to the finished tile
                    RectTransform targetRectTransform = mapTiles[y][x].GetComponent<RectTransform>();
                    scrollViewPanel.anchoredPosition = new Vector2(scrollViewPanel.anchoredPosition.x, scrollViewPanel.rect.height - 100 - targetRectTransform.anchoredPosition.y);

                    TileType tileType = mapTiles[y][x].tileType;
                    if ((tileType == TileType.Battlefield || tileType == TileType.MiniBoss || tileType == TileType.Boss) && PlayerPrefs.GetInt(PlayerPrefsKeys.rewardChosen, 0) == 0) {
                        rewardManager.ShowReward(mapTiles[y][x].tileType);
                    }
                }
            }
        }

        if (isTileActive) {
            string activeTileIndex = PlayerPrefs.GetString(PlayerPrefsKeys.tileActive, "");
            string[] activeTileIndexSplit = activeTileIndex.Split("-");
            Vector2Int activeTileIndexVector = new(int.Parse(activeTileIndexSplit[0]), int.Parse(activeTileIndexSplit[1]));
            mapTiles[activeTileIndexVector.y][activeTileIndexVector.x].SetUnlocked(true);
        }
    }

    public void MarkTileAsCurrent(MapTile tile) {
        currentTile = tile;
        string tileIndex = $"{tile.gridIndex.y}-{tile.gridIndex.x}";
        TileCompleter.ClearLastCompleted();
        TileCompleter.currentTileIndex = tileIndex;
        PlayerPrefs.SetString(PlayerPrefsKeys.tileActive, tileIndex);
    }

    private GameObject CreateMapTile(Vector2 tilePos, Vector2 gridIndex, TileType tileType) {
        GameObject mapTileObject = Instantiate(mapTilePrefab, tilePos, quaternion.identity, scrollViewPanel);
        mapTileObject.name = $"MapTile {gridIndex.y}-{gridIndex.x}";

        MapTile mapTile = mapTileObject.GetComponent<MapTile>();
        mapTile.tileType = tileType;
        mapTileObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/MapTiles/{tileType}");
        mapTile.gridIndex = gridIndex;

        mapTiles[(int)gridIndex.y].Add(mapTile);

        return mapTileObject;
    }

    private IEnumerator CreateStartTiles(int y) {
        for (int x = 0; x < 3; x++) {
            Vector2 tilePos = new(-480 + x * 480, 200);
            GameObject mapTileObject = CreateMapTile(tilePos, new(x, y), TileType.Event);

            RectTransform rect = mapTileObject.GetComponent<RectTransform>();
            rect.anchoredPosition = tilePos;

            MapTile mapTile = mapTileObject.GetComponent<MapTile>();
            if (TileCompleter.currentTileIndex == null) {
                mapTile.SetUnlocked(true);
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // Can split the path into 1 or 2 parents
    private IEnumerator CreateSplitTiles(int y, bool guaranteedSplit, bool largeGapBetweenParents, TileType tileType) {
        foreach (var childMapTile in mapTiles[y - 1]) {
            int nParents = PlayerPrefs.HasKey($"SplitTilesKey_{y - 1}")
                ? PlayerPrefs.GetInt($"SplitTilesKey_{y - 1}")
                : guaranteedSplit ? 2 : Rng.Range(1, 3);
            PlayerPrefs.SetInt($"SplitTilesKey_{y - 1}", nParents);
            PlayerPrefs.Save();

            for (int x = 0; x < nParents; x++) {
                int xOffset = largeGapBetweenParents ?
                                -120 + (x * 240) :
                                -60 + (x * 120);
                int xPos = nParents == 1 ? (int)childMapTile.transform.position.x : (int)childMapTile.transform.position.x + xOffset;
                Vector2 tilePos = new(xPos, childMapTile.transform.position.y + 200);
                GameObject parentMapTileObject = CreateMapTile(tilePos, new(mapTiles[y].Count, y), tileType);

                MapTile parentMapTile = parentMapTileObject.GetComponent<MapTile>();
                childMapTile.nextTiles.Add(parentMapTile);

                CreateMapConnector(parentMapTile, childMapTile);
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // Can merge with the neighbor tile to share the parent instead of having 1 each
    private IEnumerator CreateMergeTiles(int y, bool guaranteedMerge, TileType tileType) {
        for (int i = 0; i < mapTiles[y - 1].Count; i++) {
            MapTile childMapTile = mapTiles[y - 1][i];

            bool willMergeRight = false;
            if (i != mapTiles[y - 1].Count - 1) {
                willMergeRight = PlayerPrefs.HasKey($"MergeTilesKey_{y - 1}_{i}")
                    ? Convert.ToBoolean(PlayerPrefs.GetInt($"MergeTilesKey_{y - 1}_{i}"))
                    : guaranteedMerge || Rng.Chance(50);
                PlayerPrefs.SetInt($"MergeTilesKey_{y - 1}_{i}", willMergeRight ? 1 : 0);
                PlayerPrefs.Save();
            }

            if (willMergeRight) {
                MapTile childMapTileNeighbor = mapTiles[y - 1][i + 1];
                float xBetween = Math.Abs(childMapTile.transform.position.x - childMapTileNeighbor.transform.position.x) / 2;
                Vector2 tilePos = new(childMapTile.transform.position.x + xBetween, childMapTile.transform.position.y + 200);
                GameObject parentMapTileObject = CreateMapTile(tilePos, new(mapTiles[y].Count, y), tileType);

                MapTile parentMapTile = parentMapTileObject.GetComponent<MapTile>();
                childMapTile.nextTiles.Add(parentMapTile);
                childMapTileNeighbor.nextTiles.Add(parentMapTile);
                i++;

                CreateMapConnector(parentMapTile, childMapTile);
                CreateMapConnector(parentMapTile, childMapTileNeighbor);
            } else {
                Vector2 tilePos = new(childMapTile.transform.position.x, childMapTile.transform.position.y + 200);
                GameObject parentMapTileObject = CreateMapTile(tilePos, new(mapTiles[y].Count, y), tileType);

                MapTile parentMapTile = parentMapTileObject.GetComponent<MapTile>();
                childMapTile.nextTiles.Add(parentMapTile);

                CreateMapConnector(parentMapTile, childMapTile);
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void CreateBossTile(int y) {
        float xPosSum = 0;
        foreach (var childMapTile in mapTiles[y - 1]) {
            xPosSum += childMapTile.transform.position.x;
        }
        float xPosAverage = xPosSum / mapTiles[y - 1].Count;

        Vector2 tilePos = new(xPosAverage, mapTiles[y - 1][0].transform.position.y + 200);
        GameObject parentMapTileObject = CreateMapTile(tilePos, new(mapTiles[y].Count, y), TileType.Boss);

        foreach (var childMapTile in mapTiles[y - 1]) {
            MapTile parentMapTile = parentMapTileObject.GetComponent<MapTile>();
            childMapTile.nextTiles.Add(parentMapTile);

            CreateMapConnector(parentMapTile, childMapTile);
        }
    }

    private void CreateMapConnector(MapTile parentMapTile, MapTile childMapTile) {
        // Just some random math on how to calculate position, rotation and length of each MapConnector

        Vector2 posDiff = parentMapTile.transform.position - childMapTile.transform.position;
        float xBetween = childMapTile.transform.position.x + (posDiff.x / 2);
        float yBetween = childMapTile.transform.position.y + (posDiff.y / 2);
        Vector2 mapConnectorPos = new(xBetween, yBetween);

        float angleX = Mathf.Atan2(posDiff.y, posDiff.x) * Mathf.Rad2Deg;
        float zRotation = angleX - 90f;
        Quaternion rotation = Quaternion.Euler(0f, 0f, zRotation);

        GameObject mapConnector = Instantiate(mapConnectorPrefab, mapConnectorPos, rotation, scrollViewPanel);
        mapConnector.GetComponent<RectTransform>().sizeDelta = new Vector2(150, posDiff.magnitude - 100) * 4;
    }

    private IEnumerator CreateMapTiles() {
        for (int y = 0; y < 7; y++) {
            mapTiles.Add(new List<MapTile>());
            switch (y) {
                case 0:
                    yield return StartCoroutine(CreateStartTiles(y));
                    break;
                case 1:
                    yield return StartCoroutine(CreateSplitTiles(y, true, true, TileType.Campfire));
                    break;
                case 2:
                    yield return StartCoroutine(CreateMergeTiles(y, false, TileType.Battlefield));
                    break;
                case 3:
                    yield return StartCoroutine(CreateSplitTiles(y, false, false, TileType.Event));
                    break;
                case 4:
                    yield return StartCoroutine(CreateMergeTiles(y, false, TileType.Battlefield));
                    break;
                case 5:
                    yield return StartCoroutine(CreateMergeTiles(y, true, TileType.Shop));
                    break;
                case 6:
                    CreateBossTile(y);
                    break;
            }
        }
        UpdateTileAccess();
    }
}
