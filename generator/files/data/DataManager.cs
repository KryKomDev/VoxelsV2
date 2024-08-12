//
// VoxelsCore
// by KryKom 2024
//

using System.Text.Json;
using Kolors;
using VoxelsCoreSharp.generator.files.config;

namespace VoxelsCoreSharp.generator.files.data;

/// <summary>
/// manages region generation data
/// </summary>
public class DataManager {

    public static RegionData? load(long x, long y) {
        
        string folderPath = Path.Combine(Global.WORLD_DIR_PATH, "voxels\\region_data\\");
        string configFilePath = Path.Combine(folderPath, $"{x}.{y}.vxrd");

        // Create the "voxels" folder if it doesn't exist.
        if (!Directory.Exists(folderPath)) { 
            Debug.info("creating the '...\\voxels\\region_data\\' directory"); 
            Directory.CreateDirectory(folderPath);
            return null;
        }

        // Create the config file if it doesn't exist.
        if (!File.Exists(configFilePath)) {
            Debug.info($"creating the '...\\voxels\\region_data\\{x}.{y}.vxrd' file");
            return null;
        }

        byte[] raw = File.ReadAllBytes(configFilePath);
        RegionData data = new RegionData();
        
        // data transform - chunk state
        for (int i = 0; i < 16; i++) {
            data.chunkState[i] = (short)((raw[i * 2] << 8) + raw[i * 2 + 1]);
        }

        // data transform - biome map
        for (int xi = 0; xi < 16; xi++) {
            for (int yi = 0; yi < 16; yi++) {
                data.biomeMap[xi, yi] = raw[xi * 16 + yi + 32];
                Console.WriteLine(data.biomeMap[xi, yi]);
            }
        }

        // data transform - biome palette
        for (int i = 288; i < raw.Length; i++) {
            byte length = raw[i];
            string s = ""; 
            int j = i + 1;
            
            for (; j < raw.Length && j <= length + 288; j++) { 
                s += (char)raw[j];
            }
            
            i = j + 1;
            data.biomePalette.Add(s);
        }

        return data;
    }

    public static void write(RegionData data, long x, long y) {
        string folderPath = Path.Combine(Global.WORLD_DIR_PATH, "voxels\\region_data\\");
        string dataFilePath = Path.Combine(folderPath, $"{x}.{y}.vxrd");

        // Create the "voxels" folder if it doesn't exist.
        if (!Directory.Exists(folderPath)) { 
            Debug.info("creating the '...\\voxels\\region_data\\' directory"); 
            Directory.CreateDirectory(folderPath);
        }

        // Create the config file if it doesn't exist.
        if (!File.Exists(dataFilePath)) {
            Debug.info($"creating the '...\\voxels\\{x}.{y}.vxrd' file");
        }

        List<byte> raw = new List<byte>();
        
        for (int i = 0; i < 16; i++) {
            raw.Add((byte)(data.chunkState[i] >> 8));
            raw.Add((byte)data.chunkState[i]);
        }

        for (int xi = 0; xi < 16; xi++) {
            for (int yi = 0; yi < 16; yi++) {
                raw.Add(data.biomeMap[xi, yi]);
            }
        }

        foreach (string s in data.biomePalette) {
            raw.Add((byte)s.Length);

            foreach (char c in s) {
                raw.Add((byte)c);
            }
        }
        
        File.WriteAllBytes(dataFilePath, raw.ToArray());
    }
}