using Verse;

namespace OPToxic
{
    // Token: 0x02000008 RID: 8
    public class OPToxicDefGetValue
    {
        // Token: 0x0600001C RID: 28 RVA: 0x00002892 File Offset: 0x00000A92
        public static string OPToxicGetHediff(ThingDef thingdef)
        {
            if (thingdef.HasModExtension<OPToxicDefs>())
            {
                return thingdef.GetModExtension<OPToxicDefs>().OPToxicHediff;
            }

            return null;
        }

        // Token: 0x0600001D RID: 29 RVA: 0x000028A9 File Offset: 0x00000AA9
        public static float OPToxicGetSev(ThingDef thingdef)
        {
            if (thingdef.HasModExtension<OPToxicDefs>())
            {
                return thingdef.GetModExtension<OPToxicDefs>().OPToxicSeverity;
            }

            return 0f;
        }

        // Token: 0x0600001E RID: 30 RVA: 0x000028C5 File Offset: 0x00000AC5
        public static int OPToxicGetSevUpVal(ThingDef thingdef)
        {
            if (thingdef.HasModExtension<OPToxicDefs>())
            {
                return thingdef.GetModExtension<OPToxicDefs>().OPSevUpTickPeriod;
            }

            return 120;
        }
    }
}