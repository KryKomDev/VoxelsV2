//
// Commandier
// by KryKom 2024
//

namespace VoxelsCoreSharp.console.command.argument;

/// <summary>
/// boolean argument type
/// </summary>
public class BoolArgument : ArgumentType {

    public override object value { get; protected set; } = false;

    public override string description { get; protected set; }

    public BoolArgument(string description = "") {
        this.description = description;
    }

    private BoolArgument(bool value, string description) {
        this.value = value;
        this.description = description;
    }
    
    public override BoolArgument? parse(string raw) {
        try {
            value = bool.Parse(raw);
            return new BoolArgument((bool)value, description);
        }
        catch (Exception e) {
            return null;
        }
    }

    public override string type() => "bool";
}