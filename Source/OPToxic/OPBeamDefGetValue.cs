using Verse;

namespace OPToxic;

public static class OPBeamDefGetValue
{
    public static float OpBeamGetDmgFact(ThingDef thingDef)
    {
        return thingDef.HasModExtension<OPBeamDefs>() ? thingDef.GetModExtension<OPBeamDefs>().OPBeamDmgFactor : 1f;
    }

    public static float OpBeamGetRadius(ThingDef thingDef)
    {
        return thingDef.HasModExtension<OPBeamDefs>() ? thingDef.GetModExtension<OPBeamDefs>().OPBeamRadius : 8f;
    }

    public static int OpBeamGetNumFires(ThingDef thingDef)
    {
        return thingDef.HasModExtension<OPBeamDefs>() ? thingDef.GetModExtension<OPBeamDefs>().OPBeamNumFirePts : 3;
    }
}