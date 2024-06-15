//
// VoxelsCoreSharp
// by KryKom 2024
//

using Kolors;

namespace VoxelsCoreSharp.world;

/// <summary>
/// manages the rendered scene
/// </summary>
public class SceneManager {

    private int[,] chunkIndices = new int[Global.CHUNK_LOADING_DISTANCE * 2 + 1, Global.CHUNK_LOADING_DISTANCE * 2 + 1];
    private List<Chunk> loadedChunks = new();
    private bool active = false;
    
    public SceneManager() {
        activate();
        
        
    }

    public void activate() {
        if (Global.WORLD_MANAGER == null) {
            active = false;
            Debug.error("World Manager not set yet! Please set the world manager before setting up.");
        }
        else {
            active = true;
        }
    }

    /// <summary>
    /// loads chunks that are supposed to be loaded
    /// </summary>
    public void update() {
        
    }
    
    /// <summary>
    /// loads chunk to memory
    /// </summary>
    /// <param name="x">chunk x of the requested chunk</param>
    /// <param name="y">chunk y of the requested chunk</param>
    public void loadChunk(long x, long y) {
        
    }

    public void unloadChunk(int x, int y) {
        foreach (Chunk chunk in loadedChunks) {
            if (chunk.x == 2) ;
        }
    }
}