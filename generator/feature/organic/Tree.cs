//
// VoxelsCore
// by KryKom 2024
//

using Kolors;
using VoxelsCoreSharp.data;
using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp.generator.feature.organic;

/// <summary>
/// holds data and generator for the specific tree type
/// </summary>
public class Tree(Voxel wood, Voxel leaves, Action generate, int levels) : RegistryObject {
    public Voxel wood { get; private set; } = wood;
    public Voxel leaves { get; private set; } = leaves;
    public Action generate { get; private set; } = generate;
    public int levels { get; private set; } = levels;
}