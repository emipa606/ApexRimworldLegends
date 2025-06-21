using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace OPToxic;

public class OPPowerBeam : OrbitalStrike
{
    private static readonly IntRange flameDamageAmountRange = new(25, 50);

    private static readonly IntRange corpseFlameDamageAmountRange = new(3, 5);

    private static readonly List<Thing> tmpThings = [];

    public override void StartStrike()
    {
        base.StartStrike();
    }

    protected override void Tick()
    {
        base.Tick();
        if (Destroyed)
        {
            return;
        }

        var thingDef = def;
        var num = OPBeamDefGetValue.OpBeamGetNumFires(thingDef);
        if (num < 1)
        {
            num = 1;
        }

        if (num > 5)
        {
            num = 5;
        }

        for (var i = 0; i < num; i++)
        {
            StartRandomFireAndDoFlameDamage(thingDef);
        }
    }

    private void StartRandomFireAndDoFlameDamage(ThingDef OPBeamDef)
    {
        var effRadius = OPBeamDefGetValue.OpBeamGetRadius(OPBeamDef);
        if (effRadius < 1f)
        {
            effRadius = 1f;
        }

        if (effRadius > 15f)
        {
            effRadius = 15f;
        }

        var intVec = (from x in GenRadial.RadialCellsAround(Position, effRadius, true)
            where x.InBounds(Map)
            select x).RandomElementByWeight(x => 1f - Mathf.Min(x.DistanceTo(Position) / effRadius, 1f) + 0.05f);
        FireUtility.TryStartFireIn(intVec, Map, Rand.Range(0.1f, 0.5f), null);
        tmpThings.Clear();
        tmpThings.AddRange(intVec.GetThingList(Map));
        foreach (var thing1 in tmpThings)
        {
            var num = thing1 is not Corpse
                ? flameDamageAmountRange.RandomInRange
                : corpseFlameDamageAmountRange.RandomInRange;
            var num2 = OPBeamDefGetValue.OpBeamGetDmgFact(OPBeamDef);
            if (num2 > 2f)
            {
                num2 = 2f;
            }

            if (num2 < 0.1f)
            {
                num2 = 0.1f;
            }

            num = (int)(num * num2);
            if (num < 1)
            {
                num = 1;
            }

            if (num > 99)
            {
                num = 99;
            }

            var pawn = thing1 as Pawn;
            BattleLogEntry_DamageTaken battleLogEntryDamageTaken = null;
            if (pawn != null)
            {
                battleLogEntryDamageTaken = new BattleLogEntry_DamageTaken(pawn,
                    RulePackDefOf.DamageEvent_PowerBeam, instigator as Pawn);
                Find.BattleLog.Add(battleLogEntryDamageTaken);
            }

            var flame = DamageDefOf.Flame;
            float num3 = num;
            var thing = instigator;
            var thingDef = weaponDef;
            thing1.TakeDamage(new DamageInfo(flame, num3, 0f, -1f, thing, null, thingDef))
                .AssociateWithLog(battleLogEntryDamageTaken);
        }

        tmpThings.Clear();
    }
}