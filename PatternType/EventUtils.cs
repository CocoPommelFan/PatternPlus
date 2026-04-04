using ADOFAI;
using PatternPlus.PatternType;

namespace PatternPlus;

public class EventUtils
{
    private enum MultiplyType
    {
        Bpm, Multiplier
    }
    
    public static void AddSetSpeedToPatternStartAndEnd()
    {  
        var editor = Patches.EditorInstance.instance;
        
        float startMultiplier = FloorsUtils.CalculateSetSpeedMultiplier(Pattern.FirstPatternFloor);
        float endMultiplier = 1 / startMultiplier;

        Main.Logger.Log($"MULTIPLIER: {startMultiplier}");

        // В НАЧАЛЕ ПАТТЕРНА
        // -1 ПОТОМУ ЧТО ПРИ ДОБАВЛЕНИИ ИВЕНТА НЕ УЧИТЫВАЕТСЯ ПЕРВАЯ ПЛИТКА В floors С -999 ГРАДУСОМ
        LevelEvent startSetSpeed = new LevelEvent(Pattern.FirstPatternFloor.seqID - 1, LevelEventType.SetSpeed);
        startSetSpeed["speedType"] = MultiplyType.Multiplier;
        startSetSpeed["bpmMultiplier"] = startMultiplier;

        // В КОНЦЕ ПАТТЕРНА
        LevelEvent endSetSpeed = new LevelEvent(Pattern.LastPatternFloor.seqID, LevelEventType.SetSpeed);
        endSetSpeed["speedType"] = MultiplyType.Multiplier;
        endSetSpeed["bpmMultiplier"] = endMultiplier;

        editor.events.Add(startSetSpeed);
        editor.events.Add(endSetSpeed);

        editor.ApplyEventsToFloors();
    }
}
