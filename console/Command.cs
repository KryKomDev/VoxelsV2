//
// VoxelsCoreSharp
// by KryKomDev
//

namespace VoxelsCoreSharp.console;

public class CommandParser {
    public static Command[] commands = [
        new Command("world", [], () => Console.WriteLine($"world file location: {Global.WORLD_MANAGER.WorldFilePath}")),
        new Command("world save", [], () => {})
    
    ];
    
} 

public struct Command(string name, object[] args, Action func) {
    public string name = name;
    public object[] args = args;
    public Action func = func;
}