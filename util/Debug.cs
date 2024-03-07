//
// VoxelsCoreSharp
// by KryKom 2024
//

namespace VoxelsCoreSharp.util;

public static class Debug {

    
    public static void warn(string s) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(s);
        Console.ResetColor();
    }

    public static void err(string s) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(s);
        Console.ResetColor();
    }

    public static void printColored(string s, ConsoleColor c) {
        Console.ForegroundColor = c;
        Console.WriteLine(s);
        Console.ResetColor();
    }
    
    
}