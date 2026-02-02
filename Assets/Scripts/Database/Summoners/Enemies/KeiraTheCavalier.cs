using System;
using System.Collections.Generic;

public class KeiraTheCavalier : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "Chaarge!",
            health = 20,
            alignment = Alignment.Enemy,
            difficulty = 1,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        SetItems();
        return new List<WarriorStats>() {
            new UnicornFoal().GetStats(),
            new UnicornFoal().GetStats(),
            new UnicornFoal().GetStats(),
            new UnicornFoal().GetStats(),
            new LightPiercer().GetStats(),
            new LightPiercer().GetStats(),
            new LightPiercer().GetStats(),
            new LightPiercer().GetStats(),
            new ForestPrism().GetStats(),
            new Centoffer().GetStats(),
            new Centoffer().GetStats(),
            new Centoffer().GetStats(),
            new Centoffer().GetStats(),
            new Centaura().GetStats(),
            new SkeletonRider().GetStats(),
            new SkeletonRider().GetStats(),
            new SkeletonRider().GetStats(),
            new SkeletonRider().GetStats(),
       };
    }

    void SetItems() {
        Type itemType = typeof(HorseShoe);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}