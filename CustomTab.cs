using EditorTabLib;
using EditorTabLib.Properties;
using PatternPlus.PatternType;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PatternPlus
{
    static class CustomTab
    {
        public enum ToggleBool
        {
            Enabled,
            Disabled
        }

        public static void CreateTab(Sprite icon)
        {
            CustomTabManager.AddTab(icon, 902, "Pattern Plus", new Dictionary<SystemLanguage, string>()
            {
                { SystemLanguage.English, "Pattern Plus" },
            }, new List<Property>()
            {
                new Property_Tile(
                    name: "startTile",
                    value_default: (0, TileRelativeTo.Start),
                    hideButtons: Property_Tile.THIS_TILE,
                    key: "editor.startTile"),
                new Property_InputField(
                    name: "tileCount",
                    type: Property_InputField.InputType.Int,
                    value_default: 0,
                    min: 0,
                    max: 1024,
                    key: "ml.editor.tileCount"
                    ),
                new Property_Bool(
                    name: "showPreview",
                    value_default: true,
                    key: "ml.editor.showPreview"),
                new Property_Enum<Patterns.PatternType>(
                    name: "patternType",
                    value_default: Patterns.PatternType.Circle,
                    key: "ml.editor.patternType"),
                new Property_Bool(
                    name: "isHalf",
                    value_default: false,
                    key: "ml.editor.isHalf"),
                new Property_InputField(
                    name: "pseudoEveryNBeat",
                    type: Property_InputField.InputType.Int,
                    value_default: 0,
                    min: 0,
                    key: "ml.editor.pseudoEveryNBeat",
                    canBeDisabled: true),
                new Property_InputField(
                    name: "pseudoKeyCount",
                    type: Property_InputField.InputType.Int,
                    value_default: 2,
                    min: 1,
                    max: 32,
                    key: "ml.editor.pseudoKeyCount"),
                new Property_Bool(
                    name: "isMidSpin",
                    value_default: false,
                    key: "ml.editor.isMidSpin"),
                new Property_Button(
                    name: "create",
                    action: () => Patterns.Create(),
                    key: "ml.editor.create")
            });
        }

        public static void LoadIconSprite(ref Sprite icon)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Mods", Main.ModName, "Resources", "icon_pattern.png");
            if (File.Exists(path))
            {
                if (File.Exists(path))
                {
                    byte[] fileData = File.ReadAllBytes(path);
                    Texture2D texture = new Texture2D(0, 0);
                    if (texture.LoadImage(fileData))
                        icon = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                }
            }
        }
    }
}
