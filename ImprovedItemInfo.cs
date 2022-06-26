using Terraria.ModLoader;

namespace ImprovedItemInfo
{
    public class ImprovedItemInfo
        : Mod
    {
        public static bool IsDamageColoured { get; set; }
        public static bool IsCriticalChanceColoured { get; set; }
        public static bool IsSpeedColoured { get; set; }
        public static bool IsKnockbackColoured { get; set; }
        public static bool IsManaUseColoured { get; set; }
        public static bool DisplayAmmoTooltips { get; set; }

        public ImprovedItemInfo()
        {
            ContentAutoloadingEnabled = true;
        }
    }
}