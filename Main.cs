// 
// VoxelsCoreSharp
// by KryKom 2024
//

using VoxelsCoreSharp.util;
using VoxelsCoreSharp.maths;
using VoxelsCoreSharp.renderer;

void main() {
    // Action<(int x, int y, int z)> print = tuple => { Console.WriteLine($"x: {tuple.x}, y: {tuple.y}, z: {tuple.z}"); };
    // Bresenham3D.generateLine((0, 1000, 0), (0, 5, -500), print);
    // Debug.warn("lol");
    // Debug.err("failed");
    // Console.WriteLine("l");
    // ConsoleColors.printColoredTextHex("lol", 0xdf9bff);
    long[,] l = new long[30, 30];
    
    // Triangle.drawTriangle((15, 0), (3, 20), (25, 28), 0xdf9bff, ref l);
    Triangle.drawConvexPolygon(new []{(2, 2), (2, 26), (26, 2), (26, 26), (20, 20)}, 0xdf9bff, ref l);

    for (int x = 0; x < 30; x++) {
        for (int y = 0; y < 30; y++) {
            ConsoleColors.printColoredBckgHex("   ", (int) l[y, x]);
        }
        
        Console.WriteLine();
    }
    
}

main();