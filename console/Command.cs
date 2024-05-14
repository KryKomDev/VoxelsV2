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
    
    public static Program[] commands = [
        new Program("world", [
            new Command("info", [], () => Console.WriteLine(Global.WORLD_MANAGER.WorldFilePath)),
            new Command("load", [(int)ArgumentType.STRING], (string path) => Console.WriteLine(path) ),
            new Command("save", [], () => Console.WriteLine("closing"))
        ]),
        new Program("place", [
            new Command()
        ])
    ];
    
} 

public struct Program(string programName, Command[] commands) {
    public string programName = programName;
    public Command[] commands = commands;
}

public struct Command(string commandName, int[] args, LambdaExpression functionality) {
    public string commandName = commandName;
    public int[] args = args;
    public LambdaExpression functionality = functionality;
}

public enum ArgumentType {
    VOID = 0,
    STRING = 1,
    INT = 2,
    LONG = 3,
    BOOL = 4
}