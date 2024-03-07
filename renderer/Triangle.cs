//
// VoxelsCoreSharp
// by KryKom 2024
//

using VoxelsCoreSharp.maths;
using VoxelsCoreSharp.util;

namespace VoxelsCoreSharp.renderer;

public class Triangle {

    /// <summary>
    /// generates a triangle defined by the three points (p1, p2, p3)
    /// </summary>
    /// <param name="p1">first point</param>
    /// <param name="p2">second point</param>
    /// <param name="p3">third point</param>
    /// <param name="color">color in hexadecimal code</param>
    /// <param name="destination">the place where the triangle will be drawn, format MUST be the same as for Global.DISPLAY_COLORS</param>
    /// <seealso cref="Global.DISPLAY_COLORS"/>
    public static void generateTriangle((int x, int y) p1, (int x, int y) p2, (int x, int y) p3, int color, ref long[,] destination) {

        List<(int x, int y)> points = new List<(int x, int y)>();
        Action<(int x, int y)> plot = p => { points.Add(p); };  

        Bresenham2D.generateLine(p1, p2, plot);
    }
}