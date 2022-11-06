using RimWorld;
using UnityEngine;
using Verse;

namespace OPToxic;

public class OPToxicGas : Gas
{
    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, true);
        destroyTick = Find.TickManager.TicksGame + def.gas.expireSeconds.RandomInRange.SecondsToTicks();
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref destroyTick, "destroyTick");
    }

    public override void Tick()
    {
        if (destroyTick <= Find.TickManager.TicksGame)
        {
            Destroy();
        }

        graphicRotation += graphicRotationSpeed;
        if (Destroyed || Find.TickManager.TicksGame % OPToxicDefGetValue.OPToxicGetSevUpVal(def) != 0)
        {
            return;
        }

        var map = Map;
        var position = Position;
        var thingList = position.GetThingList(map);
        if (thingList.Count <= 0)
        {
            return;
        }

        foreach (var thing in thingList)
        {
            if (thing is Pawn pawn && !pawn.RaceProps.IsMechanoid &&
                pawn.Position == position)
            {
                DoOPToxicGas(this, pawn);
            }
        }
    }

    public void DoOPToxicGas(Thing Gas, Thing targ)
    {
        if (targ is not Pawn pawn || !pawn.health.capacities.CapableOf(PawnCapacityDefOf.Breathing))
        {
            return;
        }

        var namedSilentFail =
            DefDatabase<HediffDef>.GetNamedSilentFail(OPToxicDefGetValue.OPToxicGetHediff(Gas.def));
        if (namedSilentFail == null)
        {
            return;
        }

        var health = pawn.health;
        Hediff hediff;
        if (health == null)
        {
            hediff = null;
        }
        else
        {
            var hediffSet = health.hediffSet;
            hediff = hediffSet?.GetFirstHediffOfDef(namedSilentFail);
        }

        var statValue = pawn.GetStatValue(StatDefOf.ToxicResistance);
        statValue = Mathf.Min(statValue, 0.001f);
        var num = OPToxicDefGetValue.OPToxicGetSev(Gas.def);
        if (num < 0.01f)
        {
            num = 0.01f;
        }

        var num2 = Rand.Range(0.01f / statValue, num / statValue);
        if (hediff != null && num2 > 0f)
        {
            hediff.Severity += num2;
            return;
        }

        var hediff2 = HediffMaker.MakeHediff(namedSilentFail, pawn);
        hediff2.Severity = num2;
        pawn.health.AddHediff(hediff2);
    }
}