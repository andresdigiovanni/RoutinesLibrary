using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RoutinesLibrary.Security
{
    public class SHA256Helper
    {
        public static bool CheckFile_SHA256(string checksum, string sFilePath)
        {
            bool bOK = false;

            if (File.Exists(sFilePath))
            {
                try
                {
                    SHA256 mySHA256 = SHA256Managed.Create();
                    byte[] hashValue = null;
                    FileInfo fInfo = new FileInfo(sFilePath);
                    FileStream fileStream = fInfo.Open(FileMode.Open);

                    fileStream.Position = 0;
                    hashValue = mySHA256.ComputeHash(fileStream);
                    fileStream.Close();
                        
                    bOK = checksum == Encoding.UTF8.GetString(hashValue);
                }
                catch (Exception ex)
                {
                    throw (new Exception(System.Reflection.MethodInfo.GetCurrentMethod().ToString() + " . Error: " + ex.Message));
                }
            }

            return bOK;
        }
    }
}
