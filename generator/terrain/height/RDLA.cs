//
// VoxelsCore
// by KryKom 2024
//


using System.Data;
using System.Security.Cryptography.X509Certificates;
using Kolors;

namespace VoxelsCoreSharp.generator.terrain.height;


/// <summary>
/// Reverse Diffusion Limited Aggregation terrain generating algorithm
/// </summary>
public class RDLA {
    
    // generation pipeline:
    // 1. 'generationMap' and 'heightMap' are rescaled so that their dimensions are easily divided
    //    and multiplied back to the original state
    // 2. initial point/points is/are added to List<Point> 'generationPoints'
    // 3. for each Point 'p' in 'generationPoints' create new points, to which, set their 'sourceDirection',
    //    add them to List<Point> 'newPoints', and connect them with 'p'
    // 4. add values of points from 'newPoints' to the 'generationMap'
    // 5. add 'generationPoints' to 'points' and set 'generationPoints' to be 'newPoints'
    // 6. repeat steps 3. to 5. until 'generationMap' is filled enough
    // 7. blur the 'generationMap' and add that to the 'heightMap'
    // 8. enlarge the 'generationMap' and the 'HeightMap' 2X by using the List<Point> 'points' and their connections
    // 9. repeat steps 6. to 8. until back at the original size
    

    /// <summary>
    ///  
    /// </summary>
    /// <param name="mask"></param>
    /// <param name="transform"></param>
    /// <param name="iterations"></param>
    /// <param name="initialPoints"></param>
    /// <returns></returns>
    public static double?[,] generate(
            ref byte[,] mask, Func<double, double>? transform = null, List<(int x, int y)>? initialPoints = null) 
    {

        Map map = new Map(ref mask, transform, initialPoints);

        for (int i = 20 - 1; i >= 0; i--) {
            map.generate(i);
        }
        
        return map.generationMap;
    } 
    

    private class Map {
        
        /// <summary>
        /// final height map
        /// </summary>
        public double?[,] heightMap { get; private set; }
        
        /// <summary>
        /// generation helper map
        /// </summary>
        public double?[,] generationMap { get; private set; }
        
        /// <summary>
        /// where the height map can be generated, 0 -> no, 1 -> yes
        /// </summary>
        public byte[,] mask { get; private set; }
        
        /// <summary>
        /// list of points that create the mountain
        /// </summary>
        public List<Point> points { get; private set; } = new();

        /// <summary>
        /// list of points that will be used to generate new points 
        /// </summary>
        public List<Point> generationPoints { get; private set; } = new();
        
        /// <summary>
        /// transformation formula for adding the generation map to the <see cref="heightMap"/>
        /// </summary>
        public Func<double, double> transform { get; private set; }

        /// <summary>
        /// global Random instance
        /// </summary>
        private readonly Random random = new((int)Global.Generator.getUniqueSeed("RDLA"));
        
        private int originalXSize { get; }
        private int originalYSize { get; set; }
        private int xSize { get; }
        private int ySize { get; set; }

        /// <summary>
        /// how much are the 'generationMap' and the 'heightMap' scaled down
        /// </summary>
        private int currentScale { get; set; } = (int)Math.Pow(2, ITERATIONS);

        private const int ITERATIONS = 4;                                                                               // TODO update this to 4 after testing
        private const int NEW_POINT_SPAWN_CHANCE = 25; 
        
        
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mask">where the height map can be generated, 0 -> no, 1 -> yes</param>
        /// <param name="transform"></param>
        /// <param name="initialPoints"></param>
        public Map(ref byte[,] mask, Func<double, double>? transform, List<(int x, int y)>? initialPoints) {

            // set original size field
            originalXSize = mask.GetLength(0);
            originalYSize = mask.GetLength(1);

            // set ideal sizes for the 'generationMap' and 'heightMap' the first iteration
            (xSize, ySize) = findSize(originalXSize, originalYSize);
            
            // set 'mask'
            this.mask = mask;
            
            // set 'transform'
            this.transform = transform ?? (i => i is not -1 ? (1 - i / 1 + i) : 0);
            
            // mask 'generationMap' and 'heightMap'
            heightMap = maskMap();
            generationMap = maskMap();

            // set first points
            if (initialPoints == null || initialPoints.Count == 0) {
                Point p = new Point(findFirstPoint(), 1, null);
                generationPoints.Add(p);
            }
            else {
                foreach (var pos in initialPoints) {
                    Point p = new Point(pos, 1, null);
                    generationPoints.Add(p);
                }
            }
        }


