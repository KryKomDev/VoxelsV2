using Kolors;

namespace VoxelsCoreSharp.generator.feature.organic;

public static class WoodGroupRegistry {
    public static readonly List<WoodGroup> registry = new();
    
    private static WoodGroup register(string id, WoodGroup group) {
        foreach (var b in registry) {
            if (b.id == id) {
                Debug.error($"Could not register biome '{id}', because a biome with the same id already exists!");
                return b;
            }
        }

        group.id = id;
        registry.Add(group);
        return group;
    }

    public static WoodGroup OAK = register("oak", new WoodGroup());
}