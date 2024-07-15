//
// VoxelsCore
// by KryKom 2024
//

using Kolors;
using VoxelsCoreSharp.generator.feature.organic;
using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp.generator.terrain.biome;

/// <summary>
/// contains all available biome types
/// </summary>
public class BiomeRegistry {
    
    /// <summary>
    /// contains all available biome types
    /// </summary>
    public static readonly List<Biome> registry = new();

    private static Biome register(string id, Biome biome) {
        foreach (var b in registry) {
            if (b.id == id) {
                Debug.error($"Could not register biome {id}, because a biome with the same id already exists!");
                return b;
            }
        }

        biome.id = id;
        registry.Add(biome);
        return biome;
    }

    // public static readonly Biome ARCTIC_SEA = register("arctic_sea", new Biome(100));
    
    public static readonly Biome PLAINS = register("plains", new Biome(77, 20, (TreeRegistry.OAK, 10f), Voxels.GRASS_BLOCK));
}