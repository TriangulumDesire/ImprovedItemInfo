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
            if (item.mana <= 0 || Main.netMode == NetmodeID.Server)
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

        private bool IsManaUseTooltip(in string[] tooltipData)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    return tooltipData[^1].Equals("mana");

                default:
                    return false;
            }
        }

        private int GetTotalManaUseFromTooltip(in string[] tooltipData)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    return int.Parse(tooltipData[1]);

                default:
                    return 0;
            }
        }

        private void ReconstructTooltip(in TooltipLine tooltip, in string[] tooltipData, in string[] tooltipLines, in int manaUseDelta)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
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
            }
        }
    }
}
