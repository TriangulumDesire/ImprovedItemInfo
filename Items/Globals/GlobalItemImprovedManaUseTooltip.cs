using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class GlobalItemImprovedManaUseTooltip
        : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.mana <= 0 || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            foreach (TooltipLine tooltip in tooltips)
            {
                if (!tooltip.Name.Equals("UseMana"))
                {
                    continue;
                }

                try
                {
                    string[] tooltipData = tooltip.Text.Split(' ');

                    if (!tooltipData[^1].Equals("mana"))
                    {
                        return;
                    }

                    Item unmodifiedItem = new();
                    unmodifiedItem.CloneDefaults(item.type);

                    int totalManaUse = int.Parse(tooltipData[1]);
                    int manaUseDelta = totalManaUse - unmodifiedItem.mana;

                    if (manaUseDelta != 0)
                    {
                        tooltip.Text = $"{tooltipData[0]} {tooltipData[1]} ({(manaUseDelta > 0 ? "+" : "-")}{Math.Abs(manaUseDelta)})";

                        foreach (string tooltipElement in tooltipData.Skip(2))
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
