//
// VoxelsCore V2
// by KryKom 2024
//

using Kolors;
using VoxelsCoreSharp.world.minecraft;
using VoxelsCoreSharp.world.minecraft.block;

namespace VoxelsCoreSharp.world;

public class VoxelRegistry {
    
    private static readonly List<Voxel> registry = new();

    public static Voxel register(string id, MaterialColor color) {
        foreach (Voxel v in registry) {
            if (v.id == id) {
                Debug.error($"Could not add voxel \'{id}\' to the registry! Voxel with the same name already exists.");
                return v;
            }
        }

        Voxel a = new Voxel(id, color);
        registry.Add(a);
        return a;
    }

    public static readonly Voxel DIRT = register("minecraft:dirt", MaterialColor.DIRT);
    public static readonly Voxel GRASS = register("minecraft:grass", MaterialColor.GRASS);
    public static readonly Voxel STONE = register("minecraft:stone", MaterialColor.STONE);
    
}