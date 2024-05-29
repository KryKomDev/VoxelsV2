# MC Anvil Region file structure (.mcr)

## header (8KiB):
* chunk offset (4KiB, 4b -> int, 1024 entries): 
  * = 4 * ((x & 31) + (z & 31) * 32)
  * byte 1 - 3: offset
  * byte: size (in 4KiB), max 1MiB
* chunk timestamp (4KiB, 4b -> int, 1024 entries):
  * = last time edited (can be whatever)
  * byte 1 - 4: time in seconds

---

## payload (multiples of 4KiB):
 * byte 1 - 4: length in bytes
 * byte 5: compression type (3 = uncompressed nbt)
 * nbt
 * padding

---

## nbt structure:
* int: data version
* int: x
* int: z
* int: y
* string: generation status
  * "minecraft:empty" -> everything has to be generated
  * "minecraft:structure_starts"
  * "minecraft:structure_references"
  * "minecraft:biomes"
  * "minecraft:noise"
  * "minecraft:surface"
  * "minecraft:carvers"
  * "minecraft:features"
  * "minecraft:light"
  * "minecraft:spawn" 
  * "minecraft:full" -> generation complete
* long: last updated
* list: sections / subchunks
  * compound: 
      * byte: y position of subchunk
      * compound: block states
          * list: palette
            * compound: block
              * string: name
              * compound: properties
                * string: name
          * long: data, 4096 indices pointing to the block palette
      * compound: biomes
          * list: palette
              * string: name
          * long: data, 64 indices pointing to the block palette
      * byte: block light, 2048 bytes - 4096 * 4 bits for block emitted light
      * byte: sky light, 2048 bytes - 4096 * 4 bits for sky emitted light
  * list: block entities
  * compound: carving masks -> for not finished chunks 
    * byte: air
    * byte: liquid
  * compound: height maps
  * list: lights, only for proto-chunks
  * list: entities, only for proto-chunks
  * list: fluid ticks, see https://minecraft.wiki/w/Chunk_format#Block_format
  * list: block ticks, see https://minecraft.wiki/w/Chunk_format#Block_format
  * long: inhabited time
  * compound: blending data
    * int: min
    * int: max
  * list: post-processing, only for proto-chunks
  * compound: structures, not needed we have a custom generator with no structured

<style>

h1 {
    color: dodgerblue;
}

h2 {
    color: deepskyblue;
}

a {
    color: #56a8f5;
    text-decoration: underline;
}
</style>