//
// VoxelsCoreSharp
// by KryKom 2024
//

namespace VoxelsCoreSharp.util;

public static class Debug {

    
    public static void warn(string s) {
        ConsoleColors.printColoredTextHex($"[{DateTime.Now:HH:mm:ss}] WARN: {s}\n", (int)Colors.YELLOW_2);
    }

    public static void error(string s) {
        ConsoleColors.printColoredTextHex($"[{DateTime.Now:HH:mm:ss}] ERROR: {s}\n", (int)Colors.RED_2);
    }

    public static void info(string s) {
        ConsoleColors.printColoredTextHex($"[{DateTime.Now:HH:mm:ss}] INFO: {s}\n", (int)Colors.GREEN_3);
    }

    public static void printColored(string s, ConsoleColor c) {
        Console.ForegroundColor = c;
        Console.WriteLine(s);
        Console.ResetColor();
    }
    
    
}