public class FierceIronclaw {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 7, 7 },
            strength = new int[] { 5, 6 },
            health = new int[] { 12, 15 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Harpy,
            rarity = CardRarity.Rare,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.flying.Add();
        ability.backstab.Add();
        ability.bleed.Add();

        return stats;
    }
}