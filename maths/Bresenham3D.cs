//
// VoxelsCoreSharp
// by KryKom 2024
//

// Original

using VoxelsCoreSharp.util;

namespace VoxelsCoreSharp.maths;

public class Bresenham3D {

    /// <summary>
    /// generates a line from p1 to p2 using the Bresenham's line algorithm
    /// </summary>
    /// <param name="p1">three-dimensional coordinates of p1</param>
    /// <param name="p2">three-dimensional coordinates of p2</param>
    /// <param name="plot">action called when a new point of the line is called</param>
    public static void generateLine((int x, int y, int z) p1, (int x, int y, int z) p2, Action<(int x, int y, int z)> plot) {

        // compute the differences between the coordinates of the points
        int deltaX = Math.Abs(p1.x - p2.x);
        int deltaY = Math.Abs(p1.y - p2.y);
        int deltaZ = Math.Abs(p1.z - p2.z);
        
        // decide by which axis shall the line be computed
        // deltaX is largest -> compute by X-axis
        if (deltaY < deltaX && deltaZ < deltaX) { 

            // decide if x grows already or if the points have to be swapped
            if (p1.x < p2.x) {
                generateByX(p1, p2, true, plot);
            } else {
                generateByX(p2, p1, false, plot);
            }
        }
        // deltaY is largest -> compute by Y-axis
        else if (deltaX < deltaY && deltaZ < deltaY) { 
                
            // decide if y grows already or if the points have to be swapped
            if (p1.y < p2.y) {
                generateByY(p1, p2, true, plot);
            } else {
                generateByY(p2, p1, false, plot);
            }
            
        } 
        // deltaZ is largest -> compute by Z-axis
        else { 
            
            // decide if z grows already or if the points have to be swapped
            if (p1.z < p2.z) {
                generateByZ(p1, p2, true, plot);
            } else {
                generateByZ(p2, p1, false, plot);
            }
            
        }
    }
    

    /// <summary>
    /// computes line along x-axis
    /// </summary>
    /// <param name="p1">3D coordinate of the first point</param>
    /// <param name="p2">3D coordinate of the second point</param>
    /// <param name="srcFirst">determines if the first point is the source point</param>
    /// <param name="plot">function called when a new point of the line is called</param>
    private static void generateByX((int x, int y, int z) p1, (int x, int y, int z) p2, bool srcFirst, Action<(int x, int y, int z)> plot) {
        
        // debug
        ConsoleColors.printlnColoredTextHex("Generating by X", 0xe38100);
        Console.WriteLine($"src: {(srcFirst ? p1 : p2)}, to: {(!srcFirst ? p1 : p2)}");

        // calculate delta
        int deltaX = p2.x - p1.x;
        int deltaY = p2.y - p1.y;
        int deltaZ = p2.z - p1.z;

        // increment or decrement y
        int yi = 1;
        if (deltaY < 0) {
            yi = -1;
            deltaY *= -1;
        }
        
        // increment or decrement z
        int zi = 1;
        if (deltaZ < 0) {
            zi = -1;
            deltaZ *= -1;
        }

        // error decisions
        int Dy = 2 * deltaY - deltaX;
        int Dz = 2 * deltaZ - deltaX;
        
        // coords
        int x = srcFirst ? p1.x : p2.x;
        int y = srcFirst ? p1.y : p2.y;
        int z = srcFirst ? p1.z : p2.z;

        
        for (int xi = p1.x; xi <= p2.x; xi++) {
            
            // CALL SOMETHING HERE
            // Console.WriteLine($"x: {x}, y: {y}, z: {z}");
            plot((x, y, z));
            
            // compute error and somehow minimize it
            if (Dy > 0) {
                y += srcFirst ? yi : -yi;
                Dy += 2 * (deltaY - deltaX);
            } else {
                Dy += 2 * deltaY;
            }
            
            if (Dz > 0) {
                z += srcFirst ? zi : -zi;
                Dz += 2 * (deltaZ - deltaX);
            } else {
                Dz += 2 * deltaZ;
            }
            
            // update x
            x += srcFirst ? 1 : -1;
        }

    }
    
