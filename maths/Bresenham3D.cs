//
// VoxelsCoreSharp
// by KryKom 2024
//

// Original

namespace VoxelsCoreSharp.maths;

public class Bresenham3D {

    public static int generateLine((int x, int y, int z) p1, (int x, int y, int z) p2) {

        int deltaX = Math.Abs(p1.x - p2.x);
        int deltaY = Math.Abs(p1.y - p2.y);
        int deltaZ = Math.Abs(p1.z - p2.z);
        
        if (deltaY < deltaX && deltaZ < deltaX) { // DeltaX is largest

            if (p1.x < p2.x) {
                generateByX(p1, p2);
            } else {
                generateByX(p2, p1);
            }
            
        
        } else if (deltaX < deltaY && deltaZ < deltaY) { // DeltaY is largest
                
            if (p1.y < p2.y) {
                generateByY(p1, p2);
            } else {
                generateByY(p2, p1);
            }
            
        } else { // DeltaZ is largest
            
            if (p1.z < p2.z) {
                generateByZ(p1, p2);
            } else {
                generateByZ(p2, p1);
            }
            
        }
        
        return 0;
    }
    

    private static void generateByX((int x, int y, int z) p1, (int x, int y, int z) p2) {

        int deltaX = p2.x - p1.x;
        int deltaY = p2.y - p1.y;
        int deltaZ = p2.z - p1.z;

        // idk what, cant read it
        int yi = 1;
        if (deltaY < 0) {
            yi = -1;
            deltaY *= -1;
        }
        
        int zi = 1;
        if (deltaZ < 0) {
            zi = -1;
            deltaZ *= -1;
        }

        // error decisions
        int Dy = 2 * deltaY - deltaX;
        int Dz = 2 * deltaZ - deltaX;
        
        // coords
        int y = p1.y;
        int z = p1.z;

        for (int x = p1.x; x <= p2.x; x++) {
            
            // call something here
            Console.WriteLine($"x: {x}, y: {y}, z: {z}");
            
            
            // compute error and somehow minimize it
            if (Dy > 0) {
                y += yi;
                Dy += 2 * (deltaY - deltaX);
            } else {
                Dy += 2 * deltaY;
            }
            
            if (Dz > 0) {
                z += zi;
                Dz += 2 * (deltaZ - deltaX);
            } else {
                Dz += 2 * deltaZ;
            }
        }

    }
    
    private static void generateByY((int x, int y, int z) p1, (int x, int y, int z) p2) {
        
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
        int x = p1.x;
        int z = p1.z;

        for (int y = p1.y; y <= p2.y; y++) {

            // call something here
            Console.WriteLine($"x: {x}, y: {y}, z: {z}");

            if (Dy > 0) {
                x += xi;
                Dy += 2 * (deltaX - deltaY);
            } else {
                Dy += 2 * deltaX;
            }
            
            if (Dz > 0) {
                z += zi;
                Dz += 2 * (deltaZ - deltaY);
            } else {
                Dz += 2 * deltaZ;
            }
        }
    }

    private static void generateByZ((int x, int y, int z) p1, (int x, int y, int z) p2) {

        int deltaX = p2.x - p1.x;
        int deltaY = p2.y - p1.y;
        int deltaZ = p2.z - p1.z;

        int xi = 1;

        if (deltaZ < 0) {
            xi = -1;
            deltaZ *= -1;
        }
        int zi = 1;

        if (deltaZ < 0) {
            zi = -1;
            deltaZ *= -1;
        }

        int Dx = 2 * deltaY - deltaX;
        int Dy = 2 * deltaZ - deltaX;
        int x = p1.x;
        int y = p1.y;

        for (int z = p1.z; z <= p2.z; z++) {

            // call something here
            Console.WriteLine($"x: {x}, y: {y}, z: {z}");

            if (Dx > 0) {
                x += xi;
                Dx += 2 * (deltaX - deltaZ);
            } else {
                Dx += 2 * deltaX;
            }
            
            if (Dy > 0) {
                y += zi;
                Dy += 2 * (deltaY - deltaZ);
            } else {
                Dy += 2 * deltaY;
            }
        }
    }
}