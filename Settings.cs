using UnityModManagerNet;
using UnityEngine;

namespace PatternPlus
{
    /// <summary>
    /// Mod settings class
    /// Mod 设置类
    /// </summary>
    public class Settings : UnityModManager.ModSettings
    {
        /// <summary>
        /// Example boolean setting
        /// </summary>
        public bool EnableFeature = true;

        /// <summary>
        /// Example integer setting
        /// </summary>
        public int ExampleValue = 100;

        /// <summary>
        /// Example string setting
        /// </summary>
        public string ExampleText = "Hello World";

        /// <summary>
        /// Draw mod GUI
        /// </summary>
        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
            // Example: Draw settings UI
            GUILayout.Label("=== Mod Settings ===");
            
            EnableFeature = GUILayout.Toggle(
                EnableFeature, 
                "Enable Feature"
            );
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Example Value: ", GUILayout.Width(150));
            if (int.TryParse(
                GUILayout.TextField(ExampleValue.ToString(), GUILayout.Width(100)),
                out int newValue))
            {
                ExampleValue = newValue;
            }
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Example Text: ", GUILayout.Width(150));
            ExampleText = GUILayout.TextField(
                ExampleText, 
                GUILayout.Width(200)
            );
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Called when saving GUI
        /// </summary>
        public void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Save(modEntry);
        }

        /// <summary>
        /// Save settings
        /// </summary>
        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }

        /// <summary>
        /// Load settings
        /// </summary>
        public static Settings Load(UnityModManager.ModEntry modEntry)
        {
            return Load<Settings>(modEntry);
        }
    }
}
