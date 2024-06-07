//
// VoxelsCoreSharp
// by KryKom 2024
//

using VoxelsCoreSharp.console;
using VoxelsCoreSharp.console.command;
using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp;

/// <summary>
/// contains global variables and settings
/// </summary>
public static class Global {

    /// <summary>
    /// static constructor, called only once
    /// </summary>
    static Global() {
        registerCommands();
    }

    /// <summary>
    /// width of the rendered area in pixels
    /// </summary>
    public static uint DISPLAY_WIDTH = 1920;
    
    /// <summary>
    /// height of the rendered area in pixels
    /// </summary>
    public static uint DISPLAY_HEIGHT = 1080;
    
    /// <summary>
    /// color of rendered pixels<br/>
    /// format: 0x0rgb
    /// please use bit operations to get or set any value
    /// </summary>
    public static int[,] DISPLAY_COLORS = new int[DISPLAY_WIDTH, DISPLAY_HEIGHT];

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
    public static bool BIOME_DIMENSIONS = true;

    /// <summary>
    /// global world manager
    /// </summary>
    public static WorldManager WORLD_MANAGER;

    /// <summary>
    /// sets up the global WorldManager (Global.WORLD_MANAGER)
    /// </summary>
    /// <param name="worldFilePath"></param>
    public static void setupWorldManager(string worldFilePath) {
        WORLD_MANAGER?.close();
        WORLD_MANAGER = new WorldManager(worldFilePath);
    }

    /// <summary>
    /// binary size of a region in vxw files in bytes
    /// </summary>
    public static int BYTE_CHUNK_SIZE = (int)((CHUNK_SIZE * CHUNK_SIZE * CHUNK_SIZE + 1) * HEIGHT_LIMIT + 2);

    /// <summary>
    /// binary size of a chunk in vxw files in bytes
    /// </summary>
    public static int BYTE_REGION_SIZE = REGION_SIZE * REGION_SIZE * BYTE_CHUNK_SIZE;
    
    /// <summary>
    /// binary size of a header in vxw files in bytes
    /// </summary>
    public const int BYTE_HEADER_SIZE = 16;

    /// <summary>
    /// updates the global binary sizes of chunks and regions
    /// </summary>
    private static void updateBinarySizes() {
        BYTE_CHUNK_SIZE = (int)((CHUNK_SIZE * CHUNK_SIZE * CHUNK_SIZE + 1) * HEIGHT_LIMIT + 2);
        BYTE_REGION_SIZE = REGION_SIZE * REGION_SIZE * BYTE_CHUNK_SIZE;
    }

    /// <summary>
    /// max region coordinate
    /// </summary>
    public static int MAX_REGION_POS = (int)Math.Ceiling(WORLD_SIZE / 2f);

    /// <summary>
    /// min region coordinate
    /// </summary>
    public static int MIN_REGION_POS = (int)Math.Floor(WORLD_SIZE / 2f) * -1;

    /// <summary>
    /// offset of origin padding in the vxw region padding array in bytes
    /// </summary>
    public static int ORIGIN_PADDING_OFFSET = (int)(MIN_REGION_POS * WORLD_SIZE + MIN_REGION_POS) * -4;

    /// <summary>
    /// maximal horizontal position of player / request in voxels<br/>
    /// used for chunk loading limitation
    /// </summary>
    public static long MAX_HORIZONTAL_POS = MAX_REGION_POS * REGION_SIZE * CHUNK_SIZE - 1;

    /// <summary>
    /// minimal horizontal position of player / request in voxels<br/>
    /// used for chunk loading limitation
    /// </summary>
    public static long MIN_HORIZONTAL_POS = MIN_REGION_POS * REGION_SIZE * CHUNK_SIZE * -1;

    /// <summary>
    /// max chunk coordinate
    /// </summary>
    public static long MAX_CHUNK_POS = MAX_REGION_POS * REGION_SIZE - 1;

    /// <summary>
    /// min chunk coordinate
    /// </summary>
    public static long MIN_CHUNK_POS = MIN_REGION_POS * REGION_SIZE * -1;
    
    /// <summary>
    /// updates all precalculated variables
    /// </summary>
    public static void updatePrecalculatedVariables() {
        MAX_HORIZONTAL_POS = (long)Math.Ceiling(WORLD_SIZE / 2f) * REGION_SIZE * CHUNK_SIZE - 1;
        MIN_HORIZONTAL_POS = (long)Math.Floor(WORLD_SIZE / 2f) * REGION_SIZE * CHUNK_SIZE * -1;
        MAX_REGION_POS = (int)Math.Ceiling(WORLD_SIZE / 2f);
        MIN_REGION_POS = (int)Math.Floor(WORLD_SIZE / 2f) * -1;
        ORIGIN_PADDING_OFFSET = (int)(MIN_REGION_POS * WORLD_SIZE + MIN_REGION_POS) * -4;
        MAX_CHUNK_POS = MAX_REGION_POS * REGION_SIZE - 1;
        MIN_CHUNK_POS = MIN_REGION_POS * REGION_SIZE * -1;
        
        updateBinarySizes();
    }

    /// <summary>
    /// lists all variables and their values from Global to console 
    /// </summary>
    public static void listAll() {
        Debug.info("Global variables:");
        ConsoleColors.printlnColored(
            $"    display width: \x1B[1m{DISPLAY_WIDTH}\x1B[22m,\n" + 
            $"    display height: \x1B[1m{DISPLAY_HEIGHT}\x1B[22m,\n" + 
            $"    field of view: \x1B[1m{FOV}\x1B[22m,\n" + 
            $"    player position: \x1B[1m[{PLAYER_POS.x}, {PLAYER_POS.y}, {PLAYER_POS.z}]\x1B[22m,\n" + 
            $"    height limit: \x1B[1m{HEIGHT_LIMIT}\x1B[22m,\n" + 
            $"    chunk size: \x1B[1m{CHUNK_SIZE}\x1B[22m voxels,\n" + 
            $"    region size: \x1B[1m{REGION_SIZE}\x1B[22m chunks,\n" + 
            $"    world size: \x1B[1m{WORLD_SIZE}\x1B[22m regions,\n" + 
            $"    biome dimensions: \x1B[1m{(BIOME_DIMENSIONS ? "3d" : "2d")}\x1B[22m,\n" + 
            $"    max region coordinate: \x1B[1m{MAX_REGION_POS}\x1B[22m,\n" + 
            $"    min region coordinate: \x1B[1m{MIN_REGION_POS}\x1B[22m,\n" + 
            $"    max horizontal coordinate: \x1B[1m{MAX_HORIZONTAL_POS}\x1B[22m,\n" + 
            $"    min horizontal coordinate: \x1B[1m{MIN_HORIZONTAL_POS}\x1B[22m", 
            (int)Colors.GRAY_2);
    }

    /// <summary>
    /// global shell
    /// </summary>
    public static readonly Shell SHELL = new();

    public static void registerCommands() {
        CommandRegistry.registerCommand(WorldManager.WM);
    }
    
    /// <summary>
    /// global scene manager
    /// </summary>
    public static SceneManager SCENE_MANAGER;

    /// <summary>
    /// sets up the scene manager
    /// </summary>
    public static void setupSceneManager() {
        SCENE_MANAGER = new SceneManager();
    }

    /// <summary>
    /// size of the loaded area in chunks <br/>
    /// x to one side + 1 + x to the other side
    /// </summary>
    public static int CHUNK_LOADING_DISTANCE = 7;
}