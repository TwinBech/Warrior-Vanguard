using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Backstab {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"Can attack backwards, dealing double damage (even without moving)";
    }

    public Warrior GetEnemyBehind(Warrior dealer, GridManager gridManager) {
        if (GetValue(dealer.stats)) {
            Warrior neighbor = gridManager.GetWarriorBehindTarget(dealer);

            if (neighbor && neighbor.stats.alignment != dealer.stats.alignment) {
                return neighbor;
            }
        }
        return null;
    }

    public async Task<bool> TriggerAttack(Warrior dealer, GridManager gridManager) {
        if (GetValue(dealer.stats)) {
            Warrior neighbor = GetEnemyBehind(dealer, gridManager);

            if (neighbor) {
                await dealer.Attack(neighbor, true);
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