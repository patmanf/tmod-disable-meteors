using Terraria;
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

        if (Config.Instance.Announcements)
            Main.NewText(Language.GetTextValue("Mods.DisableMeteors.Messages.Stopped"));

        if (Config.Instance.GiveMeteoriteOre)
        {
            int amount = Main.rand.Next(350, 450);
            Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), ItemID.Meteorite, amount);

            if (Config.Instance.Announcements)
                Main.NewText(Language.GetTextValue("Mods.DisableMeteors.Messages.OresGiven", amount));
        }
    }
}