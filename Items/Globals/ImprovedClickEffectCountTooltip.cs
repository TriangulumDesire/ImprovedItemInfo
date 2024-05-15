using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
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

        private static Dictionary<string, int> _clickerEffectTotalClickAmounts = null;
        private static string _previousLanguageName = string.Empty;
        private static bool _hasClickerDataBeenInitialised = false;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ImprovedItemInfo.IsClickEffectCountImproved || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (!_hasClickerDataBeenInitialised || Language.ActiveCulture.Name != _previousLanguageName)
            {
                InitialiseClickerData();
            }

            if (_clickerEffectTotalClickAmounts is null)
            {
                return;
            }

            string itemModName = item.ModItem?.Mod?.Name;

            if (itemModName is not null && !itemModName.Equals(ClickerClassModName))
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

                    if (effectDisplayName is null || !_clickerEffectTotalClickAmounts.TryGetValue(effectDisplayName, out int totalClickAmount))
                    {
                        return;
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

        private static string[] SplitTooltip(in TooltipLine tooltip)
        {
            return Language.ActiveCulture.Name switch
            {
                "en-US" or "de-DE" => tooltip.Text.Split(": ", 2),
                "zh-Hans" => tooltip.Text.Split(":", 2),
                _ => [],
            };
        }

        private static (int, string) GetEffectClickAmountAndDisplayName(in string[] tooltipData)
        {
            int currentClickAmount = 0;
            string effectDisplayName = null;

            switch (Language.ActiveCulture.Name)
            {
                case "en-US" or "de-DE":
                    currentClickAmount = int.Parse(tooltipData.First().Split(' ')[0]);
                    effectDisplayName = tooltipData.Last()[1..^1].Split(':').Last();

                    break;

                case "zh-Hans":
                    currentClickAmount = int.Parse(tooltipData.First().Replace("次点击", null));
                    effectDisplayName = tooltipData.Last()[1..^1].Split(':').Last();

                    break;
            }

            return (currentClickAmount, effectDisplayName);
        }

        private static void ReconstructTooltip(in TooltipLine tooltip, in string[] tooltipData, in int currentClickAmount, in int clickAmountDelta)
        {
            switch (Language.ActiveCulture.Name)
            {
                case "en-US" or "de-DE":
                    tooltip.Text = $"{currentClickAmount} ({(clickAmountDelta > 0 ? "-" : "+")}{Math.Abs(clickAmountDelta)}) clicks: {tooltipData.Last()}";

                    break;

                case "zh-Hans":
                    tooltip.Text = $"{currentClickAmount}({(clickAmountDelta > 0 ? "-" : "+")}{Math.Abs(clickAmountDelta)})次点击:{tooltipData.Last()}";

                    break;
            }
        }

        private static void InitialiseClickerData()
        {
            if (!ModLoader.HasMod(ClickerClassModName))
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

            _clickerEffectTotalClickAmounts = [];

            foreach (string clickerEffectName in clickerEffectNames)
            {
                var clickerEffectData = clickerClass.Call(GetClickEffectAsDictCall, clickerClassVersionString, clickerEffectName) as Dictionary<string, object>;

                const string DisplayNameKey = "DisplayName";
                const string AmountKey = "Amount";

                if (clickerEffectData is null || clickerEffectData[DisplayNameKey] is not LocalizedText)
                {
                    continue;
                }

                string localisedClickerEffectDisplayName = (clickerEffectData[DisplayNameKey] as LocalizedText).Value;
                int totalClickAmount = (int)clickerEffectData[AmountKey];

                _clickerEffectTotalClickAmounts.TryAdd(localisedClickerEffectDisplayName, totalClickAmount);
            }

            _hasClickerDataBeenInitialised = true;
            _previousLanguageName = Language.ActiveCulture.Name;
        }
    }
}
