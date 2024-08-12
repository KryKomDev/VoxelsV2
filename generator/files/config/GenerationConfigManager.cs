//
// VoxelsCoreSharp
// by KryKom 2024
//

using System.Text.Json;
using Kolors;
using Generator = VoxelsCoreSharp.Global.Generator;

namespace VoxelsCoreSharp.generator.files.config;

/// <summary>
/// manages the config.json file within the voxels folder located in the world folder 
/// </summary>
public class GenerationConfigManager {
    
    /// <summary>
    /// loads the 
    /// </summary>
    /// <returns></returns>
    public static GenerationConfiguration? load() {
        string folderPath = Path.Combine(Global.WORLD_DIR_PATH, "voxels\\");
        string configFilePath = Path.Combine(folderPath, "config.json");

        try {
            // Create the "voxels" folder if it doesn't exist.
            if (!Directory.Exists(folderPath)) {
                Debug.info("creating the '.../voxels/' directory");
                Directory.CreateDirectory(folderPath);
            }

            // Create the config file if it doesn't exist.
            if (!File.Exists(configFilePath)) {
                Debug.info("creating the '.../voxels/config.json' file");
                var defaultConfig = new GenerationConfiguration {
                    worldPreset = Generator.WORLD_PRESET,
                    worldBorderType = Generator.WORLD_BORDER_TYPE,
                    seed = Generator.SEED,
                    vanilla = Generator.VANILLA
                };

                string defaultJson = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions {
                    WriteIndented = true
                });

                File.WriteAllText(configFilePath, defaultJson);
            }

            string jsonContent = File.ReadAllText(configFilePath);
            
            GenerationConfiguration? gc = JsonSerializer.Deserialize<GenerationConfiguration>(jsonContent);
            if (gc == null) return null;
            Generator.WORLD_PRESET = gc.worldPreset;
            Generator.SEED = gc.seed;
            Generator.WORLD_BORDER_TYPE = gc.worldBorderType;
            Generator.VANILLA = gc.vanilla;
            
            return gc;
        }
        catch (JsonException) {
            Console.WriteLine("Error parsing the config file. Check its format.");
            return null;
        }
    }

    public static void write() {

        GenerationConfiguration configuration = new GenerationConfiguration {
            worldPreset = Generator.WORLD_PRESET,
            worldBorderType = Generator.WORLD_BORDER_TYPE,
            seed = Generator.SEED,
            vanilla = Generator.VANILLA
        };
        
        string folderPath = Path.Combine(Global.WORLD_DIR_PATH, "voxels\\");
        string configFilePath = Path.Combine(folderPath, "config.json");
        
        try {
            // Create the "voxels" folder if it doesn't exist.
            if (!Directory.Exists(folderPath)) {
                Debug.info("creating the '.../voxels/' directory");
                Directory.CreateDirectory(folderPath);
            }

            // Serialize the configuration to JSON.
            string jsonContent = JsonSerializer.Serialize(configuration, new JsonSerializerOptions {
                WriteIndented = true
            });
            
            Debug.info("writing the '.../voxels/config.json' file");

            // Write the JSON content to the config file.
            File.WriteAllText(configFilePath, jsonContent);
        }
        catch (Exception e) {
            Debug.error($"Error writing config file: {e.Message}");
        }
    }
}