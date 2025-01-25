using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class ImprovedDamageTooltip
        : GlobalItem
    {
        private const string DamageTooltipName = "Damage";

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ImprovedItemInfo.IsDamageImproved || item.damage <= 0 || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            foreach (TooltipLine tooltip in tooltips)
            {
                if (!tooltip.Name.Equals(DamageTooltipName))
                {
                    continue;
                }

                try
                {
                    string[] tooltipData = tooltip.Text.Split(' ');

                    if (!IsDamageTooltip(tooltipData))
                    {
                        return;
                    }

                    Item unmodifiedItem = new();
                    unmodifiedItem.CloneDefaults(item.type);

                    int totalDamage = GetTotalDamageFromTooltip(tooltipData);
                    int damageDelta = totalDamage - unmodifiedItem.damage;

                    if (damageDelta != 0)
                    {
                        ReconstructTooltip(tooltip, tooltipData, damageDelta);

                        if (ImprovedItemInfo.IsDamageColoured)
                        {
                            tooltip.IsModifier = true;
                            tooltip.IsModifierBad = damageDelta < 0;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private static bool IsDamageTooltip(in string[] tooltipData)
        {
            return Language.ActiveCulture.Name switch
            {
                "en-US" => tooltipData[^1].Equals("damage"),
                "de-DE" => tooltipData[^1].Equals("Schaden"),
                "fr-FR" => tooltipData[2].Equals("dégâts"),
                "ru-RU" => tooltipData[^1].Equals("урон"),
                "zh-Hans" => tooltipData[^1].EndsWith("伤害"),
                _ => false,
            };
        }

        private static int GetTotalDamageFromTooltip(in string[] tooltipData)
        {
            return Language.ActiveCulture.Name switch
            {
                "en-US" or "de-DE" or "fr-FR" or "ru-RU" or "zh-Hans" => int.Parse(tooltipData[0]),
                _ => 0,
            };
        }

        private static void ReconstructTooltip(in TooltipLine tooltip, in string[] tooltipData, in int damageDelta)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US" or "de-DE" or "fr-FR" or "ru-RU" or "zh-Hans":
                    tooltip.Text = $"{tooltipData[0]} ({(damageDelta > 0 ? "+" : "-")}{Math.Abs(damageDelta)})";

                    foreach (string tooltipElement in tooltipData.Skip(1))
                    {
                        tooltip.Text += " " + tooltipElement;
                    }

                    break;

                default:
                    break;
            }
        }
    }
}
