using HarmonyLib;
using System.Collections.Generic;

namespace PatternPlus.Core
{
    [HarmonyPatch(typeof(RDString), "GetWithCheck")]
    public static class GetWithCheck
    {
        public static void Postfix(ref string __result, ref string key, ref bool exists, ref Dictionary<string, object> parameters)
        {
            if (Main.Localization.Get(key, out string value, parameters))
            {
                exists = true;
                __result = value;
            }
        }
    }
    public static class Patches
    {
        [HarmonyPatch(typeof(scnEditor), "Awake")]
        public static class EditorInstance
        {
            public static scnEditor instance;

            public static void Prefix(scnEditor __instance)
            {
                instance = __instance;
            }
        }
    }
}
