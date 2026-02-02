using System.Collections.Generic;
using UnityEngine;

public static class EnemySummoner {
    public static List<SummonerStats> allEnemySummoners = new() {
        new BaronGlutton().GetSummoner(),
        new BonestackerBanya().GetSummoner(),
        new Devil().GetSummoner(),
        new DrakSmag().GetSummoner(),
        new FredrickTheFarmer().GetSummoner(),
        new GravekeeperEternia().GetSummoner(),
        new KaeftTheUnspoken().GetSummoner(),
        new KeiraTheCavalier().GetSummoner(),
        new ProfessorForten().GetSummoner(),
        new QuillinFarsight().GetSummoner(),
        new TarrinTheUnharmed().GetSummoner(),
        new TriniaWispcaller().GetSummoner(),
        new UncixTheEnlightened().GetSummoner(),
        new ZerugSwampwalker().GetSummoner(),
    };

    public static string GetWorthyEnemySummonerName(int level) {
        List<SummonerStats> worthySummoners = allEnemySummoners.FindAll(summoner => summoner.difficulty == level);
        return Rng.Entry(worthySummoners).title;
    }
}