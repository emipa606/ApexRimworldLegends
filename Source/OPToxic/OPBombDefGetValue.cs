using Verse;

namespace OPToxic;

public class OPBombDefGetValue
{
    public static int OPBombGetDmg(ThingDef thingdef)
    {
        return thingdef.HasModExtension<OPBombDefs>() ? thingdef.GetModExtension<OPBombDefs>().OPBombDmg : 25;
    }

    public static int OPBombGetImpactRadius(ThingDef thingdef)
    {
        return thingdef.HasModExtension<OPBombDefs>() ? thingdef.GetModExtension<OPBombDefs>().OPBombImpactRadius : 12;
    }

    public static int OPBombGetBlastMinRadius(ThingDef thingdef)
    {
        return thingdef.HasModExtension<OPBombDefs>() ? thingdef.GetModExtension<OPBombDefs>().OPBombBlastMinRadius : 4;
    }

    public static int OPBombGetBlastMaxRadius(ThingDef thingdef)
    {
        return thingdef.HasModExtension<OPBombDefs>() ? thingdef.GetModExtension<OPBombDefs>().OPBombBlastMaxRadius : 6;
    }
}