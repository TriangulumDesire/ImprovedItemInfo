using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ImprovedItemInfo.Configs
{
    public class DisplayConfig
        : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("General Options")]

        [DefaultValue(false)]
        [Label("Coloured Damage Tooltip")]
        [Tooltip("Whether to use red/green colouring for the damage tooltip")]
        public bool IsDamageColoured;

        [DefaultValue(true)]
        [Label("Coloured Critical Chance Tooltip")]
        [Tooltip("Whether to use red/green colouring for the critical chance tooltip")]
        public bool IsCriticalChanceColoured;

        [DefaultValue(true)]
        [Label("Coloured Speed Tooltip")]
        [Tooltip("Whether to use red/green colouring for the speed tooltip")]
        public bool IsSpeedColoured;

        [DefaultValue(true)]
        [Label("Coloured Knockback Tooltip")]
        [Tooltip("Whether to use red/green colouring for the knockback tooltip")]
        public bool IsKnockbackColoured;

        [DefaultValue(true)]
        [Label("Coloured Mana Use Tooltip")]
        [Tooltip("Whether to use red/green colouring for the mana use tooltip")]
        public bool IsManaUseColoured;

        [DefaultValue(true)]
        [Label("Display Ammo Tooltips")]
        [Tooltip("Whether to display tooltips for ammo categories")]
        public bool DisplayAmmoTooltips;

        [Header("Cross-mod Support Options")]

        [DefaultValue(true)]
        [Label("Coloured Click Effect Count Tooltip")]
        [Tooltip("Whether to use red/green colouring for the click effect count tooltip (Clicker Class)")]
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