//
// VoxelsCoreSharp
// by KryKom
//

using System.Linq.Expressions;
using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp.console;

/// <summary>
/// voxels shell command parser
/// </summary>
public class CommandParser {
    
    private static readonly Command[] commands = [
        new Command("world", [
            new Flag("-info", [], (object[] args) => Console.WriteLine(Global.WORLD_MANAGER.WorldFilePath)),
            new Flag("-load", [(int)ArgumentType.STRING], (object[] args) => Console.WriteLine(args[0])),
            new Flag("-save", [], (object[] args) => Console.WriteLine("closing"))
        ]),
        new Command("place", [
            new Flag("-random", [(int)ArgumentType.STRING, (int)ArgumentType.LONG, (int)ArgumentType.LONG, (int)ArgumentType.LONG], 
                (object[] args) => Console.WriteLine("")),
            new Flag("-leveled", [(int)ArgumentType.STRING, (int)ArgumentType.LONG, (int)ArgumentType.LONG, (int)ArgumentType.LONG, (int)ArgumentType.INT], 
                (object[] args) => Console.WriteLine(""))
        ]),
        new Command("generate", [
            new Flag("-chunk", [], (object[] args) => Console.WriteLine("generating chunk...")),
            new Flag("-world", [(int)ArgumentType.STRING], (object[] args) => Console.WriteLine("generating world..."))
        ]),
        new Command("help", [
            new Flag("-overall", [], (object[] args) => {
                Console.WriteLine("Welcome to VoxelsShell.\n" +
                                  "syntax of every command is <command-name> <flag-name> <params...>\n" +
                                  "for more help type 'help -command <command-name>' or 'help -flag <command-name> <flag-name>'\n" +
                                  "to exit type 'exit'");
            }, "gives overall info about the shell"),
            
            new Flag("-global", [], (object[] args) => Global.listAll(), "lists all global variables and their values"),
            
            new Flag("-clear", [], (object[] args) => Console.Clear(), "clears console"),
            
            new Flag("-logo", [], (object[] args) => Voxels.printLogo(), "just prints the ascii art"),
            
            new Flag("-flag", [(int)ArgumentType.STRING, (int)ArgumentType.STRING], (object[] args) => {
                foreach (Command c in commands) {
                    if (c.commandName == (string)args[0]) {
                        foreach (Flag f in c.flags) {
                            if (f.flagName == (string)args[1]) {
                                Console.WriteLine(f.description ?? "this does not have a description...");
                                return;
                            }
                        }
                    }
                }
                
                ConsoleColors.printlnColoredTextHex("No matching flag found", (int)Colors.RED_5);
            }, "prints description of the inputted flag"),
            
            new Flag("-command", [(int)ArgumentType.STRING], (object[] args) => {
                foreach (Command c in commands) {
                    if (c.commandName == (string)args[0]) {
                        Console.WriteLine(c.description ?? "this does not have a description...");
                        return;
                    }
                }
                
                ConsoleColors.printlnColoredTextHex("No matching command found", (int)Colors.RED_5);
            }, "prints description of the inputted command")
        ]),
        new Command("setup", [
            new Flag("-wm", [(int)ArgumentType.STRING], (object[] args) => Global.setupWorldManager(args[0].ToString()!), "sets up world manager"),
            new Flag("-sm", [], (object[] args) => {}),
        ], "sets up some of the tools"),
        
        new Command("wm", [
            new Flag("-close", [], (object[] args) => Global.WORLD_MANAGER.close()),
            new Flag("-validate", [], (object[] args) => {
                if (Global.WORLD_MANAGER == null) {
                    ConsoleColors.printlnColoredTextHex("World manager not set up yet!", (int)Colors.RED_5);
                    return;
                }
                if (Global.WORLD_MANAGER.validate() == 0) {
                    ConsoleColors.printlnColoredTextHex("World file is the right format", (int)Colors.GREEN_3);
                }
                else {
                    ConsoleColors.printlnColoredTextHex("World file has incorrect format", (int)Colors.RED_5);
                }
            }),
            
            new Flag("-rh", [], (object[] args) => {
                
                if (Global.WORLD_MANAGER == null) {
                    ConsoleColors.printlnColoredTextHex("World manager not set up yet!", (int)Colors.RED_5);
                    return;
                }
                
                VXWHeader h = Global.WORLD_MANAGER.readHeader();
                ConsoleColors.printlnColoredTextHex("" + h, (int)Colors.GRAY_2);
            }),
            
            new Flag("-wh", [(int)ArgumentType.USHORT, (int)ArgumentType.USHORT, (int)ArgumentType.UINT, (int)ArgumentType.UINT, (int)ArgumentType.BOOL], (object[] args) => {
                VXWHeader h = new VXWHeader {
                    chunkSize = (ushort)args[0],
                    regionSize = (ushort)args[1],
                    maxHeight = (uint)args[2],
                    worldSize = (uint)args[3],
                    biomeDim = (bool)args[4]
                };

                if (Global.WORLD_MANAGER == null) {
                    ConsoleColors.printlnColoredTextHex("World manager not set up yet!", (int)Colors.RED_5);
                    return;
                }
                
                Global.WORLD_MANAGER.writeHeader(h.chunkSize, h.regionSize, h.maxHeight, h.worldSize, h.biomeDim, true);
            }),
            
            new Flag("-reset", [], (object[] args) => {
                if (Global.WORLD_MANAGER == null) {
                    ConsoleColors.printlnColoredTextHex("World manager not set up yet!", (int)Colors.RED_5);
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
        
    ];

    /// <summary>
    /// main shell loop method<br/>
    /// creates a shell in the console<br/>
    /// use 'exit' to exit the shell
    /// </summary>
    public void shell() {
        while (true) {
            if (parse() == 2) {
                break;
            }
        }
    }

    /// <summary>
    /// creates a shell in console
    /// </summary>
    /// <returns>0 if successfully executed, 1 if an error occured, 2 if exited</returns>
    private int parse() {
        
        // Console.Write($"\n[{DateTime.Now:HH:mm:ss}]: $ ");
        ConsoleColors.printColoredTextHex($"\n[{DateTime.Now:HH:mm:ss}]: \x1B[1m$\x1B[22m ", (int)Colors.GRAY_5);
        
        string? command = Console.ReadLine();

        if (command == "") {
            return 0;
        }
        
        if (command is null or "exit") {
            Debug.info("exiting shell...");
            return 2;
        }

        string[] input = command.Split(' ');

        if (input.Length < 2) {
            ConsoleColors.printlnColoredTextHex($"Invalid number of keywords ({input.Length}, must be 2) !", (int)Colors.RED_5);
            return 1;
        }
        
        (string commandName, string flagName) keywords = (input[0], input[1]);
        Flag? executedCommand = null;
        
        foreach (Command p in commands) {
            if (p.commandName == keywords.commandName) {
                foreach (Flag c in p.flags) {
                    if (c.flagName == keywords.flagName) {
                        executedCommand = c;
                        break;
                    }
                }
                break;
            }
        }

        if (executedCommand == null) {
            ConsoleColors.printlnColoredTextHex("Invalid command name!", (int)Colors.RED_5);
            return 1;
        }

        Action<object[]> lambda = executedCommand.Value.code;

        if (executedCommand.Value.args.Length != input.Length - 2) {
            ConsoleColors.printlnColoredTextHex($"Invalid number of arguments ({input.Length - 2}, must be {executedCommand.Value.args.Length}) !", (int)Colors.RED_5);
            return 1;
        }

        object[] args = new object[executedCommand.Value.args.Length];

        for (int i = 0; i < executedCommand.Value.args.Length; i++) {
            switch (executedCommand.Value.args[i]) {
                
                case (int)ArgumentType.STRING: {
                    args[i] = input[i + 2];
                    break;
                }

                case (int)ArgumentType.SHORT: {
                    try {
                        args[i] = short.Parse(input[i + 2]);
                    }
                    catch {
                        ConsoleColors.printlnColoredTextHex(
                            $"Invalid argument at {i} ({input[i + 2]}, must be type of: short) !", (int)Colors.RED_5);
                        return 1;
                    }
                    break;
                }

                case (int)ArgumentType.USHORT: {
                    try {
                        args[i] = ushort.Parse(input[i + 2]);
                    }
                    catch {
                        ConsoleColors.printlnColoredTextHex(
                            $"Invalid argument at {i} ({input[i + 2]}, must be type of: unsigned short) !", (int)Colors.RED_5);
                        return 1;
                    }
                    break;
                }

                case (int)ArgumentType.INT: {
                    try {
                        args[i] = int.Parse(input[i + 2]);
                    }
                    catch {
                        ConsoleColors.printlnColoredTextHex(
                            $"Invalid argument at {i} ({input[i + 2]}, must be type of: int) !", (int)Colors.RED_5);
                        return 1;
                    }
                    break;
                }

                case (int)ArgumentType.UINT: {
                    try {
                        args[i] = uint.Parse(input[i + 2]);
                    }
                    catch {
                        ConsoleColors.printlnColoredTextHex(
                            $"Invalid argument at {i} ({input[i + 2]}, must be type of: unsigned int) !", (int)Colors.RED_5);
                        return 1;
                    }
                    break;
                }

                case (int)ArgumentType.LONG: {
                    try {
                        args[i] = long.Parse(input[i + 2]);
                    }
                    catch {
                        ConsoleColors.printlnColoredTextHex(
                            $"Invalid argument at {i} ({input[i + 2]}, must be type of: long) !", (int)Colors.RED_5);
                        return 1;
                    }
                    break;
                }

                case (int)ArgumentType.ULONG: {
                    try {
                        args[i] = ulong.Parse(input[i + 2]);
                    }
                    catch {
                        ConsoleColors.printlnColoredTextHex(
                            $"Invalid argument at {i} ({input[i + 2]}, must be type of: unsigned long) !", (int)Colors.RED_5);
                        return 1;
                    }
                    break;
                }

                case (int)ArgumentType.BOOL: {
                    if (input[i + 2] == "true") {
                        args[i] = true;
                    }
                    else if (input[i + 2] == "false") {
                        args[i] = false;
                    }
                    else {
                        ConsoleColors.printlnColoredTextHex(
                            $"Invalid argument at {i} ({input[i + 2]}, must be type of: bool) !", (int)Colors.RED_5);
                        return 1;
                    }
                    break;
                }
            }
        }

        lambda(args);

        return 0;
    }
} 

/// <summary>
/// struct for holding shell sub-commands
/// </summary>
/// <param name="commandName">name of the command</param>
/// <param name="flags">array of sub-commands</param>
internal struct Command(string commandName, Flag[] flags) {
    public readonly string commandName = commandName;
    public readonly Flag[] flags = flags;
    public readonly string? description = null;

    public Command(string commandName, Flag[] flags, string? description = null) : this(commandName, flags) {
        this.description = description;
    }
}

/// <summary>
/// struct holding command data and code
/// </summary>
/// <param name="flagName">name of the command</param>
/// <param name="args">arguments inputted into the shell behind the command</param>
/// <param name="code">code that executes when run</param>
internal struct Flag(string flagName, int[] args, Action<object[]> code) {
    public readonly string flagName = flagName;
    public readonly int[] args = args;
    public readonly Action<object[]> code = code;
    public readonly string? description = null;

    public Flag(string flagName, int[] args, Action<object[]> code, string? description = null) : this(flagName, args, code) {
        this.description = description;
    }
}

/// <summary>
/// types of arguments used for the shell
/// </summary>
public enum ArgumentType {
    STRING = 1,
    SHORT = 2,
    USHORT = 3,
    INT = 4,
    UINT = 5,
    LONG = 6,
    ULONG = 7,
    BOOL = 8,
}