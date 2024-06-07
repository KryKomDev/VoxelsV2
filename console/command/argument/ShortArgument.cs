//
// Commandier
// by KryKom 2024
//

using System.Collections.ObjectModel;

namespace VoxelsCoreSharp.console.command.argument;

/// <summary>
/// short argument type
/// </summary>
public class ShortArgument : ArgumentType {

    public override object value { get; protected set; } = (short)0;
    private readonly short min;
    private readonly short max;
    
    public override string description { get; protected set; }

    public ShortArgument(short min = short.MinValue, short max = short.MaxValue, string description = "") {
        this.min = min;
        this.max = max;
        this.description = description;
    }

    private ShortArgument(short value, string description) {
        this.value = value;
        this.description = description;
    }
    
    public override ShortArgument? parse(string raw) {
        try {
            short output = short.Parse(raw);

            if (output < min) {
                Debug.error($"inputted value is lower then minimum [{min}]", true);
                return null;
            }

            if (output > max) {
                Debug.error($"inputted value is higher then maximum [{max}]", true);
                return null;
            }

            value = output;
            return new ShortArgument((short)value, description);
        }
        catch (Exception e) {
            return null;
        }
    }

    public override string type() => "int";
}

/// <summary>
/// unsigned short argument type
/// </summary>
public class UShortArgument : ArgumentType {

    public override object value { get; protected set; } = (ushort)0;
    private readonly ushort min;
    private readonly ushort max;
    
    public override string description { get; protected set; }

    public UShortArgument(ushort min = ushort.MinValue, ushort max = ushort.MaxValue, string description = "") {
        this.min = min;
        this.max = max;
        this.description = description;
    }

    private UShortArgument(ushort value, string description) {
        this.value = value;
        this.description = description;
    }
    
    public override UShortArgument? parse(string raw) {
        try {
            ushort output = ushort.Parse(raw);

            if (output < min) {
                Debug.error($"inputted value is lower then minimum [{min}]", true);
                return null;
            }

            if (output > max) {
                Debug.error($"inputted value is higher then maximum [{max}]", true);
                return null;
            }

            value = output;
            return new UShortArgument((ushort)value, description);
        }
        catch (Exception e) {
            return null;
        }
    }

    public override string type() => "ushort";
}