﻿using Harmony;

namespace NoLongCommutes
{
    public class NoLongCommutesPatches
    {
	    [HarmonyPatch(typeof(SplashMessageScreen))]
	    [HarmonyPatch("OnPrefabInit")]
	    public static class SplashMessageScreen_OnPrefabInit_Patch
	    {
		    public static void Postfix()
		    {
			    CaiLib.Logger.Logger.LogInit(ModInfo.Name, ModInfo.Version);
		    }
	    }

	    [HarmonyPatch(typeof(Tutorial))]
	    [HarmonyPatch("LongTravelTimes")]
	    public class Tutorial_LongTravelTimes_Patch
		{
		    private static bool Prefix(ref bool __result)
		    {
			    __result = true;
			    return false;
		    }
	    }
	}
}
