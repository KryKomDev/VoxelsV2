//
// VoxelsCoreSharp
// by KryKom 2024
//

namespace VoxelsCoreSharp.world;

/// <summary>
/// enum of biome types <br/>
/// used for overall biome data storing
/// </summary>
public enum BiomeType {
    
    UNKNOWN = 0, // not set

    // tropical dry
    TROPICAL_DRY = 1,
    TROPICAL_DRY_DESERT_SANDY = 2,
    TROPICAL_DRY_DESERT_STONY = 3,
    TROPICAL_DRY_DESERT_GRAVEL = 4,
    TROPICAL_DRY_DESERT_TERRACOTTA = 5,
    TROPICAL_DRY_SAVANNA = 6,

    // tropical rainy
    TROPICAL_RAINY = 10,
    TROPICAL_RAINY_JUNGLE = 11,
    TROPICAL_RAINY_BAMBOO_JUNGLE = 12,
    TROPICAL_RAINY_PLAINS = 13,

    // subtropical
    SUBTROPICAL = 20,
    SUBTROPICAL_CONIFEROUS_FOREST = 21,
    SUBTROPICAL_BROADLEAF_FOREST = 22,
    SUBTROPICAL_SAVANNA = 23,
    SUBTROPICAL_BUSH = 24,
    SUBTROPICAL_WASTELAND = 25,

    // temperate inland
    TEMPERATE_INLAND = 30,
    TEMPERATE_INLAND_CONIFEROUS_FOREST = 31,
    TEMPERATE_INLAND_BROADLEAF_FOREST = 32,
    TEMPERATE_INLAND_PLAINS = 33,
    TEMPERATE_INLAND_MEADOW = 34,
    TEMPERATE_INLAND_CHERRY_FOREST = 35,
    TEMPERATE_INLAND_MANGROVE_FOREST = 36,
    TEMPERATE_INLAND_BIRCH_FOREST = 37,
    TEMPERATE_INLAND_BEECH_FOREST = 38,

    // temperate oceanic
    TEMPERATE_OCEANIC = 40,
    TEMPERATE_OCEANIC_CONIFEROUS_FOREST = 41,
    TEMPERATE_OCEANIC_BROADLEAF_FOREST = 42,
    TEMPERATE_OCEANIC_PLAINS = 43,
    TEMPERATE_OCEANIC_THORNS = 44,

    // subpolar
    SUBPOLAR = 50,
    SUBPOLAR_FOREST = 51,
    SUBPOLAR_PLAINS = 52,
    SUBPOLAR_TAIGA = 53,

    // polar
    POLAR = 60,
    POLAR_TAIGA = 61,
    POLAR_HIGHLAND = 62,
    POLAR_TUNDRA = 63,
    POLAR_ICEBERG = 64,

    // ocean
    OCEAN = 70,
    OCEAN_WARM = 71,
    OCEAN_SUBTROPICAL = 72,
    OCEAN_TEMPERATE = 73,
    OCEAN_SUBPOLAR = 74,
    OCEAN_POLAR = 75
}