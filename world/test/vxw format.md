# VOXEL WORLD format (.vxw)

## header - 16 bytes
* three characters containing letters vxr (3 bytes)
* size of a chunk in voxels (ushort)
* region size in chunks (ushort)
* maximum height in sub-chunks (uint)
* world size in regions (uint)
* biome dimension (bool, 2d -> false, 3d -> true)

## body
* array (int) of region paddings, length of worldSize ^ 2 (pos in array = x * worldSize + y)
* region:
  * chunk (pos padding = x * regionSize + y):
    * state (byte) (0 -> not generated, 1 -> partially generated, 2 -> fully generated)
    * climate biome (byte)
    * subchunk array (bottom to top):
      * biome (byte)
      * voxels (byte)
      * x-  ->  x+
      * y-  ->  y+
      * z-  ->  z+


<!-- style -->
<style>

@import url('https://fonts.googleapis.com/css2?family=Space+Grotesk:wght@300..700&display=swap');

* {
    font-family: "Space Grotesk", sans-serif;
}

h1 {
    color: #588157;
}

h2 {
    color: #A3B18A;
}

a {
    color: #3A5A40;
}
</style>