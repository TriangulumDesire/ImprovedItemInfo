using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ImprovedItemInfo.Configs
{
    [Label("$Mods.ImprovedItemInfo.DisplayConfig.Label")]
    public class DisplayConfig
        : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("$Mods.ImprovedItemInfo.DisplayConfig.Header.GeneralOptions")]

        [DefaultValue(false)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.ColouredDamage.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.ColouredDamage.Tooltip")]
        public bool IsDamageColoured;

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.ColouredCriticalChance.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.ColouredCriticalChance.Tooltip")]
        public bool IsCriticalChanceColoured;

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.ColouredSpeed.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.ColouredSpeed.Tooltip")]
        public bool IsSpeedColoured;

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.ColouredKnockback.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.ColouredKnockback.Tooltip")]
        public bool IsKnockbackColoured;

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.ColouredManaUse.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.ColouredManaUse.Tooltip")]
        public bool IsManaUseColoured;

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.DisplayAmmoTooltips.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.DisplayAmmoTooltips.Tooltip")]
        public bool DisplayAmmoTooltips;

        [Header("$Mods.ImprovedItemInfo.DisplayConfig.Header.CrossModSupportOptions")]

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.ColouredClickEffectCount.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.ColouredClickEffectCount.Tooltip")]
        public bool IsClickEffectCountColoured;


        public override void OnChanged()
        {
            ImprovedItemInfo.IsDamageColoured = IsDamageColoured;
            ImprovedItemInfo.IsCriticalChanceColoured = IsCriticalChanceColoured;
            ImprovedItemInfo.IsSpeedColoured = IsSpeedColoured;
            ImprovedItemInfo.IsKnockbackColoured = IsKnockbackColoured;
            ImprovedItemInfo.IsManaUseColoured = IsManaUseColoured;
            ImprovedItemInfo.DisplayAmmoTooltips = DisplayAmmoTooltips;

            ImprovedItemInfo.IsClickEffectCountColoured = IsClickEffectCountColoured;
        }
    }
}