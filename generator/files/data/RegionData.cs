//
// VoxelsCore
// by KryKom 2024
//

namespace VoxelsCoreSharp.generator.files.data;

/// <summary>
/// holds crucial generation data about a region
/// </summary>
public class RegionData {
    
    /// <summary>
    /// biomes located in the region, <br/>
    /// in .rv files stored in format &lt;byte: length> &lt;string: content>
    /// </summary>
    public List<string> biomePalette { get; private set; } = new();
    
    /// <summary>
    /// 2d array of index pointers into the <see cref="biomePalette"/>, <br/>
    /// position of the byte stored in an .rv file is <c>x * 16 + y + 32</c>
    /// </summary>
    public byte[,] biomeMap { get; private set; } = new byte[16, 16];
    
    /// <summary>
    /// 2d 16x16 field of chunk generation states (0 -> not generated, 1 -> not generated), <br/>
    /// each bit of the <see cref="Int16"/> (short) datatype represents a boolean, <br/>
    /// moving in the specific short instance, means moving along x-axis; moving in the array, along y-axis, <br/>
    /// value shall be gotten with expression <c>(chunkState[y] &amp; (1 &lt;&lt; x)) == 1</c>
    /// </summary>
    public Int16[] chunkState = new Int16[Global.REGION_SIZE];
}