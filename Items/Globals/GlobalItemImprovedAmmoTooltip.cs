﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class GlobalItemImprovedAmmoTooltip
        : GlobalItem
    {
        private const string AmmoTooltipName = "Ammo";
        private const string UseAmmoTooltipName = "UseAmmo";
        private const string KnockbackTooltipName = "Knockback";

        private const int BlowpipeInternalID = 281;
        private const int BlowgunInternalID = 986;

        private const int SeedInternalID = 283;
        private const int PoisonDartInternalID = 1310;

        private static readonly Dictionary<string, Dictionary<int, string>> _ammoTypeIDLookup = new()
        {
            {
                "en-US", new()
                {
                    { 23, "Gel" },
                    { 40, "Arrow" },
                    { 71, "Coin" },
                    { 97, "Bullet" },
                    { 283, "Dart" },
                    { 771, "Rocket" },
                    { 780, "Solution" },
                    { 931, "Flare" },
                }
            },
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
                    if (!tooltip.Name.Equals(AmmoTooltipName))
                    {
                        continue;
                    }

                    try
                    {
                        string ammoType = Lang.GetItemNameValue(item.ammo);

                        if (_ammoTypeIDLookup.ContainsKey(Language.ActiveCulture.Name))
                        {
                            var localisedAmmoTypeIDLookup = _ammoTypeIDLookup[Language.ActiveCulture.Name];

                            if (localisedAmmoTypeIDLookup.ContainsKey(item.ammo))
                            {
                                ammoType = localisedAmmoTypeIDLookup[item.ammo];
                            }
                        }

                        if (item.Name != ammoType)
                        {
                            tooltip.Text = GetAmmoCategoryText(ammoType, item.ammo);
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

                    if (_ammoTypeIDLookup.ContainsKey(Language.ActiveCulture.Name))
                    {
                        var localisedAmmoTypeIDLookup = _ammoTypeIDLookup[Language.ActiveCulture.Name];

                        if (localisedAmmoTypeIDLookup.ContainsKey(item.useAmmo))
                        {
                            ammoType = localisedAmmoTypeIDLookup[item.useAmmo];
                        }
                    }

                    TooltipLine ammoTypeTooltip = null;

                    if (item.useAmmo == SeedInternalID)
                    {
                        if (item.netID == BlowpipeInternalID || item.netID == BlowgunInternalID)
                        {
                            string tooltipText = GetSeedAndDartUseAmmoCategoryText(item.useAmmo);

                            if (tooltipText is null)
                            {
                                return;
                            }

                            ammoTypeTooltip = new(Mod, UseAmmoTooltipName, tooltipText);
                        }
                        else
                        {
                            string tooltipText = GetDartUseAmmoCategoryText(ammoType);

                            if (tooltipText is null)
                            {
                                return;
                            }

                            ammoTypeTooltip = new(Mod, UseAmmoTooltipName, tooltipText);
                        }
                    }
                    else
                    {
                        string tooltipText = GetUseAmmoCategoryText(ammoType, item.useAmmo);

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

        private string GetAmmoCategoryText(in string ammoType, in int itemAmmoID)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    return $"Ammo in {ammoType} ([i:{itemAmmoID}]) category";

                default:
                    return null;
            }
        }

        private string GetSeedAndDartUseAmmoCategoryText(in int itemUseAmmoID)
        {
            string seedAmmoType = Lang.GetItemNameValue(SeedInternalID);

            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    return $"Uses {seedAmmoType} ([i:{itemUseAmmoID}])/Dart ([i:{PoisonDartInternalID}]) as ammo";

                default:
                    return null;
            }
        }

        private string GetDartUseAmmoCategoryText(in string ammoType)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    return $"Uses {ammoType} ([i:{PoisonDartInternalID}]) as ammo";

                default:
                    return null;
            }
        }

        private string GetUseAmmoCategoryText(in string ammoType, in int itemUseAmmoID)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    return $"Uses {ammoType} ([i:{itemUseAmmoID}]) as ammo";

                default:
                    return null;
            }
        }
    }
}
