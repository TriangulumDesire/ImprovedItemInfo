using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    internal class ProjectileVelocityTooltip
        : GlobalItem
    {
        private const string KnockbackTooltipName = "Knockback";
        private const string ProjectileVelocityTooltipName = "ProjectileVelocity";

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ImprovedItemInfo.DisplayProjectileVelocityTooltip || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (item.shootSpeed > 0.0f && (item.ammo > 0 || item.useAmmo > 0))
            {
                try
                {
                    Item unmodifiedItem = new();
                    unmodifiedItem.CloneDefaults(item.netID);

                    float totalProjectileVelocity = item.shootSpeed;
                    float baseProjectileVelocity = unmodifiedItem.shootSpeed;
                    float projectileVelocityDelta = (float)Math.Round(totalProjectileVelocity - baseProjectileVelocity, 2);

                    string deltaString = "";

                    if (projectileVelocityDelta != 0.0f)
                    {
                        deltaString = $" ({(projectileVelocityDelta > 0.0f ? "+" : "-")}{Math.Abs(projectileVelocityDelta)})";
                    }

                    TooltipLine projectileVelocityTooltipLine = new(
                        Mod,
                        ProjectileVelocityTooltipName,
                        Language.GetTextValue("Mods.ImprovedItemInfo.Tooltips.ProjectileVelocity", Math.Round(totalProjectileVelocity, 2), deltaString)
                    );

                    if (ImprovedItemInfo.IsProjectileVelocityColoured && projectileVelocityDelta != 0.0f)
                    {
                        projectileVelocityTooltipLine.IsModifier = true;
                        projectileVelocityTooltipLine.IsModifierBad = projectileVelocityDelta < 0.0f;
                    }

                    int knockbackTooltipIndex = tooltips.FindIndex(candidateTooltip => candidateTooltip.Name.Equals(KnockbackTooltipName));

                    if (knockbackTooltipIndex == -1 || knockbackTooltipIndex == tooltips.Count - 1)
                    {
                        tooltips.Add(projectileVelocityTooltipLine);
                    }
                    else
                    {
                        tooltips.Insert(knockbackTooltipIndex + 1, projectileVelocityTooltipLine);
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
