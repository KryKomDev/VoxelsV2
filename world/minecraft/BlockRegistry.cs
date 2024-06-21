using Kolors;
using VoxelsCoreSharp.world.minecraft.block;

namespace VoxelsCoreSharp.world.minecraft;

public class BlockRegistry {
    private static List<Block> registry = new();

    public static Block register(string id, Block block) {
        foreach (var b in registry) {
            if (id == b.id) {
                Debug.error($"Could not register new block with {b.id}, because block with the same id already exists!");
                return block;
            }
        }
        registry.Add(block);
        return block;
    }
}