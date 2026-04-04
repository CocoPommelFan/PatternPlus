using UnityModManagerNet;
using UnityEngine;

namespace PatternPlus
{
    public class Settings : UnityModManager.ModSettings
    {
        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
            GUILayout.Label("No mod settings available.");
        }

        public void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Save(modEntry);
        }

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }

        public static Settings Load(UnityModManager.ModEntry modEntry)
        {
            return Load<Settings>(modEntry);
        }
    }
}
