//
// VoxelsCoreSharp
// by KryKom 2024
//

namespace VoxelsCoreSharp.world;

/// <summary>
/// chunk struct, holds data of a part of a world
/// </summary>
public struct Chunk() {
    
    /// <summary>
    /// array of sub-chunks, content of the chunk
    /// </summary>
    public SubChunk[] content = new SubChunk[Global.HEIGHT_LIMIT];
    
    /// <summary>
    /// chunk x position of the chunk
    /// </summary>
    public long x = 0;
    
    /// <summary>
    /// chunk y position of the chunk
    /// </summary>
    public long y = 0;

    /// <summary>
    /// overall climate of the chunk
    /// </summary>
    /// TODO: see also...
    public byte climateBiome = 0;

    /// <summary>
    /// holds data about the chunk generation state <br/>
    /// 0 -> not generated<br/>
    /// 1 -> partially generated<br/>
    /// 2 -> fully generated <br/>
    /// for more info see <a href="https://github.com/KryKomDev/VoxelsV2/blob/main/world/test/vxw-format.md">vxw-format.md</a> on GitHub
    /// </summary>
    public byte state = 0;
}

/// <summary>
/// a cubic area of voxels
/// </summary>
public struct SubChunk() {

    /// <summary>
    /// biome type corresponding to the <see cref="BiomeType">BiomeType</see> enum 
    /// </summary>
    public byte biome = 0;
    
    /// <summary>
    /// 3d array of int type indices pointing to the block and biome palette<br/>
    /// first 16 bits point to block palette the next 16 point to the biome palette<br/>
    /// [x, y, z] -> [north / south, up / down, west / east]
    /// </summary>
    public byte[,,] content = new byte[Global.CHUNK_SIZE, Global.CHUNK_SIZE, Global.CHUNK_SIZE];

    /// <summary>
    /// chunk z (up / down) coordinate of the subchunk
    /// </summary>
    public int z = 0;
}