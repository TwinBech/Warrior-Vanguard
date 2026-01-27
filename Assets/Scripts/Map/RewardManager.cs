using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour {
    string rewardedCardsKey = "rewardedCards";
    public GameObject cardButtonPrefab;
    public Button skipButton;
    public List<Card> rewardedCards = new List<Card>();
    public GameObject rewardPanel;
    public DeckBuilder deckBuilder;

    private void Start() {
        rewardPanel.SetActive(false);
    }

    public void ShowReward(TileType tileType) {
        if (!LevelManager.isAlive || tileType == TileType.Boss) {

            //Achievement
            if (ItemManager.items.Count <= 0) {
                PlayerPrefs.SetInt(PlayerPrefsKeys.poorLooter, 1);
            }

            //Achievement
            if (DeckManager.deck.FindAll((card) => card.stats.rarity == CardRarity.Rare || card.stats.rarity == CardRarity.Legendary).Count == 0) {
                PlayerPrefs.SetInt(PlayerPrefsKeys.theseWereEasierToFind, 1);
            }

            SceneLoader.LoadScene(Scene.GameOver);
            return;
        }

        rewardPanel.SetActive(true);

        // If saved in local storage, load the cards from there, else generate new ones
        if (PlayerPrefs.HasKey(rewardedCardsKey)) {
            LoadRewardOptions();
        } else {
            CardRarity rarity = tileType == TileType.MiniBoss ? CardRarity.Legendary :
                                        CardRarity.Common;

            List<WarriorStats> usedStats = new List<WarriorStats>();
            foreach (Card card in rewardedCards) {
                WarriorStats stats;
                do {
                    stats = CardDatabase.GetRandomCardStats(rarity);
                } while (usedStats.Contains(stats));
                usedStats.Add(stats);
                card.SetStats(stats);
                card.UpdateCardUI();
            }

            SaveRewardOptions();
        }


    }

    public void SelectCard(Card card) {
        deckBuilder.AddCardToDeck(card);
        PlayerPrefs.SetInt(PlayerPrefsKeys.rewardChosen, 1);
        PlayerPrefs.DeleteKey(rewardedCardsKey);
        PlayerPrefs.Save();
        ClosePopup();
    }

    private void SaveRewardOptions() {
        List<string> cardTitlesAndLevels = new();
        foreach (Card card in rewardedCards) {
            cardTitlesAndLevels.Add($"{card.stats.title}_{card.stats.level}");
        }

        string cardData = string.Join(",", cardTitlesAndLevels);
        PlayerPrefs.SetString(rewardedCardsKey, cardData);
        PlayerPrefs.Save();
    }

    private void LoadRewardOptions() {
        string cardData = PlayerPrefs.GetString(rewardedCardsKey);
        string[] cardTitlesAndLevels = cardData.Split(',');

        for (int i = 0; i < rewardedCards.Count; i++) {
            WarriorStats stats = CardDatabase.GetStatsByTitleAndLevel(cardTitlesAndLevels[i]);
            rewardedCards[i].SetStats(stats);
            rewardedCards[i].UpdateCardUI();
        }
    }

    public void SkipReward() {
        PlayerPrefs.SetInt(PlayerPrefsKeys.rewardChosen, 1);
        PlayerPrefs.DeleteKey(rewardedCardsKey);
        PlayerPrefs.Save();
        ClosePopup();
    }

    private void ClosePopup() {
        rewardPanel.SetActive(false);
    }
}
