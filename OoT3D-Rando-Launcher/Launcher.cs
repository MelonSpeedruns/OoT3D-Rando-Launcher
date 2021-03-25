using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OoT3D_Rando_Launcher
{
    class Launcher
    {
        private static Process ProcessStart(string processName, string romFile)
        {
            ProcessStartInfo pInfo = new ProcessStartInfo();
            pInfo.FileName = processName;
            pInfo.Arguments = romFile;

            return Process.Start(pInfo);
        }

        public static void OpenRandomizer(string citraPath, string romFile, string fromPath, string toPath)
        {
            MessageBox.Show("Please make sure you do the following steps:\n\n1. Set the parameters the way you want.\n2. Select the \"Generate Randomizer\" option.\n3. Select the \"Citra Emulator\" option.\n4. Close Citra.");
            ProcessStart(citraPath, romFile).WaitForExit();
            MoveFiles(fromPath, toPath);
        }

        private static void MoveFiles(string fromPath, string toPath)
        {
            if (!Directory.Exists(fromPath))
            {
                MessageBox.Show("It looks like the seed wasn't generated properly...\n\nMake sure you selected the \"Citra Emulator\" option at the end!");
                return;
            }

            if (!Directory.Exists(toPath))
            {
                Directory.CreateDirectory(toPath);
            }

            foreach (string file in Directory.GetFiles(fromPath))
            {
                File.Move(file, toPath + $@"\{Path.GetFileName(file)}", true);
            }
        }

        public static void OpenGame(string citraPath, string romFile)
        {
            ProcessStart(citraPath, romFile);
        }
    }
}