//
// VoxelsCoreSharp
// by KryKom 2024
//

using VoxelsCoreSharp.util;

namespace VoxelsCoreSharp.world;

/// <summary>
/// 
/// </summary>
public class Chunk {
    
    public SubChunk[] content = new SubChunk[Global.HEIGHT_LIMIT];
    public int x;
    public int z;
    
    
}

/// <summary>
/// a 16 x 16 x 16 area of blocks
/// </summary>
public struct SubChunk(int y) {

    

    /// <summary>
    /// 3d array of int type indices pointing to the block and biome palette<br/>
    /// first 16 bits point to block palette the next 16 point to the biome palette<br/>
    /// [x, y, z] -> [north / south, up / down, west / east]
    /// </summary>
    public int[,,] blocks = new int[16, 16, 16];

    /// <summary>
    /// y (up / down) coordinate of the subchunk
    /// </summary>
    public readonly int y = y;
}