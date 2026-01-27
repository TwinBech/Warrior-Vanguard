using System.Collections.Generic;
using UnityEngine;

public static class ContinueManager {
    public static void LoadSummoner() {
        List<SummonerData> availableSummoners = SummonerManager.GetAvailableSummoners();

        int index = PlayerPrefs.GetInt(PlayerPrefsKeys.summonerIndex, 0);
        SummonerData selectedSummoner = availableSummoners[index];
        FriendlySummoner.summonerData = selectedSummoner;
    }

    public static void ReturnToMainMenu() {
        SceneLoader.LoadScene(Scene.MainMenu);
    }
}