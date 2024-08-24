//
// VoxelsCore
// by KryKom 2024
//

// This is my original terrain generation algorithm

using Kolors;
using VoxelsCoreSharp.test.gen;

namespace VoxelsCoreSharp.generator.terrain.height;

/// <summary>
/// Tree Growth Limited Generation for height maps, derived from DLA
/// </summary>
public class TGLG {
    
    public static int?[,] generate(byte[,] bitMask, int baseHeight, int? iterations = null, List<(int x, int y)>? initialPoints = null, Func<double, double>? transform = null, Func<double?, int?>? finalTransform = null) {

        Map map = new Map(bitMask, baseHeight, initialPoints, finalTransform, transform);

        if (iterations != null) {
            for (int i = 0; i < iterations; i++) {
                map.generate();
            }
        }
        else {
            while (!isFilledEnough(map.transformMap)) {
                map.generate();
            }
        }
        
        map.finalize();
        
        return map.heightMap;
    }

    private const int MIN_FILLED_PERCENTAGE = 50;

    private static bool isFilledEnough(double?[,] heightMap) {
        int filled = 0;
        int total = 0;

        foreach (var v in heightMap) {
            if (v is not 0 and not null) {
                total++;
                filled++;
            }
            else if (v is not null) {
                total++;
            }
        }

        if (total == 0) {
            Debug.error("Could not run TGLG because an in-bounds field was not found!");
            return false;
        }
        
        return filled / total * 100 >= MIN_FILLED_PERCENTAGE;
    }

    private class Map {
        public int?[,] heightMap { get; private set; }
        public byte?[,] generationMap { get; private set; }
        public double?[,] transformMap { get; private set; }
        private List<(int x, int y, int value)> possiblePoints { get; set; } = new();
        private Func<double?, int?> finalTransform { get; set; }
        private Func<double, double> transform { get; set; }
        public int xSize { get; private set; }
        public int ySize { get; private set; }
        private Random random = new((int)Global.Generator.getUniqueSeed("TGLG"));


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bitMask">mask of where the height map can and cannot be generated, 0 -> out of bounds, 1 -> in bounds</param>
        /// <param name="baseHeight">height the base terrain</param>
        /// <param name="initialPoints">points that will be used in the generation process</param>
        /// <param name="finalTransform">formula for finalization</param>
        /// <param name="transform">formula used in generation</param>
        public Map(byte[,] bitMask, int baseHeight, List<(int x, int y)>? initialPoints = null, Func<double?, int?>? finalTransform = null, Func<double, double>? transform = null) {

            xSize = bitMask.GetLength(0);
            ySize = bitMask.GetLength(1);
            heightMap = new int?[xSize, ySize];
            generationMap = new byte?[xSize, ySize];
            transformMap = new double?[xSize, ySize];
            
            for (int x = 0; x < xSize; x++) {
                for (int y = 0; y < ySize; y++) {
                    transformMap[x, y] = generationMap[x, y] = bitMask[x, y] == 0 ? null : 0;
                    heightMap[x, y] = bitMask[x, y] == 0 ? null : baseHeight;
                }
            }

            // this.transform = i => 1 - 1 / (1 + i);
            this.finalTransform = finalTransform ?? (i => i is not null ? (int)(i * 1 / 2) : null);
            this.transform = transform ?? (i => 1 - 1 / (1 + i));

            if (initialPoints == null) {
                autoSetFirstPoint();
            }
            else foreach (var p in initialPoints) {
                generationMap[p.x, p.y] = 1;
            }
        }

        public void generate() {
            // 1. get possible points
            // 2. choose some 
            // 3. add them to genMap
            // 4. add blurred genMap to the heightMap using the transform function
            
            // 0. clear the possiblePoints list
            possiblePoints = new List<(int x, int y, int value)>();
            
            // 1. get possible points
            addPossiblePoints();
            
            // 2. choose some
            foreach (var point in possiblePoints) {
                
                // 2.1 if value of point + random(-64, 256) >= 192 choose it
                if (point.value + random.Next(-64, 256) >= 192) {
                    
                    // 3. add them to genMap
                    generationMap[point.x, point.y] = (byte?)point.value;
                }
            }
            
            for (int x = 0; x < xSize; x++) {
                for (int y = 0; y < ySize; y++) {
                    transformMap[x, y] = transformMap[x, y] != null ? transform(blur(x, y)) + transformMap[x, y] : null;
                }
            }
        }

