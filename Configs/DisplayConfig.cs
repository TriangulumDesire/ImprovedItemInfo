using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ImprovedItemInfo.Configs
{
    public class DisplayConfig
        : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("$Mods.ImprovedItemInfo.Configs.DisplayConfig.Header.GeneralOptions")]

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.DamageDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.DamageDisplay.Tooltip")]
        public bool IsDamageImproved;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.CriticalChanceDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.CriticalChanceDisplay.Tooltip")]
        public bool IsCriticalChanceImproved;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.SpeedDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.SpeedDisplay.Tooltip")]
        public bool IsSpeedImproved;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.MiningSpeedDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.MiningSpeedDisplay.Tooltip")]
        public bool DisplayMiningSpeedTooltip;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.KnockbackDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.KnockbackDisplay.Tooltip")]
        public bool IsKnockbackImproved;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ManaUseDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ManaUseDisplay.Tooltip")]
        public bool IsManaUseImproved;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ProjectileVelocityDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ProjectileVelocityDisplay.Tooltip")]
        public bool DisplayProjectileVelocityTooltip;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ProjectileVelocityMultiplierDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ProjectileVelocityMultiplierDisplay.Tooltip")]
        public bool DisplayProjectileVelocityMultiplierTooltip;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.AmmoTooltipsDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.AmmoTooltipsDisplay.Tooltip")]
        public bool DisplayAmmoTooltips;

        [Header("$Mods.ImprovedItemInfo.Configs.DisplayConfig.Header.ColourOptions")]

        [DefaultValue(false)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredDamage.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredDamage.Tooltip")]
        public bool IsDamageColoured;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredCriticalChance.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredCriticalChance.Tooltip")]
        public bool IsCriticalChanceColoured;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredSpeed.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredSpeed.Tooltip")]
        public bool IsSpeedColoured;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredMiningSpeed.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredMiningSpeed.Tooltip")]
        public bool IsMiningSpeedColoured;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredKnockback.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredKnockback.Tooltip")]
        public bool IsKnockbackColoured;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredManaUse.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredManaUse.Tooltip")]
        public bool IsManaUseColoured;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredProjectileVelocity.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredProjectileVelocity.Tooltip")]
        public bool IsProjectileVelocityColoured;

        [Header("$Mods.ImprovedItemInfo.Configs.DisplayConfig.Header.CrossModSupportOptions")]

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ClickEffectCountDisplay.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ClickEffectCountDisplay.Tooltip")]
        public bool IsClickEffectCountImproved;

        [DefaultValue(true)]
        [LabelKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredClickEffectCount.Label")]
        [TooltipKey("$Mods.ImprovedItemInfo.Configs.DisplayConfig.ColouredClickEffectCount.Tooltip")]
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