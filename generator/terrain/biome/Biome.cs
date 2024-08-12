//
// VoxelsCore
// by KryKom 2024
//

using Kolors;
using VoxelsCoreSharp.data;
using VoxelsCoreSharp.generator.feature.organic;
using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp.generator.terrain.biome;

/// <summary>
/// biome structure holding info about the different biomes
/// </summary>
public class Biome(byte humidity, sbyte temperature, (Tree tree, float density)[] trees, Voxel[]? surface = null, int previewColor = 0x25b312) : RegistryObject {

    /// <summary>
    /// biome humidity level (in percent), 0 - 100
    /// </summary>
    public byte humidity { get; private set; } = (byte)(humidity > 100 ? 100 : humidity);

    /// <summary>
    /// biome temperature (in Celsius!), -50 - +50
    /// FUCK IMPERIAL UNITS!!!
    /// </summary>
    public sbyte temperature { get; private set; } = (sbyte)(temperature is < -50 or > 50 ? (temperature < -50 ? -50 : 50) : temperature);

    /// <summary>
    /// voxels generated on the surface
    /// </summary>
    public Voxel[] surface { get; private set; } = surface ?? [VoxelRegistry.GRASS_BLOCK];

    /// <summary>
    /// array of tree types generated in the biome and its density (chance per block)
    /// </summary>
    public (Tree tree, float density)[] trees { get; private set; } = trees;

    /// <summary>
    /// color representing the biome in the biome setter window in launcher
    /// </summary>
    public int previewColor { get; private set; }

    public Biome(byte humidity, sbyte temperature, (Tree tree, float density)[] tree, Voxel? surface = null, int previewColor = 0x25b312)
        : this(humidity, temperature, tree, surface != null ? [surface] : null, previewColor) {}
    
    public Biome(byte humidity, sbyte temperature, (Tree tree, float density) tree, Voxel[]? surface = null, int previewColor = 0x25b312) 
        : this(humidity, temperature, [tree], surface, previewColor) {}
    
    public Biome(byte humidity, sbyte temperature, (Tree tree, float density) tree, Voxel? surface = null, int previewColor = 0x25b312) 
        : this(humidity, temperature, [tree], surface != null ? [surface] : null, previewColor) {}
}