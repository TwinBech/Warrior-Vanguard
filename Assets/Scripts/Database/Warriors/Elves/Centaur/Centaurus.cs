public class Centaurus {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 6, 6 },
            strength = new int[] { 6, 8 },
            health = new int[] { 9, 11 },
            speed = 4,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Centaur,
            rarity = CardRarity.Rare,
            genre = Genre.Elves,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.hitAndRun.Add();
        ability.joust.Add();

        return stats;
    }
}