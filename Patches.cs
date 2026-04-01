using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace PatternPlus
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
        [HarmonyPatch(typeof(scrController), "Start")]
        public static class ControllerStartPatch
        {
            public static void Prefix(scrController __instance)
            {
                Main.Logger?.Log("Controller start");
            }
            public static void Postfix(scrController __instance)
            {
                Main.Logger?.Log("Controller started");

                // Example: Use settings
                if (Main.Settings.EnableFeature)
                {
                    Main.Logger?.Log($"Feature enabled, example value: {Main.Settings.ExampleValue}");
                }
            }
        }

        [HarmonyPatch(typeof(scnEditor), "CopyFloor")]
        public static class EditorStartPatch
        {
            public static void Prefix(scnEditor __instance)
            {
                Main.Logger?.Log($"Copied");
            }

            public static void Postfix(scnEditor __instance)
            {
                foreach(var cp in __instance.clipboard)
                {
                    if (cp is scnEditor.FloorData floorData)
                    Main.Logger?.Log($"Clipboard | {floorData.floatDirection}");
                }

                Main.Logger?.Log($"{__instance.clipboardContent.ToString()}");
            }
        }
    }
}
