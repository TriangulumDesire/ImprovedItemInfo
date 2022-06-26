using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class GlobalItemImprovedKnockbackTooltip
        : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.knockBack <= 0.0f || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            foreach (TooltipLine tooltip in tooltips)
            {
                if (!tooltip.Name.Equals("Knockback"))
                {
                    continue;
                }

                try
                {
                    string[] tooltipData = tooltip.Text.Split(' ');

                    if (!tooltipData[^1].Equals("knockback"))
                    {
                        return;
                    }

                    Item unmodifiedItem = new();
                    unmodifiedItem.CloneDefaults(item.type);

                    float totalKnockback = item.knockBack;
                    float knockbackDelta = (float)Math.Round(totalKnockback - unmodifiedItem.knockBack, 3);

                    tooltip.Text = Math.Round(totalKnockback, 2).ToString();

                    if (Math.Abs(knockbackDelta) > float.Epsilon)
                    {
                        tooltip.Text += $" ({(knockbackDelta > 0.0f ? "+" : "-")}{ Math.Abs(Math.Round(knockbackDelta, 2))})";
                    }

                    for (int i = 0; i < tooltipData.Length; ++i)
                    {
                        tooltip.Text += ((i == 0) ? " (" : " ") + tooltipData[i] + ((i == tooltipData.Length - 2) ? ")" : "");
                    }

                    if (Math.Abs(knockbackDelta) > float.Epsilon)
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
    }
}
