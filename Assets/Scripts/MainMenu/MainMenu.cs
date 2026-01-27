using UnityEngine;

public class MainMenu : MonoBehaviour {
    public GameObject collection;
    public GameObject achievements;
    public void StartNewGame() {
        DeleteStoredValues();
        SceneLoader.LoadScene(Scene.SummonerSelector);
    }

    public void ContinueGame() {
        ItemManager.LoadAvailableItems();
        ContinueManager.LoadSummoner();
        SceneLoader.LoadScene(Scene.Map);
    }

    public void ExitGame() {
        SceneLoader.ExitGame();
    }

    public void LoadCredits() {
        SceneLoader.LoadScene(Scene.Credits);
    }

    public void ToggleCollection() {
        collection.SetActive(!collection.activeSelf);
    }

    public void ToggleAchievements() {
        achievements.SetActive(!achievements.activeSelf);
    }

    void DeleteStoredValues() {
        TileCompleter.currentTileIndex = null;
        DeleteTemporaryPlayerPrefs();
    }

    void DeleteTemporaryPlayerPrefs() {
        //Delete all keys, and reassign the permanent keys afterwards
        int humanExp = ExperienceManager.GetExperience(Genre.Human);
        int elvesExp = ExperienceManager.GetExperience(Genre.Elves);
        int undeadExp = ExperienceManager.GetExperience(Genre.Undead);
        int underworldExp = ExperienceManager.GetExperience(Genre.Underworld);

        int humanLevel = ExperienceManager.GetLevel(Genre.Human);
        int elvesLevel = ExperienceManager.GetLevel(Genre.Elves);
        int undeadLevel = ExperienceManager.GetLevel(Genre.Undead);
        int underworldLevel = ExperienceManager.GetLevel(Genre.Underworld);

        int humanWins = ProgressHelper.GetWins(Genre.Human);
        int elvesWins = ProgressHelper.GetWins(Genre.Elves);
        int undeadWins = ProgressHelper.GetWins(Genre.Undead);
        int underworldWins = ProgressHelper.GetWins(Genre.Underworld);

        int adrenalineRush = PlayerPrefs.GetInt(PlayerPrefsKeys.adrenalineRush, 0);
        int swarm = PlayerPrefs.GetInt(PlayerPrefsKeys.swarm, 0);
        int controllingTheBattlefield = PlayerPrefs.GetInt(PlayerPrefsKeys.controllingTheBattlefield, 0);
        int triFlame = PlayerPrefs.GetInt(PlayerPrefsKeys.triFlame, 0);
        int safetyFirst = PlayerPrefs.GetInt(PlayerPrefsKeys.safetyFirst, 0);
        int spookyScarySkeletons = PlayerPrefs.GetInt(PlayerPrefsKeys.spookyScarySkeletons, 0);
        int poorLooter = PlayerPrefs.GetInt(PlayerPrefsKeys.poorLooter, 0);
        int theseWereEasierToFind = PlayerPrefs.GetInt(PlayerPrefsKeys.theseWereEasierToFind, 0);
        int carefulSpender = PlayerPrefs.GetInt(PlayerPrefsKeys.carefulSpender, 0);
        int saveEveryResource = PlayerPrefs.GetInt(PlayerPrefsKeys.saveEveryResource, 0);
        int livingOnTheEdge = PlayerPrefs.GetInt(PlayerPrefsKeys.livingOnTheEdge, 0);
        int flawless = PlayerPrefs.GetInt(PlayerPrefsKeys.flawless, 0);
        int heroPowerDeactivated = PlayerPrefs.GetInt(PlayerPrefsKeys.heroPowerDeactivated, 0);

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Human), humanExp);
        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Elves), elvesExp);
        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Undead), undeadExp);
        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Underworld), underworldExp);

        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Human), humanLevel);
        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Elves), elvesLevel);
        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Undead), undeadLevel);
        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Underworld), underworldLevel);

        PlayerPrefs.SetInt(ProgressHelper.WinsKey(Genre.Human), humanWins);
        PlayerPrefs.SetInt(ProgressHelper.WinsKey(Genre.Elves), elvesWins);
        PlayerPrefs.SetInt(ProgressHelper.WinsKey(Genre.Undead), undeadWins);
        PlayerPrefs.SetInt(ProgressHelper.WinsKey(Genre.Underworld), underworldWins);

        PlayerPrefs.SetInt(PlayerPrefsKeys.adrenalineRush, adrenalineRush);
        PlayerPrefs.SetInt(PlayerPrefsKeys.swarm, swarm);
        PlayerPrefs.SetInt(PlayerPrefsKeys.controllingTheBattlefield, controllingTheBattlefield);
        PlayerPrefs.SetInt(PlayerPrefsKeys.triFlame, triFlame);
        PlayerPrefs.SetInt(PlayerPrefsKeys.safetyFirst, safetyFirst);
        PlayerPrefs.SetInt(PlayerPrefsKeys.spookyScarySkeletons, spookyScarySkeletons);
        PlayerPrefs.SetInt(PlayerPrefsKeys.poorLooter, poorLooter);
        PlayerPrefs.SetInt(PlayerPrefsKeys.theseWereEasierToFind, theseWereEasierToFind);
        PlayerPrefs.SetInt(PlayerPrefsKeys.carefulSpender, carefulSpender);
        PlayerPrefs.SetInt(PlayerPrefsKeys.saveEveryResource, saveEveryResource);
        PlayerPrefs.SetInt(PlayerPrefsKeys.livingOnTheEdge, livingOnTheEdge);
        PlayerPrefs.SetInt(PlayerPrefsKeys.flawless, flawless);
        PlayerPrefs.SetInt(PlayerPrefsKeys.heroPowerDeactivated, heroPowerDeactivated);
        PlayerPrefs.Save();
    }
}
