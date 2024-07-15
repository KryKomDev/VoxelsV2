//
// VoxelsCore
// by KryKom 2024
//

using Kolors;
using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp.generator.feature.organic;

public class TreeRegistry {
    public static readonly List<Tree> registry = new();
    
    private static Tree register(string id, Tree tree) {
        foreach (var b in registry) {
            if (b.id == id) {
                Debug.error($"Could not register biome '{id}', because a biome with the same id already exists!");
                return b;
            }
        }

        tree.id = id;
        registry.Add(tree);
        return tree;
    }

    public static Tree OAK = register("oak", new Tree(Voxels.OAK_LOG, Voxels.OAK_LEAVES, () => { }, 3)); // todo this mfs

}