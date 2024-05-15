using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class ImprovedSpeedTooltip
        : GlobalItem
    {
        private const string SpeedTooltipName = "Speed";

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ImprovedItemInfo.IsSpeedImproved || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            Player player = Main.player[item.playerIndexTheItemIsReservedFor];

            foreach (TooltipLine tooltip in tooltips)
            {
                if (!tooltip.Name.Equals(SpeedTooltipName))
                {
                    continue;
                }

                try
                {
                    string[] tooltipData = tooltip.Text.Split(' ');

                    if (!IsSpeedTooltip(tooltipData))
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

                    ReconstructTooltip(tooltip, tooltipData, totalSpeed, speedDelta);

                    if (ImprovedItemInfo.IsSpeedColoured && speedDelta != 0)
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

        private static bool IsSpeedTooltip(in string[] tooltipData)
        {
            return Language.ActiveCulture.Name switch
            {
                "en-US" => tooltipData[^1].Equals("speed"),
                "de-DE" => tooltipData[^1].Equals("Tempo"),
                "zh-Hans" => tooltipData[^1].EndsWith("速度"),
                _ => false,
            };
        }

        private static void ReconstructTooltip(in TooltipLine tooltip, in string[] tooltipData, in int totalSpeed, in int speedDelta)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US" or "de-DE":
                    tooltip.Text = $"{totalSpeed}";

                    if (speedDelta != 0)
                    {
                        tooltip.Text += $" ({(speedDelta > 0 ? "+" : "-")}{Math.Abs(speedDelta)})";
                    }

                    for (int i = 0; i < tooltipData.Length; ++i)
                    {
                        tooltip.Text += ((i == 0) ? " (" : " ") + tooltipData[i] + ((i == tooltipData.Length - 2) ? ")" : "");
                    }

                    break;

                case "zh-Hans":
                    tooltip.Text = $"{totalSpeed}";

                    if (speedDelta != 0)
                    {
                        tooltip.Text += $" ({(speedDelta > 0 ? "+" : "-")}{Math.Abs(speedDelta)})";
                    }

                    tooltip.Text += "(";
                    tooltip.Text += tooltipData[0][..^"速度".Length];
                    tooltip.Text += ")速度";

                    foreach (string tooltipElement in tooltipData.Skip(1))
                    {
                        tooltip.Text += tooltipElement;
                    }

                    break;
            }
        }
    }
}
