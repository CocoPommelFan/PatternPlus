using ADOFAI;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PatternPlus.Pattern
{
    public static class LevelEventData
    {
        private static Dictionary<string, object> _eventData;
        public static Dictionary<string, object> EventData
        {
            get
            {
                if (_eventData == null)
                {
                    LevelEvent levelEvent = EditorTabLib.CustomTabManager.GetEvent((LevelEventType)902);
                    if (levelEvent != null)
                    {
                        _eventData = AccessTools.FieldRefAccess<LevelEvent, Dictionary<string, object>>("data")(levelEvent);
                    }

                }
                return _eventData;
            }
        }

        public static int TileCount => (int)EventData["tileCount"];
        public static PatternType PatternType => (PatternType)EventData["patternType"];
        public static bool ShowPreview => (bool)EventData["showPreview"];
        public static float PseudoAngle => (float)EventData["pseudoAngle"];
        public static bool IsHalf => (bool)EventData["isHalf"];
        public static bool IsInversed => (bool)EventData["isInversed"];
        public static float RadiusScale1 => (float)EventData["radiusScale1"];
        public static float RadiusScale2 => (float)EventData["radiusScale2"];
        public static float TileScale => (float)EventData["tileScale"];
        public static int PseudoKeyCount => (int)EventData["pseudoKeyCount"];
        public static int PseudoEveryNBeat => (int)EventData["pseudoEveryNBeat"];
        public static bool IsMidSpin => (bool)EventData["isMidSpin"];
    }
}
