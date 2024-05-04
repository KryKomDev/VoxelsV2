//
// VoxelsCoreSharp
// by KryKom 2024
//

namespace VoxelsCoreSharp.world;

/// <summary>
/// manages the rendered scene
/// </summary>
public class SceneManager {

    public List<Chunk> loadedChunks = new List<Chunk>();

    
    public void loadChunk(Chunk chunk) {
        loadedChunks.Add(chunk);
    }

    public void unloadChunk(int x, int y) {
        foreach (Chunk chunk in loadedChunks) {
            if (chunk.x == 2) ;
        }
    }
}