using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
public class Refuge {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"Your other warriors take half damage. This takes the rest";
    }

    public int Trigger(Warrior dealer, int damage) {
        if (GetValue(dealer.stats)) {
            damage = Mathf.CeilToInt(damage / 2f);
        }
        return damage;
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