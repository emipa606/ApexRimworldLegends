using RimWorld;
using UnityEngine;
using Verse;

namespace OPToxic
{
    // Token: 0x02000005 RID: 5
    public class OPOrbitalBomb : Gas
    {
        // Token: 0x06000012 RID: 18 RVA: 0x000024E3 File Offset: 0x000006E3
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, true);
            destroyTick = Find.TickManager.TicksGame + def.gas.expireSeconds.RandomInRange.SecondsToTicks();
        }

        // Token: 0x06000013 RID: 19 RVA: 0x00002518 File Offset: 0x00000718
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref destroyTick, "destroyTick");
        }

        // Token: 0x06000014 RID: 20 RVA: 0x00002534 File Offset: 0x00000734
        public override void Tick()
        {
            if (destroyTick <= Find.TickManager.TicksGame)
            {
                Destroy();
            }

            graphicRotation += graphicRotationSpeed;
            if (this.DestroyedOrNull())
            {
                return;
            }

            var map = Map;
            var position = Position;
            if (Find.TickManager.TicksGame % 10 == 0)
            {
                FleckMaker.ThrowSmoke(this.TrueCenter() + new Vector3(0f, 0f, 0.1f), map, 1f);
            }

            if (Find.TickManager.TicksGame % 300 != 0)
            {
                return;
            }

            var opbombardment = (OPBombardment) GenSpawn.Spawn(DefDatabase<ThingDef>.GetNamed("OPBombardment"),
                position, map);
            opbombardment.duration = 120;
            opbombardment.instigator = this;
            opbombardment.weaponDef = ThingDefOf.OrbitalTargeterBombardment;
            opbombardment.StartStrike();
            if (!this.DestroyedOrNull())
            {
                Destroy();
            }
        }
    }
}