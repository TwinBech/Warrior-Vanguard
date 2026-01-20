public class HydricTheConsumer {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 8, 8 },
            strength = new int[] { 3, 3 },
            health = new int[] { 17, 17 },
            speed = 2,
            range = 1,
            damageType = DamageType.Physical,
            race = Race.Hydra,
            rarity = CardRarity.Legendary,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.whirlwind.Add();
        ability.regeneration.Add(4, 5);
        ability.bloodlust.Add(1, 2);
        ability.cannibalism.Add(2, 3);

        return stats;
    }
}