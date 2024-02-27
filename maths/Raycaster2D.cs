//
// VoxelsCoreSharp
// by KryKom 2024
//

namespace VoxelsCoreSharp.maths;

public class Raycaster2D {
    private static int[,] field = new int[10, 10];

    public static void setField(int[,] field) {
        Raycaster2D.field = Raycaster2D.field;
    }

    public static (double x, double y) pointsToPath ((int x, int y) from, (int x, int y) to, int length) {

        int deltaX = (from.x - to.x) * -1;
        int deltaY = (from.y - to.y) * -1;

        // length between the points
        double c = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
        
        double ratio = length / c;

        double x = ratio * deltaX + from.x;
        double y = ratio * deltaY + from.y;
        
        return (x, y);
    }
}