using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace OPToxic
{
    // Token: 0x02000006 RID: 6
    public class OPPowerBeam : OrbitalStrike
    {
        // Token: 0x04000009 RID: 9
        public const float Radius = 8f;

        // Token: 0x0400000A RID: 10
        private const int FiresStartedPerTick = 3;

        // Token: 0x0400000B RID: 11
        private static readonly IntRange FlameDamageAmountRange = new IntRange(25, 50);

        // Token: 0x0400000C RID: 12
        private static readonly IntRange CorpseFlameDamageAmountRange = new IntRange(3, 5);

        // Token: 0x0400000D RID: 13
        private static readonly List<Thing> tmpThings = new List<Thing>();

        // Token: 0x06000016 RID: 22 RVA: 0x0000261F File Offset: 0x0000081F
        public override void StartStrike()
        {
            base.StartStrike();
        }

        // Token: 0x06000017 RID: 23 RVA: 0x00002628 File Offset: 0x00000828
        public override void Tick()
        {
            base.Tick();
            if (Destroyed)
            {
                return;
            }

            var thingDef = def;
            var num = OPBeamDefGetValue.OPBeamGetNumFires(thingDef);
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

        // Token: 0x06000018 RID: 24 RVA: 0x00002670 File Offset: 0x00000870
        private void StartRandomFireAndDoFlameDamage(ThingDef OPBeamDef)
        {
            var EffRadius = OPBeamDefGetValue.OPBeamGetRadius(OPBeamDef);
            if (EffRadius < 1f)
            {
                EffRadius = 1f;
            }

            if (EffRadius > 15f)
            {
                EffRadius = 15f;
            }

            var intVec = (from x in GenRadial.RadialCellsAround(Position, EffRadius, true)
                where x.InBounds(Map)
                select x).RandomElementByWeight(x => 1f - Mathf.Min(x.DistanceTo(Position) / EffRadius, 1f) + 0.05f);
            FireUtility.TryStartFireIn(intVec, Map, Rand.Range(0.1f, 0.5f));
            tmpThings.Clear();
            tmpThings.AddRange(intVec.GetThingList(Map));
            foreach (var thing1 in tmpThings)
            {
                var num = !(thing1 is Corpse)
                    ? FlameDamageAmountRange.RandomInRange
                    : CorpseFlameDamageAmountRange.RandomInRange;
                var num2 = OPBeamDefGetValue.OPBeamGetDmgFact(OPBeamDef);
                if (num2 > 2f)
                {
                    num2 = 2f;
                }

                if (num2 < 0.1f)
                {
                    num2 = 0.1f;
                }

                num = (int) (num * num2);
                if (num < 1)
                {
                    num = 1;
                }

                if (num > 99)
                {
                    num = 99;
                }

                var pawn = thing1 as Pawn;
                BattleLogEntry_DamageTaken battleLogEntry_DamageTaken = null;
                if (pawn != null)
                {
                    battleLogEntry_DamageTaken = new BattleLogEntry_DamageTaken(pawn,
                        RulePackDefOf.DamageEvent_PowerBeam, instigator as Pawn);
                    Find.BattleLog.Add(battleLogEntry_DamageTaken);
                }

                var flame = DamageDefOf.Flame;
                float num3 = num;
                var thing = instigator;
                var thingDef = weaponDef;
                thing1.TakeDamage(new DamageInfo(flame, num3, 0f, -1f, thing, null, thingDef))
                    .AssociateWithLog(battleLogEntry_DamageTaken);
            }

            tmpThings.Clear();
        }
    }
}