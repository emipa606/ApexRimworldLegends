using Verse;

namespace OPToxic;

public class OPToxicDefGetValue
{
    public static string OpToxicGetHediff(ThingDef thingDef)
    {
        return thingDef.HasModExtension<OPToxicDefs>() ? thingDef.GetModExtension<OPToxicDefs>().OPToxicHediff : null;
    }

    public static float OpToxicGetSev(ThingDef thingDef)
    {
        return thingDef.HasModExtension<OPToxicDefs>() ? thingDef.GetModExtension<OPToxicDefs>().OPToxicSeverity : 0f;
    }

    public static int OpToxicGetSevUpVal(ThingDef thingDef)
    {
        return thingDef.HasModExtension<OPToxicDefs>()
            ? thingDef.GetModExtension<OPToxicDefs>().OPSevUpTickPeriod
            : 120;
    }
}