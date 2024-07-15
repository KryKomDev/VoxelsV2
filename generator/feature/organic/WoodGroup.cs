//
// VoxelsCore
// by KryKom 2024
//

using Kolors;
using VoxelsCoreSharp.world;

namespace VoxelsCoreSharp.generator.feature.organic;

/// <summary>
/// groups different voxels to complementing group
/// </summary>
public struct WoodGroup(string id, Voxel log, Voxel strippedLog, Voxel leaves, Voxel planks, Voxel slab, Voxel stairs, Voxel sign, Voxel button, Voxel wood, Voxel strippedWood) {
    
    private string _id = "";
    public string id {
        get => _id;
        set { if (_id != "") _id = value; else Debug.error($"Cannot set an already-set id of biome {id}!"); }
    }
    public Voxel log { get; private set; } = log;
    public Voxel strippedLog { get; private set; } = strippedLog;
    public Voxel leaves { get; private set; } = leaves;
    public Voxel planks { get; private set; } = planks;
    public Voxel slab { get; private set; } = slab;
    public Voxel stairs { get; private set; } = stairs;
    public Voxel sign { get; private set; } = sign;
    public Voxel button { get; private set; } = button;
    public Voxel wood { get; private set; } = wood;
    public Voxel strippedWood { get; private set; } = strippedWood;
}