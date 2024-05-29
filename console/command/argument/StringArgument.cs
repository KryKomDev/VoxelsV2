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
    
    public StringArgument() {
        
    }

    private StringArgument(string value) {
        this.value = value;
    }

    public override StringArgument parse(string raw) {
        value = raw;
        return new StringArgument(value: (string)value);
    }

    public override string type() => "string";
}