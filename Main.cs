// 
// VoxelsCoreSharp
// by KryKom 2024
//

using VoxelsCoreSharp.util;
using VoxelsCoreSharp.maths;

void main() {
    Action<(int x, int y, int z)> print = tuple => { Console.WriteLine($"x: {tuple.x}, y: {tuple.y}, z: {tuple.z}"); };
    Bresenham3D.generateLine((0, 1000, 0), (0, 5, -500), print);
    // Debug.warn("lol");
    // Debug.err("failed");
    // Console.WriteLine("l");
    ConsoleColors.printColoredTextHex("lol", 0xdf9bff);
}

main();