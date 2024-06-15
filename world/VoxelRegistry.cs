//
// VoxelsCore V2
// by KryKom 2024
//

using Kolors;
using VoxelsCoreSharp.world.minecraft;

namespace VoxelsCoreSharp.world;

public class VoxelRegistry {
    
    private static readonly List<Voxel> registry = new();

    public static Voxel? register(string id, int color, Block minecraftBlock) {
        foreach (Voxel v in registry) {
            if (v.id == id) {
                Debug.error($"Could not add voxel \'{id}\' to the registry! Voxel with the same name already exists.");
                return null;
            }
        }

        Voxel a = new Voxel(id, color, minecraftBlock);
        registry.Add(a);
        return a;
    }

    public static Voxel? register(string id, int color) {
        return null;
    }

    public static readonly Voxel DIRT = register("dirt", 2);
}