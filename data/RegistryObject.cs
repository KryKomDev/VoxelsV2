//
// VoxelsCore
// by KryKom 2024
//

using Kolors;

namespace VoxelsCoreSharp.data;

/// <summary>
/// base class for all objects that are registered using the <see cref="Registry{T}"/> class
/// </summary>
public abstract class RegistryObject {
    
    /// <summary>
    /// unique object id
    /// </summary>
    private string _id = "";
    public string id {
        get => _id;
        set { if (_id != "") _id = value; else Debug.error($"Cannot set an already-set id of {GetType()} '{id}'!"); }
    }
}