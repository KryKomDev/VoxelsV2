namespace VoxelsCoreSharp.world.minecraft.block;

public class Block(BlockBehaviour behavior) {

    private BlockBehaviour behavior = behavior;
    public string id { get; private set; }
}