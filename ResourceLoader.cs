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
                    throw new InvalidOperationException("Mod is not initialized / Mod 未初始化");
                
                return Path.Combine(Main.Mod.Path, "Resources");
            }
        }

        /// <returns>File content as string</returns>
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

        /// <summary>
        /// Loads an image file as a Texture2D from the Resources folder
        /// </summary>
        /// <param name="fileName">File name relative to Resources folder</param>
        /// <returns>Texture2D or null if failed</returns>
        public static Texture2D LoadTexture(string fileName)
        {
            string filePath = Path.Combine(ResourcesPath, fileName);
            
            if (!File.Exists(filePath))
            {
                Main.Mod?.Logger.Error($"Image file not found / 图像文件未找到: {filePath}");
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

        /// <summary>
        /// Loads binary data from a file in the Resources folder
        /// </summary>
        /// <param name="fileName">File name relative to Resources folder</param>
        /// <returns>Byte array or empty array if failed</returns>
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

        /// <summary>
        /// Checks if a file exists in the Resources folder
        /// </summary>
        /// <param name="fileName">File name relative to Resources folder</param>
        /// <returns>True if file exists</returns>
        public static bool FileExists(string fileName)
        {
            string filePath = Path.Combine(ResourcesPath, fileName);
            return File.Exists(filePath);
        }

        /// <summary>
        /// Gets all files in the Resources folder
        /// </summary>
        /// <param name="searchPattern">Search pattern (e.g., "*.txt")</param>
        /// <param name="searchOption">Search option</param>
        /// <returns>Array of file paths</returns>
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
