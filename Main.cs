// 
// VoxelsCoreSharp
// by KryKom 2024
//

using VoxelsCoreSharp.maths;

void main() {
    // Bresenham.generateLine((0, 0), (4, 8));
    (double x, double y) p = Raycaster2D.pointsToPath((0, 0), (-4, 0), 12);
    Console.WriteLine($"x: {p.x}, y: {p.y}");
}

main();