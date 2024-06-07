//
// VoxelsCoreSharp
// by KryKom
//

using VoxelsCoreSharp.console.command.argument;

namespace VoxelsCoreSharp.console.command;
    
    /*
    private static readonly OldCommand[] commands = [
        new OldCommand("world", [
            new Flag("-info", [], (object[] args) => Console.WriteLine(Global.WORLD_MANAGER.WorldFilePath)),
            new Flag("-load", [(int)EArgumentType.STRING], (object[] args) => Console.WriteLine(args[0])),
            new Flag("-save", [], (object[] args) => Console.WriteLine("closing"))
        ]),
        new OldCommand("place", [
            new Flag("-random", [(int)EArgumentType.STRING, (int)EArgumentType.LONG, (int)EArgumentType.LONG, (int)EArgumentType.LONG], 
                (object[] args) => Console.WriteLine("")),
            new Flag("-leveled", [(int)EArgumentType.STRING, (int)EArgumentType.LONG, (int)EArgumentType.LONG, (int)EArgumentType.LONG, (int)EArgumentType.INT], 
                (object[] args) => Console.WriteLine(""))
        ]),
        new OldCommand("generate", [
            new Flag("-chunk", [], (object[] args) => Console.WriteLine("generating chunk...")),
            new Flag("-world", [(int)EArgumentType.STRING], (object[] args) => Console.WriteLine("generating world..."))
        ]),
        new OldCommand("help", [
            new Flag("-overall", [], (object[] args) => {
                Console.WriteLine("Welcome to VoxelsShell.\n" +
                                  "syntax of every command is <command-name> <flag-name> <params...>\n" +
                                  "for more help type 'help -command <command-name>' or 'help -flag <command-name> <flag-name>'\n" +
                                  "to exit type 'exit'");
            }, "gives overall info about the shell"),
            
            new Flag("-global", [], (object[] args) => Global.listAll(), "lists all global variables and their values"),
            
            new Flag("-clear", [], (object[] args) => Console.Clear(), "clears console"),
            
            new Flag("-logo", [], (object[] args) => Voxels.printLogo(), "just prints the ascii art"),
            
            new Flag("-flag", [(int)EArgumentType.STRING, (int)EArgumentType.STRING], (object[] args) => {
                foreach (OldCommand c in commands) {
                    if (c.commandName == (string)args[0]) {
                        foreach (Flag f in c.flags) {
                            if (f.flagName == (string)args[1]) {
                                Console.WriteLine(f.description ?? "this does not have a description...");
                                return;
                            }
                        }
                    }
                }
                
                ConsoleColors.printlnColored("No matching flag found", (int)Colors.RED_5);
            }, "prints description of the inputted flag"),
            
            new Flag("-command", [(int)EArgumentType.STRING], (object[] args) => {
                foreach (OldCommand c in commands) {
                    if (c.commandName == (string)args[0]) {
                        Console.WriteLine(c.description ?? "this does not have a description...");
                        return;
                    }
                }
                
                ConsoleColors.printlnColored("No matching command found", (int)Colors.RED_5);
            }, "prints description of the inputted command")
        ]),
        new OldCommand("setup", [
            new Flag("-wm", [(int)EArgumentType.STRING], (object[] args) => Global.setupWorldManager(args[0].ToString()!), "sets up world manager"),
            new Flag("-sm", [], (object[] args) => {}),
        ], "sets up some of the tools"),
        
        new OldCommand("wm", [
            new Flag("-close", [], (object[] args) => Global.WORLD_MANAGER.close()),
            new Flag("-validate", [], (object[] args) => {
                if (Global.WORLD_MANAGER == null) {
                    ConsoleColors.printlnColored("World manager not set up yet!", (int)Colors.RED_5);
                    return;
                }
                if (Global.WORLD_MANAGER.validate() == 0) {
                    ConsoleColors.printlnColored("World file is the right format", (int)Colors.GREEN_3);
                }
                else {
                    ConsoleColors.printlnColored("World file has incorrect format", (int)Colors.RED_5);
                }
            }),
            
            new Flag("-rh", [], (object[] args) => {
                
                if (Global.WORLD_MANAGER == null) {
                    ConsoleColors.printlnColored("World manager not set up yet!", (int)Colors.RED_5);
                    return;
                }
                
                VXWHeader h = Global.WORLD_MANAGER.readHeader();
                ConsoleColors.printlnColored("" + h, (int)Colors.GRAY_2);
            }),
            
            new Flag("-wh", [(int)EArgumentType.USHORT, (int)EArgumentType.USHORT, (int)EArgumentType.UINT, (int)EArgumentType.UINT, (int)EArgumentType.BOOL], (object[] args) => {
                VXWHeader h = new VXWHeader {
                    chunkSize = (ushort)args[0],
                    regionSize = (ushort)args[1],
                    maxHeight = (uint)args[2],
                    worldSize = (uint)args[3],
                    biomeDim = (bool)args[4]
                };

                if (Global.WORLD_MANAGER == null) {
                    ConsoleColors.printlnColored("World manager not set up yet!", (int)Colors.RED_5);
                    return;
                }
                
                Global.WORLD_MANAGER.writeHeader(h.chunkSize, h.regionSize, h.maxHeight, h.worldSize, h.biomeDim, true);
            }),
            
            new Flag("-reset", [], (object[] args) => {
                if (Global.WORLD_MANAGER == null) {
                    ConsoleColors.printlnColored("World manager not set up yet!", (int)Colors.RED_5);
                    return;
                }
                
                Console.Write("This will reset the current world file content, \ndo you want to continue? [y/n]: ");
                string response = Console.ReadLine();

                while (response != "y" && response != "n") {
                    Console.Write("invalid answer, try again. [y/n]: ");
                    response = Console.ReadLine();
                }

                if (response == "y") {
                    
                }
            }, "resets the contents of the world file")
        ]),
        
    ]; */

public class Command {
    public readonly string name;
    public readonly ArgumentType[] arguments;
    private readonly Action<object[]> code;
    public readonly string description;

    public Command(string name, ArgumentType[] arguments, Action<object[]> code, string description = "this does not have a description...") {
        this.name = name;
        this.arguments = arguments;
        this.code = code;
        this.description = description;
    }

    public void run(string raw) {
        object[]? args = ArgumentParser.parse(raw, arguments);

        if (args == null) {
            return;
        }

        code(args);
    }
}
