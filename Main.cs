// 
// VoxelsCoreSharp
// by KryKom 2024
//

// NOTE: use 'git diff --stat 4b825dc642cb6eb9a060e54bf8d69288fbee4904' to count lines

using VoxelsCoreSharp.console;
using VoxelsCoreSharp.console.command.argument;
using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp;

public static class Voxels {
    public static void Main(string[] args) {
        
        /*
        Console.WriteLine("\x1B[38;2;15;138;29m\u2588\u2588\u2591     \u2588\u2588\u2591   \u2588\u2588\u2588\u2588\u2588\u2588\u2591    \u2588\u2588\u2591    \u2588\u2588\u2591  \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2591  \u2588\u2588\u2591         \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2591\n\u2588\u2588\u2591     \u2588\u2588\u2591  \u2588\u2588\u2591    \u2588\u2588\u2591   \u2588\u2588\u2591  \u2588\u2588\u2591   \u2588\u2588\u2591        \u2588\u2588\u2591        \u2588\u2588\u2588\u2591\n \u2588\u2588\u2591   \u2588\u2588\u2591   \u2588\u2588\u2591    \u2588\u2588\u2591    \u2588\u2588\u2588\u2588\u2591     \u2588\u2588\u2588\u2588\u2588\u2588\u2591    \u2588\u2588\u2591         \u2588\u2588\u2588\u2588\u2588\u2588\u2591\n  \u2588\u2588\u2591 \u2588\u2588\u2591    \u2588\u2588\u2591    \u2588\u2588\u2591   \u2588\u2588\u2591  \u2588\u2588\u2591   \u2588\u2588\u2591        \u2588\u2588\u2591             \u2588\u2588\u2588\u2591\n   \u2588\u2588\u2588\u2588\u2591      \u2588\u2588\u2588\u2588\u2588\u2588\u2591    \u2588\u2588\u2591    \u2588\u2588\u2591  \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2591  \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2591  \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2591\n");
        
        Console.WriteLine("████░░  ████░░    ████████░░    ████░░  ████░░  ████████████░░  ████░░            ██████████░░\n" +
                          "████░░  ████░░  ████░░  ████░░    ████████░░    ████░░          ████░░          ████░░\n" +
                          "████░░  ████░░  ████░░  ████░░      ████░░      ████░░          ████░░              ██████░░\n" + 
                          "  ████████░░    ████░░  ████░░      ████░░      ████████░░      ████░░                  ████░░\n" +
                          "    ████░░      ████░░  ████░░    ████████░░    ████░░          ████░░                  ████░░\n" +
                          "    ████░░        ████████░░    ████░░  ████░░  ████████████░░  ████████████░░  ██████████░░\n\n");
        */

        object?[] a = ArgumentParser.parse("12 123.3 \"asd asd\"", [new IntArgument(), new FloatArgument(), new StringArgument()]);
        
        printLogo();

        Global.COMMAND_PARSER.shell();
        
    }
    
    public static void printLogo() {
        ConsoleColors.printColoredTextHex("             _\n" +
                                          "       _ _ /   \\ _ _\n" +
                                          "   _ /               \\ _\n" +
                                          " |   \\ _ _       _ _ /   |\n" +
                                          " |         \\ _ /         |\n" +
                                          " |           |           |\n" +
                                          " |           |           |\n" +
                                          " | _         |         _ |\n" +
                                          "     \\ _ _   |   _ _ /\n" +
                                          "           \\ ! /\n\n"
            , 0x7ad380);
        ConsoleColors.printlnColoredTextHex("        Voxels by:\n   KryKom & ZlomenyMesic\n", 0x4e8752);
        Console.WriteLine("============================\n");
    }
}
