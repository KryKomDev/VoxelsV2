//
// Commandier
// by KryKom 2024
//

namespace VoxelsCoreSharp.console.command.argument;

/// <summary>
/// superclass for commandier argument types
/// </summary>
public abstract class ArgumentType {

    public abstract object value { get; protected set; }

    public abstract ArgumentType? parse(string raw);

    public abstract string type();
    
    public abstract string description { get; protected set; }
}
