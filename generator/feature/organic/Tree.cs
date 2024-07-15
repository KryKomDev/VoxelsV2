//
// VoxelsCore
// by KryKom 2024
//

using Kolors;
using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp.generator.feature.organic;

/// <summary>
/// holds data and generator for the specific tree type
/// </summary>
public struct Tree(Voxel wood, Voxel leaves, Action generate, int levels) {
    public Voxel wood { get; private set; } = wood;
    public Voxel leaves { get; private set; } = leaves;
    public Action generate { get; private set; } = generate;
    public int levels { get; private set; } = levels;
    private string _id = "";
    public string id {
        get => _id;
        set { if (_id != "") _id = value; else Debug.error($"Cannot set an already-set id of tree type '{id}'!"); }
    }
}