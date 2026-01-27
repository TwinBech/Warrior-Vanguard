using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class SummonerSelectionManager : MonoBehaviour {
    public TMP_Text summonerDescriptionText;
    public GameObject summonerSelectionPanel;
    public Button summoner1Button;
    public Button summoner2Button;
    public GameObject summoner2LockPanel;
    public Button startButton;
    public Card card;
    public Slider humanExpSlider;
    public Slider elvesExpSlider;
    public Slider undeadExpSlider;
    public Slider underworldExpSlider;
    public TMP_Text humanExpText;
    public TMP_Text elvesExpText;
    public TMP_Text undeadExpText;
    public TMP_Text underworldExpText;
    public GameObject elvesLockPanel;
    public GameObject undeadLockPanel;
    public GameObject underworldLockPanel;
    List<SummonerData> availableSummoners;
    SummonerData selectedSummoner;
    int summoner1Index;
    int summoner2Index;

    void Start() {
        ToggleSummonerSelectionPanel(false);
        UpdateLockedSummoners();
        UpdateExpSliders(Genre.Human);
        UpdateExpSliders(Genre.Elves);
        UpdateExpSliders(Genre.Undead);
        UpdateExpSliders(Genre.Underworld);

        availableSummoners = SummonerManager.GetAvailableSummoners();
    }

    void UpdateLockedSummoners() {
        elvesLockPanel.SetActive(ProgressHelper.GetWins(Genre.Human) <= 0);
        undeadLockPanel.SetActive(ProgressHelper.GetWins(Genre.Elves) <= 0);
        underworldLockPanel.SetActive(ProgressHelper.GetWins(Genre.Undead) <= 0);
    }

    void UpdateExpSliders(Genre genre) {
        Slider expSlider = humanExpSlider;
        TMP_Text expText = humanExpText;
        switch (genre) {
            case Genre.Human:
                expSlider = humanExpSlider;
                expText = humanExpText;
                break;
            case Genre.Elves:
                expSlider = elvesExpSlider;
                expText = elvesExpText;
                break;
            case Genre.Undead:
                expSlider = undeadExpSlider;
                expText = undeadExpText;
                break;
            case Genre.Underworld:
                expSlider = underworldExpSlider;
                expText = underworldExpText;
                break;
        }

        expSlider.maxValue = ExperienceManager.IsMaxLevel(genre) ? 1 : ExperienceManager.GetXpForNextLevel(genre);
        expSlider.value = ExperienceManager.IsMaxLevel(genre) ? 1 : ExperienceManager.GetExperience(genre);
        expText.text = $"Level {ExperienceManager.GetLevel(genre)}";
    }

    public void SelectClass(int index) {
        ToggleSummonerSelectionPanel(true);
        startButton.interactable = false;

        summoner1Index = index * 2;
        summoner2Index = (index * 2) + 1;

        summoner1Button.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Summoners/{availableSummoners[summoner1Index].title}");
        summoner2Button.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Summoners/{availableSummoners[summoner2Index].title}");
        summoner2LockPanel.SetActive(ExperienceManager.GetLevel(availableSummoners[summoner2Index].genre) < 3);
    }

    public void ClickSummoner(int index) {
        startButton.interactable = true;

        summonerDescriptionText.text = index == 0 ?
        availableSummoners[summoner1Index].description :
        availableSummoners[summoner2Index].description;

        selectedSummoner = availableSummoners[index + summoner1Index];
        PlayerPrefs.SetInt(PlayerPrefsKeys.summonerIndex, index + summoner1Index);
        PlayerPrefs.Save();
        FriendlySummoner.summonerData = selectedSummoner;

        // Set opacity of clicked button to 1 and the other to 0.5
        Button pressedButton = index == 0 ? summoner1Button : summoner2Button;
        Button otherButton = index == 0 ? summoner2Button : summoner1Button;

        pressedButton.image.color = ColorPalette.GetColor(ColorEnum.White);
        otherButton.image.color = ColorPalette.AddTransparency(otherButton.image.color, 50);
    }

    public void ClickStart() {
        //TEST
        for (int i = 0; i < 10; i++) {
            card.SetStats(CardDatabase.allCards[i]);
            DeckManager.AddCard(card);
        }

        // StartingDecks.SetStartingDeck(selectedSummoner.title, card);
        ItemManager.InitAvailableItems();
        SceneLoader.LoadScene(Scene.Map);
    }

    public void ToggleSummonerSelectionPanel(bool shouldShow) {
        if (summonerSelectionPanel) summonerSelectionPanel.SetActive(shouldShow);
    }

    public void ReturnToMainMenu() {
        SceneLoader.LoadScene(Scene.MainMenu);
    }

    public void ResetLevelsClicked() {
        ExperienceManager.ResetAllLevels();
        UpdateExpSliders(Genre.Human);
        UpdateExpSliders(Genre.Elves);
        UpdateExpSliders(Genre.Undead);
        UpdateExpSliders(Genre.Underworld);
    }

    public void AddExpClicked() {
        ExperienceManager.AddExperience(Genre.Human, 90);
        ExperienceManager.AddExperience(Genre.Elves, 90);
        ExperienceManager.AddExperience(Genre.Undead, 90);
        ExperienceManager.AddExperience(Genre.Underworld, 90);
        UpdateExpSliders(Genre.Human);
        UpdateExpSliders(Genre.Elves);
        UpdateExpSliders(Genre.Undead);
        UpdateExpSliders(Genre.Underworld);
    }
}