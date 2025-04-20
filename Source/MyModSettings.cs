using Verse;

namespace DraftMoodMod
{
    public class MyModSettings : ModSettings
    {
        // We define our adjustable values here
        // It's important they are public!
        public int masterThreshold = 15; // Default threshold for Master Warrior
        public int skilledThreshold = 7;  // Default threshold for Skilled Warrior
        public int masterMoodBonus = 7; // Default mood bonus for Master Warrior
        public int skilledMoodBonus = 3;  // Default mood bonus for Skilled Warrior

        // This method is used to save and load settings
        public override void ExposeData()
        {
            // We use the Scribe method for each setting
            // Look(ref variable, "save_tag", default_value)
            Scribe_Values.Look(ref masterThreshold, "masterThreshold", 15);
            Scribe_Values.Look(ref skilledThreshold, "skilledThreshold", 7);
            Scribe_Values.Look(ref masterMoodBonus, "masterMoodBonus", 7);
            Scribe_Values.Look(ref skilledMoodBonus, "skilledMoodBonus", 3);

            base.ExposeData(); // Calling the base class method is important
        }

        // Helper method to reset settings to defaults
        public void ResetToDefaults()
        {
            masterThreshold = 15;
            skilledThreshold = 7;
            masterMoodBonus = 7;
            skilledMoodBonus = 3;
        }
    }
}