//
// ConsoleColors
// by KryKom 2024 
// ---------------------
// libcolors.dll
// by KryKom 2024
//

using System.Runtime.InteropServices;

namespace VoxelsCoreSharp.console;

public static class ConsoleColors {
    
    // colored text import from libcolors.dll by KryKom
    
    /// <summary>
    /// prints a colored string in the console without newline
    /// </summary>
    /// <param name="s">string to print</param>
    /// <param name="r">red value of the color</param>
    /// <param name="g">green value of the color</param>
    /// <param name="b">blue value of the color</param>
    [DllImport("C:/Users/krystof/Desktop/projects/Voxels/VoxelsCore/VoxelsCoreSharp/console/libcolors.dll")] public static extern void printColoredText (string s, byte r, byte g, byte b);
    
    /// <summary>
    /// prints a string with colored background in the console without newline
    /// </summary>
    /// <param name="s">string to print</param>
    /// <param name="r">red value of the color</param>
    /// <param name="g">green value of the color</param>
    /// <param name="b">blue value of the color</param>
    [DllImport("C:/Users/krystof/Desktop/projects/Voxels/VoxelsCore/VoxelsCoreSharp/console/libcolors.dll")] public static extern void printColoredBckg (string s, byte r, byte g, byte b);
    
    
    /// <summary>
    /// prints a colored string in the console with newline
    /// </summary>
    /// <param name="s">string to print</param>
    /// <param name="r">red value of the color</param>
    /// <param name="g">green value of the color</param>
    /// <param name="b">blue value of the color</param>
    [DllImport("C:/Users/krystof/Desktop/projects/Voxels/VoxelsCore/VoxelsCoreSharp/console/libcolors.dll")] public static extern void printlnColoredText (string s, byte r, byte g, byte b);
    
    /// <summary>
    /// prints a string with colored background in the console with newline
    /// </summary>
    /// <param name="s">string to print</param>
    /// <param name="r">red value of the color</param>
    /// <param name="g">green value of the color</param>
    /// <param name="b">blue value of the color</param>
    [DllImport("C:/Users/krystof/Desktop/projects/Voxels/VoxelsCore/VoxelsCoreSharp/console/libcolors.dll")] public static extern void printlnColoredBckg (string s, byte r, byte g, byte b);
    
    // TODO fix hex functions in libcolors.dll
    // [DllImport("C:/Users/krystof/Desktop/projects/Voxels/VoxelsCore/VoxelsCoreSharp/console/libcolors.dll")] public static extern void printColoredTextHex (string s, int hex);
    // [DllImport("C:/Users/krystof/Desktop/projects/Voxels/VoxelsCore/VoxelsCoreSharp/console/libcolors.dll")] public static extern void printColoredBckgHex (string s, int hex);
    // [DllImport("C:/Users/krystof/Desktop/projects/Voxels/VoxelsCore/VoxelsCoreSharp/console/libcolors.dll")] public static extern void printlnColoredTextHex (string s, int hex);
    // [DllImport("C:/Users/krystof/Desktop/projects/Voxels/VoxelsCore/VoxelsCoreSharp/console/libcolors.dll")] public static extern void printlnColoredBckgHex (string s, int hex);
    
    /// <summary>
    /// prints a colored string in the console without newline
    /// uses printColoredText(...) from libcolors.dll
    /// </summary>
    /// <param name="s">string to print</param>
    /// <param name="hex">hexadecimal value of the color</param>
    /// <seealso cref="printColoredText"/>
    public static void printColoredTextHex(string s, int hex) {
        printColoredText(s, (byte) (hex >> 16), (byte) (hex >> 8), (byte) hex);
    }
    
    /// <summary>
    /// prints a colored string in the console with newline
    /// uses printlnColoredText(...) from libcolors.dll
    /// </summary>
    /// <param name="s">string to print</param>
    /// <param name="hex">hexadecimal value of the color</param>
    /// <seealso cref="printlnColoredText"/>
    public static void printlnColoredTextHex(string s, int hex) {
        printlnColoredText(s, (byte) (hex >> 16), (byte) (hex >> 8), (byte) hex);
    }

    public static void printColoredBckgHex(string s, int hex) {
        printColoredBckg(s, (byte) (hex >> 16), (byte) (hex >> 8), (byte) hex);
    }
}