//
// VoxelsCore
// by KryKom 2024
//

using Kolors;
using VoxelsCoreSharp.generator.feature.organic;
using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp.generator.terrain.biome;

/// <summary>
/// biome structure holding info about the different biomes
/// </summary>
public class Biome(byte humidity, sbyte temperature, (Tree tree, float density)[] trees, Voxel[]? surface = null) {

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
    public Voxel[] surface { get; private set; } = surface ?? [Voxels.GRASS_BLOCK];

    /// <summary>
    /// array of tree types generated in the biome and its density (chance per block)
    /// </summary>
    public (Tree tree, float density)[] trees { get; private set; } = trees;

    /// <summary>
    /// biome id
    /// </summary>
    private string _id = "";
    public string id {
        get => _id;
        set { if (_id != "") _id = value; else Debug.error($"Cannot set an already-set id of biome '{id}'!"); }
    }

    public Biome(byte humidity, sbyte temperature, (Tree tree, float density)[] tree, Voxel? surface = null)
        : this(humidity, temperature, tree, surface != null ? [surface] : null) {}
    
    public Biome(byte humidity, sbyte temperature, (Tree tree, float density) tree, Voxel[]? surface = null) 
        : this(humidity, temperature, [tree], surface) {}
    
    public Biome(byte humidity, sbyte temperature, (Tree tree, float density) tree, Voxel? surface = null) 
        : this(humidity, temperature, [tree], surface != null ? [surface] : null) {}
}