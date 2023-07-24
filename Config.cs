using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace DisableMeteors;

public class Config : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    public static Config Instance;

    [DefaultValue(true)]
    public bool DisableMeteorDropping;

    [DefaultValue(false)]
    public bool GiveMeteoriteOre;

    [DefaultValue(true)]
    public bool Announcements;
}
