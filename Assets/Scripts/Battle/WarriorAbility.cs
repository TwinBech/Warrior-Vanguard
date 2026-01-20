using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
public class WarriorAbility {


    // Identity abilities
    public Construct construct = new();
    public Flying flying = new();
    public Revive revive = new();
    public LifeSteal lifeSteal = new();
    public LifeTransfer lifeTransfer = new();
    public RaiseDead raiseDead = new();
    public Skeletal skeletal = new();
    public Cannibalism cannibalism = new();
    public Afterlife afterlife = new();
    public Stoneskin stoneskin = new();
    public Rooting rooting = new();
    public FamiliarGround familiarGround = new();
    public FaeMagic faeMagic = new();
    public SapPower sapPower = new();
    public Bleed bleed = new();
    public Joust joust = new();
    public LightningBolt lightningBolt = new();
    public Cleave cleave = new();
    public Spawn spawn = new();
    public Vulnerability vulnerability = new();
    public Enlighten enlighten = new();
    public Intelligence intelligence = new();
    public BloodPact bloodPact = new();
    public CheatDeath cheatDeath = new();
    public Farming farming = new();
    public Whirlwind whirlwind = new();
    public Reload reload = new();
    public Swap swap = new();

    // Common abilities
    public Armor armor = new();
    public Resistance resistance = new();
    public Guard guard = new();
    public Weaken weaken = new();
    public Bloodlust bloodlust = new();
    public Vengeance vengeance = new();
    public Poison poison = new();
    public Retaliate retaliate = new();
    public FirstStrike firstStrike = new();
    public Stealth stealth = new();
    public Splash splash = new();
    public Incorporeal incorporeal = new();
    public FrozenTouch frozenTouch = new();
    public DarkTouch darkTouch = new();
    public HitAndRun hitAndRun = new();
    public Pierce pierce = new();
    public DoubleStrike doubleStrike = new();
    public Multishot multishot = new();
    public Regeneration regeneration = new();
    public Bash bash = new();
    public Heal heal = new();
    public Repair repair = new();
    public Spikes spikes = new();
    public Carnivore carnivore = new();
    public Backstab backstab = new();
    public Explosion explosion = new();
    public Enflame enflame = new();
    public Immolate immolate = new();
    public Inspire inspire = new();
    public Drawing drawing = new();
    public DeathDraw deathDraw = new();
    public Artist artist = new();
    public FriendDiscount friendDiscount = new();
    public FamilyDiscount familyDiscount = new();
    public SelfHarm selfHarm = new();
    public SoulSiphon soulSiphon = new();
    public Immune immune = new();
    public Demolish demolish = new();
    public KnockBack knockBack = new();
    public SoulCollect soulCollect = new();
    public SoulImbue soulImbue = new();
    public StealEssence stealEssence = new();

    // Unique abilities
    public HydraSplit hydraSplit = new();
    public PermaStealth permaStealth = new();
    public DeathCall deathCall = new();
    public BoneSculptor boneSculptor = new();
    public PoisonCloud poisonCloud = new();
    public Possess possess = new();
    public BoneSpread boneSpread = new();
    public WeakeningAura weakeningAura = new();
    public PoisoningAura poisoningAura = new();
    public Firewall firewall = new();
    public CemeteryGates cemeteryGates = new();
    public MassResistance massResistance = new();
    public GreedyStrike greedyStrike = new();
    public ForestStrength forestStrength = new();
    public EvilInspiration evilInspiration = new();
    public ThickSkin thickSkin = new();
    public PhoenixAshes phoenixAshes = new();
    public EternalNightmare eternalNightmare = new();
    public Rebirth rebirth = new();
    public SpellImmunity spellImmunity = new();
    public Sprout sprout = new();
    public SapEnergy sapEnergy = new();
    public Looting looting = new();
    public MassHeal massHeal = new();
    public MassRepair massRepair = new();
    public LushGrounds lushGrounds = new();
    public ForestProtection forestProtection = new();
    public Thunderstorm thunderstorm = new();
    public StaticEntrance staticEntrance = new();
    public HumanShield humanShield = new();
    public Seduce seduce = new();
    public MassEnflame massEnflame = new();
    public MassImmolate massImmolate = new();
    public Silence silence = new();
    public MassSilence massSilence = new();
    public LifeInDeath lifeInDeath = new();
    public DragonRecruiter dragonRecruiter = new();
    public ScrollStudies scrollStudies = new();
    public SummoningSpirits summoningSpirits = new();
    public RaceDiscount raceDiscount = new();
    public MassSelfHarm massSelfHarm = new();
    public Whalecome whalecome = new();
    public Cloak cloak = new();
    public StrengthenByFireAbility strengthenByFireAbility = new();
    public Reckoning reckoning = new();
    public Builder builder = new();
    public MassBuilder massBuilder = new();
    public ForestFriend forestFriend = new();
    public TurnSwap turnSwap = new();
    public Purge purge = new();
    public UnstableEnergy unstableEnergy = new();
    public Refuge refuge = new();

    // Buffs and Debuffs
    public Poisoned poisoned = new();
    public Stunned stunned = new();
    public Rooted rooted = new();
    public Weakened weakened = new();
    public Bleeding bleeding = new();
    public Burning burning = new();
    public Seduced seduced = new();
    public Vulnerable vulnerable = new();

    // Summoner abilities
    public SummonWisp summonWisp = new();
    public SummonMobyRichard summonMobyRichard = new();
    public SummonFlotSam summonFlotSam = new();

    public List<string> GetAbilityText(WarriorStats stats) {
        List<string> returnValue = new();

        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields) {
            object abilityInstance = field.GetValue(this);
            object[] parameters = { stats };
            MethodInfo method = abilityInstance.GetType().GetMethod("GetTitle");

            string description = (string)method.Invoke(abilityInstance, parameters);
            if (description != "") {
                FieldInfo buffField = abilityInstance.GetType().GetField("buffType");
                BuffType buffType = (BuffType)buffField.GetValue(abilityInstance);
                if (buffType == BuffType.Debuff) {
                    description = ColorPalette.AddColorToText(description, ColorPalette.GetColor(ColorEnum.Red));
                }
                returnValue.Add(description);
            }
        }

        return returnValue;
    }

    public void DisplayAbilityTooltip(TooltipManager tooltipManager, WarriorStats stats) {
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields) {
            object abilityInstance = field.GetValue(this);
            object[] parameters = { stats };
            MethodInfo titleMethod = abilityInstance.GetType().GetMethod("GetTitle");
            string title = (string)titleMethod.Invoke(abilityInstance, parameters);
            if (title == "") continue;
            MethodInfo descriptionMethod = abilityInstance.GetType().GetMethod("GetDescription");
            string description = (string)descriptionMethod.Invoke(abilityInstance, parameters);

            tooltipManager.AddTooltip(title, description, 0.5f);
        }
    }

    public void SetWarriorAbility(WarriorAbility ability) {
        var abilityType = typeof(WarriorAbility);
        var fields = abilityType.GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (var field in fields) {
            object fromAbility = field.GetValue(ability);
            object toAbility = field.GetValue(this);

            if (fromAbility == null || toAbility == null) continue;

            var valueField = fromAbility.GetType()
                .GetField("value", BindingFlags.NonPublic | BindingFlags.Instance);

            if (valueField != null) {
                Array value = (Array)valueField.GetValue(fromAbility);

                var cloned = value.Clone();
                valueField.SetValue(toAbility, cloned);
            }
        }
    }
}