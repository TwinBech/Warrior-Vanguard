public class LakeSerpant {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 4, 4 },
            strength = new int[] { 2, 3 },
            health = new int[] { 6, 6 },
            speed = 2,
            range = 1,
            damageType = DamageType.Physical,
            race = Race.Hydra,
            rarity = CardRarity.Common,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.whirlwind.Add();
        ability.regeneration.Add(2, 3);

        return stats;
    }
}