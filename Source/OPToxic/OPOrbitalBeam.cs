using RimWorld;
using UnityEngine;
using Verse;

namespace OPToxic
{
    // Token: 0x02000004 RID: 4
    public class OPOrbitalBeam : Gas
    {
        // Token: 0x0600000E RID: 14 RVA: 0x000023A6 File Offset: 0x000005A6
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, true);
            destroyTick = Find.TickManager.TicksGame + def.gas.expireSeconds.RandomInRange.SecondsToTicks();
        }

        // Token: 0x0600000F RID: 15 RVA: 0x000023DB File Offset: 0x000005DB
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref destroyTick, "destroyTick");
        }

        // Token: 0x06000010 RID: 16 RVA: 0x000023F8 File Offset: 0x000005F8
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

            var oppowerBeam =
                (OPPowerBeam) GenSpawn.Spawn(DefDatabase<ThingDef>.GetNamed("OPPowerBeam"), position, map);
            oppowerBeam.duration = 120;
            oppowerBeam.instigator = this;
            oppowerBeam.weaponDef = ThingDefOf.OrbitalTargeterPowerBeam;
            oppowerBeam.StartStrike();
            if (!this.DestroyedOrNull())
            {
                Destroy();
            }
        }
    }
}