        /// <summary>
        /// finishes the generation by switching to heightMap from transformMap
        /// </summary>
        public void finalize() {
            for (int x = 0; x < xSize; x++) {
                for (int y = 0; y < ySize; y++) {
                    heightMap[x, y] = transformMap[x, y] != null ? (finalTransform(transformMap[x, y])) : null;
                }
            }
        }

        private void addPossiblePoints() {
            // 1. go through the generation map and create a list of all points that have a neighbour, tf that's all?

            for (int x = 0; x < xSize; x++) {
                for (int y = 0; y < ySize; y++) {
                    (bool state, int value) cell = hasPointNeighbour(x, y);
                    if (generationMap[x, y] != null && cell.state) {
                        possiblePoints.Add((x, y, cell.value));
                    }
                }
            }
        }

        private (bool state, int value) hasPointNeighbour(int x, int y) {
            if (x > 0 && generationMap[x - 1, y] is not 0 and not null) {
                return (true, Math.Min((int)(generationMap[x - 1, y]! + random.Next(1, 5)), 255));
            }
            else if (x < xSize - 1 && generationMap[x + 1, y] is not 0 and not null) {
                return (true, Math.Min((int)(generationMap[x + 1, y]! + random.Next(1, 5)), 255));
            }
            else if (y > 0 && generationMap[x, y - 1] is not 0 and not null) {
                return (true, Math.Min((int)(generationMap[x, y - 1]! + random.Next(1, 5)), 255));
            }
            else if (y < ySize - 1 && generationMap[x, y + 1] is not 0 and not null) {
                return (true, Math.Min((int)(generationMap[x, y + 1]! + random.Next(1, 5)), 255));
            }
            else return (false, 0);
        }

        
        private static readonly int[,] MASK = {{0, 1, 1, 1, 0}, {1, 1, 1, 1, 1}, {1, 1, 1, 1, 2}, {1, 1, 1, 1, 1}, {0, 1, 1, 1, 0}};
        
        
        private double blur(int x, int y) {
            int sum = 0;
            int count = 0;
            
            for (int xi = x - 2; xi < x + 2; xi++) {
                for (int yi = y - 2; yi < y + 2; yi++) {
                    if (xi >= 0 && yi >= 0 && xi < xSize && yi < ySize && generationMap[xi, yi] is not 0 and not null) {
                        count++;
                        sum++;
                    } 
                    else if (xi >= 0 && yi >= 0 && xi < xSize && yi < ySize && generationMap[xi, yi] is not null) {
                        count++;
                    }
                }
            }

            return count != 0 ? sum / (double)count : 0;
        }

        private void autoSetFirstPoint() {
            (long x, long y) sum = (0, 0);
            int count = 0;

            for (int x = 0; x < xSize; x++) {
                for (int y = 0; y < ySize; y++) {
                    if (heightMap[x, y] != null) {
                        count++;
                        sum.x += x;
                        sum.y += y;
                    }
                }
            }

            if (count == 0) {
                Debug.error("Could not auto-set first point in TGLG because not a single in-bounds field was found!");
                return;
            }

            Console.WriteLine($"[{sum.x / count}; {sum.y / count}]");
            generationMap[sum.x / count, sum.y / count] = 1;
        }
    }

    private class Point {
        public int x { get; set; }
        public int y { get; set; }
        
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
        public byte connections { get; set; }
        public bool closed { get; set; }

        public bool connectedWith(int direction) {
            if (direction is < 0 or > 7) {
                Debug.error($"Incorrect direction inputted for point at [{x}, {y}]");
                return false;
            }
            
            return (connections & (0b1 << direction)) == 0b1 << direction;
        }
    }
}