//
// VoxelsCoreSharp
// by KryKom 2024
//

using Commandier;
using Commandier.argument;
using Kolors;
using VoxelsCoreSharp.libs;

namespace VoxelsCoreSharp.world;

/// <summary>
/// region struct, holds data of a larger part of a world 
/// </summary>
public class Region {
    
    /// <summary>
    /// array of sub-chunks, content of the chunk
    /// </summary>
    public Chunk[,] content = new Chunk[Global.REGION_SIZE, Global.REGION_SIZE];
    
    /// <summary>
    /// chunk x position of the chunk
    /// </summary>
    public long x = 0;
    
    /// <summary>
    /// chunk y position of the chunk
    /// </summary>
    public long y = 0;

    /// <summary>
    /// calculates constant biome point value of this chunk, based on <see cref="computeBPPos"/>
    /// </summary>
    /// <returns>relative x and y coordinates of the point</returns>
    public (int x, int y) getBPPos() {
        return computeBPPos(x, y);
    }

    /// <summary>
    /// calculates constant biome point value of a region, based on hashing a string with position and seed, uses <see cref="Hash.GetHashCode(string)"/>
    /// </summary>
    /// <param name="x">x of the region</param>
    /// <param name="y">y of the region</param>
    /// <returns>relative x and y coordinates of the point</returns>
    public static (int x, int y) computeBPPos(long x, long y) {
        ushort hash = (ushort)$"r.{x}.{y}.{Global.Generator.SEED}".GetHashCode();
        return (hash >> 8, ((ushort)(hash << 8)) >> 8);
    }
}

/// <summary>
/// chunk struct, holds data of a part of a world
/// </summary>
public class Chunk {
    
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
    public byte climateBiome = 0; // TODO this shall not be

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