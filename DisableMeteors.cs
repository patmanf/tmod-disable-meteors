using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DisableMeteors;

public class DisableMeteors : Mod
{
    public override void Load() => On_WorldGen.dropMeteor += StopMeteor;

    private void StopMeteor(On_WorldGen.orig_dropMeteor orig)
    {
        if (!Config.Instance.DisableMeteorDropping)
        {
            orig();
            return;
        }

        ChatHelper.BroadcastChatMessage(
            NetworkText.FromKey("Mods.DisableMeteors.Messages.Stopped"),
            Color.White
        );

        if (Config.Instance.GiveMeteoriteOre)
        {
            int activePlayerCount = Main.player.Where(player => player.active).ToArray().Length;

            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                if (!player.active) continue;

                int amount = Main.rand.Next(400, 500) / activePlayerCount;
                player.QuickSpawnItem(player.GetSource_FromThis(), ItemID.Meteorite, amount);

                ChatHelper.SendChatMessageToClient(
                    NetworkText.FromKey("Mods.DisableMeteors.Messages.OresGiven", amount),
                    Color.White,
                    i
                );
            }
        }
    }
}