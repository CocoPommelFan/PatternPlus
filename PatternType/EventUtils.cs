using ADOFAI;
using PatternPlus.PatternType;

namespace PatternPlus;

public class EventUtils
{
    private enum MultiplyType
    {
        Bpm, Multiplier
    }
    
    public static void AddSetSpeedToPatternStart(scnEditor editor)
    {  
        float multiplier = FloorsUtils.CalculateSetSpeedMultiplier(Pattern.FirstPatternFloor);

        Main.Logger.Log($"MULTIPLIER: {multiplier}");

        // -1 ПОТОМУ ЧТО ПРИ ДОБАВЛЕНИИ ИВЕНТА НЕ УЧИТЫВАЕТСЯ ПЕРВАЯ ПЛИТКА В floors С -999 ГРАДУСОМ
        LevelEvent startEvent = new LevelEvent(FloorsUtils.GetFirstPatternFloorSeqID() - 1, LevelEventType.SetSpeed);
        startEvent["speedType"] = MultiplyType.Multiplier;
        startEvent["bpmMultiplier"] = multiplier;

        editor.events.Add(startEvent);

        editor.ApplyEventsToFloors();
    }
}
