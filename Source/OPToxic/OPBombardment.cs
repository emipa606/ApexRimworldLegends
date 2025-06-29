using System.Linq;
using RimWorld;
using Verse;

namespace OPToxic;

public class OPBombardment : OrbitalStrike
{
    private const int BombIntervalTicks = 28;

    private const int StartRandomFireEveryTicks = 30;

    private static readonly SimpleCurve DistanceChanceFactor;

    // Note: this type is marked as 'beforefieldinit'.
    static OPBombardment()
    {
        var simpleCurve = new SimpleCurve { new CurvePoint(0f, 1f), new CurvePoint(30f, 0.1f) };
        DistanceChanceFactor = simpleCurve;
    }

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

        if (Find.TickManager.TicksGame % BombIntervalTicks == 0)
        {
            CreateRandomExplosion();
        }

        if (Find.TickManager.TicksGame % StartRandomFireEveryTicks == 0)
        {
            StartRandomFire();
        }
    }

    private void CreateRandomExplosion()
    {
        var thingDef = def;
        var num = OPBombDefGetValue.OpBombGetDmg(thingDef);
        if (num < 1)
        {
            num = 1;
        }

        if (num > 99)
        {
            num = 99;
        }

        var num2 = OPBombDefGetValue.OpBombGetImpactRadius(thingDef);
        if (num2 < 1)
        {
            num2 = 1;
        }

        if (num2 > 30)
        {
            num2 = 30;
        }

        var num3 = OPBombDefGetValue.OpBombGetBlastMinRadius(thingDef);
        var num4 = OPBombDefGetValue.OpBombGetBlastMaxRadius(thingDef);
        if (num4 > 10)
        {
            num4 = 10;
        }

        if (num4 < 1)
        {
            num4 = 1;
        }

        if (num3 > num4)
        {
            num3 = num4;
        }

        if (num3 < 1)
        {
            num3 = 1;
        }

        var intVec = (from x in GenRadial.RadialCellsAround(Position, num2, true)
            where x.InBounds(Map)
            select x).RandomElementByWeight(x => DistanceChanceFactor.Evaluate(x.DistanceTo(Position)));
        float num5 = Rand.Range(num3, num4);
        var map = Map;
        var bomb = DamageDefOf.Bomb;
        var thing = instigator;
        var weapon = weaponDef;
        GenExplosion.DoExplosion(intVec, map, num5, bomb, thing, num, -1f, null, weapon, thingDef);
    }

    private void StartRandomFire()
    {
        var num = OPBombDefGetValue.OpBombGetImpactRadius(def) + 2;
        FireUtility.TryStartFireIn((from x in GenRadial.RadialCellsAround(Position, num, true)
                where x.InBounds(Map)
                select x).RandomElementByWeight(x => DistanceChanceFactor.Evaluate(x.DistanceTo(Position))), Map,
            Rand.Range(0.1f, 0.925f), null);
    }
}