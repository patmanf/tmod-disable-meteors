using Terraria;
using Terraria.ModLoader;

namespace DisableMeteors;

public class DropMeteorCommand : ModCommand
{
    public override CommandType Type => CommandType.World;
    public override string Command => "dropmeteor";
    public override void Action(CommandCaller caller, string input, string[] args)
    {
        WorldGen.dropMeteor();
    }
}
