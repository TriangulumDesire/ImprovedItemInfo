using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class GlobalItemImprovedAmmoTooltip
        : GlobalItem
    {
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
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (item.ammo > 0)
            {
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (!tooltip.Name.Equals("Ammo"))
                    {
                        continue;
                    }

                    try
                    {
                        string ammoType = Lang.GetItemNameValue(item.ammo);

                        if (_ammoTypeIDLookup.ContainsKey(item.ammo))
                        {
                            ammoType = _ammoTypeIDLookup[item.ammo];
                        }

                        if (item.Name != ammoType)
                        {
                            tooltip.Text = $"Ammo in {ammoType} ([i:{item.ammo}]) category";
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
                    string ammoType = Lang.GetItemNameValue(item.useAmmo);

                    if (_ammoTypeIDLookup.ContainsKey(item.useAmmo))
                    {
                        ammoType = _ammoTypeIDLookup[item.useAmmo];
                    }

                    TooltipLine ammoTypeTooltip = null;

                    if (item.useAmmo == 283)
                    {
                        if (item.netID == 281 || item.netID == 986)
                        {
                            string seedAmmoType = Lang.GetItemNameValue(283);

                            ammoTypeTooltip = new(Mod, "useAmmo", $"Uses {seedAmmoType} ([i:{item.useAmmo}])/Dart ([i:1310]) as ammo");
                        }
                        else
                        {
                            ammoTypeTooltip = new(Mod, "useAmmo", $"Uses {ammoType} ([i:1310]) as ammo");
                        }
                    }
                    else
                    {
                        ammoTypeTooltip = new(Mod, "useAmmo", $"Uses {ammoType} ([i:{item.useAmmo}]) as ammo");
                    }

                    int knockbackTooltipIndex = tooltips.FindIndex(candidateTooltip => candidateTooltip.Name.Equals("Knockback"));

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
