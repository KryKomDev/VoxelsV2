// 
// VoxelsCoreSharp
// by KryKom 2024
//

using System.Diagnostics;
using VoxelsCoreSharp.util;
using VoxelsCoreSharp.maths;
using VoxelsCoreSharp.renderer;
using VoxelsCoreSharp.world;
using Debug = VoxelsCoreSharp.util.Debug;

void main() {
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

    Global.WORLD_MANAGER.writeHeader(4, 4, 3, 4, false, true);
    Global.WORLD_MANAGER.loadHeader();
    // Global.WORLD_MANAGER.generateRegionPaddingArray(false);
    Global.WORLD_MANAGER.readChunk(-16, -15);

    // VXWHeader header = wm.readHeader();
    // Console.WriteLine(header.ToString());
}

main();


void printLogo() {
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
                  , (int)Colors.GREEN_1);
    ConsoleColors.printlnColoredTextHex("        Voxels by:\n   KryKom & ZlomenyMesic\n", 0x15751a);
    Console.WriteLine("----------------------------\nLog:\n");
}