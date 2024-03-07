//
// VoxelsCoreSharp
// by KryKom 2024
//

namespace VoxelsCoreSharp.util;

/// <summary>
/// contains global variables and settings
/// </summary>
public class Global {

    /// <summary>
    /// width of the rendered area in pixels
    /// </summary>
    public static uint DISPLAY_WIDTH = 1920;
    
    /// <summary>
    /// height of the rendered area in pixels
    /// </summary>
    public static uint DISPLAY_HEIGHT = 1080;
    
    /// <summary>
    /// color and depth map of rendered pixels<br></br>
    /// first (least significant) byte is for red channel,<br></br>
    /// second byte is for green channel,<br></br>
    /// third byte for blue,<br></br>
    /// 5 remaining bytes are for depth<br></br>
    /// please use bit operations to get or set any value
    /// </summary>
    public static long[,] DISPLAY_COLORS = new long[DISPLAY_WIDTH, DISPLAY_HEIGHT];

    /// <summary>
    /// field of view in degrees
    /// defines the size of the rendered area
    /// </summary>
    public static int FOV = 70;
}