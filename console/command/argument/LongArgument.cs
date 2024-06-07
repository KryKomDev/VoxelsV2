//
// Commandier
// by KryKom 2024
//

namespace VoxelsCoreSharp.console.command.argument;

/// <summary>
/// long argument type
/// </summary>
public class LongArgument : ArgumentType {

    public override object value { get; protected set; } = 0L;
    private readonly long min;
    private readonly long max;
    
    public override string description { get; protected set; }

    public LongArgument(long min = long.MinValue, long max = long.MaxValue, string description = "") {
        this.min = min;
        this.max = max;
        this.description = description;
    }

    private LongArgument(long value, string description) {
        this.value = value;
        this.description = description;
    }
    
    public override LongArgument? parse(string raw) {
        try {
            long output = long.Parse(raw);

            if (output < min) {
                Debug.error($"inputted value is lower then minimum [{min}]", true);
                return null;
            }

            if (output > max) {
                Debug.error($"inputted value is higher then maximum [{max}]", true);
                return null;
            }

            value = output;
            return new LongArgument((long)value, description);
        }
        catch (Exception e) {
            return null;
        }
    }

    public override string type() => "long";
}

/// <summary>
/// unsigned long argument type
/// </summary>
public class ULongArgument : ArgumentType {

    public override object value { get; protected set; } = 0UL;
    private readonly ulong min;
    private readonly ulong max;
    
    public override string description { get; protected set; }

    public ULongArgument(ulong min = ulong.MinValue, ulong max = ulong.MaxValue, string description = "") {
        this.min = min;
        this.max = max;
        this.description = description;
    }

    private ULongArgument(ulong value, string description) {
        this.value = value;
        this.description = description;
    }
    
    public override ULongArgument? parse(string raw) {
        try {
            ulong output = ulong.Parse(raw);

            if (output < min) {
                Debug.error($"inputted value is lower then minimum [{min}]", true);
                return null;
            }

            if (output > max) {
                Debug.error($"inputted value is higher then maximum [{max}]", true);
                return null;
            }

            value = output;
            return new ULongArgument((ulong)value, description);
        }
        catch (Exception e) {
            return null;
        }
    }

    public override string type() => "ulong";
}