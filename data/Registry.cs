//
// VoxelsCore
// by KryKom 2024
//

using Kolors;

namespace VoxelsCoreSharp.data;

/// <summary>
/// registry for large amounts of instances of a class
/// </summary>
/// <typeparam name="T">type of stored object</typeparam>
public abstract class Registry<T> where T : RegistryObject {
    
    public static List<T> registry { get; private set; } = new();
    
    /// <summary>
    /// registers new object
    /// </summary>
    /// <param name="id">id of the instance</param>
    /// <param name="registeredItem">the instance</param>
    /// <returns>the instance with the id</returns>
    protected static T register(string id, T registeredItem) {

        registeredItem.id = id;
        
        foreach (T o in registry) {
            if (o.id == id) {
                Debug.error($"Could not add registry object \'{id}\' of type \'{registeredItem.GetType()}\' to the registry! Object with the same id already exists.");
                return o;
            }
        }
        
        registry.Add(registeredItem);
        return registeredItem;
    }
}
