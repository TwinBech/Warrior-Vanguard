using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Guard {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"Can attack a random nearby enemy (even without moving)";
    }

    public Warrior GetRandomNearbyEnemy(Warrior dealer, GridManager gridManager) {
        if (GetValue(dealer.stats)) {
            List<Warrior> enemies = gridManager.GetNearbyEnemies(dealer);
            if (enemies.Count == 0) return null;

            Warrior randomEnemy = Rng.Entry(enemies);
            return randomEnemy;
        }
        return null;
    }

    public async Task<bool> TriggerAttack(Warrior dealer, GridManager gridManager) {
        if (GetValue(dealer.stats)) {
            Warrior nearbyEnemy = GetRandomNearbyEnemy(dealer, gridManager);

            if (nearbyEnemy) {
                await dealer.Attack(nearbyEnemy);
                dealer.stats.attackedThisTurn = true;
                return true;
            }
        }
        return false;
    }

    bool[] value = new bool[] { false, false };

    public bool GetValue(WarriorStats stats) {
        return value[stats.level];
    }

    public void Add(bool unupgradedValue, bool upgradedValue) {
        bool[] newValues = new bool[] { unupgradedValue, upgradedValue };
        for (int i = 0; i < 2; i++) {
            value[i] = newValues[i];
        }
    }

    public void Add() {
        Add(true, true);
    }

    public void Remove() {
        Add(false, false);
    }

    public string GetTitle(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{GetAbilityName()}\n";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }

    public BuffType buffType = BuffType.None;
}