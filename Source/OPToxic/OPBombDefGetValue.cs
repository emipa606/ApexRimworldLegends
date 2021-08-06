using Verse;

namespace OPToxic
{
    // Token: 0x0200000A RID: 10
    public class OPBombDefGetValue
    {
        // Token: 0x06000021 RID: 33 RVA: 0x0000290B File Offset: 0x00000B0B
        public static int OPBombGetDmg(ThingDef thingdef)
        {
            if (thingdef.HasModExtension<OPBombDefs>())
            {
                return thingdef.GetModExtension<OPBombDefs>().OPBombDmg;
            }

            return 25;
        }

        // Token: 0x06000022 RID: 34 RVA: 0x00002923 File Offset: 0x00000B23
        public static int OPBombGetImpactRadius(ThingDef thingdef)
        {
            if (thingdef.HasModExtension<OPBombDefs>())
            {
                return thingdef.GetModExtension<OPBombDefs>().OPBombImpactRadius;
            }

            return 12;
        }

        // Token: 0x06000023 RID: 35 RVA: 0x0000293B File Offset: 0x00000B3B
        public static int OPBombGetBlastMinRadius(ThingDef thingdef)
        {
            if (thingdef.HasModExtension<OPBombDefs>())
            {
                return thingdef.GetModExtension<OPBombDefs>().OPBombBlastMinRadius;
            }

            return 4;
        }

        // Token: 0x06000024 RID: 36 RVA: 0x00002952 File Offset: 0x00000B52
        public static int OPBombGetBlastMaxRadius(ThingDef thingdef)
        {
            if (thingdef.HasModExtension<OPBombDefs>())
            {
                return thingdef.GetModExtension<OPBombDefs>().OPBombBlastMaxRadius;
            }

            return 6;
        }
    }
}