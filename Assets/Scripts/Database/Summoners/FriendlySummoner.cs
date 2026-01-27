using UnityEngine;

public static class FriendlySummoner {
    public static int maxHealth = 70;
    public static int currentHealth = 70;
    public static int extraDeploymentArea = 0;
    public static SummonerData summonerData = new();
    private static string healthKey = "healthKey";
    private static string maxHealthKey = "maxHealthKey";

    public static void GainHealth(int health) {
        currentHealth += health;
        PlayerPrefs.SetInt(healthKey, currentHealth);
        PlayerPrefs.Save();
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    public static void LoseHealth(int health) {
        currentHealth -= health;
        PlayerPrefs.SetInt(healthKey, currentHealth);
        PlayerPrefs.Save();
        if (currentHealth < 0) {
            LevelManager.isAlive = false;
            SceneLoader.LoadScene(Scene.GameOver);
        }
    }

    public static void GainMaxHealth(int health) {
        maxHealth += health;
        PlayerPrefs.SetInt(maxHealthKey, maxHealth);
        PlayerPrefs.Save();
    }

    public static void LoseMaxHealth(int health) {
        maxHealth -= health;
        PlayerPrefs.SetInt(maxHealthKey, maxHealth);
        PlayerPrefs.Save();
    }

    public static int GetHealth() {
        if (PlayerPrefs.HasKey(healthKey)) {
            return PlayerPrefs.GetInt(healthKey);
        }
        return currentHealth;
    }

    public static int GetMaxHealth() {
        if (PlayerPrefs.HasKey(maxHealthKey)) {
            return PlayerPrefs.GetInt(maxHealthKey);
        }
        return maxHealth;
    }
}