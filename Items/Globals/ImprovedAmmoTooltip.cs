using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class ImprovedAmmoTooltip
        : GlobalItem
    {
        private const string AmmoTooltipName = "Ammo";
        private const string UseAmmoTooltipName = "UseAmmo";
        private const string KnockbackTooltipName = "Knockback";

        private const int BlowpipeInternalID = 281;
        private const int BlowgunInternalID = 986;

        private const int SeedInternalID = 283;

        private static readonly Dictionary<int, string> _ammoTypeIDLookup = new()
        {
            { 23, "Gel" },
            { 40, "Arrow" },
            { 71, "Coin" },
            { 97, "Bullet" },
            { 283, "Dart" },
            { 771, "Rocket" },
            { 780, "Solution" },
            { 931, "Flare" },
        };

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ImprovedItemInfo.DisplayAmmoTooltips || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (item.ammo > 0)
            {
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (!tooltip.Name.Equals(AmmoTooltipName))
                    {
                        continue;
                    }

                    try
                    {
                        string ammoType = _ammoTypeIDLookup.TryGetValue(item.ammo, out string ammoName)
                            ? Language.GetTextValue($"Mods.ImprovedItemInfo.AmmoCategory.{ammoName}")
                            : Lang.GetItemNameValue(item.ammo);

                        if (item.Name != ammoType)
                        {
                            tooltip.Text = Language.GetTextValue("Mods.ImprovedItemInfo.Tooltips.AmmoCategory", ammoType, item.ammo);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            if (item.useAmmo > 0)
            {
                try
                {
                    string ammoType = _ammoTypeIDLookup.TryGetValue(item.useAmmo, out string ammoName)
                        ? Language.GetTextValue($"Mods.ImprovedItemInfo.AmmoCategory.{ammoName}")
                        : Lang.GetItemNameValue(item.useAmmo);

                    TooltipLine ammoTypeTooltip = null;

                    if (item.useAmmo == SeedInternalID)
                    {
                        if (item.netID == BlowpipeInternalID || item.netID == BlowgunInternalID)
                        {
                            string tooltipText = Language.GetTextValue("Mods.ImprovedItemInfo.Tooltips.UsesSeedAndDartAmmunition", Lang.GetItemNameValue(SeedInternalID), item.useAmmo);

                            if (tooltipText is null)
                            {
                                return;
                            }

                            ammoTypeTooltip = new(Mod, UseAmmoTooltipName, tooltipText);
                        }
                        else
                        {
                            string tooltipText = Language.GetTextValue("Mods.ImprovedItemInfo.Tooltips.UsesDartAmmunition", ammoType);

                            if (tooltipText is null)
                            {
                                return;
                            }

                            ammoTypeTooltip = new(Mod, UseAmmoTooltipName, tooltipText);
                        }
                    }
                    else
                    {
                        string tooltipText = Language.GetTextValue("Mods.ImprovedItemInfo.Tooltips.UsesAmmunition", ammoType, item.useAmmo);

                        if (tooltipText is null)
                        {
                            return;
                        }

                        ammoTypeTooltip = new(Mod, UseAmmoTooltipName, tooltipText);
                    }

                    int knockbackTooltipIndex = tooltips.FindIndex(candidateTooltip => candidateTooltip.Name.Equals(KnockbackTooltipName));

                    if (knockbackTooltipIndex == -1 || knockbackTooltipIndex == tooltips.Count - 1)
                    {
                        tooltips.Add(ammoTypeTooltip);
                    }
                    else
                    {
                        tooltips.Insert(knockbackTooltipIndex + 1, ammoTypeTooltip);
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
