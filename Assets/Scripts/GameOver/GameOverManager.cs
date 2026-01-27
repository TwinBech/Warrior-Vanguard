using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GameOver : MonoBehaviour {
    public TMP_Text GameOverText;
    public Image summonerImage;
    public Slider expSlider;
    public TMP_Text expText;
    public GameObject levelUpPanel;
    public Button MainMenuButton;
    public Card cardUnlocked1;
    public Card cardUnlocked2;
    public Card cardUnlocked3;

    private void Start() {
        levelUpPanel.SetActive(false);
        MainMenuButton.interactable = false;
        if (LevelManager.isAlive) {
            GameOverText.text = "You Win! Good job!";
            ProgressHelper.WinGame(FriendlySummoner.summonerData.genre);
        } else {
            GameOverText.text = "You Lost! Sucks to be you..";
        }
        summonerImage.sprite = Resources.Load<Sprite>($"Images/Summoners/{FriendlySummoner.summonerData.title}");
        UpdateExpAnimation();
    }

    async private void UpdateExpAnimation() {
        Genre genre = FriendlySummoner.summonerData.genre;
        expText.text = $"Level {ExperienceManager.GetLevel(genre)}";

        int startValue = ExperienceManager.GetExperience(genre);
        int targetValue = startValue + ExperienceManager.GetTempExperience();
        if (targetValue >= ExperienceManager.GetXpForNextLevel(genre)) {
            targetValue = ExperienceManager.GetXpForNextLevel(genre);
        }
        ExperienceManager.AddTempExperience(startValue - targetValue); // Decrease temp XP for next level

        if (ExperienceManager.IsMaxLevel(genre)) {
            expSlider.value = expSlider.maxValue;
            return;
        }

        expSlider.value = startValue;
        expSlider.maxValue = ExperienceManager.GetXpForNextLevel(genre);

        float animationTime = 1.5f;
        float elapsedTime = 0f;

        while (elapsedTime < animationTime) {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, targetValue, elapsedTime / animationTime);
            expSlider.value = newValue;

            await Task.Yield();
        }

        expSlider.value = targetValue;
        if (targetValue >= ExperienceManager.GetXpForNextLevel(genre) && !ExperienceManager.IsMaxLevel(genre)) {
            ExperienceManager.AddExperience(genre, targetValue - startValue);
            ShowLevelUpPanel();
        } else {
            ExperienceManager.AddExperience(genre, targetValue - startValue);
            MainMenuButton.interactable = true;
        }
    }

    private void ShowLevelUpPanel() {
        levelUpPanel.SetActive(true);
        List<WarriorStats> unlockedCardsStats = CardDatabase.allCards.FindAll(card => card.genre == FriendlySummoner.summonerData.genre && card.levelUnlocked == ExperienceManager.GetLevel(FriendlySummoner.summonerData.genre));
        cardUnlocked1.SetStats(unlockedCardsStats[0]);
        cardUnlocked2.SetStats(unlockedCardsStats[1]);
        cardUnlocked3.SetStats(unlockedCardsStats[2]);
        cardUnlocked1.UpdateCardUI();
        cardUnlocked2.UpdateCardUI();
        cardUnlocked3.UpdateCardUI();
    }

    public void LevelUpContinueButtonPressed() {
        levelUpPanel.SetActive(false);
        if (ExperienceManager.GetTempExperience() > 0) {
        }
        UpdateExpAnimation();
    }

    public void LoadMainMenu() {
        SceneLoader.LoadScene(Scene.MainMenu);
    }
}