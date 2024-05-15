//
// VoxelsCoreSharp
// by KryKom 2024
//

namespace VoxelsCoreSharp.console;

/// <summary>
/// debug console utils 
/// </summary>
public static class Debug {
    
    /// <summary>
    /// prints yellow warning text
    /// </summary>
    /// <param name="s">desired string message</param>
    public static void warn(string s) {
        ConsoleColors.printColoredTextHex($"[{DateTime.Now:HH:mm:ss}] WARN: {s}\n", (int)Colors.YELLOW_2);
    }

    /// <summary>
    /// prints red error text
    /// </summary>
    /// <param name="s">desired string message</param>
    public static void error(string s) {
        ConsoleColors.printColoredTextHex($"[{DateTime.Now:HH:mm:ss}] ERROR: {s}\n", (int)Colors.RED_2);
    }

    /// <summary>
    /// prints green info text
    /// </summary>
    /// <param name="s">desired string message</param>
    public static void info(string s) {
        ConsoleColors.printColoredTextHex($"[{DateTime.Now:HH:mm:ss}] INFO: {s}\n", (int)Colors.GREEN_3);
    }
}