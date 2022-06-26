using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class ImprovedCriticalChanceTooltip
        : GlobalItem
    {
        private const string CriticalChanceTooltipName = "CritChance";

        private static readonly Dictionary<DamageClass, DamageClass> _damageClassLookup = new()
        {
            { DamageClass.Melee, DamageClass.Melee },
            { DamageClass.Magic, DamageClass.Magic },
            { DamageClass.Ranged, DamageClass.Ranged },
            { DamageClass.Summon, DamageClass.Summon },
            { DamageClass.MeleeNoSpeed, DamageClass.Melee },
            { DamageClass.SummonMeleeSpeed, DamageClass.Melee },
            { DamageClass.MagicSummonHybrid, DamageClass.Magic },
            { DamageClass.Throwing, DamageClass.Throwing },
        };

        public ImprovedCriticalChanceTooltip()
        {
            // TODO: Add damage classes from other mods into lookup table (rogue, clicker, healer, bard, et cetera).
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.damage <= 0 || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            Player player = Main.player[item.playerIndexTheItemIsReservedFor];

            foreach (TooltipLine tooltip in tooltips)
            {
                if (!tooltip.Name.Equals(CriticalChanceTooltipName))
                {
                    continue;
                }

                try
                {
                    string[] tooltipData = tooltip.Text.Split(' ');

                    if (!IsCriticalChanceTooltip(tooltipData))
                    {
                        return;
                    }

                    const int BaseCriticalChance = 4;
                    int totalCriticalChance = GetTotalCriticalChanceFromTooltip(tooltipData);
                    int initialCriticalChance = 0;

                    if (item.ModItem is null)
                    {
                        Item unmodifiedItem = new();
                        unmodifiedItem.CloneDefaults(item.type);

                        initialCriticalChance = unmodifiedItem.crit + BaseCriticalChance;
                    }
                    else
                    {
                        var matchedDamageClassCriticalChances = from damageClassEntry in _damageClassLookup
                                                                where damageClassEntry.Key == item.DamageType
                                                                select (int)player.GetCritChance(damageClassEntry.Value);

                        if (!matchedDamageClassCriticalChances.Any())
                        {
                            return;
                        }

                        int classCriticalChance = matchedDamageClassCriticalChances.First();

                        initialCriticalChance = totalCriticalChance - (int)player.GetCritChance(DamageClass.Generic) - classCriticalChance + BaseCriticalChance;
                    }

                    int criticalChanceDelta = totalCriticalChance - initialCriticalChance;

                    if (criticalChanceDelta != 0)
                    {
                        ReconstructTooltip(tooltip, tooltipData, criticalChanceDelta);

                        if (ImprovedItemInfo.IsCriticalChanceColoured)
                        {
                            tooltip.IsModifier = true;
                            tooltip.IsModifierBad = criticalChanceDelta < 0;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private bool IsCriticalChanceTooltip(in string[] tooltipData)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    return tooltipData[^1].Equals("chance");

                default:
                    return false;
            }
        }

        private int GetTotalCriticalChanceFromTooltip(in string[] tooltipData)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    return int.Parse(tooltipData[0][0..^1]);

                default:
                    return 0;
            }
        }

        private void ReconstructTooltip(in TooltipLine tooltip, in string[] tooltipData, in int criticalChanceDelta)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    tooltip.Text = $"{tooltipData[0]} ({(criticalChanceDelta > 0 ? "+" : "-")}{Math.Abs(criticalChanceDelta)}%)";

                    foreach (string tooltipElement in tooltipData.Skip(1))
                    {
                        tooltip.Text += " " + tooltipElement;
                    }

                    break;
            }
        }
    }
}