        /// <summary>
        /// finds a size that can be easily scaled down and up again
        /// </summary>
        /// <param name="xSize">original x-size</param>
        /// <param name="ySize">original y-size</param>
        /// <returns>(new x-size, new y-size)</returns>
        private static (int xSize, int ySize) findSize(int xSize, int ySize) {
            (int xSize, int ySize) dimensions = (256, 256);

            while (dimensions.xSize < xSize) {
                dimensions.xSize += 256;
            }

            while (dimensions.ySize < ySize) {
                dimensions.ySize += 265;
            }

            dimensions.xSize /= (int)Math.Pow(2, ITERATIONS);
            dimensions.ySize /= (int)Math.Pow(2, ITERATIONS);
            
            return dimensions;
        }


        /// <summary>
        /// finds the ideal position for the first generation point
        /// </summary>
        /// <returns>x and y position of the point</returns>
        /// <exception cref="ConstraintException">if no valid generation field was found</exception>
        private (int x, int y) findFirstPoint() {
            (long x, long y) sum = (0, 0);
            int count = 0;

            for (int x = 0; x < xSize; x++) {
                for (int y = 0; y < ySize; y++) {
                    if (generationMap[x, y] == null) continue;
                    
                    sum.x += x;
                    sum.y += y;
                    count++;
                }
            }

            if (count == 0) {
                Debug.error("Could not run ReverseDLA terrain generation, because no valid generation field was found!");
                throw new ConstraintException();
            }

            return ((int x, int y))(sum.x / count, sum.y / count);
        }


        /// <summary>
        /// masks a map using the 'mask' field
        /// </summary>
        private double?[,] maskMap() {
            var map = new double?[xSize, ySize];

            for (int x = 0; x < xSize; x++) {
                for (int y = 0; y < ySize; y++) {
                    map[x, y] = (x * currentScale >= originalXSize || y * currentScale >= originalYSize || 
                                 mask[x * currentScale, y * currentScale] == 0) ? 
                        null : 0;
                }
            }

            return map;
        }

        
        /// <summary>
        /// the main generation method
        /// </summary>
        /// <param name="iteration">currently running iteration number</param>
        public void generate(int iteration) {
            // 1. go through the 'generationPoints' list and generate new points and connections
            // 2. if no new points weren't generated try again
            // 3. place the points from 'generationPoints' on the 'generationMap' 
            // 4. add new points to 'points' list
            // 5. move points from 'generationPoints' to 'points' and move the new points to the 'generationPoints' list
            // 6. if this iteration is the last, add all points from 'newPoints' to the 'generationMap'

            // 1. go through the 'generationPoints' list and generate new points and connections
            List<Point> newPoints = new List<Point>();

            foreach (var gp in generationPoints) {
                
                // for each direction
                for (int direction = 0; direction < 8; direction++) {
                    
                    // if it does not point to the source point of 'gp' and this is randomly picked 
                    if (gp.sourceDirection != direction && random.Next(1, 100) <= NEW_POINT_SPAWN_CHANCE &&
                            // obstruction level growth limitation
                            sum(gp.x, gp.y, 7) < random.Next(1, 49)) {
                        
                        // add the connection
                        gp.setConnection(direction, true);
                        
                        // create new point
                        Point p = new Point(
                            getConnectionPosition(gp.x, gp.y, direction), 
                            transform(iteration), 
                            (byte?)((direction + 4) % 8)
                        );
                        
                        // and add it
                        newPoints.Add(p);
                    }
                }
            }

            // 2. if no new points weren't generated try again                                                          // TODO fix all filled problem
            if (newPoints.Count == 0) generate(iteration);
            
            // 3. place the points from 'generationPoints' on the 'generationMap' 
            foreach (var p in generationPoints) generationMap[p.x, p.y] = p.value;
            
            // 4. add new points to 'points' list
            points.AddRange(newPoints);

            // 5. move points from 'generationPoints' to 'points' and move the new points to the 'generationPoints' list
            generationPoints = newPoints;

            // 6. if this iteration is the last, add all points from 'newPoints' to the 'generationMap'
            if (iteration <= 0)
                foreach (var p in newPoints) generationMap[p.x, p.y] = p.value;
        }


