//
// VoxelsCoreSharp
// by KryKom 2024
//

using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp.util;

/// <summary>
/// contains global variables and settings
/// </summary>
public static class Global {

    /// <summary>
    /// width of the rendered area in pixels
    /// </summary>
    public static uint DISPLAY_WIDTH = 1920;
    
    /// <summary>
    /// height of the rendered area in pixels
    /// </summary>
    public static uint DISPLAY_HEIGHT = 1080;
    
    /// <summary>
    /// color and depth map of rendered pixels<br/>
    /// first (least significant) byte is for red channel,<br/>
    /// second byte is for green channel,<br/>
    /// third byte for blue,<br/>
    /// 5 remaining bytes are for depth<br/>
    /// please use bit operations to get or set any value
    /// </summary>
    public static long[,] DISPLAY_COLORS = new long[DISPLAY_WIDTH, DISPLAY_HEIGHT];

    /// <summary>
    /// field of view in degrees
    /// defines the size of the rendered area
    /// </summary>
    public static int FOV = 70;

    /// <summary>
    /// position of the player / camera<br/>
    /// x-axis: north / south<br/>
    /// y-axis: east / west<br/>
    /// z-axis: up / down
    /// </summary>
    public static (long x, long y, long z) PLAYER_POS = (0, 0, 0);

    /// <summary>
    /// height limit in subchunk size (16)
    /// </summary>
    public static uint HEIGHT_LIMIT = 16;

    /// <summary>
    /// size of a chunk in voxels
    /// </summary>
    public static ushort CHUNK_SIZE = 16;

    /// <summary>
    /// size of a region in chunks
    /// </summary>
    public static ushort REGION_SIZE = 16;

    /// <summary>
    /// size of the world in regions
    /// </summary>
    public static uint WORLD_SIZE = 64;

    /// <summary>
    /// dimensions of biomes; 2d -> false, 3d -> true
    /// </summary>
    public static bool BIOME_DIMENSION = true;

    /// <summary>
    /// global world manager
    /// </summary>
    public static WorldManager WORLD_MANAGER;

    /// <summary>
    /// sets up the global WorldManager (Global.WORLD_MANAGER)
    /// </summary>
    /// <param name="worldFilePath"></param>
    public static void setupWorldManager(string worldFilePath) {
        WORLD_MANAGER = new WorldManager(worldFilePath);
    }

    /// <summary>
    /// binary size of a region in vxw files in bytes
    /// </summary>
    public static int BINARY_CHUNK_SIZE = (int)((CHUNK_SIZE * CHUNK_SIZE * CHUNK_SIZE + 1) * HEIGHT_LIMIT + 2);

    /// <summary>
    /// binary size of a chunk in vxw files in bytes
    /// </summary>
    public static int BINARY_REGION_SIZE = REGION_SIZE * REGION_SIZE * BINARY_CHUNK_SIZE;
    
    /// <summary>
    /// binary size of a header in vxw files in bytes
    /// </summary>
    public const int BINARY_HEADER_SIZE = 16;

    /// <summary>
    /// updates the global binary sizes of chunks and regions
    /// </summary>
    private static void updateBinarySizes() {
        BINARY_CHUNK_SIZE = (int)((CHUNK_SIZE * CHUNK_SIZE * CHUNK_SIZE + 1) * HEIGHT_LIMIT + 2);
        BINARY_REGION_SIZE = REGION_SIZE * REGION_SIZE * BINARY_CHUNK_SIZE;
    }

    /// <summary>
    /// maximal horizontal position of player <br/>
    /// used for chunk loading limitation
    /// </summary>
    public static long MAX_HORIZONTAL_POS = WORLD_SIZE / 2 * REGION_SIZE * CHUNK_SIZE + CHUNK_SIZE / 2;

    /// <summary>
    /// updates precalculated variables
    /// </summary>
    public static void updatePrecalculatedVariables() {
        MAX_HORIZONTAL_POS = WORLD_SIZE / 2 * REGION_SIZE * CHUNK_SIZE + CHUNK_SIZE / 2;
        
        updateBinarySizes();
    }
    
    
}