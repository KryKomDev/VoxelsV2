//
// Commandier 
// by KryKom 2024
//

using VoxelsCoreSharp.console.command.argument;

namespace VoxelsCoreSharp.console.command;

public class Shell {
    
    private bool running = false;

    public void start() {
        running = true;
        
        while (running) {
            parse();
        }
    }

    private void parse() {
        ConsoleColors.printColored($"\n[{DateTime.Now:HH:mm:ss}]: \x1B[1m$\x1B[22m ", (int)Colors.GRAY_5);
        string? raw = Console.ReadLine();
        
        if (string.IsNullOrEmpty(raw)) {
            return;
        }
        
        raw += " ";
        
        string command = "";
        int argsStart = 0;

        for (int i = 0; i < raw.Length; i++) {
            if (raw[i] != ' ') {
                command += raw[i];
            }
            else {
                argsStart = i + 1;
                break;
            }
        }

        Command? runnable = null;

        foreach (Command c in CommandRegistry.getRegistry()) {
            if (c.name == command) {
                runnable = c;
            }
        }

        if (runnable == null) {
            Debug.error("No available command with this name.");
            return;
        }

        string args = "";

        for (int i = argsStart; i < raw.Length; i++) {
            args += raw[i];
        }
        
        runnable.run(args);
    }

    public void stop() {
        running = false;
    }
}