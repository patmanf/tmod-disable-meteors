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
    public override void Load() => On.Terraria.WorldGen.dropMeteor += StopMeteor;

    private void StopMeteor(On.Terraria.WorldGen.orig_dropMeteor orig)
    {
        if (!Config.Instance.DisableMeteorDropping)
        {
            orig();
            return;
        }

        ChatHelper.BroadcastChatMessage(
            NetworkText.FromKey("[c/32FF82:A meteorite burned up in the atmosphere!]"),
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
                    NetworkText.FromKey($"[c/FFF014:Collected {amount} Meteorite] [i/s{amount}:116]", amount),
                    Color.White,
                    i
                );
            }
        }
    }
}