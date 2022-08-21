using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ImprovedItemInfo.Items.Globals
{
    internal class ProjectileVelocityMultiplierTooltip
        : GlobalItem
    {
        private const string KnockbackTooltipName = "Knockback";
        private const string ProjectileVelocityMultiplierTooltipName = "ProjectileVelocityMultiplier";

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ImprovedItemInfo.DisplayProjectileVelocityMultiplierTooltip || Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (item.ammo > 0 || item.useAmmo > 0)
            {
                try
                {
                    Projectile projectile = new();
                    projectile.SetDefaults(item.shoot);

                    if (projectile is null)
                    {
                        return;
                    }

                    int projectileVelocityMultiplier = projectile.extraUpdates;

                    if (projectileVelocityMultiplier == 0 && item.ammo <= 0)
                    {
                        return;
                    }

                    ++projectileVelocityMultiplier;

                    TooltipLine projectileVelocityMultiplierTooltipLine = new(
                        Mod,
                        ProjectileVelocityMultiplierTooltipName,
                        Language.GetTextValue("Mods.ImprovedItemInfo.Tooltips.ProjectileVelocityMultiplier", projectileVelocityMultiplier)
                    );

                    int knockbackTooltipIndex = tooltips.FindIndex(candidateTooltip => candidateTooltip.Name.Equals(KnockbackTooltipName));

                    if (knockbackTooltipIndex == -1 || knockbackTooltipIndex == tooltips.Count - 1)
                    {
                        tooltips.Add(projectileVelocityMultiplierTooltipLine);
                    }
                    else
                    {
                        tooltips.Insert(knockbackTooltipIndex + 1, projectileVelocityMultiplierTooltipLine);
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
