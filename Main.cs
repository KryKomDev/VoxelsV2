﻿// 
// VoxelsCoreSharp
// by KryKom 2024
//

// NOTE: use 'git diff --stat 4b825dc642cb6eb9a060e54bf8d69288fbee4904' to count lines

using Commandier;
using Kolors;
using VoxelsCoreSharp.console;
using VoxelsCoreSharp.console.command;
using VoxelsCoreSharp.console.command.argument;
using VoxelsCoreSharp.world;
using Command = Commandier.Command;

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
        
        ConsoleColors.printlnColored("TESTING COLORS!", 0xff0088);
        ConsoleColors.printlnColoredB("   ", 0xff0088);
        
        printLogo();
        
        // Global.COMMAND_PARSER.shell();
        
        Shell.SHELL.start();
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
}
