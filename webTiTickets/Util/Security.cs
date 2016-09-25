using System;
using System.Text;
using System.Security.Cryptography;

namespace webTiTickets.Util
{
    public class Security
    {
        public static String ComputeHash(String sPlaintText, String sHashAlgorithm, byte[] bSaltBytes)
        {
            // If salt is not specified, generate it.
            if (bSaltBytes == null)
            {
                // Define min and max salt sizes.
                const int minSaltSize = 4;
                const int maxSaltSize = 8;

                // Generate a random number for the size of the salt.
                var random = new Random();
                var saltSize = random.Next(minSaltSize, maxSaltSize);

                // Allocate a byte array, which will hold the salt.
                bSaltBytes = new byte[saltSize];

                // Initialize a random number generator.
                var rng = new RNGCryptoServiceProvider();

                // Fill the salt with cryptographically strong byte values.
                rng.GetNonZeroBytes(bSaltBytes);
            }

            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(sPlaintText);

            // Allocate array, which will hold plain text and salt.
            var plainTextWithSaltBytes =
            new byte[plainTextBytes.Length + bSaltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (var i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (var i = 0; i < bSaltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = bSaltBytes[i];

            HashAlgorithm hash;

            // Make sure hashing algorithm name is specified.
            if (sHashAlgorithm == null)
                sHashAlgorithm = "";

            // Initialize appropriate hashing algorithm class.
            switch (sHashAlgorithm.ToUpper())
            {

                case "SHA384":
                    hash = new SHA384Managed();
                    break;

                case "SHA512":
                    hash = new SHA512Managed();
                    break;

                default:
                    hash = new MD5CryptoServiceProvider();
                    break;
            }

            // Compute hash value of our plain text with appended salt.
            var hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            var hashWithSaltBytes = new byte[hashBytes.Length +
            bSaltBytes.Length];

            // Copy hash bytes into resulting array.
            for (var i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (var i = 0; i < bSaltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = bSaltBytes[i];

            // Convert result into a base64-encoded string.
            var hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        public static bool VerifyHash(string sPlainText, string sHashAlgorithm, string sHashValue)
        {

            // Convert base64-encoded hash value into a byte array.
            var hashWithSaltBytes = Convert.FromBase64String(sHashValue);

            // We must know size of hash (without salt).
            int hashSizeInBits;

            // Make sure that hashing algorithm name is specified.
            if (sHashAlgorithm == null)
                sHashAlgorithm = "";

            // Size of hash is based on the specified algorithm.
            switch (sHashAlgorithm.ToUpper())
            {

                case "SHA384":
                    hashSizeInBits = 384;
                    break;

                case "SHA512":
                    hashSizeInBits = 512;
                    break;

                default: // Must be MD5
                    hashSizeInBits = 128;
                    break;
            }

            // Convert size of hash from bits to bytes.
            int hashSizeInBytes = hashSizeInBits / 8;

            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            // Allocate array to hold original salt bytes retrieved from hash.
            var saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            // Copy salt from the end of the hash to the new array.
            for (var i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            // Compute a new hash string.
            var expectedHashString = ComputeHash(sPlainText, sHashAlgorithm, saltBytes);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            return (sHashValue == expectedHashString);
        }
    }
}