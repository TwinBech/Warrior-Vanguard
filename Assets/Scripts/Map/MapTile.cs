using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTile : MonoBehaviour {
    public bool isUnlocked = false;
    public bool isCompleted = false;
    public List<MapTile> nextTiles;
    public Button tileButton;
    public GameObject checkmark;
    public TileType tileType;
    public int level = 1;
    public Vector2 gridIndex;

    private void Start() {
        tileButton = GetComponent<Button>();
        UpdateTileAccess();
        tileButton.onClick.AddListener(TileClicked);
    }

    public void SetUnlocked(bool state) {
        isUnlocked = state;
        UpdateTileAccess();
    }

    public void MarkAsCompleted(bool state) {
        isCompleted = state;
        checkmark.SetActive(state);
    }

    public void UnlockNextTiles() {
        foreach (MapTile tile in nextTiles) {
            tile.isUnlocked = true;
            tile.UpdateTileAccess();
        }
    }

    public void UpdateTileAccess() {
        tileButton.interactable = isUnlocked;
        if (isUnlocked) {
            transform.localScale = new(1.25f, 1.25f);
        }
    }

    private void TileClicked() {
        TileManager tileManager = FindFirstObjectByType<TileManager>();
        tileManager.MarkTileAsCurrent(this);

        LevelManager.SetCurrentTile(this);
        PlayerPrefs.SetInt(PlayerPrefsKeys.rewardChosen, 0);

        //TEST - Get summoner with the same image as the tile
        // string enemySummonerName = gameObject.GetComponent<Image>().sprite.name;
        // GameManager.enemySummonerName = enemySummonerName;

        switch (tileType) {
            case TileType.Boss:
                PlayerPrefs.SetInt(PlayerPrefsKeys.flawless_helper, 1);
                PlayerPrefs.SetInt(PlayerPrefsKeys.heroPowerDeactivated_helper, 1);
                GameManager.enemySummonerName = EnemySummoner.GetWorthyEnemySummonerName(level);
                SceneLoader.LoadScene(Scene.Battlefield);
                break;
            case TileType.MiniBoss:
            case TileType.Battlefield:
                PlayerPrefs.SetInt(PlayerPrefsKeys.flawless_helper, 0);
                PlayerPrefs.SetInt(PlayerPrefsKeys.heroPowerDeactivated_helper, 0);
                GameManager.enemySummonerName = EnemySummoner.GetWorthyEnemySummonerName(level);
                SceneLoader.LoadScene(Scene.Battlefield);
                break;
            case TileType.Shop:
                SceneLoader.LoadScene(Scene.Shop);
                break;
            case TileType.Event:
                SceneLoader.LoadScene(Scene.Event);
                break;
            case TileType.Campfire:
                SceneLoader.LoadScene(Scene.Campfire);
                break;
        }
    }
}
