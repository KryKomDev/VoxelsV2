//
// Commandier
// by KryKom 2024
//

using VoxelsCoreSharp.console.command.argument;
using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp.console.command;

public class CommandRegistry {

    /// <summary>
    /// list of all available commands
    /// </summary>
    private static List<Command> COMMAND_REGISTRY = new List<Command>();

    /// <summary>
    /// returns the entire command register
    /// </summary>
    /// <returns></returns>
    public static List<Command> getRegistry() {
        return COMMAND_REGISTRY;
    }

    /// <summary>
    /// adds a new command to the command register
    /// </summary>
    /// <param name="c"></param>
    public static void registerCommand(Command c) {
        
        foreach (Command test in COMMAND_REGISTRY) {
            if (c.name == test.name) {
                Debug.error($"A command with the same name already exists! Command {c.name} was not added to the command register.");
                return;
            }
        }
        
        COMMAND_REGISTRY.Add(c);
    }

    /// <summary>
    /// registers default commands
    /// </summary>
    private static void registerDefault() {
        registerCommand(HELP);
        registerCommand(EXIT);
    }

    /// <summary>
    /// static constructor, called only once
    /// </summary>
    static CommandRegistry() {
        registerDefault();
    }
    
    
    // --- COMMAND CODE ---
    
    public static readonly Command HELP = new Command("help", [new StringArgument("command_name")], (object[] args) => {
        if ((string)args[0] == "list") {
            Console.WriteLine("Available commands:");
            
            foreach (Command c in COMMAND_REGISTRY) {
                Console.WriteLine($" {c.name}: {c.description}");
            }
            return;
        }

        if ((string)args[0] == "welcome") {
            Console.WriteLine("Welcome to Commandier.\n" +
                              "syntax of every command is <command_name> <args...>\n" +
                              "for more help for specific command type 'help <command_name>'\n" +
                              "to exit type 'exit'");
            return;
        }
        
        foreach (Command c in COMMAND_REGISTRY) {
            if (c.name == (string)args[0]) {
                Console.WriteLine(c.description);
                
                if (c.arguments.Length != 0) {
                    Console.WriteLine($"Usage for {c.name}:");
                    foreach (ArgumentType a in c.arguments) {
                        Console.WriteLine($"    <{a.type()}: {a.description}> ");
                    }
                }
                
                return;
            }
        }
        
        Debug.error("No available command with this name.");
    }, "helps you understand the commands and the shell");

    public static readonly Command EXIT = new Command("exit", [], args => {
        Global.SHELL.stop();
    }, "exits the shell");
}