using ADOFAI;
using HarmonyLib;
using PatternPlus.Pattern.Core.Base;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PatternPlus.Pattern.Core;

public class EventUtils
{
    public enum MultiplyType
    {
        Bpm, Multiplier
    }

    
    public static void CreateRadiusScaleEventWithValue(int floor, float value)
    {
        LevelEvent levelEvent = new LevelEvent(floor, LevelEventType.ScaleRadius);
        ref Dictionary<string, object> eventData = ref AccessTools.FieldRefAccess<LevelEvent, Dictionary<string, object>>("data")(levelEvent);
        eventData["scale"] = value;

        Patches.EditorInstance.Instance.events.Add(levelEvent);
        Patches.EditorInstance.Instance.ApplyEventsToFloors();
    }

    public static void CreateSetSpeedEventWithMultiplier(int floor, EventUtils.MultiplyType multiplyType, float multiplier)
    {
        LevelEvent levelEvent = new LevelEvent(floor, LevelEventType.SetSpeed);
        ref Dictionary<string, object> eventData = ref AccessTools.FieldRefAccess<LevelEvent, Dictionary<string, object>>("data")(levelEvent);
        eventData["speedType"] = multiplyType;
        eventData["bpmMultiplier"] = multiplier;

        Patches.EditorInstance.Instance.events.Add(levelEvent);
        Patches.EditorInstance.Instance.ApplyEventsToFloors();
    }
}