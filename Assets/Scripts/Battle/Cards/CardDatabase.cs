using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public static class CardDatabase {
    public static List<WarriorStats> allCards = new() {
        new Mario().GetStats(),
        new Haven().GetStats(),
        new ObeliskSiphoner().GetStats(),
        new ThePunisher().GetStats(),
        new GraveDanger().GetStats(),
        new CemeteryKeeper().GetStats(),
        new Shoveler().GetStats(),
        new Drawpy().GetStats(),
        new ArmoredMinotaur().GetStats(),
        new Centaudor().GetStats(),
        new Centoffer().GetStats(),
        new Archmagi().GetStats(),
        new DruidOfTheStorm().GetStats(),
        new ElderBeartamer().GetStats(),
        new Hermit().GetStats(),
        new MrHammer().GetStats(),
        new ExperiencedConstructor().GetStats(),
        new DemolitionMan().GetStats(),
        new HomeMechanic().GetStats(),
        new BarrageCannoneer().GetStats(),
        new Ballistarius().GetStats(),
        new HeavyCrossbowman().GetStats(),
        new LightCrossbowman().GetStats(),
        new AngelOfDeath().GetStats(),
        new GrimReaper().GetStats(),
        new JudgementCaller().GetStats(),
        new Boney().GetStats(),
        new FireAtWill().GetStats(),
        new StrengthenByFire().GetStats(),
        new IntrusiveTermite().GetStats(),
        new Arise().GetStats(),
        new MassResurrection().GetStats(),
        new SwarmOfTheNature().GetStats(),
        new ElvenTechniques().GetStats(),
        new SlowDown().GetStats(),
        new SkeletonFetcher().GetStats(),
        new Drawbie().GetStats(),
        new BabyHydra().GetStats(),
        new ShadowSerpant().GetStats(),
        new InfernalReptilian().GetStats(),
        new AlleyStalker().GetStats(),
        new SilentAssasin().GetStats(),
        new NightChaser().GetStats(),
        new WindWalker().GetStats(),
        new StatueOfValor().GetStats(),
        new CastleGate().GetStats(),
        new CannonTower().GetStats(),
        new CherryPicker().GetStats(),
        new CombineHarvester().GetStats(),
        new FirePortal().GetStats(),
        new SummonAid().GetStats(),
        new Reinforcement().GetStats(),
        new Centarcher().GetStats(),
        new MoltenBlade().GetStats(),
        new Berserk().GetStats(),
        new Vampirism().GetStats(),
        new CreativeSoulbender().GetStats(),
        new CoalbeardSketcher().GetStats(),
        new SketchySketcher().GetStats(),
        new ImpressiveArtist().GetStats(),
        new ForbiddenLibrary().GetStats(),
        new DarkRider().GetStats(),
        new DreadKnight().GetStats(),
        new MidnightSoulburner().GetStats(),
        new TheFourHorsemen().GetStats(),
        new GambleGimple().GetStats(),
        new FavoriteChild().GetStats(),
        new FriendlyFiend().GetStats(),
        new ArtProfessorArnold().GetStats(),
        new PencilCraftsman().GetStats(),
        new DrawfulHobbyist().GetStats(),
        new PactConjuring().GetStats(),
        new PentagramEscapee().GetStats(),
        new DemonicAbomination().GetStats(),
        new ArchPainmaker().GetStats(),
        new Sanctuary().GetStats(),
        new DivineShield().GetStats(),
        new Parasite().GetStats(),
        new SpiritualGrounds().GetStats(),
        new MountainDragon().GetStats(),
        new InternationalLibrary().GetStats(),
        new DragonLeader().GetStats(),
        new LightDragon().GetStats(),
        new Battlemonk().GetStats(),
        new LocalBookkeeper().GetStats(),
        new MarketingManager().GetStats(),
        new HRSusan().GetStats(),
        new KarenTheLibrarian().GetStats(),
        new FencingRookie().GetStats(),
        new Vitalblade().GetStats(),
        new GrandDualist().GetStats(),
        new UnmatchedEpeewielder().GetStats(),
        new HellHound().GetStats(),
        new MeltingMagma().GetStats(),
        new FieryAvatar().GetStats(),
        new SuccubusSeducer().GetStats(),
        new FlameWarden().GetStats(),
        new AidFromTheSpirits().GetStats(),
        new CounterStrike().GetStats(),
        new Blind().GetStats(),
        new SharpSight().GetStats(),
        new Thorns().GetStats(),
        new Haste().GetStats(),
        new Embiggen().GetStats(),
        new TimeHealsAllWounds().GetStats(),
        new GuardianAngel().GetStats(),
        new InfernalShooter().GetStats(),
        new HadesCompanion().GetStats(),
        new CerberusPack().GetStats(),
        new Firebreather().GetStats(),
        new Damnation().GetStats(),
        new HardshellPest().GetStats(),
        new CrispRat().GetStats(),
        new Stormseer().GetStats(),
        new MargeTheCharged().GetStats(),
        new LightningCaller().GetStats(),
        new Apprentice().GetStats(),
        new UnstableExplosives().GetStats(),
        new TearsOfTheLight().GetStats(),
        new HolyEmpower().GetStats(),
        new HolyFlame().GetStats(),
        new GuidingStrength().GetStats(),
        new Fireball().GetStats(),
        new Armageddon().GetStats(),
        new RainOfFire().GetStats(),
        new Firebolt().GetStats(),
        new DiveBomber().GetStats(),
        new GriffinCombatant().GetStats(),
        new SkyGlider().GetStats(),
        new EarlyBird().GetStats(),
        new SirSpearmint().GetStats(),
        new MortalLance().GetStats(),
        new HeavyRider().GetStats(),
        new GoodKnight().GetStats(),
        new Bodyguard().GetStats(),
        new Defender().GetStats(),
        new PoiSonRogue().GetStats(),
        new SuspiciousGargoyle().GetStats(),
        new ThirdEyeHarpy().GetStats(),
        new HappyHarpy().GetStats(),
        new FierceIronclaw().GetStats(),
        new SkeletonMage().GetStats(),
        new SkeletonRider().GetStats(),
        new SkeletonWarrior().GetStats(),
        new SkeletonArcher().GetStats(),
        new CorruptedSprite().GetStats(),
        new GlisteringFairy().GetStats(),
        new Pinkxie().GetStats(),
        new ShinyButterfly().GetStats(),
        new Whitefur().GetStats(),
        new WerewolfRunner().GetStats(),
        new NightHunter().GetStats(),
        new MoonProwler().GetStats(),
        new RejuvenatingOak().GetStats(),
        new ThornBush().GetStats(),
        new LightkeeperZealot().GetStats(),
        new BountyHunter().GetStats(),
        new LuckyLooter().GetStats(),
        new YoungPriestess().GetStats(),
        new WiseMonk().GetStats(),
        new Watchtower().GetStats(),
        new KingsGuard().GetStats(),
        new AngryMob().GetStats(),
        new Peasant().GetStats(),
        new Squire().GetStats(),
        new Gunslinger().GetStats(),
        new FlameThrower().GetStats(),
        new RockThrower().GetStats(),
        new MinotaurBaby().GetStats(),
        new MinotaurKing().GetStats(),
        new MinotaurLord().GetStats(),
        new ElderwoodElder().GetStats(),
        new UprootedWoods().GetStats(),
        new WanderingBirch().GetStats(),
        new BranchManager().GetStats(),
        new CaveTroll().GetStats(),
        new TrollKing().GetStats(),
        new Grumpy().GetStats(),
        new ClubCrasher().GetStats(),
        new Phoenix().GetStats(),
        new GhostDragon().GetStats(),
        new BlackDragon().GetStats(),
        new GoldDragon().GetStats(),
        new BoneDragon().GetStats(),
        new Reanimate().GetStats(),
        new Centaura().GetStats(),
        new CentaurWarrior().GetStats(),
        new GreedyDwarf().GetStats(),
        new MultibowNovice().GetStats(),
        new LongbowGrandmaster().GetStats(),
        new WoodElf().GetStats(),
        new ElvenArcher().GetStats(),
        new RadiantProtector().GetStats(),
        new ForestPrism().GetStats(),
        new LightPiercer().GetStats(),
        new UnicornFoal().GetStats(),
        new ZombieHydra().GetStats(),
        new LastBreath().GetStats(),
        new SkinToBones().GetStats(),
        new PoisonPotion().GetStats(),
        new Disarm().GetStats(),
        new AgingCurse().GetStats(),
        new UnholyStorm().GetStats(),
        new ZombieMinion().GetStats(),
        new Necropolis().GetStats(),
        new SoulStealer().GetStats(),
        new VileMutation().GetStats(),
        new CorpseBehemoth().GetStats(),
        new LichQueen().GetStats(),
        new BloodMerchant().GetStats(),
        new PileOfBones().GetStats(),
        new WailingWall().GetStats(),
        new VoidBeing().GetStats(),
        new ChillingWraith().GetStats(),
        new WindDancer().GetStats(),
        new BoneConjurer().GetStats(),
        new EldritchSorcerer().GetStats(),
        new FrozenTombcarver().GetStats(),
        new BoneGnawer().GetStats(),
        new FrenziedGhoul().GetStats(),
        new SinisterHowler().GetStats(),
        new ShadowyEntity().GetStats(),
        new TheOriginal().GetStats(),
        new VampireApprentice().GetStats(),
        new VampireElder().GetStats(),
        new PlagueWalker().GetStats(),
        new Luigi().GetStats(),
        new Mortana().GetStats(),
    };

    public static List<WarriorStats> GetAvailableCards() {
        return allCards.FindAll(card => card.genre == FriendlySummoner.summonerData.genre && card.levelUnlocked <= ExperienceManager.GetLevel(FriendlySummoner.summonerData.genre));
    }

    public static WarriorStats GetRandomCardStats(CardRarity rarity = CardRarity.None, CardType cardType = CardType.None) {
        List<WarriorStats> cards = GetAvailableCards();

        if (rarity != CardRarity.None) {
            cards = cards.FindAll(card => card.rarity == rarity);
        }

        if (cardType != CardType.None) {
            cards = cards.FindAll(card => card.cardType == cardType);
        }

        return Rng.Entry(cards);
    }

    public static WarriorStats GetStatsByTitleAndLevel(string titleAndLevel) {
        string title = titleAndLevel.Split('_')[0];
        string level = titleAndLevel.Split('_')[1];

        WarriorStats stats = new();
        stats.SetStats(allCards.Find(stats => stats.title == title));

        if (stats == null) {
            return null;
        }

        if (level != "0") {
            stats.level = 1;
        }

        return stats;
    }

    public static WarriorStats GetRandomWarriorWithSpecificCost(int cost, Alignment alignment) {
        List<WarriorStats> warriors = GetAvailableCards().Where(card => card.GetCost() == cost && card.cardType == CardType.Warrior).ToList();
        WarriorStats randomWarrior = Rng.Entry(warriors);
        randomWarrior.alignment = alignment;
        return randomWarrior;
    }

    public static WarriorStats GetRandomWarriorWithSpecificRace(Race race, Alignment alignment) {
        List<WarriorStats> warriors = allCards.Where(card => card.race == race).ToList();
        WarriorStats randomWarrior = Rng.Entry(warriors);
        randomWarrior.alignment = alignment;
        return randomWarrior;
    }

    public static WarriorStats GetRandomSpell(Alignment alignment) {
        List<WarriorStats> spells = GetAvailableCards().Where(card => card.cardType == CardType.Spell).ToList();
        WarriorStats randomSpell = Rng.Entry(spells);
        randomSpell.alignment = alignment;
        return randomSpell;
    }
}
