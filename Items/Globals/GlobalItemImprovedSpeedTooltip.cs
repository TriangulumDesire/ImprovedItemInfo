using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class GlobalItemImprovedSpeedTooltip
        : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            Player player = Main.player[item.playerIndexTheItemIsReservedFor];

            foreach (TooltipLine tooltip in tooltips)
            {
                if (!tooltip.Name.Equals("Speed"))
                {
                    continue;
                }

                try
                {
                    string[] tooltipData = tooltip.Text.Split(' ');

                    if (!tooltipData[^1].Equals("speed"))
                    {
                        return;
                    }

                    Item unmodifiedItem = new();
                    unmodifiedItem.CloneDefaults(item.type);
                    bool isCalamityModItem = item.ModItem?.Mod?.Name == "CalamityMod";

                    int totalSpeed = item.useAnimation;
                    int baseSpeed = unmodifiedItem.useAnimation;

                    if (isCalamityModItem)
                    {
                        --totalSpeed;
                        --baseSpeed;
                    }

                    if (item.DamageType == DamageClass.Melee || item.DamageType == DamageClass.SummonMeleeSpeed)
                    {
                        if (!isCalamityModItem)
                        {
                            totalSpeed = item.useTime;
                            baseSpeed = unmodifiedItem.useTime;
                        }

                        float meleeModifier = 1.0f / player.GetTotalAttackSpeed(DamageClass.Melee);

                        totalSpeed = (int)(Math.Round(totalSpeed * meleeModifier));
                    }

                    int speedDelta = totalSpeed - baseSpeed;

                    tooltip.Text = $"{totalSpeed}";

                    if (speedDelta != 0)
                    {
                        tooltip.Text += $" ({(speedDelta > 0 ? "+" : "-")}{Math.Abs(speedDelta)})";
                    }
                    
                    for (int i = 0; i < tooltipData.Length; ++i)
                    {
                        tooltip.Text += ((i == 0) ? " (" : " ") + tooltipData[i] + ((i == tooltipData.Length - 2) ? ")" : "");
                    }

                    if (speedDelta != 0)
                    {
                        tooltip.IsModifier = true;
                        tooltip.IsModifierBad = speedDelta > 0;
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
