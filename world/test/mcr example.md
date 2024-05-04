header (8KiB):
    chunk offset (4KiB, 4b -> int, 1024 entries): 
        = 4 * ((x & 31) + (z & 31) * 32)
        1. - 3. byte: offset
        4. byte: size (in 4KiB), max 1MiB
    chunk timestamp (4KiB, 4b -> int, 1024 entries):
        = last time edited (can be whatever)
        1. - 4. byte: time in seconds

---

payload (multiples of 4KiB):
    1. - 4. byte: length in bytes
    5. byte: compression type (3 = uncompressed nbt)
    nbt
    padding

---

nbt structure:
    int: data version
    int: x
    int: z
    int: y
    string: generation status
        "minecraft:empty" -> everything has to be generated
        "minecraft:structure_starts"
        "minecraft:structure_references"
        "minecraft:biomes"
        "minecraft:noise"
        "minecraft:surface"
        "minecraft:carvers"
        "minecraft:features"
        "minecraft:light"
        "minecraft:spawn" 
        "minecraft:full" -> generation complete
    long: last updated
    list: sections / subchunks
        compound: 
            byte: y position of subchunk
            compound: block states
                list: palette
                    compound: block
                        string: name
                        compound: properties
                            string: name
                long: data, 4096 indices pointing to the block palette
            compound: biomes
                list: palette
                    string: name
                long: data, 64 indices pointing to the block palette
            byte: block light, 2048 bytes - 4096 * 4 bits for block emitted light
            byte: sky light, 2048 bytes - 4096 * 4 bits for sky emitted light
    list: block entities
    compound: carving masks -> for not finished chunks 
        byte: air
        byte: liquid
    compound: height maps
    list: lights, only for proto-chunks
    list: entities, only for proto-chunks
    list: fluid ticks, see https://minecraft.wiki/w/Chunk_format#Block_format
    list: block ticks, see https://minecraft.wiki/w/Chunk_format#Block_format
    long: inhabited time
    compound: blending data
        int: min
        int: max
    list: post processing, only for proto-chunks
    compound: structures, not needed we have a custom generator with no structured