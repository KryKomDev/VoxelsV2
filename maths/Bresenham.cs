//
// VoxelsCoreSharp
// by KryKom 2024
//

// This is my implementation based on the wikipedia pseudocode

namespace VoxelsCoreSharp.maths;

public class Bresenham {

    public static int generateLine((int x, int y) p1, (int x, int y) p2) {

        if (Math.Abs(p1.y - p2.y) < Math.Abs(p1.x - p2.x)) {

            if (p1.x < p2.x) {
                generateLow(p1, p2);
            } else {
                generateLow(p2, p1);
            }
                
        } else {
                
            if (p1.y < p2.y) {
                generateHigh(p1, p2);
            } else {
                generateHigh(p2, p1);
            }
        }
        
        return 0;
    }
    

    private static void generateLow((int x, int y) p1, (int x, int y) p2) {

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
            Console.WriteLine($"x: {x}, y: {y}");

            if (D > 0) {
                y += yi;
                D += 2 * (deltaY - deltaX);
            } else {
                D += 2 * deltaY;
            }
        }

    }
    
    private static void generateHigh((int x, int y) p1, (int x, int y) p2) {
        
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
            Console.WriteLine($"x: {x}, y: {y}");

            if (D > 0) {
                x += xi;
                D += 2 * (deltaX - deltaY);
            } else {
                D += 2 * deltaX;
            }
        }
    }
}