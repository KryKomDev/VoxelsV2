//
// VoxelsCoreSharp
// by KryKom 2024
//

using VoxelsCoreSharp.util;

namespace VoxelsCoreSharp.world;

/// <summary>
/// reads / writes .vxw world files
/// </summary>
public class WorldManager {

    private string worldFilePath { get; set; }
    private bool validated { get; set; } = false;
    private readonly FileStream fileStream;

    public WorldManager(string worldFilePath) {
        this.worldFilePath = worldFilePath;
        Debug.info($"setting up new WorldManager for path \"{worldFilePath}\"...");

        fileStream = new FileStream(worldFilePath, FileMode.Open, FileAccess.ReadWrite);

        if (validate() == 0) {
            validated = true;
        }
        else {
            Debug.warn($"file at \"{worldFilePath}\" is not in the correct format");
        }
    }

    /// <summary>
    /// verifies whether the file of the file is valid
    /// </summary>
    /// <returns>0 if the file is valid, 1 if invalid</returns>
    public int validate() {

        fileStream.Seek(0, SeekOrigin.Begin);

        byte[] buffer = new byte[3];
        fileStream.Read(buffer, 0, buffer.Length);

        if (buffer[0] == (byte)'v' && buffer[1] == (byte)'x' && buffer[2] == (byte)'w') {
            return 0; // valid
        }
        else {
            return 1; // invalid
        }
    }

    /// <summary>
    /// reads a vxw header from the world file
    /// </summary>
    /// <returns>header in the VXWHeader struct</returns>
    public VXWHeader readHeader() {
        
        Debug.info("reading world file...");
        
        VXWHeader header = new VXWHeader();
        
        if (!validated) {
            Debug.warn("invalid world file. Reading this can result in corrupt data.");
        }

        byte[] buffer = new byte[13];
        
        fileStream.Seek(3, SeekOrigin.Begin);
            
        fileStream.Read(buffer, 0, buffer.Length);

        header.chunkSize = (ushort)((buffer[0] << 8) + buffer[1]);
        header.regionSize = (ushort)((buffer[2] << 8) + buffer[3]);
        header.worldSize = (uint)((buffer[4] << 24) + (buffer[5] << 16) + (buffer[6] << 8) + buffer[7]);
        header.maxHeight = (uint)((buffer[8] << 24) + (buffer[9] << 16) + (buffer[10] << 8) + buffer[11]);
        header.biomeDim = buffer[12] == 0;
        
        return header;
    }

    /// <summary>
    /// writes a .vxw header to the world file
    /// </summary>
    /// <param name="chunkSize">size of a chunk in voxels</param>
    /// <param name="regionSize">size of a region in chunks</param>
    /// <param name="worldSize">size of the world in regions</param>
    /// <param name="maxHeight">chunk height in sub-chunks</param>
    /// <param name="biomeDim">biome dimension (true - 3D, false - 2D)</param>
    public void writeHeader(ushort chunkSize, ushort regionSize, uint worldSize, uint maxHeight, bool biomeDim, bool ignoreInvalidFormat) {
        
        Debug.info("writing to world file...");
        
        byte[] buffer = new byte[Global.BINARY_HEADER_SIZE];
        
        if (!validated && !ignoreInvalidFormat) {
            Debug.error("invalid world file. Writing to this file can result in data corruption. Aborting...");
            return;
        }

        // why do i do this to myself!?
        buffer[0] = (byte)'v';
        buffer[1] = (byte)'x';
        buffer[2] = (byte)'w';
        buffer[3] = (byte)(chunkSize >> 8);
        buffer[4] = (byte)chunkSize;
        buffer[5] = (byte)(regionSize >> 8);
        buffer[6] = (byte)regionSize;
        buffer[7] = (byte)(worldSize >> 24);
        buffer[8] = (byte)(worldSize >> 16);
        buffer[9] = (byte)(worldSize >> 8);
        buffer[10] = (byte)worldSize;
        buffer[11] = (byte)(maxHeight >> 24);
        buffer[12] = (byte)(maxHeight >> 16);
        buffer[13] = (byte)(maxHeight >> 8);
        buffer[14] = (byte)maxHeight;
        buffer[15] = (byte)(biomeDim ? 0 : 1);
        
        fileStream.Seek(0, SeekOrigin.Begin);
            
        fileStream.Write(buffer, 0, buffer.Length);

    }

