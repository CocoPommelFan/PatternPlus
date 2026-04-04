using System;
using System.Linq;
using ADOFAI;
using PatternPlus.Core;
using PatternPlus.Features.Speed;

namespace PatternPlus.Features.Events;

public class EventManager
{
    private enum MultiplyType
    {
        Bpm, Multiplier
    }
    
    public static void AddSetSpeedToPatternStartAndEnd()
    {  
        var editor = Patches.EditorInstance.instance;
        
        float startMultiplier = SpeedCalculator.CalculateSetSpeedMultiplier(Pattern.Pattern.PatternFloors);
        float endMultiplier = 1 / startMultiplier;

        Main.Logger.Log($"MULTIPLIER: {startMultiplier}");

        // В НАЧАЛЕ ПАТТЕРНА
        // -1 ПОТОМУ ЧТО ПРИ ДОБАВЛЕНИИ ИВЕНТА НЕ УЧИТЫВАЕТСЯ ПЕРВАЯ ПЛИТКА В floors С -999 ГРАДУСОМ
        LevelEvent startSetSpeed = new LevelEvent(Pattern.Pattern.FirstPatternFloor.seqID - 1, LevelEventType.SetSpeed);
        startSetSpeed["speedType"] = MultiplyType.Multiplier;
        startSetSpeed["bpmMultiplier"] = startMultiplier;

        // В КОНЦЕ ПАТТЕРНА
        LevelEvent endSetSpeed = new LevelEvent(Pattern.Pattern.LastPatternFloor.seqID, LevelEventType.SetSpeed);
        endSetSpeed["speedType"] = MultiplyType.Multiplier;
        endSetSpeed["bpmMultiplier"] = endMultiplier;

        editor.events.Add(startSetSpeed);
        editor.events.Add(endSetSpeed);

        editor.ApplyEventsToFloors();
    }

    public static void AddRadiusScaleToWholePattern()
    {
        scnEditor editor = Core.Patches.EditorInstance.instance;

        LevelEvent levelEvent = EditorTabLib.CustomTabManager.GetEvent((LevelEventType)902);

        float radiusScale1 = (float)levelEvent.data["radiusScale1"];
        float radiusScale2 = (float)levelEvent.data["radiusScale2"];

        if (radiusScale1 == 100 && radiusScale2 == 100)
            return;

        Core.Main.Logger.Log($"{radiusScale1} - {radiusScale2}");

        for (int i = 0; i < Pattern.Pattern.PatternFloors.Count; i++)
        {
            if (i % 2 == 0)
            {
                LevelEvent radiusScale1Event = new LevelEvent(Pattern.Pattern.PatternFloors[i].seqID, LevelEventType.ScaleRadius);
                radiusScale1Event.data["scale"] = radiusScale1;
                editor.events.Add(radiusScale1Event);
            }
            else
            {
                LevelEvent radiusScale2Event = new LevelEvent(Pattern.Pattern.PatternFloors[i].seqID, LevelEventType.ScaleRadius);
                radiusScale2Event.data["scale"] = radiusScale2;
                editor.events.Add(radiusScale2Event);
            }
            editor.ApplyEventsToFloors();
        }

        LevelEvent finalRadiusScaleEvent = new LevelEvent(Pattern.Pattern.PatternFloors.Last().seqID + 1, LevelEventType.ScaleRadius);
        finalRadiusScaleEvent.data["scale"] = 100f;
        editor.events.Add(finalRadiusScaleEvent);
        editor.ApplyEventsToFloors();
    }

}
