using RimWorld;
using Verse;

namespace OPToxic
{
    // Token: 0x0200000D RID: 13
    public class OPToxicGas : Gas
    {
        // Token: 0x0600002B RID: 43 RVA: 0x000029ED File Offset: 0x00000BED
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, true);
            destroyTick = Find.TickManager.TicksGame + def.gas.expireSeconds.RandomInRange.SecondsToTicks();
        }

        // Token: 0x0600002C RID: 44 RVA: 0x00002A22 File Offset: 0x00000C22
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref destroyTick, "destroyTick");
        }

        // Token: 0x0600002D RID: 45 RVA: 0x00002A3C File Offset: 0x00000C3C
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

        // Token: 0x0600002E RID: 46 RVA: 0x00002B10 File Offset: 0x00000D10
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

            var statValue = pawn.GetStatValue(StatDefOf.ToxicSensitivity);
            var num = OPToxicDefGetValue.OPToxicGetSev(Gas.def);
            if (num < 0.01f)
            {
                num = 0.01f;
            }

            var num2 = Rand.Range(0.01f * statValue, num * statValue);
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
}