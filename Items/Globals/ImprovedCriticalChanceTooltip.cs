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
            if (ModLoader.TryGetMod("ClickerClass", out _))
            {
                if (ModContent.TryFind("ClickerClass/ClickerDamage", out DamageClass clickerDamage))
                {
                    _damageClassLookup.Add(clickerDamage, clickerDamage);
                }
            }
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

                    string itemModName = item.ModItem?.Mod?.Name;

                    if (itemModName is null || !itemModName.Equals("CalamityMod"))
                    {
                        Item unmodifiedItem = new();
                        unmodifiedItem.CloneDefaults(item.type);

                        initialCriticalChance = unmodifiedItem.crit + BaseCriticalChance;
                    }
                    else
                    {
                        var matchedDamageClassCriticalChances = from damageClassEntry in _damageClassLookup
                                                                where damageClassEntry.Key == item.DamageType
                                                                select (int)player.GetTotalCritChance(damageClassEntry.Value);

                        if (!matchedDamageClassCriticalChances.Any())
                        {
                            return;
                        }

                        int classCriticalChance = matchedDamageClassCriticalChances.First();
                        var test = player.GetTotalCritChance(DamageClass.Generic);

                        initialCriticalChance = totalCriticalChance - classCriticalChance + BaseCriticalChance;
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

        private static bool IsCriticalChanceTooltip(in string[] tooltipData)
        {
            return Language.ActiveCulture.Name switch
            {
                "en-US" => tooltipData[^1].Equals("chance"),
                _ => false,
            };
        }

        private static int GetTotalCriticalChanceFromTooltip(in string[] tooltipData)
        {
            return Language.ActiveCulture.Name switch
            {
                "en-US" => int.Parse(tooltipData[0][0..^1]),
                _ => 0,
            };
        }

        private static void ReconstructTooltip(in TooltipLine tooltip, in string[] tooltipData, in int criticalChanceDelta)
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
