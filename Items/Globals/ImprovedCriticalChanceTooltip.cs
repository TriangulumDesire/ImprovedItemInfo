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
            if (ModLoader.TryGetMod("ClickerClass", out _))
            {
                if (ModContent.TryFind("ClickerClass/ClickerDamage", out DamageClass clickerDamage))
                {
                    _damageClassLookup.Add(clickerDamage, clickerDamage);
                }
            }

            if (ModLoader.TryGetMod("CalamityMod", out _))
            {
                if (ModContent.TryFind("CalamityMod/RogueDamageClass", out DamageClass rogueDamage))
                {
                    _damageClassLookup.Add(rogueDamage, rogueDamage);
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ImprovedItemInfo.IsCriticalChanceImproved || item.damage <= 0 || Main.netMode == NetmodeID.Server)
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

                        initialCriticalChance = totalCriticalChance - classCriticalChance - item.crit + BaseCriticalChance;
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
                "de-DE" => tooltipData[^1].Equals("Trefferchance"),
                "fr-FR" => tooltipData[^1].Equals("critique"),
                "ru-RU" => tooltipData[^1].Equals("удара"),
                "zh-Hans" => tooltipData[^1].EndsWith("暴击率"),
                _ => false,
            };
        }

        private static int GetTotalCriticalChanceFromTooltip(in string[] tooltipData)
        {
            return Language.ActiveCulture.Name switch
            {
                "en-US" or "de-DE" or "fr-FR" or "ru-RU" => int.Parse(tooltipData[0][0..^1]),
                "zh-Hans" => int.Parse(tooltipData[0].Split('%')[0]),
                _ => 0,
            };
        }

        private static void ReconstructTooltip(in TooltipLine tooltip, in string[] tooltipData, in int criticalChanceDelta)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US" or "de-DE" or "fr-FR" or "ru-RU":
                    tooltip.Text = $"{tooltipData[0]} ({(criticalChanceDelta > 0 ? "+" : "-")}{Math.Abs(criticalChanceDelta)}%)";

                    foreach (string tooltipElement in tooltipData.Skip(1))
                    {
                        tooltip.Text += " " + tooltipElement;
                    }

                    break;

                case "zh-Hans":
                    string[] splitTooltipData = tooltipData[0].Split('%');

                    tooltip.Text = $"{splitTooltipData[0]}% ({(criticalChanceDelta > 0 ? "+" : "-")}{Math.Abs(criticalChanceDelta)}%)";

                    foreach (string splitTooltipDataElement in splitTooltipData.Skip(1))
                    {
                        tooltip.Text += splitTooltipDataElement;
                    }

                    foreach (string tooltipElement in tooltipData.Skip(1))
                    {
                        tooltip.Text += tooltipElement;
                    }

                    break;

                default:
                    break;
            }
        }
    }
}
