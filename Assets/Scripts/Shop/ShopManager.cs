using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    string shopCardsKey = "shopCards";
    string shopItemsKey = "shopItems";
    public List<Card> cardsForSale = new List<Card>();
    public List<Item> itemsForSale = new List<Item>();
    public TMP_Text actionInfoText;
    public TMP_Text removeCardButtonText;
    public Button removeCardButton;
    public GameObject removeCardPanel;
    public Transform deckListContainer;
    public GameObject cardPrefab;
    public DeckBuilder deckBuilder;

    private void Start() {
        PopulateShop();
    }

    void PopulateShop() {
        if (PlayerPrefs.HasKey(shopCardsKey) && PlayerPrefs.HasKey(shopItemsKey)) {
            LoadShop();
        } else {
            List<WarriorStats> tempCards = new List<WarriorStats>(CardDatabase.GetAvailableCards());
            foreach (Card card in cardsForSale) {
                int randomIndex = Random.Range(0, tempCards.Count);
                WarriorStats stats = tempCards[randomIndex];
                card.SetStats(stats);
                card.SetHoverCardFromMap();
                card.UpdateCardUI();
                tempCards.RemoveAt(randomIndex);
            }

            List<Item> tempItems = new List<Item>(ItemManager.availableItems);
            foreach (Item itemForSale in itemsForSale) {
                int randomIndex = Random.Range(0, tempItems.Count);
                Item item = tempItems[randomIndex];
                itemForSale.SetItem(item);
                tempItems.RemoveAt(randomIndex);
            }

            SaveShop();
        }

        UpdateRemoveCardButton();
        removeCardPanel.SetActive(false);
    }

    private void UpdateRemoveCardButton() {
        removeCardButton.interactable = DeckManager.GetDeck().Count > 1 && GoldManager.gold >= GoldManager.RemoveCardCost;
        removeCardButtonText.text = $"Remove Card ({GoldManager.RemoveCardCost} Gold)";
    }

    public void BuyCard(Card card) {
        if (GoldManager.SpendGold(50)) {
            deckBuilder.AddCardToDeck(card);
            cardsForSale.Remove(card);

            card.SetHoverCardFromMap();
            card.HideCard();

            Destroy(card.gameObject);
            SaveShop();
            actionInfoText.text = $"Added {card.stats.displayTitle} to your deck";
        } else {
            actionInfoText.text = $"Not enough gold!";
        }

    }

    public void BuyItem(Item item) {
        if (GoldManager.SpendGold(50)) {
            ItemManager.AddItem(item);
            itemsForSale.Remove(item);

            item.RemoveTooltips();
            Destroy(item.gameObject);
            SaveShop();
            actionInfoText.text = $"Bought {item.displayTitle}";
        } else {
            actionInfoText.text = $"Not enough gold!";
        }
    }

    public void RemoveCardButtonClicked() {
        removeCardPanel.SetActive(true);
        foreach (Transform child in deckListContainer) {
            Destroy(child.gameObject);
        }

        foreach (WarriorStats stats in DeckManager.GetDeck()) {
            GameObject cardItem = Instantiate(cardPrefab, deckListContainer);
            cardItem.transform.localScale = new Vector2(1.5f, 1.5f);
            cardItem.GetComponent<DragDrop>().enabled = false;
            cardItem.GetComponent<ObjectAnimation>().enabled = false;
            Card card = cardItem.GetComponent<Card>();
            card.SetStats(stats);
            card.UpdateCardUI();
            card.GetComponent<Button>().onClick.AddListener(() => RemoveCard(card));
        }
    }

    private void RemoveCard(Card card) {
        deckBuilder.RemoveCardFromDeck(card);
        GoldManager.SpendGold(GoldManager.RemoveCardCost);
        GoldManager.RemoveCardCost += 25;
        UpdateRemoveCardButton();
        removeCardPanel.SetActive(false);
        actionInfoText.text = $"Removed {card.stats.displayTitle} from your deck";
    }

    public void CloseRemoveCardPanel() {
        removeCardPanel.SetActive(false);
    }

    private void SaveShop() {
        List<string> cardTitlesAndLevels = new();
        List<string> itemTitles = new();
        foreach (Card card in cardsForSale) {
            cardTitlesAndLevels.Add($"{card.stats.title}_{card.stats.level}");
        }

        foreach (Item item in itemsForSale) {
            itemTitles.Add($"{item.title}");
        }

        string cardData = string.Join(",", cardTitlesAndLevels);
        PlayerPrefs.SetString(shopCardsKey, cardData);

        string itemData = string.Join(",", itemTitles);
        PlayerPrefs.SetString(shopItemsKey, itemData);

        PlayerPrefs.Save();
    }

    private void LoadShop() {
        string cardData = PlayerPrefs.GetString(shopCardsKey);
        string[] cardTitlesAndLevels = cardData.Split(',');

        for (int i = 0; i < cardsForSale.Count; i++) {
            if (i >= cardTitlesAndLevels.Length) {
                // This happens if cards have already been bought, and then reloading the shop.
                cardsForSale[i].gameObject.SetActive(false);
            } else {
                WarriorStats stats = CardDatabase.GetStatsByTitleAndLevel(cardTitlesAndLevels[i]);
                cardsForSale[i].SetStats(stats);
                cardsForSale[i].SetHoverCardFromMap();
                cardsForSale[i].UpdateCardUI();
            }
        }

        string itemData = PlayerPrefs.GetString(shopItemsKey);
        string[] itemTitles = itemData.Split(',');

        for (int i = 0; i < itemsForSale.Count; i++) {
            if (i >= itemTitles.Length) {
                // This happens if items have already been bought, and then reloading the shop.
                itemsForSale[i].gameObject.SetActive(false);
            } else {
                Item item = ItemManager.GetItemByTitle(itemTitles[i]);
                itemsForSale[i].SetItem(item);
            }
        }
    }

    public void ReturnToMap() {
        PlayerPrefs.DeleteKey(shopCardsKey);
        PlayerPrefs.DeleteKey(shopItemsKey);
        TileCompleter.MarkTileAsCompleted();
        SceneLoader.LoadScene(Scene.Map);
    }
}
