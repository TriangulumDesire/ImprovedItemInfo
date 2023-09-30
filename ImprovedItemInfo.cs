using Terraria.ModLoader;

namespace ImprovedItemInfo
{
    public class ImprovedItemInfo
        : Mod
    {
        public static bool IsDamageImproved { get; set; }
        public static bool IsCriticalChanceImproved { get; set; }
        public static bool IsSpeedImproved { get; set; }
        public static bool DisplayMiningSpeedTooltip { get; set; }
        public static bool IsKnockbackImproved { get; set; }
        public static bool IsManaUseImproved { get; set; }
        public static bool DisplayProjectileVelocityTooltip { get; set; }
        public static bool DisplayProjectileVelocityMultiplierTooltip { get; set; }
        public static bool DisplayAmmoTooltips { get; set; }

        public static bool IsDamageColoured { get; set; }
        public static bool IsCriticalChanceColoured { get; set; }
        public static bool IsSpeedColoured { get; set; }
        public static bool IsMiningSpeedColoured { get; set; }
        public static bool IsKnockbackColoured { get; set; }
        public static bool IsManaUseColoured { get; set; }
        public static bool IsProjectileVelocityColoured { get; set; }

        public static bool IsClickEffectCountImproved { get; set; }
        public static bool IsClickEffectCountColoured { get; set; }

        public ImprovedItemInfo()
        {
            ContentAutoloadingEnabled = true;
        }
    }
}