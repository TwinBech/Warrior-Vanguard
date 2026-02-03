using System;
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
    private int floorsPerSector = 11;

    private void Start() {
        CreateMapTiles();
        UpdateTileAccess();
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

    private void CreateStartTiles(int sector, int y) {
        for (int x = 0; x < 3; x++) {
            Vector2 tilePos = new(-480 + x * 480, 200 + (sector * 2300));
            GameObject mapTileObject = CreateMapTile(tilePos, new(x, y + (sector * floorsPerSector)), TileType.Event);

            RectTransform rect = mapTileObject.GetComponent<RectTransform>();
            rect.anchoredPosition = tilePos;

            MapTile mapTile = mapTileObject.GetComponent<MapTile>();
            if (TileCompleter.currentTileIndex == null) {
                mapTile.SetUnlocked(true);
            }
        }
    }

    private void CreateStraightTiles(int sector, int y, TileType tileType) {
        int yBelow = y - 1 + (sector * floorsPerSector);
        for (int x = 0; x < mapTiles[yBelow].Count; x++) {
            MapTile childMapTile = mapTiles[yBelow][x];
            {
                Vector2 tilePos = new(childMapTile.transform.position.x, childMapTile.transform.position.y + 200);
                TileType mapTileType = tileType == TileType.None ? MapTile.GetRandomTileType(new() { childMapTile.tileType }) : tileType;
                GameObject parentMapTileObject = CreateMapTile(tilePos, new(mapTiles[y].Count, y + (sector * floorsPerSector)), mapTileType);

                MapTile parentMapTile = parentMapTileObject.GetComponent<MapTile>();
                childMapTile.nextTiles.Add(parentMapTile);

                CreateMapConnector(parentMapTile, childMapTile);
            }
        }
    }

    // Can split the path into 1 or 2 parents
    private void CreateSplitTiles(int sector, int y, TileType tileType, bool guaranteedSplit, bool largeGapBetweenParents) {
        int yBelow = y - 1 + (sector * floorsPerSector);
        for (int x = 0; x < mapTiles[yBelow].Count; x++) {
            MapTile childMapTile = mapTiles[yBelow][x];
            int nParents = PlayerPrefs.HasKey($"SplitTilesKey_{yBelow}_{x}")
                ? PlayerPrefs.GetInt($"SplitTilesKey_{yBelow}_{x}")
                : guaranteedSplit ? 2 : Rng.Range(1, 3);
            PlayerPrefs.SetInt($"SplitTilesKey_{yBelow}_{x}", nParents);
            PlayerPrefs.Save();

            for (int i = 0; i < nParents; i++) {
                int xOffset = largeGapBetweenParents ?
                                -120 + (i * 240) :
                                -60 + (i * 120);
                int xPos = nParents == 1 ? (int)childMapTile.transform.position.x : (int)childMapTile.transform.position.x + xOffset;
                Vector2 tilePos = new(xPos, childMapTile.transform.position.y + 200);
                TileType mapTileType = tileType == TileType.None ? MapTile.GetRandomTileType(new() { childMapTile.tileType }) : tileType;
                GameObject parentMapTileObject = CreateMapTile(tilePos, new(mapTiles[y].Count, y + (sector * floorsPerSector)), mapTileType);

                MapTile parentMapTile = parentMapTileObject.GetComponent<MapTile>();
                childMapTile.nextTiles.Add(parentMapTile);

                CreateMapConnector(parentMapTile, childMapTile);
            }
        }
    }

    // Can merge with the neighbor tile to share the parent instead of having 1 each
    private void CreateMergeTiles(int sector, int y, TileType tileType, bool guaranteedMerge) {
        int yBelow = y - 1 + (sector * floorsPerSector);
        for (int x = 0; x < mapTiles[yBelow].Count; x++) {
            MapTile childMapTile = mapTiles[yBelow][x];

            bool willMergeRight = false;
            if (x != mapTiles[yBelow].Count - 1) {
                willMergeRight = PlayerPrefs.HasKey($"MergeTilesKey_{yBelow}_{x}")
                    ? Convert.ToBoolean(PlayerPrefs.GetInt($"MergeTilesKey_{yBelow}_{x}"))
                    : guaranteedMerge || Rng.Chance(50);
                PlayerPrefs.SetInt($"MergeTilesKey_{yBelow}_{x}", willMergeRight ? 1 : 0);
                PlayerPrefs.Save();
            }

            if (willMergeRight) {
                MapTile childMapTileNeighbor = mapTiles[yBelow][x + 1];
                float xBetween = Math.Abs(childMapTile.transform.position.x - childMapTileNeighbor.transform.position.x) / 2;
                Vector2 tilePos = new(childMapTile.transform.position.x + xBetween, childMapTile.transform.position.y + 200);
                TileType mapTileType = tileType == TileType.None ? MapTile.GetRandomTileType(new() { childMapTile.tileType, childMapTileNeighbor.tileType }) : tileType;
                GameObject parentMapTileObject = CreateMapTile(tilePos, new(mapTiles[y].Count, y + (sector * floorsPerSector)), mapTileType);

                MapTile parentMapTile = parentMapTileObject.GetComponent<MapTile>();
                childMapTile.nextTiles.Add(parentMapTile);
                childMapTileNeighbor.nextTiles.Add(parentMapTile);
                x++;

                CreateMapConnector(parentMapTile, childMapTile);
                CreateMapConnector(parentMapTile, childMapTileNeighbor);
            } else {
                Vector2 tilePos = new(childMapTile.transform.position.x, childMapTile.transform.position.y + 200);
                TileType mapTileType = tileType == TileType.None ? MapTile.GetRandomTileType(new() { childMapTile.tileType }) : tileType;
                GameObject parentMapTileObject = CreateMapTile(tilePos, new(mapTiles[y].Count, y + (sector * floorsPerSector)), mapTileType);

                MapTile parentMapTile = parentMapTileObject.GetComponent<MapTile>();
                childMapTile.nextTiles.Add(parentMapTile);

                CreateMapConnector(parentMapTile, childMapTile);
            }
        }
    }

    private void CreateMiniBossTile(int sector, int y) {
        float xPosSum = 0;
        int yBelow = y - 1 + (sector * floorsPerSector);
        foreach (var childMapTile in mapTiles[yBelow]) {
            xPosSum += childMapTile.transform.position.x;
        }
        float xPosAverage = xPosSum / mapTiles[yBelow].Count;

        Vector2 tilePos = new(xPosAverage, mapTiles[yBelow][0].transform.position.y + 300);
        GameObject parentMapTileObject = CreateMapTile(tilePos, new(mapTiles[y].Count, y + (sector * floorsPerSector)), TileType.MiniBoss);

        foreach (var childMapTile in mapTiles[yBelow]) {
            MapTile parentMapTile = parentMapTileObject.GetComponent<MapTile>();
            childMapTile.nextTiles.Add(parentMapTile);

            CreateMapConnector(parentMapTile, childMapTile);
        }
    }

    private void CreateMapConnector(MapTile parentMapTile, MapTile childMapTile) {
        // Just some random math on how to calculate position, rotation and length of each MapConnector

        Vector2 posDiff = parentMapTile.transform.position - childMapTile.transform.position;
        float posDiffDivision = parentMapTile.tileType == TileType.MiniBoss ? 2.5f : 2;
        float xBetween = childMapTile.transform.position.x + (posDiff.x / posDiffDivision);
        float yBetween = childMapTile.transform.position.y + (posDiff.y / posDiffDivision);
        Vector2 mapConnectorPos = new(xBetween, yBetween);

        float angleX = Mathf.Atan2(posDiff.y, posDiff.x) * Mathf.Rad2Deg;
        float zRotation = angleX - 90f;
        Quaternion rotation = Quaternion.Euler(0f, 0f, zRotation);

        GameObject mapConnector = Instantiate(mapConnectorPrefab, mapConnectorPos, rotation, scrollViewPanel);
        int magnitudeReduction = parentMapTile.tileType == TileType.MiniBoss ? 200 : 120;
        mapConnector.GetComponent<RectTransform>().sizeDelta = new Vector2(150, posDiff.magnitude - magnitudeReduction) * 4;
    }

    private void CreateMapTiles() {
        for (int sector = 0; sector < 3; sector++) {
            for (int y = 0; y < floorsPerSector; y++) {
                mapTiles.Add(new List<MapTile>());
                switch (y) {
                    case 0:
                        CreateStartTiles(sector, y);
                        break;
                    case 1:
                        CreateSplitTiles(sector, y, TileType.Battlefield, true, true);
                        break;
                    case 2:
                        CreateStraightTiles(sector, y, TileType.None);
                        break;
                    case 3:
                        CreateMergeTiles(sector, y, TileType.None, false);
                        break;
                    case 4:
                        CreateStraightTiles(sector, y, TileType.None);
                        break;
                    case 5:
                        CreateSplitTiles(sector, y, TileType.None, false, false);
                        break;
                    case 6:
                        CreateStraightTiles(sector, y, TileType.None);
                        break;
                    case 7:
                        CreateMergeTiles(sector, y, TileType.None, true);
                        break;
                    case 8:
                        CreateSplitTiles(sector, y, TileType.None, false, false);
                        break;
                    case 9:
                        CreateMergeTiles(sector, y, TileType.Campfire, true);
                        break;
                    case 10:
                        CreateMiniBossTile(sector, y);
                        break;
                }
            }
        }
    }
}
