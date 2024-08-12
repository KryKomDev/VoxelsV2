//
// made for VoxelsCoreSharp
// by KryKom and ChatGPT
//

using System.Security.Cryptography;
using System.Text;

namespace VoxelsCoreSharp.libs;

public static class Hash {
    
    public static int GetHashCode(this string input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input));

        // Serialize the string to a byte array (using UTF-8 encoding)
        byte[] bytes = Encoding.UTF8.GetBytes(input);

        // Compute the hash code using SHA-256
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(bytes);
            // Take the first 4 bytes of the hash as the integer hash code
            return BitConverter.ToInt32(hashBytes, 0);
        }
    }
}