using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class ImprovedManaUseTooltip
        : GlobalItem
    {
        private const string ManaUseTooltipName = "UseMana";

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ImprovedItemInfo.IsManaUseImproved || item.mana <= 0 || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            foreach (TooltipLine tooltip in tooltips)
            {
                if (!tooltip.Name.Equals(ManaUseTooltipName))
                {
                    continue;
                }

                try
                {
                    string[] tooltipLines = tooltip.Text.Split('\n');
                    string[] tooltipData = tooltipLines[0].Split(' ');

                    if (!IsManaUseTooltip(tooltipData))
                    {
                        return;
                    }

                    Item unmodifiedItem = new();
                    unmodifiedItem.CloneDefaults(item.type);

                    int totalManaUse = GetTotalManaUseFromTooltip(tooltipData);
                    int manaUseDelta = totalManaUse - unmodifiedItem.mana;

                    if (manaUseDelta != 0)
                    {
                        ReconstructTooltip(tooltip, tooltipData, tooltipLines, manaUseDelta);

                        if (ImprovedItemInfo.IsManaUseColoured)
                        {
                            tooltip.IsModifier = true;
                            tooltip.IsModifierBad = manaUseDelta > 0;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private static bool IsManaUseTooltip(in string[] tooltipData)
        {
            return Language.ActiveCulture.Name switch
            {
                "en-US" or "fr-FR" => tooltipData[^1].Equals("mana"),
                "de-DE" => tooltipData[^1].Equals("Mana"),
                "ru-RU" => tooltipData[^1].Equals("маны"),
                "zh-Hans" => tooltipData[^1].Contains("魔力"),
                _ => false,
            };
        }

        private static int GetTotalManaUseFromTooltip(in string[] tooltipData)
        {
            return Language.ActiveCulture.Name switch
            {
                "en-US" or "de-DE" or "fr-FR" or "ru-RU" => int.Parse(tooltipData[1]),
                "zh-Hans" => int.Parse(tooltipData[0]["使用".Length..^"魔力".Length]),
                _ => 0,
            };
        }

        private static void ReconstructTooltip(in TooltipLine tooltip, in string[] tooltipData, in string[] tooltipLines, in int manaUseDelta)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US" or "de-DE" or "fr-FR" or "ru-RU":
                    tooltip.Text = $"{tooltipData[0]} {tooltipData[1]} ({(manaUseDelta > 0 ? "+" : "-")}{Math.Abs(manaUseDelta)})";

                    foreach (string tooltipElement in tooltipData.Skip(2))
                    {
                        tooltip.Text += " " + tooltipElement;
                    }

                    foreach (string tooltipLine in tooltipLines.Skip(1))
                    {
                        tooltip.Text += "\n" + tooltipLine;
                    }

                    break;

                case "zh-Hans":
                    tooltip.Text = tooltipData[0][..^"魔力".Length];
                    tooltip.Text += $"({(manaUseDelta > 0 ? "+" : "-")}{Math.Abs(manaUseDelta)})";
                    tooltip.Text += "魔力";

                    foreach (string tooltipElement in tooltipData.Skip(2))
                    {
                        tooltip.Text += " " + tooltipElement;
                    }

                    foreach (string tooltipLine in tooltipLines.Skip(1))
                    {
                        tooltip.Text += "\n" + tooltipLine;
                    }

                    break;

                default:
                    break;
            }
        }
    }
}
