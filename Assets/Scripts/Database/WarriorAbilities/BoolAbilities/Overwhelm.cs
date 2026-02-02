using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Overwhelm {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{Keyword.Kill}: Excess damage is dealt to the enemy summoner";
    }

    public async Task<bool> TriggerKill(Warrior dealer, Summoner enemySummoner, int excessDamage, GridManager gridManager) {
        if (GetValue(dealer.stats)) {
            await enemySummoner.TakeDamage(dealer, excessDamage, gridManager, dealer.stats.damageType);
            return true;
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