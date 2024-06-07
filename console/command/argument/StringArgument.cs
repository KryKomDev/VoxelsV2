//
// Commandier
// by KryKom 2024
//

namespace VoxelsCoreSharp.console.command.argument;

/// <summary>
/// string argument type
/// </summary>
public class StringArgument : ArgumentType {

    public override object value { get; protected set; } = "";
    
    public override string description { get; protected set; }

    public StringArgument(string description = "") {
        this.description = description;
    }

    private StringArgument(string value, string description) {
        this.value = value;
        this.description = description;
    }

    public override StringArgument parse(string raw) {
        value = raw;
        return new StringArgument((string)value, description);
    }

    public override string type() => "string";
}