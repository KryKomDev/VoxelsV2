using VoxelsCoreSharp.data;
using VoxelsCoreSharp.world.minecraft;
using VoxelsCoreSharp.world.minecraft.block;

namespace VoxelsCoreSharp.world;

public class Voxel : RegistryObject {
    
    public readonly int color;

    public Voxel() {
        color = (int)MaterialColor.NONE;
    }

    public Voxel(MaterialColor color) {
        this.color = (int)color;
    }

}