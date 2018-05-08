using System;
using System.IO;

namespace RoutinesLibrary.Core.IO
{
    public class FileHelper
    {
        public static bool IsFileOpen(string filePath)
        {
            bool isOpen = false;

            try
            {
                FileStream fs = File.OpenWrite(filePath);
                fs.Close();
            }
            catch (IOException)
            {
                isOpen = true;
            }

            return isOpen;
        }
    }
}