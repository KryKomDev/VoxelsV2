//
// Commandier
// by KryKom 2024
//

namespace VoxelsCoreSharp.console.command.argument;

/// <summary>
/// floating point number argument type
/// </summary>
public class FloatArgument : ArgumentType {

    public override object value { get; protected set; } = 0f;
    private readonly float min;
    private readonly float max;

    public FloatArgument(float min = float.MinValue, float max = float.MaxValue) {
        this.min = min;
        this.max = max;
    }

    private FloatArgument(float value) {
        this.value = value;
    }
    
    public override FloatArgument? parse(string raw) {
        raw = raw.Replace('.', ',');
        
        try {
            float output = float.Parse(raw);

            if (output < min) {
                Debug.error($"inputted value is lower then minimum [{min}]", true);
                return null;
            }

            if (output > max) {
                Debug.error($"inputted value is higher then maximum [{max}]", true);
                return null;
            }

            value = output;
            return new FloatArgument(value: (float)value);
        }
        catch (Exception e) {
            return null;
        }
    }

    public override string type() => "float";
}