    /// <summary>
    /// loads world header settings into Global
    /// </summary>
    public void loadHeader() {
        VXWHeader header = readHeader();

        Global.CHUNK_SIZE = header.chunkSize;
        Global.HEIGHT_LIMIT = header.maxHeight;
        Global.WORLD_SIZE = header.worldSize;
        Global.REGION_SIZE = header.regionSize;
        Global.BIOME_DIMENSION = header.biomeDim;
        
        Global.updatePrecalculatedVariables();
    }
    
    /// <summary>
    /// reads a chunk from a vxw world file
    /// </summary>
    /// <param name="x">any x position of the chunk</param>
    /// <param name="y">any y position of the chunk</param>
    /// <returns>chunk if generated or partially generated, null if region not generated</returns>
    public Chunk? readChunk(long x, long y) {
        
        if (x > Global.MAX_HORIZONTAL_POS || x < -Global.MAX_HORIZONTAL_POS || y > Global.MAX_HORIZONTAL_POS || y < -Global.MAX_HORIZONTAL_POS) {
            Debug.warn("position out of bounds!");
            return null;
        }

        byte[] buffer = new byte[Global.BINARY_CHUNK_SIZE];
        byte[] paddingRaw = new byte[4];

        long? regionStart;

        // data reading

        // padding of the region in ptr array
        long arrayPadding = getRegionPaddingOffset(x, y);
            
        fileStream.Seek(arrayPadding, SeekOrigin.Begin);
        fileStream.Read(paddingRaw, 0, 4);

        // total region padding in file
        int padding = ((paddingRaw[0] << 24) + (paddingRaw[1] << 16) + (paddingRaw[2] << 8) + paddingRaw[3]);
        regionStart = getRegionPadding(padding);

        // check if not generated
        if (regionStart == null) {
            Debug.info("region not generated");
            return null;
        }

        fileStream.Seek((long)((x * Global.WORLD_SIZE + y) * Global.BINARY_CHUNK_SIZE + regionStart)!, SeekOrigin.Begin);
        fileStream.Read(buffer, 0, Global.BINARY_CHUNK_SIZE);

        // data extraction
        Chunk chunk = new() {
            state = buffer[0],
            climateBiome = buffer[1],
            x = x / Global.CHUNK_SIZE,
            y = y / Global.CHUNK_SIZE
        };

        int byteCount = 2;

        for (int i = 0; i < Global.HEIGHT_LIMIT; i++) {
            chunk.content[i].biome = buffer[byteCount];
            byteCount++;

            for (int iz = 0; iz < Global.CHUNK_SIZE; iz++) {

                chunk.content[i].z = iz;

                for (int iy = 0; iy < Global.CHUNK_SIZE; iy++) {
                    for (int ix = 0; ix < Global.CHUNK_SIZE; ix++) {
                        chunk.content[i].content[ix, iy, iz] = buffer[byteCount];
                        byteCount++;
                    }
                }
            }
        }

        return chunk;
    }


    /// <summary>
    /// finds the highest region index in the region padding array
    /// </summary>
    /// <returns>the highest index</returns>
    public int getHighestRegionIndex() {
        int max = 0;

        for (int i = 16; i < Global.BINARY_HEADER_SIZE + Global.WORLD_SIZE * Global.WORLD_SIZE * 4; i += 4) {

            byte[] buffer = new byte[4];

            fileStream.Seek(i, SeekOrigin.Begin);
            fileStream.Read(buffer, 0, 4);

            int current = (buffer[0] << 24) + (buffer[1] << 16) + (buffer[2] << 8) + buffer[3];

            max = current > max ? current : max;
        }

        return max;
    }


