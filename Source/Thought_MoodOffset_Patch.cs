using HarmonyLib;
using RimWorld;
using Verse;

namespace DraftMoodMod
{
    [HarmonyPatch(typeof(Thought), "MoodOffset")] // Target the MoodOffset method of the Thought class
    public static class Thought_MoodOffset_Patch
    {
        // This will run AFTER the original method (Postfix)
        // __instance: The Thought object itself
        // __result: The value returned by the original method (float mood effect)
        static void Postfix(Thought __instance, ref float __result)
        {
            // Check if this is our custom thought
            if (__instance.def == ThoughtDef.Named("MySkilledFighterMood"))
            {
                // Get mod settings
                var settings = SettingsInterface.settings;
                if (settings == null) return; // Don't modify if settings aren't loaded

                // Get the current active stage index of the thought
                int stageIndex = __instance.CurStageIndex;

                // Apply mood bonus from settings based on stage index
                if (stageIndex == 0) // Master Warrior (first stage in XML)
                {
                    __result = settings.masterMoodBonus; // Use Master bonus from settings
                }
                else if (stageIndex == 1) // Skilled Warrior (second stage in XML)
                {
                    __result = settings.skilledMoodBonus; // Use Skilled bonus from settings
                }
            }
        }
    }
}