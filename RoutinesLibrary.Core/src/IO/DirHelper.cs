using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace RoutinesLibrary.Core.IO
{
    public class DirHelper
    {
        /// <summary>
        /// Copy files from the source folder to destination folder overwriting the content
        /// </summary>
        /// <param name="sourceDirName">Source folder</param>
        /// <param name="destDirName">Destination folder</param>
        /// <remarks>
        /// This method check if is posible to override a file and retry it
        /// </remarks>
        public static void DirectoryCopy(string sourceDirName, string destDirName)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath);
            }
        }
    }
}