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

    public ShortArgument(short min = short.MinValue, short max = short.MaxValue) {
        this.min = min;
        this.max = max;
    }

    private ShortArgument(short value) {
        this.value = value;
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
            return new ShortArgument(value: (short)value);
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

    public UShortArgument(ushort min = ushort.MinValue, ushort max = ushort.MaxValue) {
        this.min = min;
        this.max = max;
    }

    private UShortArgument(ushort value) {
        this.value = value;
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
            return new UShortArgument(value: (ushort)value);
        }
        catch (Exception e) {
            return null;
        }
    }

    public override string type() => "ushort";
}