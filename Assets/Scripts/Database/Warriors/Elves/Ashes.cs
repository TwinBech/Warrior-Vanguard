public class Ashes {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 0, 0 },
            strength = new int[] { 0, 0 },
            health = new int[] { 1, 1 },
            speed = 0,
            range = 0,
            damageType = DamageType.Physical,
            race = Race.Dragon,
            rarity = CardRarity.None,
            genre = Genre.Elves,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.rebirth.Add();

        return stats;
    }
}