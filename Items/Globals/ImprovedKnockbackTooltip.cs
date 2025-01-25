﻿using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class ImprovedKnockbackTooltip
        : GlobalItem
    {
        private const string KnockbackTooltipName = "Knockback";

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ImprovedItemInfo.IsKnockbackImproved || item.knockBack <= 0.0f || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            foreach (TooltipLine tooltip in tooltips)
            {
                if (!tooltip.Name.Equals(KnockbackTooltipName))
                {
                    continue;
                }

                try
                {
                    string[] tooltipLines = tooltip.Text.Split('\n');
                    string[] tooltipData = tooltipLines[0].Split(' ');

                    if (!IsKnockbackTooltip(tooltipData))
                    {
                        return;
                    }

                    Item unmodifiedItem = new();
                    unmodifiedItem.CloneDefaults(item.type);

                    float totalKnockback = item.knockBack;
                    float knockbackDelta = (float)Math.Round(totalKnockback - unmodifiedItem.knockBack, 3);

                    ReconstructTooltip(tooltip, tooltipData, tooltipLines, totalKnockback, knockbackDelta);

                    if (ImprovedItemInfo.IsKnockbackColoured && Math.Abs(knockbackDelta) > float.Epsilon)
                    {
                        tooltip.IsModifier = true;
                        tooltip.IsModifierBad = knockbackDelta < 0.0f;
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private static bool IsKnockbackTooltip(in string[] tooltipData)
        {
            return Language.ActiveCulture.Name switch
            {
                "en-US" => tooltipData[^1].Equals("knockback"),
                "de-DE" => tooltipData[^1].Equals("Rückstoß"),
                "fr-FR" => tooltipData[0].Equals("Recul"),
                "ru-RU" => tooltipData[^1].Equals("отбрасывание"),
                "zh-Hans" => tooltipData[^1].EndsWith("击退力"),
                _ => false,
            };
        }

        private static void ReconstructTooltip(in TooltipLine tooltip, in string[] tooltipData, in string[] tooltipLines, in float totalKnockback, in float knockbackDelta)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US" or "de-DE" or "ru-RU":
                    tooltip.Text = Math.Round(totalKnockback, 2).ToString();

                    if (Math.Abs(knockbackDelta) > float.Epsilon)
                    {
                        tooltip.Text += $" ({(knockbackDelta > 0.0f ? "+" : "-")}{Math.Abs(Math.Round(knockbackDelta, 2))})";
                    }

                    for (int i = 0; i < tooltipData.Length; ++i)
                    {
                        tooltip.Text += ((i == 0) ? " (" : " ") + tooltipData[i] + ((i == tooltipData.Length - 2) ? ")" : "");
                    }

                    foreach (string tooltipLine in tooltipLines.Skip(1))
                    {
                        tooltip.Text += "\n" + tooltipLine;
                    }

                    break;

                case "fr-FR":
                    tooltip.Text = Math.Round(totalKnockback, 2).ToString();

                    if (Math.Abs(knockbackDelta) > float.Epsilon)
                    {
                        tooltip.Text += $" ({(knockbackDelta > 0.0f ? "+" : "-")}{Math.Abs(Math.Round(knockbackDelta, 2))})";
                    }

                    for (int i = 0; i < tooltipData.Length; ++i)
                    {
                        string tooltipDataToUse = tooltipData[i];

                        if (i == 0)
                        {
                            tooltipDataToUse = tooltipDataToUse.ToLowerInvariant();
                        }

                        tooltip.Text += ((i == 1) ? " (" : " ") + tooltipDataToUse + ((i == tooltipData.Length - 1) ? ")" : "");
                    }

                    foreach (string tooltipLine in tooltipLines.Skip(1))
                    {
                        tooltip.Text += "\n" + tooltipLine;
                    }

                    break;

                case "zh-Hans":
                    tooltip.Text = Math.Round(totalKnockback, 2).ToString();

                    if (Math.Abs(knockbackDelta) > float.Epsilon)
                    {
                        tooltip.Text += $" ({(knockbackDelta > 0.0f ? "+" : "-")}{Math.Abs(Math.Round(knockbackDelta, 2))})";
                    }

                    tooltip.Text += "(";
                    tooltip.Text += tooltipData[0][..^"击退力".Length];
                    tooltip.Text += ")击退力";

                    foreach (string tooltipElement in tooltipData.Skip(1))
                    {
                        tooltip.Text += tooltipElement;
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
