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

    public static Voxel register(string id, Voxel voxel) {

        voxel.id = id;
        
        foreach (Voxel v in registry) {
            if (v.id == id) {
                Debug.error($"Could not add voxel \'{id}\' to the registry! Voxel with the same name already exists.");
                return v;
            }
        }
        
        registry.Add(voxel);
        return voxel;
    }
}