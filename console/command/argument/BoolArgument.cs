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

    public BoolArgument() {
        
    }

    private BoolArgument(bool value) {
        this.value = value;
    }
    
    public override BoolArgument? parse(string raw) {
        try {
            value = bool.Parse(raw);
            return new BoolArgument((bool)value);
        }
        catch (Exception e) {
            return null;
        }
    }

    public override string type() => "bool";
}