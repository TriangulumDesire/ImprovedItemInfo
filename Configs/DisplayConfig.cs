using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ImprovedItemInfo.Configs
{
    public class DisplayConfig
        : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("$Mods.ImprovedItemInfo.DisplayConfig.Header.GeneralOptions")]

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.DamageDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.DamageDisplay.Tooltip")]
        public bool IsDamageImproved;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.CriticalChanceDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.CriticalChanceDisplay.Tooltip")]
        public bool IsCriticalChanceImproved;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.SpeedDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.SpeedDisplay.Tooltip")]
        public bool IsSpeedImproved;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.MiningSpeedDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.MiningSpeedDisplay.Tooltip")]
        public bool DisplayMiningSpeedTooltip;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.KnockbackDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.KnockbackDisplay.Tooltip")]
        public bool IsKnockbackImproved;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.ManaUseDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.ManaUseDisplay.Tooltip")]
        public bool IsManaUseImproved;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.ProjectileVelocityDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.ProjectileVelocityDisplay.Tooltip")]
        public bool DisplayProjectileVelocityTooltip;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.ProjectileVelocityMultiplierDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.ProjectileVelocityMultiplierDisplay.Tooltip")]
        public bool DisplayProjectileVelocityMultiplierTooltip;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.AmmoTooltipsDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.AmmoTooltipsDisplay.Tooltip")]
        public bool DisplayAmmoTooltips;

        [Header("$Mods.ImprovedItemInfo.DisplayConfig.Header.ColourOptions")]

        [DefaultValue(false)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredDamage.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredDamage.Tooltip")]
        public bool IsDamageColoured;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredCriticalChance.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredCriticalChance.Tooltip")]
        public bool IsCriticalChanceColoured;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredSpeed.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredSpeed.Tooltip")]
        public bool IsSpeedColoured;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredMiningSpeed.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredMiningSpeed.Tooltip")]
        public bool IsMiningSpeedColoured;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredKnockback.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredKnockback.Tooltip")]
        public bool IsKnockbackColoured;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredManaUse.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredManaUse.Tooltip")]
        public bool IsManaUseColoured;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredProjectileVelocity.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredProjectileVelocity.Tooltip")]
        public bool IsProjectileVelocityColoured;

        [Header("$Mods.ImprovedItemInfo.DisplayConfig.Header.CrossModSupportOptions")]

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.ClickEffectCountDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.ClickEffectCountDisplay.Tooltip")]
        public bool IsClickEffectCountImproved;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredClickEffectCount.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.DisplayConfig.ColouredClickEffectCount.Tooltip")]
        public bool IsClickEffectCountColoured;

        public override void OnChanged()
        {
            ImprovedItemInfo.IsDamageImproved = IsDamageImproved;
            ImprovedItemInfo.IsCriticalChanceImproved = IsCriticalChanceImproved;
            ImprovedItemInfo.IsKnockbackImproved = IsKnockbackImproved;
            ImprovedItemInfo.IsSpeedImproved = IsSpeedImproved;
            ImprovedItemInfo.DisplayMiningSpeedTooltip = DisplayMiningSpeedTooltip;
            ImprovedItemInfo.IsManaUseImproved = IsManaUseImproved;
            ImprovedItemInfo.DisplayProjectileVelocityTooltip = DisplayProjectileVelocityTooltip;
            ImprovedItemInfo.DisplayProjectileVelocityMultiplierTooltip = DisplayProjectileVelocityMultiplierTooltip;
            ImprovedItemInfo.DisplayAmmoTooltips = DisplayAmmoTooltips;

            ImprovedItemInfo.IsDamageColoured = IsDamageColoured;
            ImprovedItemInfo.IsCriticalChanceColoured = IsCriticalChanceColoured;
            ImprovedItemInfo.IsSpeedColoured = IsSpeedColoured;
            ImprovedItemInfo.IsMiningSpeedColoured = IsMiningSpeedColoured;
            ImprovedItemInfo.IsKnockbackColoured = IsKnockbackColoured;
            ImprovedItemInfo.IsManaUseColoured = IsManaUseColoured;
            ImprovedItemInfo.IsProjectileVelocityColoured = IsProjectileVelocityColoured;

            ImprovedItemInfo.IsClickEffectCountImproved = IsClickEffectCountImproved;
            ImprovedItemInfo.IsClickEffectCountColoured = IsClickEffectCountColoured;
        }
    }
}