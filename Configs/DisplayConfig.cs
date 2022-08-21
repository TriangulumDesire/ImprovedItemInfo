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

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.DamageDisplay.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.DamageDisplay.Tooltip")]
        public bool IsDamageImproved;

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.CriticalChanceDisplay.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.CriticalChanceDisplay.Tooltip")]
        public bool IsCriticalChanceImproved;

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.SpeedDisplay.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.SpeedDisplay.Tooltip")]
        public bool IsSpeedImproved;

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.KnockbackDisplay.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.KnockbackDisplay.Tooltip")]
        public bool IsKnockbackImproved;

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.ManaUseDisplay.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.ManaUseDisplay.Tooltip")]
        public bool IsManaUseImproved;

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.ProjectileVelocityDisplay.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.ProjectileVelocityDisplay.Tooltip")]
        public bool DisplayProjectileVelocityTooltip;

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.ProjectileVelocityMultiplierDisplay.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.ProjectileVelocityMultiplierDisplay.Tooltip")]
        public bool DisplayProjectileVelocityMultiplierTooltip;

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.AmmoTooltipsDisplay.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.AmmoTooltipsDisplay.Tooltip")]
        public bool DisplayAmmoTooltips;

        [Header("$Mods.ImprovedItemInfo.DisplayConfig.Header.ColourOptions")]

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
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.ColouredProjectileVelocity.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.ColouredProjectileVelocity.Tooltip")]
        public bool IsProjectileVelocityColoured;

        [Header("$Mods.ImprovedItemInfo.DisplayConfig.Header.CrossModSupportOptions")]

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.ClickEffectCountDisplay.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.ClickEffectCountDisplay.Tooltip")]
        public bool IsClickEffectCountImproved;

        [DefaultValue(true)]
        [Label("$Mods.ImprovedItemInfo.DisplayConfig.ColouredClickEffectCount.Label")]
        [Tooltip("$Mods.ImprovedItemInfo.DisplayConfig.ColouredClickEffectCount.Tooltip")]
        public bool IsClickEffectCountColoured;

        public override void OnChanged()
        {
            ImprovedItemInfo.IsDamageImproved = IsDamageImproved;
            ImprovedItemInfo.IsCriticalChanceImproved = IsCriticalChanceImproved;
            ImprovedItemInfo.IsKnockbackImproved = IsKnockbackImproved;
            ImprovedItemInfo.IsSpeedImproved = IsSpeedImproved;
            ImprovedItemInfo.IsManaUseImproved = IsManaUseImproved;
            ImprovedItemInfo.DisplayProjectileVelocityTooltip = DisplayProjectileVelocityTooltip;
            ImprovedItemInfo.DisplayProjectileVelocityMultiplierTooltip = DisplayProjectileVelocityMultiplierTooltip;
            ImprovedItemInfo.DisplayAmmoTooltips = DisplayAmmoTooltips;

            ImprovedItemInfo.IsDamageColoured = IsDamageColoured;
            ImprovedItemInfo.IsCriticalChanceColoured = IsCriticalChanceColoured;
            ImprovedItemInfo.IsSpeedColoured = IsSpeedColoured;
            ImprovedItemInfo.IsKnockbackColoured = IsKnockbackColoured;
            ImprovedItemInfo.IsManaUseColoured = IsManaUseColoured;
            ImprovedItemInfo.IsProjectileVelocityColoured = IsProjectileVelocityColoured;

            ImprovedItemInfo.IsClickEffectCountImproved = IsClickEffectCountImproved;
            ImprovedItemInfo.IsClickEffectCountColoured = IsClickEffectCountColoured;
        }
    }
}