using Verse;

namespace OPToxic;

public class OPBeamDefGetValue
{
    public static float OPBeamGetDmgFact(ThingDef thingdef)
    {
        return thingdef.HasModExtension<OPBeamDefs>() ? thingdef.GetModExtension<OPBeamDefs>().OPBeamDmgFactor : 1f;
    }

    public static float OPBeamGetRadius(ThingDef thingdef)
    {
        return thingdef.HasModExtension<OPBeamDefs>() ? thingdef.GetModExtension<OPBeamDefs>().OPBeamRadius : 8f;
    }

    public static int OPBeamGetNumFires(ThingDef thingdef)
    {
        return thingdef.HasModExtension<OPBeamDefs>() ? thingdef.GetModExtension<OPBeamDefs>().OPBeamNumFirePts : 3;
    }
}