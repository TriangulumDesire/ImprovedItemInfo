using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    internal class MiningSpeedTooltip
        : GlobalItem
    {
        private const string SpeedTooltipName = "Speed";
        private const string MiningSpeedTooltipName = "MiningSpeed";

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ImprovedItemInfo.DisplayMiningSpeedTooltip || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (item.pick <= 0 && item.axe <= 0 && item.hammer <= 0)
            {
                return;
            }

            try
            {
                Item unmodifiedItem = new();
                unmodifiedItem.CloneDefaults(item.netID);

                int totalMiningSpeed = item.useTime;
                int baseMiningSpeed = unmodifiedItem.useTime;
                int miningSpeedDelta = totalMiningSpeed - baseMiningSpeed;

                string deltaString = "";

                if (miningSpeedDelta != 0)
                {
                    deltaString = $" ({(miningSpeedDelta > 0 ? "+" : "-")}{Math.Abs(miningSpeedDelta)})";
                }

                const float FrameTicksPerSecond = 60.0f;

                TooltipLine miningSpeedTooltipLine = new(
                    Mod,
                    MiningSpeedTooltipName,
                    Language.GetTextValue("Mods.ImprovedItemInfo.Tooltips.MiningSpeed", totalMiningSpeed, deltaString, Math.Round(FrameTicksPerSecond / (float)totalMiningSpeed, 2))
                );

                if (ImprovedItemInfo.IsMiningSpeedColoured && miningSpeedDelta != 0)
                {
                    miningSpeedTooltipLine.IsModifier = true;
                    miningSpeedTooltipLine.IsModifierBad = miningSpeedDelta > 0;
                }

                int speedTooltipIndex = tooltips.FindIndex(candidateTooltip => candidateTooltip.Name.Equals(SpeedTooltipName));

                if (speedTooltipIndex == -1 || speedTooltipIndex == tooltips.Count - 1)
                {
                    tooltips.Add(miningSpeedTooltipLine);
                }
                else
                {
                    tooltips.Insert(speedTooltipIndex + 1, miningSpeedTooltipLine);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
