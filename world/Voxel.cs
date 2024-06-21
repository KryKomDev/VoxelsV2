using VoxelsCoreSharp.world.minecraft;
using VoxelsCoreSharp.world.minecraft.block;

namespace VoxelsCoreSharp.world;

public class Voxel(string id, MaterialColor color) {

    public readonly string id = id;
    public readonly int color = (int)color;
}