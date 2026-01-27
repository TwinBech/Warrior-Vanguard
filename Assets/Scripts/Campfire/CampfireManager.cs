using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CampfireManager : MonoBehaviour {
    public GameObject restButton;
    public GameObject upgradeCardButton;
    public GameObject upgradeCardPanel;
    public GameObject cardUpgradeView;
    public Card unUpgradedCardView;
    public Card upgradedCardView;
    public GameObject cardPrefab;
    public TMP_Text infoText;
    public TMP_Text upgradeCardText;
    public Transform deckListContainer;
    public DeckBuilder deckBuilder;
    public TMP_Text restButtonText;
    public SummonerManager summonerManager;
    private int healAmount;
    private Card cardToUpgrade;
    private void Start() {
        upgradeCardPanel.SetActive(false);
        cardUpgradeView.SetActive(false);
        healAmount = (int)(FriendlySummoner.GetMaxHealth() * 0.3);
        restButtonText.text = $"Rest - Heal {healAmount} HP";
    }

    public void RestButtonClicked() {
        FriendlySummoner.GainHealth(healAmount);
        infoText.text = $"You have rested and healed {healAmount} HP";
        FinishResting();
    }

    public void UpgradeCardButtonClicked() {
        upgradeCardPanel.SetActive(true);
        foreach (Transform child in deckListContainer) {
            Destroy(child.gameObject);
        }

        foreach (WarriorStats stats in DeckManager.GetDeck()) {
            if (stats.level > 0) continue;
            GameObject cardItem = Instantiate(cardPrefab, deckListContainer);
            cardItem.transform.localScale = new Vector2(1.5f, 1.5f);
            cardItem.GetComponent<DragDrop>().enabled = false;
            cardItem.GetComponent<ObjectAnimation>().enabled = false;
            Card card = cardItem.GetComponent<Card>();
            card.SetStats(stats);
            card.UpdateCardUI();
            card.GetComponent<Button>().onClick.AddListener(() => ClickCardToUpgrade(card));
        }
    }

    private void ClickCardToUpgrade(Card card) {
        cardToUpgrade = card;
        unUpgradedCardView.SetStats(card.stats);
        unUpgradedCardView.UpdateCardUI();

        upgradedCardView.SetStats(card.stats);
        upgradedCardView.stats.level += 1;
        upgradedCardView.UpdateCardUI();

        upgradeCardText.text = $"Upgrade {card.stats.title}?";
        cardUpgradeView.SetActive(true);
    }

    public void UpgradeCard() {
        deckBuilder.UpgradeCardInDeck(cardToUpgrade);
        upgradeCardPanel.SetActive(false);
        cardUpgradeView.SetActive(false);
        infoText.text = $"{cardToUpgrade.stats.title} has been upgraded!";
        FinishResting();
    }

    public void CloseUpgradeCardPanel() {
        upgradeCardPanel.SetActive(false);
    }

    public void CloseCardUpgradeView() {
        cardUpgradeView.SetActive(false);
    }

    public void ReturnButtonClicked() {
        TileCompleter.MarkTileAsCompleted();
        SceneLoader.LoadScene(Scene.Map);
    }

    private void FinishResting() {
        TileCompleter.MarkTileAsCompleted();
        restButton.SetActive(false);
        upgradeCardButton.SetActive(false);
    }
}