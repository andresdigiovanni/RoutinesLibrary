using System;
using System.IO;
using System.Security.Cryptography;

namespace RoutinesLibrary.Security
{
    public class AesHelper
    {
        public static byte[] Encrypt(byte[] plainText, byte[] Key, byte[] IV)
        {
            // Check arguments
            if (ReferenceEquals(plainText, null) || plainText.Length <= 0)
            {
                throw (new ArgumentNullException("plainText"));
            }

            if (ReferenceEquals(Key, null) || Key.Length <= 0)
            {
                throw (new ArgumentNullException("Key"));
            }

            if (ReferenceEquals(IV, null) || IV.Length <= 0)
            {
                throw (new ArgumentNullException("Key"));
            }

            byte[] encrypted = null;

            // Create an Aes object with the specified key and IV
            using (Aes aesAlg = AesManaged.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Padding = PaddingMode.Zeros;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(plainText, 0, plainText.Length);
                        csEncrypt.Flush();
                        csEncrypt.Close();
                    }

                    encrypted = msEncrypt.ToArray();
                }
            }

            // Return the encrypted bytes from the memory stream
            return encrypted;
        }

        public static byte[] Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments
            if (ReferenceEquals(cipherText, null) || cipherText.Length <= 0)
            {
                throw (new ArgumentNullException("cipherText"));
            }

            if (ReferenceEquals(Key, null) || Key.Length <= 0)
            {
                throw (new ArgumentNullException("Key"));
            }

            if (ReferenceEquals(IV, null) || IV.Length <= 0)
            {
                throw (new ArgumentNullException("Key"));
            }

            // Declare the string used to hold the decrypted text
            byte[] clearBytes = null;

            // Create an Aes object with the specified key and IV
            using (Aes aesAlg = AesManaged.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Padding = PaddingMode.Zeros;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                    {
                        csDecrypt.Write(cipherText, 0, cipherText.Length);
                        csDecrypt.Flush();
                        csDecrypt.Close();
                    }

                    clearBytes = msDecrypt.ToArray();
                }
            }

            return clearBytes;
        }
    }
}
