// 
// VoxelsCoreSharp
// by KryKom 2024
//

// NOTE: use 'git diff --stat 4b825dc642cb6eb9a060e54bf8d69288fbee4904' to count lines

using Commandier;
using Kolors;
using VoxelsCoreSharp.generator.files.config;
using VoxelsCoreSharp.generator.files.data;
using VoxelsCoreSharp.generator.terrain.height;
using VoxelsCoreSharp.test.gen;
using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp;

public static class VoxelsCoreSharp {
    public static void Main(string[] args) {
        
        
        // Console.WriteLine("\x1B[38;2;15;138;29m\u2588\u2588\u2591     \u2588\u2588\u2591   \u2588\u2588\u2588\u2588\u2588\u2588\u2591    \u2588\u2588\u2591    \u2588\u2588\u2591  \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2591  \u2588\u2588\u2591         \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2591\n\u2588\u2588\u2591     \u2588\u2588\u2591  \u2588\u2588\u2591    \u2588\u2588\u2591   \u2588\u2588\u2591  \u2588\u2588\u2591   \u2588\u2588\u2591        \u2588\u2588\u2591        \u2588\u2588\u2588\u2591\n \u2588\u2588\u2591   \u2588\u2588\u2591   \u2588\u2588\u2591    \u2588\u2588\u2591    \u2588\u2588\u2588\u2588\u2591     \u2588\u2588\u2588\u2588\u2588\u2588\u2591    \u2588\u2588\u2591         \u2588\u2588\u2588\u2588\u2588\u2588\u2591\n  \u2588\u2588\u2591 \u2588\u2588\u2591    \u2588\u2588\u2591    \u2588\u2588\u2591   \u2588\u2588\u2591  \u2588\u2588\u2591   \u2588\u2588\u2591        \u2588\u2588\u2591             \u2588\u2588\u2588\u2591\n   \u2588\u2588\u2588\u2588\u2591      \u2588\u2588\u2588\u2588\u2588\u2588\u2591    \u2588\u2588\u2591    \u2588\u2588\u2591  \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2591  \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2591  \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2591\n");
        
        // Console.WriteLine("████░░  ████░░    ████████░░    ████░░  ████░░  ████████████░░  ████░░            ██████████░░\n" +
        //                   "████░░  ████░░  ████░░  ████░░    ████████░░    ████░░          ████░░          ████░░\n" +
        //                   "████░░  ████░░  ████░░  ████░░      ████░░      ████░░          ████░░              ██████░░\n" + 
        //                   "  ████████░░    ████░░  ████░░      ████░░      ████████░░      ████░░                  ████░░\n" +
        //                   "    ████░░      ████░░  ████░░    ████████░░    ████░░          ████░░                  ████░░\n" +
        //                   "    ████░░        ████████░░    ████░░  ████░░  ████████████░░  ████████████░░  ██████████░░\n\n");
        
        // ConsoleColors.printlnColored("TESTING COLORS!", 0xff0088);
        // ConsoleColors.printlnColoredB("   ", 0xff0088);
        //
        // printLogo();
        Debug.debugLevel = Debug.DebugLevel.ALL;

        HeightMapTest.run();
        
        // Global.registerCommands();
        //
        // Global.WORLD_DIR_PATH = "C:\\Users\\krystof\\Desktop\\projects\\Voxels\\VoxelsCore\\VoxelsCoreSharp\\world\\test";
        //
        // Debug.debugLevel = Debug.DebugLevel.ALL;
        // // Global.COMMAND_PARSER.shell();
        //
        // Shell.onStart = printLogo2;
        // Shell.SHELL.start();
        // Global.SHELL.start();
    }
    
    public static void printLogo() {
        ConsoleColors.printColored("             _\n" +
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
        ConsoleColors.printlnColored("        Voxels by:\n   KryKom & ZlomenyMesic\n", 0x4e8752);
        Console.WriteLine("============================\n");
    }

    public static void printLogo2() {
        ConsoleColors.printColored("\n  \x1B[1m\u2588\u2588\u2591 \u2588\u2588\u2591 \u2588\u2588\u2588\u2588\u2588\u2588\u2591 \u2588\u2588\u2591 \u2588\u2588\u2591 \u2588\u2588\u2588\u2588\u2588\u2588\u2591 \u2588\u2588\u2591       \u2588\u2588\u2588\u2588\u2591\n  \u2588\u2588\u2591 \u2588\u2588\u2591 \u2588\u2588\u2591 \u2588\u2588\u2591   \u2588\u2588\u2591   \u2588\u2588\u2591     \u2588\u2588\u2591       \u2588\u2588\u2591\n    \u2588\u2588\u2591   \u2588\u2588\u2588\u2588\u2588\u2588\u2591 \u2588\u2588\u2591 \u2588\u2588\u2591 \u2588\u2588\u2588\u2588\u2588\u2588\u2591 \u2588\u2588\u2588\u2588\u2588\u2588\u2591 \u2588\u2588\u2588\u2588\u2591\n                   by KryKom\n", Shell.PALETTE.colors[3]);
    }
}
