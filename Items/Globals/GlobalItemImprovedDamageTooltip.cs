using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class GlobalItemImprovedDamageTooltip
        : GlobalItem
    {
        private const string DamageTooltipName = "Damage";

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.damage <= 0 || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            foreach (TooltipLine tooltip in tooltips)
            {
                if (!tooltip.Name.Equals(DamageTooltipName))
                {
                    continue;
                }

                try
                {
                    string[] tooltipData = tooltip.Text.Split(' ');

                    if (!IsDamageTooltip(tooltipData))
                    {
                        return;
                    }

                    Item unmodifiedItem = new();
                    unmodifiedItem.CloneDefaults(item.type);

                    int totalDamage = GetTotalDamageFromTooltip(tooltipData);
                    int damageDelta = totalDamage - unmodifiedItem.damage;

                    if (damageDelta != 0)
                    {
                        ReconstructTooltip(tooltip, tooltipData, damageDelta);

                        if (ImprovedItemInfo.IsDamageColoured)
                        {
                            tooltip.IsModifier = true;
                            tooltip.IsModifierBad = damageDelta < 0;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private bool IsDamageTooltip(in string[] tooltipData)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    return tooltipData[^1].Equals("damage");

                default:
                    return false;
            }
        }

        private int GetTotalDamageFromTooltip(in string[] tooltipData)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    return int.Parse(tooltipData[0]);

                default:
                    return 0;
            }
        }

        private void ReconstructTooltip(in TooltipLine tooltip, in string[] tooltipData, in int damageDelta)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    tooltip.Text = $"{tooltipData[0]} ({(damageDelta > 0 ? "+" : "-")}{Math.Abs(damageDelta)})";

                    foreach (string tooltipElement in tooltipData.Skip(1))
                    {
                        tooltip.Text += " " + tooltipElement;
                    }

                    break;
            }
        }
    }
}
