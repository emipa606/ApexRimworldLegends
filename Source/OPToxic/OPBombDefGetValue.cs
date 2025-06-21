using Verse;

namespace OPToxic;

public class OPBombDefGetValue
{
    public static int OpBombGetDmg(ThingDef thingDef)
    {
        return thingDef.HasModExtension<OPBombDefs>() ? thingDef.GetModExtension<OPBombDefs>().OPBombDmg : 25;
    }

    public static int OpBombGetImpactRadius(ThingDef thingDef)
    {
        return thingDef.HasModExtension<OPBombDefs>() ? thingDef.GetModExtension<OPBombDefs>().OPBombImpactRadius : 12;
    }

    public static int OpBombGetBlastMinRadius(ThingDef thingDef)
    {
        return thingDef.HasModExtension<OPBombDefs>() ? thingDef.GetModExtension<OPBombDefs>().OPBombBlastMinRadius : 4;
    }

    public static int OpBombGetBlastMaxRadius(ThingDef thingDef)
    {
        return thingDef.HasModExtension<OPBombDefs>() ? thingDef.GetModExtension<OPBombDefs>().OPBombBlastMaxRadius : 6;
    }
}