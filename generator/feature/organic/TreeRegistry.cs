//
// VoxelsCore
// by KryKom 2024
//

using Kolors;
using VoxelsCoreSharp.data;
using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp.generator.feature.organic;

public class TreeRegistry : Registry<Tree> {
    
    public static Tree OAK = register("oak", new Tree(VoxelRegistry.OAK_LOG, VoxelRegistry.OAK_LEAVES, () => { }, 3)); // TODO this mfs

}