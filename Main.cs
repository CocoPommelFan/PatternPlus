using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Localizations;
using UnityModManagerNet;

namespace PatternPlus
{
    public static class Main
    {
        public static string ModName = "PatternPlus";
        public static Sprite icon;
        public static UnityModManager.ModEntry Mod { get; private set; }
        public static UnityModManager.ModEntry.ModLogger Logger { get; private set; }
        public static Localization Localization;
        public static Harmony Harmony { get; private set; }
        public static Settings Settings { get; private set; } = null!;

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            Mod = modEntry;
            Logger = modEntry.Logger;

            Settings = Settings.Load(modEntry);
            
            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = Settings.OnGUI;
            modEntry.OnSaveGUI = Settings.OnSaveGUI;
            
            Harmony = new Harmony(modEntry.Info.Id);

            CustomTab.LoadIconSprite(ref icon);

            Localization = Localization.Load(
                key: "1yfLNR9_BzZ_xLW8Q2DVqklZ5iQCvvR1Uikl-Z0mbAVs",
                gid: 1667440741,
                modEntry: modEntry,
                onLoad: (keyValue) => (LocalizationUtils.ReplaceClassName(keyValue.Item1), keyValue.Item2));
            
            modEntry.Logger.Log("Mod loaded");
            return true;
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            if (value)
            {
                modEntry.Logger.Log("Mod enabled");
                Harmony?.PatchAll(Assembly.GetExecutingAssembly());

                CustomTab.CreateTab(icon);
            }
            else
            {
                modEntry.Logger.Log("Mod disabled");
                Harmony?.UnpatchAll();
            }
            return true;
        }
    }
}