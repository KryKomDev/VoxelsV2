//
// VoxelsCoreSharp
// by KryKom 2024
//

// WARNING: DISCONTINUED, left here to add lines of code written

using VoxelsCoreSharp.maths;

namespace VoxelsCoreSharp.renderer;

public class Mesh2D {

    /// <summary>
    /// draws a triangle defined by the three points (p1, p2, p3)
    /// </summary>
    /// <param name="p1">first point</param>
    /// <param name="p2">second point</param>
    /// <param name="p3">third point</param>
    /// <param name="color">color in hexadecimal code</param>
    /// <param name="destination">the place where the triangle will be drawn, format MUST be the same as for Global.DISPLAY_COLORS</param>
    /// <seealso cref="Global.DISPLAY_COLORS"/>
    public static void drawTriangle((int x, int y) p1, (int x, int y) p2, (int x, int y) p3, int color, ref long[,] destination) { // TODO fix some coords issues

        // min and max height of the triangle
        int minY = Math.Min(p1.y, Math.Min(p2.y, p3.y));
        int maxY = Math.Max(p1.y, Math.Max(p2.y, p3.y));
        
        // height (along y-axis) of the triangle
        int height = maxY - minY + 1;
        
        // lowest and highest x for y
        // [y, 0] -> lowest x (most left)
        // [y, 1] -> highest x (most right)
        int?[,] points = new int?[height, 2];
        
        // adds point to points[,]
        Action<(int x, int y)> plot = p => {
            if (points[p.y - minY, 0] == null || points[p.y - minY, 0] > p.x) {
                points[p.y - minY, 0] = p.x;
            } 
            
            if (points[p.y - minY, 1] == null || points[p.y - minY, 1] < p.x) {
                points[p.y - minY, 1] = p.x;
            } 
        };  

        // generate edges of the triangle
        Bresenham2D.generateLine(p1, p2, plot);
        Bresenham2D.generateLine(p1, p3, plot);
        Bresenham2D.generateLine(p2, p3, plot);

        // fill the triangle and write it to the destination
        for (int y = 0; y < height; y++) {
            for (int? x = points[y, 0]; x <= points[y, 1]; x++) {
                destination[(int)x, y + minY] = color;
            }
        }
    }

    /// <summary>
    /// generates a convex polygon bounded by the points <br/>
    /// the order of the points does not depend as the algorithm will connect every point with every other
    /// </summary>
    /// <param name="points">array of points</param>
    /// <param name="color">color in hexadecimal code</param>
    /// <param name="destination">the place where the triangle will be drawn, format MUST be the same as for Global.DISPLAY_COLORS</param>
    /// <seealso cref="Global.DISPLAY_COLORS"/>
    public static void drawConvexPolygon((int x, int y)[] points, int color, ref long[,] destination) {
        
        // min and max height of the triangle
        int minY = Int32.MaxValue;
        int maxY = Int32.MinValue;

        // set the points
        for (int i = 0; i < points.Length; i++) {
            minY = minY > points[i].y ? points[i].y : minY;
            maxY = maxY < points[i].y ? points[i].y : maxY;
        }
        
        // height (along y-axis) of the triangle
        int height = maxY - minY + 1;
        
        // lowest and highest x for y
        // [y, 0] -> lowest x (most left)
        // [y, 1] -> highest x (most right)
        int?[,] edges = new int?[height, 2];
        
        // adds point to points[,]
        Action<(int x, int y)> plot = p => {
            if (edges[p.y - minY, 0] == null || edges[p.y - minY, 0] > p.x) {
                edges[p.y - minY, 0] = p.x;
            } 
            
            if (edges[p.y - minY, 1] == null || edges[p.y - minY, 1] < p.x) {
                edges[p.y - minY, 1] = p.x;
            }
        };  

        // generate edges of the triangle
        for (int i = 0; i < points.Length; i++) {
            for (int j = i + 1; j < points.Length; j++) {
                Bresenham2D.generateLine(points[i], points[j], plot);
            }
        }

        // fill the triangle and write it to the destination
        for (int y = 0; y < height; y++) {
            for (int? x = edges[y, 0]; x <= edges[y, 1]; x++) {
                destination[(int)x, y + minY] = color;
            }
        }
    }
    
    /// <summary>
    /// generates a polygon bounded by the points <br/>
    /// the order of the points depends as the lines will be generated from nth to n+1th point, so the shape can easily break
    /// </summary>
    /// <param name="points">array of points</param>
    /// <param name="color">color in hexadecimal code</param>
    /// <param name="destination">the place where the triangle will be drawn, format MUST be the same as for Global.DISPLAY_COLORS</param>
    /// <seealso cref="Global.DISPLAY_COLORS"/>
    public static void drawFastPolygon((int x, int y)[] points, int color, ref long[,] destination) {
        
        // min and max height of the triangle
        int minY = Int32.MaxValue;
        int maxY = Int32.MinValue;

        // set the points
        for (int i = 0; i < points.Length; i++) {
            minY = minY > points[i].y ? points[i].y : minY;
            maxY = maxY < points[i].y ? points[i].y : maxY;
        }
        
        // height (along y-axis) of the triangle
        int height = maxY - minY + 1;
        
        // lowest and highest x for y
        // [y, 0] -> lowest x (most left)
        // [y, 1] -> highest x (most right)
        int?[,] edges = new int?[height, 2];
        
        // adds point to points[,]
        Action<(int x, int y)> plot = p => {
            if (edges[p.y - minY, 0] == null || edges[p.y - minY, 0] > p.x) {
                edges[p.y - minY, 0] = p.x;
            } 
            
            if (edges[p.y - minY, 1] == null || edges[p.y - minY, 1] < p.x) {
                edges[p.y - minY, 1] = p.x;
            }
        };  
        
        // generate edges
        for (int i = 0; i < points.Length - 1; i++) {
            Bresenham2D.generateLine(points[i], points[i + 1], plot);
        }

        // generate last line
        Bresenham2D.generateLine(points[0], points[points.Length - 1], plot); 
        
        // fill the triangle and write it to the destination
        for (int y = 0; y < height; y++) {
            for (int? x = edges[y, 0]; x <= edges[y, 1]; x++) {
                destination[(int)x, y + minY] = color;
            }
        }
    }
}