using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class GlobalItemImprovedDamageTooltip
        : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.damage <= 0 || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            foreach (TooltipLine tooltip in tooltips)
            {
                if (!tooltip.Name.Equals("Damage"))
                {
                    continue;
                }

                try
                {
                    string[] tooltipData = tooltip.Text.Split(' ');

                    if (!tooltipData[^1].Equals("damage"))
                    {
                        return;
                    }

                    Item unmodifiedItem = new();
                    unmodifiedItem.CloneDefaults(item.type);

                    int totalDamage = int.Parse(tooltipData[0]);
                    int damageDelta = totalDamage - unmodifiedItem.damage;

                    if (damageDelta != 0)
                    {
                        tooltip.Text = $"{tooltipData[0]} ({(damageDelta > 0 ? "+" : "-")}{Math.Abs(damageDelta)})";

                        foreach (string tooltipElement in tooltipData.Skip(1))
                        {
                            tooltip.Text += " " + tooltipElement;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
