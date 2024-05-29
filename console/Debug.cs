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
    /// <param name="hideTime">hides time if true</param>
    public static void warn(string s, bool hideTime = false) {
        ConsoleColors.printColoredTextHex(hideTime ? $"WARN: {s}\n" : $"[{DateTime.Now:HH:mm:ss}] WARN: {s}\n", (int)Colors.YELLOW_2);
    }

    /// <summary>
    /// prints red error text
    /// </summary>
    /// <param name="s">desired string message</param>
    /// <param name="hideTime">hides time if true</param>
    public static void error(string s, bool hideTime = false) {
        ConsoleColors.printColoredTextHex(hideTime ? $"WARN: {s}\n" : $"[{DateTime.Now:HH:mm:ss}] ERROR: {s}\n", (int)Colors.RED_2);
    }

    /// <summary>
    /// prints green info text
    /// </summary>
    /// <param name="s">desired string message</param>
    /// <param name="hideTime">hides time if true</param>
    public static void info(string s, bool hideTime = false) {
        ConsoleColors.printColoredTextHex(hideTime ? $"WARN: {s}\n" : $"[{DateTime.Now:HH:mm:ss}] INFO: {s}\n", (int)Colors.GREEN_3);
    }
}