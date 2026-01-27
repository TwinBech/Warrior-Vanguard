using UnityEngine;
using System.Collections.Generic;

public static class LevelManager {
    private static List<MapTile> accessedTiles = new();
    public static bool isAlive = true;

    public static void CompleteLevel() {
        //Achievement
        if (FriendlySummoner.currentHealth == 1) {
            PlayerPrefs.SetInt(PlayerPrefsKeys.adrenalineRush, 1);
            PlayerPrefs.Save();
        }

        TileCompleter.MarkTileAsCompleted();
        GoldManager.AddGold(50);
        ExperienceManager.AddTempExperience(25);


        SceneLoader.LoadScene(Scene.Map);
        ItemManager.enemyItem = null;
    }

    public static void LoseLevel() {
        isAlive = false;
        SceneLoader.LoadScene(Scene.GameOver);
    }

    public static void SetCurrentTile(MapTile tile) {
        accessedTiles.Add(tile);
    }

    public static List<MapTile> getAccessedTiles() {
        return accessedTiles;
    }
}
