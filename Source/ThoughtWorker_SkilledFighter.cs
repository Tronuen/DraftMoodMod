using RimWorld;
using Verse;

namespace DraftMoodMod
{
    public class ThoughtWorker_SkilledFighter : ThoughtWorker
    {
        // This method calculates the current state of the thought for a pawn.
        // We override this method. RimWorld periodically calls this for each pawn to check thought state.
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            // Get settings (from static variable in SettingsInterface class)
            var settings = SettingsInterface.settings;
            // If settings failed to load (shouldn't happen theoretically, but good to check)
            if (settings == null) return ThoughtState.Inactive;

            // If pawn isn't drafted, thought is inactive
            if (p.drafter == null || !p.Drafted)
            {
                return ThoughtState.Inactive; // Exit immediately if not drafted
            }
            // Basic checks: Does pawn exist? Has skills? Is humanlike? (Not mechanoid etc.)
            if (p == null || p.skills == null || !p.RaceProps.Humanlike || p.needs?.mood?.thoughts == null)
            {
                return ThoughtState.Inactive; // Thought inactive if conditions aren't met
            }

            // Get relevant skill levels
            int shootingSkill = p.skills.GetSkill(SkillDefOf.Shooting).Level;
            int meleeSkill = p.skills.GetSkill(SkillDefOf.Melee).Level;

            // Check highest condition first (shooting or melee >= 15)
            if (shootingSkill >= settings.masterThreshold || meleeSkill >= settings.masterThreshold)
            {
                return ThoughtState.ActiveAtStage(0);  // Activate stage 0 (first stage in XML, +7 mood)
            }
            // Check middle condition (shooting or melee 7<->14)
            else if (shootingSkill >= settings.skilledThreshold || meleeSkill >= settings.skilledThreshold)
            {
                return ThoughtState.ActiveAtStage(1);  // Activate stage 1 (second stage in XML, +3 mood)
            }
            else
            {
                return ThoughtState.Inactive;  // Thought is inactive
            }
        }
    }
}