//
// VoxelsCoreSharp
// by KryKom 2024
//

// This is my implementation based on the wikipedia pseudocode

namespace VoxelsCoreSharp.maths;

public class Bresenham2D {

    /// <summary>
    /// generates a 2D line
    /// </summary>
    /// <param name="p1">2D coordinates of the first point</param>
    /// <param name="p2">2D coordinates of the second point</param>
    /// <param name="plot">action called when a new point of the line is generated</param>
    public static void generateLine((int x, int y) p1, (int x, int y) p2, Action<(int x, int y)> plot) {

        // decide by which axis shall the line be computed
        // deltaX ( Math.Abs(p1.x - p2.x) ) is larger -> compute by X-axis
        if (Math.Abs(p1.y - p2.y) < Math.Abs(p1.x - p2.x)) {

            // decide if x grows already or if the points have to be swapped
            if (p1.x < p2.x) {
                generateByX(p1, p2, plot);
            } else {
                generateByX(p2, p1, plot);
            }
                
        } 
        // deltaY ( Math.Abs(p1.y - p2.y) ) is larger -> compute by Y-axis
        else {
            
            // decide if y grows already or if the points have to be swapped
            if (p1.y < p2.y) {
                generateByY(p1, p2, plot);
            } else {
                generateByY(p2, p1, plot);
            }
        }
    }
    
    /// <summary>
    /// generates a line along x-axis
    /// </summary>
    /// <param name="p1">2D coordinates of the first point</param>
    /// <param name="p2">2D coordinates of the second point</param>
    /// <param name="plot">action called when a new point of the line is generated</param>
    private static void generateByX((int x, int y) p1, (int x, int y) p2, Action<(int x, int y)> plot) {

        int deltaX = p2.x - p1.x;
        int deltaY = p2.y - p1.y;

        int yi = 1;

        if (deltaY < 0) {
            yi = -1;
            deltaY *= -1;
        }

        int D = 2 * deltaY - deltaX;
        int y = p1.y;

        for (int x = p1.x; x <= p2.x; x++) {
            
            // CALL SOMETHING HERE
            // Console.WriteLine($"x: {x}, y: {y}");
            plot((x, y));

            if (D > 0) {
                y += yi;
                D += 2 * (deltaY - deltaX);
            } else {
                D += 2 * deltaY;
            }
        }

    }
    
    /// <summary>
    /// generates a line along y-axis
    /// </summary>
    /// <param name="p1">2D coordinates of the first point</param>
    /// <param name="p2">2D coordinates of the second point</param>
    /// <param name="plot">action called when a new point of the line is generated</param>
    private static void generateByY((int x, int y) p1, (int x, int y) p2, Action<(int x, int y)> plot) {
        
        // 
        int deltaX = p2.x - p1.x;
        int deltaY = p2.y - p1.y;

        int xi = 1;

        if (deltaX < 0) {
            xi = -1;
            deltaX *= -1;
        }

        int D = 2 * deltaY - deltaX;
        int x = p1.x;

        for (int y = p1.y; y <= p2.y; y++) {
            
            // CALL SOMETHING HERE
            // Console.WriteLine($"x: {x}, y: {y}");
            plot((x, y));

            if (D > 0) {
                x += xi;
                D += 2 * (deltaX - deltaY);
            } else {
                D += 2 * deltaX;
            }
        }
    }
}