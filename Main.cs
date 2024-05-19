// 
// VoxelsCoreSharp
// by KryKom 2024
//

// NOTE: use 'git diff --stat 4b825dc642cb6eb9a060e54bf8d69288fbee4904' to count lines

using VoxelsCoreSharp.console;
using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp;

public static class Voxels {
    public static void Main(string[] args) {
        // Action<(int x, int y, int z)> print = tuple => { Console.WriteLine($"x: {tuple.x}, y: {tuple.y}, z: {tuple.z}"); };
        // Bresenham3D.generateLine((0, 1000, 0), (0, 5, -500), print);
        // Debug.warn("lol");
        // Debug.error("failed");
        // Console.WriteLine("l");
        // ConsoleColors.printColoredTextHex("lol", 0xdf9bff);
        // long[,] l = new long[30, 30];
        //
        // // for (int i = 10000; i > -1; i--) {
        // //     Mesh2D.drawTriangle((15, 0), (3, 20), (25, 28), 0xdf9bff, ref l);
        // //     Mesh2D.drawTriangle((20, 5), (10, 10), (15, 25), 0x673793, ref l);
        // // }
        // Mesh2D.drawConvexPolygon([(2, 8), (15, 2), (2, 20), (15, 28), (26, 7), (27, 23)], (int)Colors.GRAY_5, ref l);
        // Mesh2D.drawFastPolygon([(2, 8), (2, 20), (15, 2), (15, 28), (26, 7), (27, 23)], (int)Colors.GRAY_5, ref l);
        //
        // for (int x = 0; x < 30; x++) {
        //     for (int y = 0; y < 30; y++) {
        //         ConsoleColors.printColoredBckgHex("   ", (int) l[y, x]);
        //     }
        //     
        //     Console.WriteLine();
        // }
        //
        // ConsoleColors.printlnColoredTextHex("C:/Users/krykom/Desktop>", (int)Colors.GRAY_5);
        // ConsoleColors.printlnColoredTextHex("C:/Users/krykom/Desktop>", (int)Colors.GRAY_4);
        // ConsoleColors.printlnColoredTextHex("C:/Users/krykom/Desktop>", (int)Colors.GRAY_3);
        // ConsoleColors.printlnColoredTextHex("C:/Users/krykom/Desktop>", (int)Colors.GRAY_2);
        // ConsoleColors.printlnColoredTextHex("C:/Users/krykom/Desktop>", (int)Colors.GRAY_1);
            
        printLogo();
            
        Global.setupWorldManager("C:/Users/krystof/Desktop/projects/Voxels/VoxelsCore/VoxelsCoreSharp/world/test/test.vxw");
        
        Global.WORLD_MANAGER.writeHeader(1, 1, 1, 1, false, true);
        Global.WORLD_MANAGER.loadHeader();
        // Global.WORLD_MANAGER.generateRegionPaddingArray(false);
        // Global.WORLD_MANAGER.readChunk(-16, -15);

        SubChunk sc = new SubChunk();
        sc.biome = 3;
        sc.content[0, 0, 0] = 4;

        Chunk c = new Chunk();
        c.climateBiome = 2;
        c.state = 1;
        c.content = [sc];
        
        Global.WORLD_MANAGER.writeChunk(c);
        Chunk? c2 = Global.WORLD_MANAGER.readChunk(0, 0);
        
        // Global.listAll();
        
        // Global.WORLD_MANAGER.generateRegionSector(0, 0);
        // Global.WORLD_MANAGER.generateRegionSector(15, 15);
        
        // Debug.warn($"{WorldManager.getRegionPaddingOffset(31, 31)}");
        
        // Global.WORLD_MANAGER.close();

        Global.COMMAND_PARSER.shell();

        
        // VXWHeader header = wm.readHeader();
        // Console.WriteLine(header.ToString());
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