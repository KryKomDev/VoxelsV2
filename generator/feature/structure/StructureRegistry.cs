using VoxelsCoreSharp.data;

namespace VoxelsCoreSharp.generator.feature.structure;

public class StructureRegistry : Registry<Structure> {
    
    public static readonly Structure TEST = register("voxels:test", new Structure());
}