using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Poisoned {
    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{Keyword.Overturn}: Take {GetValue(stats)} magical damage";
    }

    public async Task<bool> TriggerOverturn(Warrior target) {
        if (GetValue(target.stats) > 0) {
            await target.TakeDamage(null, GetValue(target.stats), DamageType.Magical, DamageSource.Poisoned);
            return true;
        }
        return false;
    }

    int[] value = new int[] { 0, 0 };

    public int GetValue(WarriorStats stats) {
        return value[stats.level];
    }

    public void Add(int unupgradedValue, int upgradedValue) {
        int[] newValues = new int[] { unupgradedValue, upgradedValue };
        for (int i = 0; i < 2; i++) {
            if (value[i] < newValues[i]) {
                value[i] = newValues[i];
            }
        }
    }

    public void Add(int value) {
        Add(value, value);
    }

    public void Remove() {
        for (int i = 0; i < 2; i++) {
            value[i] = 0;
        }
    }

    public string GetTitle(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{GetAbilityName()}: {GetValue(stats)}\n";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }

    public BuffType buffType = BuffType.Debuff;
}