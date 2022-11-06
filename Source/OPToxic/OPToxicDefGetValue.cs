using Verse;

namespace OPToxic;

public class OPToxicDefGetValue
{
    public static string OPToxicGetHediff(ThingDef thingdef)
    {
        return thingdef.HasModExtension<OPToxicDefs>() ? thingdef.GetModExtension<OPToxicDefs>().OPToxicHediff : null;
    }

    public static float OPToxicGetSev(ThingDef thingdef)
    {
        return thingdef.HasModExtension<OPToxicDefs>() ? thingdef.GetModExtension<OPToxicDefs>().OPToxicSeverity : 0f;
    }

    public static int OPToxicGetSevUpVal(ThingDef thingdef)
    {
        return thingdef.HasModExtension<OPToxicDefs>()
            ? thingdef.GetModExtension<OPToxicDefs>().OPSevUpTickPeriod
            : 120;
    }
}