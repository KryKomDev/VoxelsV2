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
/// contains all available biome types
/// </summary>
public class BiomeRegistry : Registry<Biome> {

    // public static readonly Biome ARCTIC_SEA = register("arctic_sea", new Biome(100));
    
    public static readonly Biome PLAINS = register("minecraft:plains", new Biome(77, 20, (TreeRegistry.OAK, 10f), VoxelRegistry.GRASS_BLOCK));
}