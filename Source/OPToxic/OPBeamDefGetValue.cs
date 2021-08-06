using Verse;

namespace OPToxic
{
    // Token: 0x0200000C RID: 12
    public class OPBeamDefGetValue
    {
        // Token: 0x06000027 RID: 39 RVA: 0x00002996 File Offset: 0x00000B96
        public static float OPBeamGetDmgFact(ThingDef thingdef)
        {
            if (thingdef.HasModExtension<OPBeamDefs>())
            {
                return thingdef.GetModExtension<OPBeamDefs>().OPBeamDmgFactor;
            }

            return 1f;
        }

        // Token: 0x06000028 RID: 40 RVA: 0x000029B2 File Offset: 0x00000BB2
        public static float OPBeamGetRadius(ThingDef thingdef)
        {
            if (thingdef.HasModExtension<OPBeamDefs>())
            {
                return thingdef.GetModExtension<OPBeamDefs>().OPBeamRadius;
            }

            return 8f;
        }

        // Token: 0x06000029 RID: 41 RVA: 0x000029CE File Offset: 0x00000BCE
        public static int OPBeamGetNumFires(ThingDef thingdef)
        {
            if (thingdef.HasModExtension<OPBeamDefs>())
            {
                return thingdef.GetModExtension<OPBeamDefs>().OPBeamNumFirePts;
            }

            return 3;
        }
    }
}