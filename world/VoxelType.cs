namespace VoxelsCoreSharp.world;

// DISCONTINUED !!!

public enum VoxelType {
    UNKNOWN = 0, // can be overwritten by a structure
    AIR = 1,  // cannot be overwritten by a structure

    // stone blocks
    STONE = 2,
    COBBLESTONE = 3,
    GRAVEL = 4,
    GRANITE = 5,
    DIORITE = 6,
    ANDESITE = 7,
    BASALT = 8,
    TUFF = 9,
    DEEPSLATE = 10,
    SAND = 11,
    SANDSTONE = 12,
    MUD = 13,
    LIMESTONE = 14,
    CALCITE = 15,
    DOLOMITE = 16,
    DIRT = 17,
    IRON_ORE = 18,
    COPPER_ORE = 19,
    COAL_ORE = 20,
    DIAMOND_ORE = 21,
    EMERALD_ORE = 22,
    RUBY_ORE = 23, // = redstone ore
    GOLD_ORE = 24,
    LAPIS_ORE = 25,
    DEEPSLATE_IRON_ORE = 26, 
    DEEPSLATE_COPPER_ORE = 27,
    DEEPSLATE_COAL_ORE = 28,
    DEEPSLATE_DIAMOND_ORE = 29,
    DEEPSLATE_EMERALD_ORE = 30, 
    DEEPSLATE_RUBY_ORE = 31, // = deepslate redstone ore
    DEEPSLATE_GOLD_ORE = 32,
    DEEPSLATE_LAPIS_ORE = 33,

    // organic blocks
    MOSS = 40,
    GRASS = 41,
    PODZOL = 42,

    OAK_WOOD = 43,
    OAK_LEAVES = 44,
    BIRCH_WOOD = 45,
    BIRCH_LEAVES = 46,
    CHERRY_WOOD = 47,
    CHERRY_LEAVES = 48,
    POPLAR_WOOD = 49,
    POPLAR_LEAVES = 50,
    LINDEN_WOOD = 51,
    LINDEN_LEAVES = 52,
    BEECH_WOOD = 53,
    BEECH_LEAVES = 54,
    ACACIA_WOOD = 55,
    ACACIA_LEAVES = 56,
    JUNGLE_WOOD = 57,
    JUNGLE_LEAVES = 58,
    DARK_OAK_WOOD = 59,
    DARK_OAK_LEAVES = 60,
    MANGROVE_WOOD = 61,
    MANGROVE_LEAVES = 62,
    AZALEA_WOOD = 63,
    AZALEA_LEAVES = 64,
    SPRUCE_WOOD = 65,
    SPRUCE_LEAVES = 66,
    PINE_WOOD = 67,
    PINE_LEAVES = 68,
    GIANT_SEQUOIA_WOOD = 69,
    GIANT_SEQUOIA_LEAVES = 70,

    // other blocks
    WATER = 90,
    ICE = 91,
    SNOW = 92,
}