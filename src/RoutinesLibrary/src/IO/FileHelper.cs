using System;
using System.IO;

namespace RoutinesLibrary.IO
{
    public class FileHelper
    {
        public static bool IsFileOpen(string filePath, FileAccess fileAccess = FileAccess.ReadWrite)
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, fileAccess);
                fs.Close();
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }
    }
}