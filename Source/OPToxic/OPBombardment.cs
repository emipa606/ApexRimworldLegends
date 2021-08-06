using System.Linq;
using RimWorld;
using Verse;

namespace OPToxic
{
    // Token: 0x02000003 RID: 3
    public class OPBombardment : OrbitalStrike
    {
        // Token: 0x04000001 RID: 1
        private const int ImpactAreaRadius = 12;

        // Token: 0x04000002 RID: 2
        private const int ExplosionRadiusMin = 4;

        // Token: 0x04000003 RID: 3
        private const int ExplosionRadiusMax = 6;

        // Token: 0x04000004 RID: 4
        public const int EffectiveRadius = 13;

        // Token: 0x04000005 RID: 5
        public const int RandomFireRadius = 15;

        // Token: 0x04000006 RID: 6
        private const int BombIntervalTicks = 28;

        // Token: 0x04000007 RID: 7
        private const int StartRandomFireEveryTicks = 30;

        // Token: 0x04000008 RID: 8
        private static readonly SimpleCurve DistanceChanceFactor;

        // Token: 0x06000009 RID: 9 RVA: 0x00002322 File Offset: 0x00000522
        // Note: this type is marked as 'beforefieldinit'.
        static OPBombardment()
        {
            var simpleCurve = new SimpleCurve {new CurvePoint(0f, 1f), new CurvePoint(30f, 0.1f)};
            DistanceChanceFactor = simpleCurve;
        }

        // Token: 0x06000004 RID: 4 RVA: 0x0000217A File Offset: 0x0000037A
        public override void StartStrike()
        {
            base.StartStrike();
        }

        // Token: 0x06000005 RID: 5 RVA: 0x00002182 File Offset: 0x00000382
        public override void Tick()
        {
            base.Tick();
            if (Destroyed)
            {
                return;
            }

            if (Find.TickManager.TicksGame % 28 == 0)
            {
                CreateRandomExplosion();
            }

            if (Find.TickManager.TicksGame % 30 == 0)
            {
                StartRandomFire();
            }
        }

        // Token: 0x06000006 RID: 6 RVA: 0x000021BC File Offset: 0x000003BC
        private void CreateRandomExplosion()
        {
            var thingDef = def;
            var num = OPBombDefGetValue.OPBombGetDmg(thingDef);
            if (num < 1)
            {
                num = 1;
            }

            if (num > 99)
            {
                num = 99;
            }

            var num2 = OPBombDefGetValue.OPBombGetImpactRadius(thingDef);
            if (num2 < 1)
            {
                num2 = 1;
            }

            if (num2 > 30)
            {
                num2 = 30;
            }

            var num3 = OPBombDefGetValue.OPBombGetBlastMinRadius(thingDef);
            var num4 = OPBombDefGetValue.OPBombGetBlastMaxRadius(thingDef);
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

        // Token: 0x06000007 RID: 7 RVA: 0x000022B4 File Offset: 0x000004B4
        private void StartRandomFire()
        {
            var num = OPBombDefGetValue.OPBombGetImpactRadius(def) + 2;
            FireUtility.TryStartFireIn((from x in GenRadial.RadialCellsAround(Position, num, true)
                    where x.InBounds(Map)
                    select x).RandomElementByWeight(x => DistanceChanceFactor.Evaluate(x.DistanceTo(Position))), Map,
                Rand.Range(0.1f, 0.925f));
        }
    }
}