using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class MiningSpeedTooltip
        : GlobalItem
    {
        public enum FormatMode
        {
            None,
            MiningSpeedOnly,
            HitsPerSecondOnly,
            BothMiningSpeedAndHitsPerSecond,
        }

        private const string SpeedTooltipName = "Speed";
        private const string MiningSpeedTooltipName = "MiningSpeed";

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ImprovedItemInfo.MiningSpeedTooltipDisplay == FormatMode.None || Main.netMode == NetmodeID.Server)
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

                string miningSpeedDeltaString = "";

                if (miningSpeedDelta != 0)
                {
                    miningSpeedDeltaString = $" ({(miningSpeedDelta > 0 ? "+" : "-")}{Math.Abs(miningSpeedDelta)})";
                }

                const float FrameTicksPerSecond = 60.0f;

                float hitsPerSecond = (float)Math.Round(FrameTicksPerSecond / (float)totalMiningSpeed, 2);
                float baseHitsPerSecond = (float)Math.Round(FrameTicksPerSecond / (float)baseMiningSpeed, 2);
                float hitsPerSecondDelta = (float)Math.Round(hitsPerSecond - baseHitsPerSecond, 2);

                string hitsPerSecondDeltaString = "";

                if (hitsPerSecondDelta != 0.0f)
                {
                    hitsPerSecondDeltaString = $" ({(hitsPerSecondDelta > 0 ? "+" : "-")}{Math.Abs(hitsPerSecondDelta)})";
                }

                TooltipLine miningSpeedTooltipLine = new(
                    Mod,
                    MiningSpeedTooltipName,
                    GetFormattedTooltipText(totalMiningSpeed, miningSpeedDeltaString, hitsPerSecond, hitsPerSecondDeltaString)
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

        private string GetFormattedTooltipText(int totalMiningSpeed, in string miningSpeedDeltaString, float hitsPerSecond, in string hitsPerSecondDeltaString)
        {
            switch (ImprovedItemInfo.MiningSpeedTooltipDisplay)
            {
                case FormatMode.MiningSpeedOnly:
                    return Language.GetTextValue(
                        "Mods.ImprovedItemInfo.Tooltips.MiningSpeed",
                        totalMiningSpeed,
                        miningSpeedDeltaString
                    );

                case FormatMode.HitsPerSecondOnly:
                    return Language.GetTextValue(
                        "Mods.ImprovedItemInfo.Tooltips.HitsPerSecond",
                        hitsPerSecond,
                        hitsPerSecondDeltaString
                    );

                case FormatMode.BothMiningSpeedAndHitsPerSecond:
                    return Language.GetTextValue(
                        "Mods.ImprovedItemInfo.Tooltips.MiningSpeedAndHitsPerSecond",
                        totalMiningSpeed,
                        miningSpeedDeltaString,
                        hitsPerSecond,
                        hitsPerSecondDeltaString
                    );

                case FormatMode.None:
                default:
                    return "";
            }
        }
    }
}
