using UnityEngine;
using Verse;

namespace DraftMoodMod
{
    public class SettingsInterface : Mod
    {
        public static MyModSettings settings; 

        public SettingsInterface(ModContentPack content) : base(content)
        {
            // Load or create settings for the first time
            settings = GetSettings<MyModSettings>();
        }

        // Name that will appear in the mod options list
        public override string SettingsCategory()
        {
            return "DraftMoodMod_SettingsCategory".Translate(); // Translated name for the settings category
        }


        private void DrawLabelAndSlider(Listing_Standard listing, string labelKey, ref int value, float min, float max, float labelWidthPct = 0.7f)
        {
            float rowHeight = 30f; // Text.LineHeight * 1.5f

            // Get an area ("Rect") of this height from Listing_Standard
            Rect rowRect = listing.GetRect(rowHeight);

            // Split area: Create Rects for Label and Slider
            // Label part (left, percentage of width)
            Rect labelRect = new Rect(
                rowRect.x,
                rowRect.y,
                rowRect.width * labelWidthPct,
                rowRect.height
            );

            // Slider part (right, remaining width, leaving a small gap)
            float gap = rowRect.width * 0.05f; // %5 space
            float sliderWidth = rowRect.width * (1f - labelWidthPct - 0.05f);   // Remaining width after label and gap
            Rect sliderRect = new Rect(
                labelRect.xMax + gap, // Start from the end of the label rect + gap
                rowRect.y,
                sliderWidth,
                rowRect.height
            );

            // --- Draw ---

            // 1. Draw the Label
            TextAnchor originalAnchor = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleLeft; // Center the label vertically, left-align it horizontally

            Widgets.Label(labelRect, $"{labelKey.Translate()}: {value}");   // Translate the label key and append the current value
            Text.Anchor = originalAnchor; // Restore original anchor

            // 2. Draw the Slider
            int newValue = (int)Widgets.HorizontalSlider(
                sliderRect,       // Area where the slider will be drawn  
                value,            // Current value (converted to float)  
                min,              // Minimum value (should be float)  
                max,              // Maximum value (should be float)  
                true,             // Clamp value between min/max range? Yes.  
                null,             // Extra label to display on slider (we don't want this)  
                null,             // Label to display on the left of slider (we don't want this)  
                null,             // Label to display on the right of slider (we don't want this)  
                1f                // Step size for rounding the value (1 = integers)  
            );

            // 3. Update the value if it has changed
            if (newValue != value)
            {
                value = newValue;
            }

            // 4. Add the standard space after this special line (to match the other elements of Listing_Standard)
            listing.Gap(listing.verticalSpacing);
        }

        // Method that draws the contents of the settings window
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            // --- Master Warrior Threshold ---
            DrawLabelAndSlider(listingStandard, "DraftMoodMod_MasterThresholdSliderLabel", ref settings.masterThreshold, 0f, 20f); // Range 0-20

            // --- Skilled Warrior Threshold ---
            DrawLabelAndSlider(listingStandard, "DraftMoodMod_SkilledThresholdSliderLabel", ref settings.skilledThreshold, 0f, 20f); // Range 0-20

            // --- Master Warrior Mood Bonus ---
            DrawLabelAndSlider(listingStandard, "DraftMoodMod_MasterMoodBonusSliderLabel", ref settings.masterMoodBonus, 0f, 20f); // Range 0-20

            // --- Skilled Warrior Mood Bonus ---
            DrawLabelAndSlider(listingStandard, "DraftMoodMod_SkilledMoodBonusSliderLabel", ref settings.skilledMoodBonus, 0f, 20f); // Range 0-20
            listingStandard.GapLine(24f); // Slightly larger gap

            // --- Reset Button ---
            if (listingStandard.ButtonText("DraftMoodMod_ResetButton".Translate()))
            {
                settings.ResetToDefaults();
            }

            listingStandard.End();
            base.DoSettingsWindowContents(inRect); // Call base class method
        }

        // Called when settings window closes (optional but good practice to implement)
        public override void WriteSettings()
        {
            base.WriteSettings();
            // GetSettings<>() already triggers saving of settings,
            // but additional checks or logging could be done here.
        }
    }
}