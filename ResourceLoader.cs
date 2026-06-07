using System;
using System.IO;
using UnityEngine;

namespace PatternPlus
{
    public static class ResourceLoader
    {
        public static string ResourcesPath
        {
            get
            {
                if (Main.Mod == null)
                    throw new InvalidOperationException("Mod is not initialized");
                
                return Path.Combine(Main.Mod.Path, "Resources");
            }
        }

        public static string LoadTextFile(string fileName)
        {
            string filePath = Path.Combine(ResourcesPath, fileName);
            
            if (!File.Exists(filePath))
            {
                Main.Mod?.Logger.Error($"Text file not found: {filePath}");
                return string.Empty;
            }

            try
            {
                string content = File.ReadAllText(filePath);
                Main.Mod?.Logger.Log($"Loaded text file: {fileName}");
                return content;
            }
            catch (Exception ex)
            {
                Main.Mod?.Logger.Error($"Failed to load text file: {fileName}\n{ex}");
                return string.Empty;
            }
        }

        public static Texture2D LoadTexture(string fileName)
        {
            string filePath = Path.Combine(ResourcesPath, fileName);
            
            if (!File.Exists(filePath))
            {
                Main.Mod?.Logger.Error($"Image file not found: {filePath}");
                return null;
            }

            try
            {
                byte[] fileData = File.ReadAllBytes(filePath);
                Texture2D texture = new Texture2D(2, 2);
                
                if (texture.LoadImage(fileData))
                {
                    Main.Mod?.Logger.Log($"Loaded texture: {fileName} ({texture.width}x{texture.height})");
                    return texture;
                }
                else
                {
                    Main.Mod?.Logger.Error($"Failed to load image data: {fileName}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Main.Mod?.Logger.Error($"Failed to load texture: {fileName}\n{ex}");
                return null;
            }
        }

        public static byte[] LoadBinaryFile(string fileName)
        {
            string filePath = Path.Combine(ResourcesPath, fileName);
            
            if (!File.Exists(filePath))
            {
                Main.Mod?.Logger.Error($"Binary file not found: {filePath}");
                return Array.Empty<byte>();
            }

            try
            {
                byte[] data = File.ReadAllBytes(filePath);
                Main.Mod?.Logger.Log($"Loaded binary file: {fileName} ({data.Length} bytes)");
                return data;
            }
            catch (Exception ex)
            {
                Main.Mod?.Logger.Error($"Failed to load binary file: {fileName}\n{ex}");
                return Array.Empty<byte>();
            }
        }

        public static bool FileExists(string fileName)
        {
            string filePath = Path.Combine(ResourcesPath, fileName);
            return File.Exists(filePath);
        }

        public static string[] GetFiles(string searchPattern = "*.*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            try
            {
                if (!Directory.Exists(ResourcesPath))
                {
                    Main.Mod?.Logger.Warning($"Resources folder not found: {ResourcesPath}");
                    return Array.Empty<string>();
                }

                return Directory.GetFiles(ResourcesPath, searchPattern, searchOption);
            }
            catch (Exception ex)
            {
                Main.Mod?.Logger.Error($"Failed to get files: {ex}");
                return Array.Empty<string>();
            }
        }
    }
}
