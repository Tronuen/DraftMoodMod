using HarmonyLib;
using Verse;
using System.Reflection;

namespace DraftMoodMod
{
    [StaticConstructorOnStartup]
    public static class ModInitializer
    {
        static ModInitializer()
        {
            var harmony = new Harmony("Tronuen.DraftMoodMod"); 

            // Bulunduğu assembly'deki (DLL) TÜM Harmony patch'lerini uygula
            harmony.PatchAll(Assembly.GetExecutingAssembly()); 

            Log.Message("[DraftMoodMod] Successfully Installed and Harmony Patches Applied!");
        }
    }
}