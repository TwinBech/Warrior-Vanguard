
public static class StartingDecks {
    public enum SummonerType {
        Human1,
        Human2,
        Elven1,
        Elven2,
        Undead1,
        Undead2,
        Underworld1,
        Underworld2
    }

    public static void SetStartingDeck(string summonerType, Card card) {
        switch (summonerType) {
            case "HumanSummoner1":
                AddCardToDeck(new LightDragon().GetStats(), card);
                AddCardToDeck(new Peasant().GetStats(), card);
                AddCardToDeck(new Peasant().GetStats(), card);
                AddCardToDeck(new Defender().GetStats(), card);
                AddCardToDeck(new Defender().GetStats(), card);
                AddCardToDeck(new Squire().GetStats(), card);
                // AddCardToDeck(new Archer().GetStats(), card);
                // AddCardToDeck(new Archer().GetStats(), card);
                AddCardToDeck(new YoungPriestess().GetStats(), card);
                AddCardToDeck(new HeavyRider().GetStats(), card);
                AddCardToDeck(new DrawfulHobbyist().GetStats(), card);
                AddCardToDeck(new MarketingManager().GetStats(), card);
                AddCardToDeck(new HolyFlame().GetStats(), card);
                AddCardToDeck(new HolyFlame().GetStats(), card);
                AddCardToDeck(new GuidingStrength().GetStats(), card);
                AddCardToDeck(new HolyEmpower().GetStats(), card);
                break;
            case "HumanSummoner2":
                AddCardToDeck(new Mario().GetStats(), card);
                AddCardToDeck(new Mario().GetStats(), card);
                AddCardToDeck(new Mario().GetStats(), card);
                AddCardToDeck(new Mario().GetStats(), card);
                AddCardToDeck(new Mario().GetStats(), card);
                AddCardToDeck(new Luigi().GetStats(), card);
                AddCardToDeck(new Luigi().GetStats(), card);
                AddCardToDeck(new Luigi().GetStats(), card);
                AddCardToDeck(new Luigi().GetStats(), card);
                AddCardToDeck(new Luigi().GetStats(), card);
                break;
            case "ElvenSummoner1":
                AddCardToDeck(new CoalbeardSketcher().GetStats(), card);
                AddCardToDeck(new CoalbeardSketcher().GetStats(), card);
                AddCardToDeck(new Watchtower().GetStats(), card);
                AddCardToDeck(new Centoffer().GetStats(), card);
                AddCardToDeck(new ClubCrasher().GetStats(), card);
                AddCardToDeck(new WoodElf().GetStats(), card);
                AddCardToDeck(new WoodElf().GetStats(), card);
                AddCardToDeck(new BranchManager().GetStats(), card);
                AddCardToDeck(new WerewolfRunner().GetStats(), card);
                AddCardToDeck(new ShinyButterfly().GetStats(), card);
                AddCardToDeck(new Pinkxie().GetStats(), card);
                AddCardToDeck(new Embiggen().GetStats(), card);
                AddCardToDeck(new Embiggen().GetStats(), card);
                AddCardToDeck(new Thorns().GetStats(), card);
                AddCardToDeck(new Haste().GetStats(), card);
                break;
            case "ElvenSummoner2":
                AddCardToDeck(new CoalbeardSketcher().GetStats(), card);
                AddCardToDeck(new CoalbeardSketcher().GetStats(), card);
                AddCardToDeck(new Watchtower().GetStats(), card);
                AddCardToDeck(new Centoffer().GetStats(), card);
                AddCardToDeck(new ClubCrasher().GetStats(), card);
                AddCardToDeck(new WoodElf().GetStats(), card);
                AddCardToDeck(new WoodElf().GetStats(), card);
                AddCardToDeck(new BranchManager().GetStats(), card);
                AddCardToDeck(new WerewolfRunner().GetStats(), card);
                AddCardToDeck(new ShinyButterfly().GetStats(), card);
                AddCardToDeck(new Pinkxie().GetStats(), card);
                AddCardToDeck(new Embiggen().GetStats(), card);
                AddCardToDeck(new Embiggen().GetStats(), card);
                AddCardToDeck(new Thorns().GetStats(), card);
                AddCardToDeck(new Haste().GetStats(), card);
                break;
            case "UndeadSummoner1":
                AddCardToDeck(new ZombieMinion().GetStats(), card);
                AddCardToDeck(new ZombieMinion().GetStats(), card);
                AddCardToDeck(new BoneGnawer().GetStats(), card);
                AddCardToDeck(new EldritchSorcerer().GetStats(), card);
                AddCardToDeck(new SkeletonArcher().GetStats(), card);
                AddCardToDeck(new SkeletonArcher().GetStats(), card);
                AddCardToDeck(new SkeletonWarrior().GetStats(), card);
                AddCardToDeck(new VampireApprentice().GetStats(), card);
                AddCardToDeck(new WindDancer().GetStats(), card);
                AddCardToDeck(new DarkRider().GetStats(), card);
                AddCardToDeck(new DarkRider().GetStats(), card);
                AddCardToDeck(new PoisonPotion().GetStats(), card);
                AddCardToDeck(new PoisonPotion().GetStats(), card);
                AddCardToDeck(new Arise().GetStats(), card);
                AddCardToDeck(new AgingCurse().GetStats(), card);
                break;
            case "UndeadSummoner2":
                AddCardToDeck(new ZombieMinion().GetStats(), card);
                AddCardToDeck(new ZombieMinion().GetStats(), card);
                AddCardToDeck(new BoneGnawer().GetStats(), card);
                AddCardToDeck(new EldritchSorcerer().GetStats(), card);
                AddCardToDeck(new SkeletonArcher().GetStats(), card);
                AddCardToDeck(new SkeletonArcher().GetStats(), card);
                AddCardToDeck(new SkeletonWarrior().GetStats(), card);
                AddCardToDeck(new VampireApprentice().GetStats(), card);
                AddCardToDeck(new WindDancer().GetStats(), card);
                AddCardToDeck(new DarkRider().GetStats(), card);
                AddCardToDeck(new DarkRider().GetStats(), card);
                AddCardToDeck(new PoisonPotion().GetStats(), card);
                AddCardToDeck(new PoisonPotion().GetStats(), card);
                AddCardToDeck(new Arise().GetStats(), card);
                AddCardToDeck(new AgingCurse().GetStats(), card);
                break;
            case "UnderworldSummoner1":
                AddCardToDeck(new FlameWarden().GetStats(), card);
                AddCardToDeck(new FriendlyFiend().GetStats(), card);
                AddCardToDeck(new RockThrower().GetStats(), card);
                AddCardToDeck(new RockThrower().GetStats(), card);
                AddCardToDeck(new SketchySketcher().GetStats(), card);
                AddCardToDeck(new HappyHarpy().GetStats(), card);
                AddCardToDeck(new IntrusiveTermite().GetStats(), card);
                AddCardToDeck(new IntrusiveTermite().GetStats(), card);
                AddCardToDeck(new CrispRat().GetStats(), card);
                AddCardToDeck(new HellHound().GetStats(), card);
                AddCardToDeck(new HellHound().GetStats(), card);
                AddCardToDeck(new Firebolt().GetStats(), card);
                AddCardToDeck(new Firebolt().GetStats(), card);
                AddCardToDeck(new MoltenBlade().GetStats(), card);
                AddCardToDeck(new RainOfFire().GetStats(), card);
                break;
            case "UnderworldSummoner2":
                AddCardToDeck(new FlameWarden().GetStats(), card);
                AddCardToDeck(new FriendlyFiend().GetStats(), card);
                AddCardToDeck(new RockThrower().GetStats(), card);
                AddCardToDeck(new RockThrower().GetStats(), card);
                AddCardToDeck(new SketchySketcher().GetStats(), card);
                AddCardToDeck(new HappyHarpy().GetStats(), card);
                AddCardToDeck(new IntrusiveTermite().GetStats(), card);
                AddCardToDeck(new IntrusiveTermite().GetStats(), card);
                AddCardToDeck(new CrispRat().GetStats(), card);
                AddCardToDeck(new HellHound().GetStats(), card);
                AddCardToDeck(new HellHound().GetStats(), card);
                AddCardToDeck(new Firebolt().GetStats(), card);
                AddCardToDeck(new Firebolt().GetStats(), card);
                AddCardToDeck(new MoltenBlade().GetStats(), card);
                AddCardToDeck(new RainOfFire().GetStats(), card);
                break;
        }
    }

    static void AddCardToDeck(WarriorStats stats, Card card) {
        card.SetStats(stats);
        DeckManager.AddCard(card);
    }
}