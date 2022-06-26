using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class GlobalItemImprovedKnockbackTooltip
        : GlobalItem
    {
        private const string KnockbackTooltipName = "Knockback";

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.knockBack <= 0.0f || Main.netMode == NetmodeID.Server)
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

        private bool IsKnockbackTooltip(in string[] tooltipData)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    return tooltipData[^1].Equals("knockback");

                default:
                    return false;
            }
        }

        private void ReconstructTooltip(in TooltipLine tooltip, in string[] tooltipData, in string[] tooltipLines, in float totalKnockback, in float knockbackDelta)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
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
            }
        }
    }
}