    /// <summary>
    /// computes line along y-axis
    /// </summary>
    /// <param name="p1">3D coordinate of the first point</param>
    /// <param name="p2">3D coordinate of the second point</param>
    /// <param name="srcFirst">determines if the first point is the source point</param>
    /// <param name="plot">function called when a new point of the line is called</param>
    private static void generateByY((int x, int y, int z) p1, (int x, int y, int z) p2, bool srcFirst, Action<(int x, int y, int z)> plot) {
        
        ConsoleColors.printlnColoredTextHex("Generating by Y", 0x03bd00);
        Console.WriteLine($"src: {(srcFirst ? p1 : p2)}, to: {(!srcFirst ? p1 : p2)}");
        
        int deltaX = p2.x - p1.x;
        int deltaY = p2.y - p1.y;
        int deltaZ = p2.z - p1.z;

        int xi = 1;
        if (deltaX < 0) {
            xi = -1;
            deltaX *= -1;
        }
        
        int zi = 1;
        if (deltaZ < 0) {
            zi = -1;
            deltaZ *= -1;
        }

        int Dy = 2 * deltaY - deltaX;
        int Dz = 2 * deltaZ - deltaX;
        
        // coords
        int x = srcFirst ? p1.x : p2.x;
        int y = srcFirst ? p1.y : p2.y;
        int z = srcFirst ? p1.z : p2.z;

        for (int yi = p1.y; yi <= p2.y; yi++) {

            // CALL SOMETHING HERE
            // Console.WriteLine($"x: {x}, y: {y}, z: {z}");
            plot((x, y, z));
            
            if (Dy > 0) {
                x += srcFirst ? xi : -xi;
                Dy += 2 * (deltaX - deltaY);
            } else {
                Dy += 2 * deltaX;
            }
            
            if (Dz > 0) {
                z += srcFirst ? zi : -zi;
                Dz += 2 * (deltaZ - deltaY);
            } else {
                Dz += 2 * deltaZ;
            }
            
            y += srcFirst ? 1 : -1;
        }
    }

    /// <summary>
    /// computes line along z-axis
    /// </summary>
    /// <param name="p1">3D coordinate of the first point</param>
    /// <param name="p2">3D coordinate of the second point</param>
    /// <param name="srcFirst">determines if the first point is the source point</param>
    /// <param name="plot">function called when a new point of the line is called</param>
    private static void generateByZ((int x, int y, int z) p1, (int x, int y, int z) p2, bool srcFirst, Action<(int x, int y, int z)> plot) {
        
        ConsoleColors.printlnColoredTextHex("Generating by Z", 0x0388fc);
        Console.WriteLine($"src: {(srcFirst ? p1 : p2)}, to: {(!srcFirst ? p1 : p2)}");

        int deltaX = p2.x - p1.x;
        int deltaY = p2.y - p1.y;
        int deltaZ = p2.z - p1.z;

        int xi = 1;
        if (deltaZ < 0) {
            xi = -1;
            deltaZ *= -1;
        }
        
        int yi = 1;
        if (deltaZ < 0) {
            yi = -1;
            deltaZ *= -1;
        }

        int Dx = 2 * deltaY - deltaX;
        int Dy = 2 * deltaZ - deltaX;
        
        // coords
        int x = srcFirst ? p1.x : p2.x;
        int y = srcFirst ? p1.y : p2.y;
        int z = srcFirst ? p1.z : p2.z;
        
        for (int zi = p1.z; zi <= p2.z; zi++) {

            // CALL SOMETHING HERE
            // Console.WriteLine($"x: {x}, y: {y}, z: {z}");
            plot((x, y, z));

            if (Dx > 0) {
                x += srcFirst ? xi : -xi;
                Dx += 2 * (deltaX - deltaZ);
            } else {
                Dx += 2 * deltaX;
            }
            
            if (Dy > 0) {
                y += srcFirst ? yi : -yi;
                Dy += 2 * (deltaY - deltaZ);
            } else {
                Dy += 2 * deltaY;
            }

            z += srcFirst ? 1 : -1;
        }
    }
}