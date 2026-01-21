using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Burnout {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 0, 0 },
            rarity = CardRarity.None,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "Your deck is empty! Your summoner takes damage instead",
            "Your deck is empty! Your summoner takes damage instead"
            },
            race = Race.None,
            genre = Genre.None,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        List<Task> asyncFunctions = new();
        await Task.WhenAll(asyncFunctions);
    }
}