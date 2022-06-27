using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    public class ImprovedClickEffectCountTooltip
        : GlobalItem
    {
        private const string ClickerClassModName = "ClickerClass";

        private const string ClickEffectTooltipStem = "ClickEffect";

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string itemModName = item.ModItem?.Mod?.Name;

            if ((itemModName is not null && !itemModName.Equals(ClickerClassModName)) || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            foreach (TooltipLine tooltip in tooltips)
            {
                if (!tooltip.Name.StartsWith(ClickEffectTooltipStem))
                {
                    continue;
                }

                try
                {
                    string[] tooltipData = SplitTooltip(tooltip);

                    if (tooltipData.Length == 0)
                    {
                        return;
                    }

                    var (currentClickAmount, effectDisplayName) = GetEffectClickAmountAndDisplayName(tooltipData);

                    if (effectDisplayName is null)
                    {
                        return;
                    }

                    Mod clickerClass = ModLoader.GetMod(ClickerClassModName);
                    string clickerClassVersionString = clickerClass.Version.ToString();

                    const string AllEffectNamesCall = "GetAllEffectNames";
                    const string GetClickEffectAsDictCall = "GetClickEffectAsDict";

                    var clickerEffectNames = clickerClass.Call(AllEffectNamesCall, clickerClassVersionString) as List<string>;

                    if (clickerEffectNames is null)
                    {
                        return;
                    }

                    int totalClickAmount = 0;

                    foreach (string clickerEffectName in clickerEffectNames)
                    {
                        var clickerEffectData = clickerClass.Call(GetClickEffectAsDictCall, clickerClassVersionString, clickerEffectName) as Dictionary<string, object>;

                        if (clickerEffectData is null)
                        {
                            continue;
                        }

                        const string DisplayNameKey = "DisplayName";
                        const string AmountKey = "Amount";

                        if ((clickerEffectData[DisplayNameKey] as string).Equals(effectDisplayName))
                        {
                            totalClickAmount = (int)clickerEffectData[AmountKey];
                        }
                    }

                    int clickAmountDelta = totalClickAmount - currentClickAmount;

                    if (clickAmountDelta != 0)
                    {
                        ReconstructTooltip(tooltip, tooltipData, currentClickAmount, clickAmountDelta);

                        if (ImprovedItemInfo.IsClickEffectCountColoured)
                        {
                            tooltip.IsModifier = true;
                            tooltip.IsModifierBad = clickAmountDelta < 0;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private string[] SplitTooltip(in TooltipLine tooltip)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    return tooltip.Text.Split(": ", 2);

                default:
                    return Array.Empty<string>();
            }
        }

        private (int, string) GetEffectClickAmountAndDisplayName(in string[] tooltipData)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    int currentClickAmount = int.Parse(tooltipData.First().Split(' ')[0]);
                    string effectDisplayName = tooltipData.Last()[1..^1].Split(':').Last();

                    return (currentClickAmount, effectDisplayName);

                default:
                    return (0, null);
            }
        }

        private void ReconstructTooltip(in TooltipLine tooltip, in string[] tooltipData, in int currentClickAmount, in int clickAmountDelta)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US":
                    tooltip.Text = $"{currentClickAmount} ({(clickAmountDelta > 0 ? "-" : "+")}{Math.Abs(clickAmountDelta)}) clicks: {tooltipData.Last()}";

                    break;
            }
        }
    }
}