        /// <summary>
        /// calculates how many points are located in the generation map in a 5x5 area around a point 
        /// </summary>
        /// <param name="x">x position of the point</param>
        /// <param name="y">y position of the point</param>
        /// <returns>how many points are present</returns>
        private int sum(int x, int y, int size) {

            int sum = 0;
            
            size = size % 2 == 0 ? size + 1 : size;
            int hs = size / 2;
            
            for (int xi = -hs; xi <= hs; xi++) {
                for (int yi = -hs; yi <= hs; yi++) {
                    if (xi + x >= 0 && yi + y >= 0 && xi + x < xSize && yi + y < ySize && 
                        generationMap[xi + x, yi + y] is not 0 and not null) {
                        sum++;
                    }  
                }
            }

            return sum;
        }

        
        /// <summary>
        /// calculates a new position from position of the original point and a direction
        /// </summary>
        /// <param name="x">x position of original point</param>
        /// <param name="y">y position of original point</param>
        /// <param name="direction">relative direction from the original point to the new point</param>
        /// <returns></returns>
        private static (int x, int y) getConnectionPosition(int x, int y, int direction) {
            switch (direction) {
                case 0: return (x - 1, y);       // left
                case 1: return (x - 1, y + 1); // left-up
                case 2: return (x, y + 1);       // up
                case 3: return (x + 1, y + 1); // up-right
                case 4: return (x + 1, y);       // right
                case 5: return (x + 1, y - 1); // right-down
                case 6: return (x, y - 1);       // down
                case 7: return (x - 1, y - 1); // down-left
                default:
                    Debug.error($"Incorrect direction inputted for point at [{x}, {y}]");
                    return getConnectionPosition(x, y, direction % 8);
            }
        }


        public void upscale() {
            // 
        }
    }
    
    
    /// <summary>
    /// helper point class / struct
    /// </summary>
    private class Point(int x, int y, double value, byte? sourceDirection) {

        public int x { get; set; } = x;
        public int y { get; set; } = y;
        public double value { get; private set; } = value;
        
        /// <summary>
        /// which points are connected <br/>
        /// +-----+-----------+     <br/>
        /// | bit | direction |     <br/>
        /// +-----+-----------+     <br/>
        /// |  0  | left      |     <br/>
        /// |  1  | left-up   |     <br/>
        /// |  2  | up        |     <br/>
        /// |  3  | up-right  |     <br/>
        /// |  4  | right     |     <br/>
        /// |  5  | right-down|     <br/>
        /// |  6  | down      |     <br/>
        /// |  7  | down-left |     <br/>
        /// +-----+-----------+
        /// </summary>
        private byte connections { get; set; }

        /// <summary>
        /// points to the source point, from which this point was generated
        /// </summary>
        public byte? sourceDirection { get; private set; } = sourceDirection;

        
        /// <summary>
        /// helper point class / struct
        /// </summary>
        public Point((int x, int y) position, double value, byte? sourceDirection) : 
            this(position.x, position.y, value, sourceDirection) { }

        
        /// <summary>
        /// returns the state of a connection of the point in a direction
        /// </summary>
        /// <param name="direction">direction of the connection, see <see cref="connections"/> for more info</param>
        /// <returns>state of the connection, false if the direction arg is &lt;0 or >7 (invalid)</returns>
        public bool isConnected(int direction) {
            if (direction is >= 0 and <= 7) 
                return (connections & (0b1 << direction)) == 0b1 << direction;
            
            Debug.error($"Incorrect direction inputted for point at [{x}, {y}]");
            return false;
        }

        
        /// <summary>
        /// sets the state of a connection of the point in a direction
        /// </summary>
        /// <param name="direction">direction of the connection, see <see cref="connections"/> for more info</param>
        /// <param name="state">state of the connection, true -> connected, false -> not connected</param>
        public void setConnection(int direction, bool state) {
            if (direction is >= 0 and <= 7) 
                if (state) 
                    connections |= (byte) (0b1 << direction);
                else 
                    connections &= (byte)~(0b1 << direction);
            else Debug.error($"Incorrect direction inputted for point at [{x}, {y}]");
        }
    }
}