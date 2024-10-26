using System;
using System.IO;

namespace Prepper
{
    class Program
    {
        static void Main(string[] args)
        {
            // Automatically set the root directory to the current directory
            string rootDirectory = Directory.GetCurrentDirectory();

            // Define the folder to look for
            string targetFolderName = "CUCM";

            // Define the subfolders to delete
            string[] subfoldersToDelete = { "ccm", "Common", "Projects", "src", "vos", "Webcontent" };

            // Define the subfolders to keep
            string[] subfoldersToKeep = { "ccmpd", "ccmcip" };

            // Get the target folder path
            string targetFolderPath = Path.Combine(rootDirectory, targetFolderName);

            if (Directory.Exists(targetFolderPath))
            {
                Console.WriteLine($"Found {targetFolderName} folder.");

                // Delete specified subfolders
                foreach (string subfolder in subfoldersToDelete)
                {
                    string subfolderPath = Path.Combine(targetFolderPath, subfolder);
                    if (Directory.Exists(subfolderPath))
                    {
                        Directory.Delete(subfolderPath, true);
                        Console.WriteLine($"Deleted {subfolder} folder.");
                    }
                }

                // Check contents of each subfolder in the CUCM folder
                foreach (string subfolder in Directory.GetDirectories(targetFolderPath))
                {
                    foreach (string innerSubfolder in Directory.GetDirectories(subfolder))
                    {
                        string folderName = Path.GetFileName(innerSubfolder);
                        if (Array.IndexOf(subfoldersToKeep, folderName) == -1)
                        {
                            Directory.Delete(innerSubfolder, true);
                            Console.WriteLine($"Deleted {innerSubfolder} folder inside {subfolder}.");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"{targetFolderName} folder not found.");
            }
        }
    }
}