    /// <summary>
    /// generates a plain region sector in the world file
    /// </summary>
    /// <param name="x">any x position in the region</param>
    /// <param name="y">any y position in the region</param>
    public void generateRegionSector(int x, int y) {

        int index = getHighestRegionIndex() + 1;

        byte[] test = new byte[4];

        fileStream.Seek(getRegionPaddingOffset(x, y), SeekOrigin.Begin);
        fileStream.Read(test, 0, 4);

        // if already generated
        if ((test[0] | test[1] | test[2] | test[3]) != 0) {
            Debug.warn("sector already generated!");
            return;
        }

        setRegionPadding(x, y, index);

        // continue
        byte[] buffer = new byte[Global.BINARY_REGION_SIZE];

        fileStream.Seek(Global.BINARY_HEADER_SIZE + Global.WORLD_SIZE * Global.WORLD_SIZE * 4 + (index - 1) * Global.BINARY_REGION_SIZE, SeekOrigin.Begin);

        fileStream.Write(buffer, 0, Global.BINARY_REGION_SIZE);
    }


    /// <summary>
    /// generates blank (or dummy data) region padding array 
    /// </summary>
    /// <param name="dummyData">if true, writes dummy data into the array</param>
    public void generateRegionPaddingArray(bool dummyData) {
        
        Debug.info(dummyData ? "generating dummy region padding array..." : "generating blank region padding array...");

        int size = (int)(Global.WORLD_SIZE * Global.WORLD_SIZE * 4);
        byte[] buffer = new byte[size];

        int c = 0;
        if (dummyData == true) {
            for (int i = 0; i < size; i += 4) {
                buffer[i + 3] = (byte)c;
                c++;
            }
        }
        
        fileStream.Seek(Global.BINARY_HEADER_SIZE, SeekOrigin.Begin);
            
        fileStream.Write(buffer, 0, size);
    }

    
    /// <summary>
    /// computes region padding padding in the vxw region padding array
    /// </summary>
    /// <param name="x">any x in the region</param>
    /// <param name="y">any y in the region</param>
    /// <returns>the exact number of bytes before the region padding</returns>
    public static long getRegionPaddingOffset(long x, long y) {
        
        x /= Global.CHUNK_SIZE * Global.REGION_SIZE;
        y /= Global.CHUNK_SIZE * Global.REGION_SIZE;

        long offset = Global.BINARY_HEADER_SIZE + (x * Global.WORLD_SIZE + y) * 4 + ((Global.WORLD_SIZE * Global.WORLD_SIZE) / 2) * 4;

        return offset;
    }


    /// <summary>
    /// computes the padding of a region for index from padding array
    /// </summary>
    /// <param name="index">index of the</param>
    /// <returns>total padding in bytes, or null if chunk is not generated</returns>
    public static long? getRegionPadding(int index) {

        // region not generated yet
        if (index == 0) return null;
        
        long padding = Global.BINARY_REGION_SIZE * (index - 1) + Global.BINARY_HEADER_SIZE + Global.WORLD_SIZE * Global.WORLD_SIZE * 4;

        return padding;
    }


    /// <summary>
    /// sets a value in the region padding array
    /// </summary>
    /// <param name="x">any x in the region</param>
    /// <param name="y">any y in the region</param>
    /// <param name="value">value of the field</param>
    public void setRegionPadding(long x, long y, int value) {

        byte[] buffer = new byte[4];
        buffer[0] = (byte)(value >> 24);
        buffer[1] = (byte)(value >> 16);
        buffer[2] = (byte)(value >> 8);
        buffer[3] = (byte)value;
        
        fileStream.Seek(getRegionPaddingOffset(x, y), SeekOrigin.Begin);
        fileStream.Write(buffer);
    }
}

/// <summary>
/// holds header data from the vxw world file
/// </summary>
/// <seealso cref="vxw-format.md"/>
public struct VXWHeader {
    public ushort chunkSize;
    public ushort regionSize;
    public uint maxHeight;
    public uint worldSize;
    public bool biomeDim;

    public override string ToString() {
        return $"c-size: {chunkSize}, r-size: {regionSize}, w-size: {worldSize}, m-height: {maxHeight}, biomeDim: {biomeDim}";
    }
}