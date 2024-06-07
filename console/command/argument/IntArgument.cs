//
// Commandier
// by KryKom 2024
//

namespace VoxelsCoreSharp.console.command.argument;

/// <summary>
/// integer argument type
/// </summary>
public class IntArgument : ArgumentType {

    public override object value { get; protected set; } = 0;
    private readonly int min;
    private readonly int max;
    
    public override string description { get; protected set; }

    public IntArgument(int min = Int32.MinValue, int max = Int32.MaxValue, string description = "") {
        this.min = min;
        this.max = max;
        this.description = description;
    }

    private IntArgument(int value, string description) {
        this.value = value;
        this.description = description;
    }
    
    public override IntArgument? parse(string raw) {
        try {
            int output = int.Parse(raw);

            if (output < min) {
                Debug.error($"inputted value is lower then minimum [{min}]", true);
                return null;
            }

            if (output > max) {
                Debug.error($"inputted value is higher then maximum [{max}]", true);
                return null;
            }

            value = output; 
            return new IntArgument((int)value, description);
        }
        catch (Exception e) {
            return null;
        }
    }

    public override string type() => "int";
}

/// <summary>
/// unsigned integer argument type
/// </summary>
public class UIntArgument : ArgumentType {

    public override object value { get; protected set; } = 0;
    private readonly uint min;
    private readonly uint max;
    
    public override string description { get; protected set; }

    public UIntArgument(uint min = UInt32.MinValue, uint max = UInt32.MaxValue, string description = "") {
        this.min = min;
        this.max = max;
        this.description = description;
    }

    private UIntArgument(uint value, string description) {
        this.value = value;
        this.description = description;
    }
    
    public override UIntArgument? parse(string raw) {
        try {
            uint output = uint.Parse(raw);

            if (output < min) {
                Debug.error($"inputted value is lower then minimum [{min}]", true);
                return null;
            }

            if (output > max) {
                Debug.error($"inputted value is higher then maximum [{max}]", true);
                return null;
            }

            value = output;
            return new UIntArgument((uint)value, description);
        }
        catch (Exception e) {
            return null;
        }
    }

    public override string type() => "uint";
}