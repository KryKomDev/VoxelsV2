//
// VoxelsCoreSharp
// by KryKomDev
//

using System.Linq.Expressions;

namespace VoxelsCoreSharp.console;

/// <summary>
/// voxels shell command parser
/// </summary>
public class CommandParser {
    
    private static readonly Program[] commands = [
        new Program("world", [
            new Command("info", [], (object[] args) => Console.WriteLine(Global.WORLD_MANAGER.WorldFilePath)),
            new Command("load", [(int)ArgumentType.STRING], (object[] args) => Console.WriteLine(args[0])),
            new Command("save", [], (object[] args) => Console.WriteLine("closing"))
        ]),
        new Program("place", [
            new Command("random", [(int)ArgumentType.STRING, (int)ArgumentType.LONG, (int)ArgumentType.LONG, (int)ArgumentType.LONG], 
                (object[] args) => Console.WriteLine("")),
            new Command("leveled", [(int)ArgumentType.STRING, (int)ArgumentType.LONG, (int)ArgumentType.LONG, (int)ArgumentType.LONG, (int)ArgumentType.INT], 
                (object[] args) => Console.WriteLine(""))
        ]),
        new Program("generate", [
            new Command("chunk", [], (object[] args) => Console.WriteLine("generating chunk...")),
            new Command("world", [(int)ArgumentType.STRING], (object[] args) => Console.WriteLine("generating world..."))
        ]),
        new Program("help", [
            new Command("overall", [], (object[] args) => {
                
            }),
            new Command("global", [], (object[] args) => Global.listAll()),
            new Command("clear", [], (object[] args) => Console.Clear())
        ]),
        new Program("setup", [
            new Command("wm", [(int)ArgumentType.STRING], (object[] args) => Global.setupWorldManager(args[0].ToString()!)),
            new Command("sm", [], (object[] args) => {}),
        ])
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
        
        Console.Write($"\n[{DateTime.Now:HH:mm:ss}] \\> ");
        
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
        
        (string progName, string commName) keywords = (input[0], input[1]);
        Command? executedCommand = null;
        
        foreach (Program p in commands) {
            if (p.programName == keywords.progName) {
                foreach (Command c in p.commands) {
                    if (c.commandName == keywords.commName) {
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
                case 1:
                    args[i] = input[i + 2];
                    break;
                
                case 2:
                    try {
                        args[i] = int.Parse(input[i + 2]);
                    }
                    catch {
                        ConsoleColors.printlnColoredTextHex(
                            $"Invalid argument at {i} ({input[i + 2]}, must be type of: int) !", (int)Colors.RED_5);
                        return 1;
                    }
                    break;
                
                case 3:
                    try {
                        args[i] = long.Parse(input[i + 2]);
                    }
                    catch {
                        ConsoleColors.printlnColoredTextHex(
                            $"Invalid argument at {i} ({input[i + 2]}, must be type of: long) !", (int)Colors.RED_5);
                        return 1;
                    }
                    break;
                
                case 4:
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

        lambda(args);

        return 0;
    }
} 

/// <summary>
/// struct for holding shell sub-commands
/// </summary>
/// <param name="programName">name of the command</param>
/// <param name="commands">array of sub-commands</param>
internal struct Program(string programName, Command[] commands) {
    public readonly string programName = programName;
    public readonly Command[] commands = commands;
}

/// <summary>
/// struct holding command data and code
/// </summary>
/// <param name="commandName">name of the command</param>
/// <param name="args">arguments inputted into the shell behind the command</param>
/// <param name="code">code that executes when run</param>
internal struct Command(string commandName, int[] args, Action<object[]> code) {
    public readonly string commandName = commandName;
    public readonly int[] args = args;
    public readonly Action<object[]> code = code;
}

/// <summary>
/// types of arguments used for the shell
/// </summary>
public enum ArgumentType {
    STRING = 1,
    INT = 2,
    LONG = 3,
    BOOL = 4
}