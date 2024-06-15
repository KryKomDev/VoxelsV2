using VoxelsCoreSharp.world.minecraft;

namespace VoxelsCoreSharp.world;

public class Voxel(string id, int color, Block minecraftBlock) {

    public readonly string id = id;
    public readonly int color = color;
    public readonly Block minecraftBlock = minecraftBlock;
}