using RimWorld;
using UnityEngine;
using Verse;

namespace OPToxic;

public class OPOrbitalBeam : Gas
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
            (OPPowerBeam)GenSpawn.Spawn(DefDatabase<ThingDef>.GetNamed("OPPowerBeam"), position, map);
        oppowerBeam.duration = 120;
        oppowerBeam.instigator = this;
        oppowerBeam.weaponDef = DefDatabase<ThingDef>.GetNamedSilentFail("OrbitalTargeterPowerBeam");
        oppowerBeam.StartStrike();
        if (!this.DestroyedOrNull())
        {
            Destroy();
        }
    }
}