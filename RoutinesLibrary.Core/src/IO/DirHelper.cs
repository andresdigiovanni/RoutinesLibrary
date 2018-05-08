using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace RoutinesLibrary.Core.IO
{
    public class DirHelper
    {
        private const int TIME_WAIT_UNLOCK_FILE_SO = 1000;
        private const int TRIES_WAIT_UNLOCK_FILE_SO = 30;


        public static bool CreateDirectory(string dirName)
        {
            if (dirName == "")
            {
                return false;
            }

            if (!Directory.Exists(dirName))
            {
                try
                {
                    Directory.CreateDirectory(dirName);
                }
                catch (Exception) { }
            }

            return Directory.Exists(dirName);
        }

        /// <summary>
        /// Copy files from the source folder to destination folder overwriting the content
        /// </summary>
        /// <param name="source">Source folder</param>
        /// <param name="destination">Destination folder</param>
        /// <remarks>
        /// This method check if is posible to override a file and retry it
        /// </remarks>
        public static bool CopyDirectory(string source, string destination)
        {
            bool bOk = true;

            try
            {
                //Recursively copy directories
                foreach (string sourceSubDir in Directory.GetDirectories(source))
                {
                    string destSubDir = Path.Combine(destination, sourceSubDir.Split(Path.DirectorySeparatorChar).Last());

                    CreateDirectory(destSubDir);
                    bOk &= CopyDirectory(sourceSubDir, destSubDir);
                }

                //Go ahead and copy each file in "source" to the "destination" directory
                foreach (string fileDir in Directory.GetFiles(source))
                {
                    int triesRemaining = TRIES_WAIT_UNLOCK_FILE_SO;

                    //We verify that the OS has closed the file in order to override it
                    while (triesRemaining > 0)
                    {
                        if (!FileHelper.IsFileOpen(Path.Combine(destination, Path.GetFileName(fileDir))))
                        {
                            break;
                        }

                        Thread.Sleep(TIME_WAIT_UNLOCK_FILE_SO);
                        triesRemaining--;
                    }

                    File.Copy(Path.Combine(source, Path.GetFileName(fileDir)),
                              Path.Combine(destination, Path.GetFileName(fileDir)));
                }
            }
            catch (Exception)
            {
                bOk = false;
            }

            return bOk;
        }
    }
}