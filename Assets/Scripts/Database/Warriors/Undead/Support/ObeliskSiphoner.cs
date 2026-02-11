public class ObeliskSiphoner {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 6, 6 },
            strength = new int[] { 1, 2 },
            health = new int[] { 8, 10 },
            speed = 0,
            range = 0,
            damageType = DamageType.Magical,
            race = Race.Support,
            rarity = CardRarity.Rare,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.construct.Add();
        ability.unstableEnergy.Add();
        ability.stealEssence.Add();

        return stats;
    }
}