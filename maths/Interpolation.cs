//
// VoxelsCore
// by KryKom 2024
//

// with help of BingAI

using Kolors;

namespace VoxelsCoreSharp.maths;


public static class Interpolation {
        
    /// <summary>
    /// interpolates the value at [x, y] from the image argument using the closest interpolation
    /// </summary>
    /// <param name="image">source of the interpolated values</param>
    /// <param name="x">x of the point</param>
    /// <param name="y">y of the point</param>
    /// <returns>interpolated value at [x, y]</returns>
    public static float? closest(ref float?[,] image, float x, float y) {
        if (validateBicubic(x, y, image.GetLength(0), image.GetLength(1))) return null;
            
        return image[(int)Math.Round(x), (int)Math.Round(y)];
    }


    /// <summary>
    /// interpolates the value at [x, y] from the image argument using the bilinear interpolation
    /// </summary>
    /// <param name="image">source of the interpolated values</param>
    /// <param name="x">x of the point</param>
    /// <param name="y">y of the point</param>
    /// <returns>interpolated value at [x, y]</returns>
    public static float? linear(ref float?[,] image, float x, float y) {
        
        if (validateBicubic(x, y, image.GetLength(0), image.GetLength(1))) return null;

        int width = image.GetLength(0);
        int height = image.GetLength(1);

        int x1 = (int)Math.Floor(x);
        int y1 = (int)Math.Floor(y);
        int x2 = x1 + 1;
        int y2 = y1 + 1;

        if (x1 < 0 || x2 >= width || y1 < 0 || y2 >= height)
        {
            return null;
        }

        float? Q11 = image[x1, y1];
        float? Q21 = image[x2, y1];
        float? Q12 = image[x1, y2];
        float? Q22 = image[x2, y2];

        Q11 ??= 0;
        Q21 ??= 0;
        Q12 ??= 0;
        Q22 ??= 0;

        float fracX = x - x1;
        float fracY = y - y1;

        float? R1 = (1 - fracX) * Q11 + fracX * Q21;
        float? R2 = (1 - fracX) * Q12 + fracX * Q22;

        return (1 - fracY) * R1 + fracY * R2;
    }


    /// <summary>
    /// two-dimensional cubic interpolation
    /// </summary>
    /// <param name="image">source values</param>
    /// <param name="x">x-axis position</param>
    /// <param name="y">y-axis position</param>
    public static float? bicubic(ref float?[,] image, float x, float y) {
        
        if (validateBicubic(x, y, image.GetLength(0), image.GetLength(1))) return null;
        
        int width = image.GetLength(0);
        int height = image.GetLength(1);

        int x1 = (int)Math.Floor(x);
        int y1 = (int)Math.Floor(y);

        if (x1 < 1 || x1 >= width - 2 || y1 < 1 || y1 >= height - 2)
        {
            return null;
        }

        float?[] arr = new float?[16];
        for (int i = -1; i <= 2; i++)
        {
            for (int j = -1; j <= 2; j++)
            {
                arr[(i + 1) * 4 + (j + 1)] = image[x1 + i, y1 + j];
            }
        }

        float fracX = x - x1;
        float fracY = y - y1;

        float?[] col = new float?[4];
        for (int i = 0; i < 4; i++)
        {
            col[i] = cubic(arr[i * 4], arr[i * 4 + 1], arr[i * 4 + 2], arr[i * 4 + 3], fracX);
        }

        return cubic(ref col, 0, fracY);
    }


    /// <summary>
    /// one-dimensional cubic interpolation
    /// </summary>
    /// <param name="image">first 4 values taken as input for the
    /// <see cref="cubic(float?, float?, float?, float?, float)"/></param>
    /// <param name="offset">offset in the image argument array</param>
    /// <param name="x">position</param>
    /// <seealso cref="Interpolation.cubic(float?, float?, float?, float?, float)"/>
    public static float? cubic(ref float?[] image, int offset, float x) {
        if (offset + 3 > image.Length) {
            Debug.error("Got invalid input in a cubic interpolation function! " +
                        "Could not calculate the value, returning null.");
            return null;
        }
        return cubic(image[0 + offset], image[1 + offset], image[2 + offset], image[3 + offset], x);
    }

    
    /// <summary>
    /// one-dimensional cubic interpolation
    /// </summary>
    /// <param name="v0">value</param>
    /// <param name="v1">value</param>
    /// <param name="v2">value</param>
    /// <param name="v3">value</param>
    /// <param name="x">position</param>
    public static float? cubic(float? v0, float? v1, float? v2, float? v3, float x) {

        v0 ??= 0;
        v1 ??= 0;
        v2 ??= 0;
        v3 ??= 0;
        
        float A = (float)((v3 - v2) - (v0 - v1));
        float B = (float)((v0 - v1) - A);
        float C = (float)(v2 - v0);
        float D = (float)v1;

        return A * (x * x * x) + B * (x * x) + C * (x) + D;
    }
    
    
    /// <summary>
    /// validates the image inputted into the bilinear function
    /// </summary>
    private static bool validateBilinear(float x, float y, int xSize, int ySize) {
        if (!(x < 0) && !(x > xSize - 1) && !(y < 0) && !(y > ySize - 1)) return true;
            
        Debug.error("Got invalid input in a bilinear interpolation function! " +
                    "Could not calculate the value, returning null.");
        return false;
    }
    
    
    /// <summary>
    /// validates the image inputted into the bicubic function
    /// </summary>
    private static bool validateBicubic(float x, float y, int xSize, int ySize) {
        if (!(x < 1) && !(x > xSize - 2) && !(y < 1) && !(y > ySize - 2) && xSize >= 4 && ySize >= 4) return true;
            
        Debug.error("Got invalid input in a bicubic interpolation function! " +
                    "Could not calculate the value, returning null.");
        return false;
    }
}


/// <summary>
/// types of interpolation for DLA/RDLA and etc. blurring 
/// </summary>
public enum InterpolationType {
    CLOSEST = 0,
    BILINEAR = 1,
    BICUBIC = 2,
    CUSTOM = 3
}