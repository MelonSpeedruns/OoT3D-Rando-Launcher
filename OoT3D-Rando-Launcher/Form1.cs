using System;
using System.IO;
using System.Windows.Forms;

namespace OoT3D_Rando_Launcher
{
    public partial class Form1 : Form
    {
        private string emuPath;
        private string romPath;
        private string randomizerRomPath;

        private string filesPath = Path.Combine("user", "sdmc", "luma", "titles", "0004000000033500");
        private string modPath = Path.Combine("user", "load", "mods", "0004000000033500");

        public Form1()
        {
            InitializeComponent();
            RestoreSavedValues();
        }

        public void SaveValues()
        {
            Properties.Settings.Default.emuPath = textBox1.Text;
            Properties.Settings.Default.romPath = textBox2.Text;
            Properties.Settings.Default.randomizerRomPath = textBox3.Text;
            Properties.Settings.Default.Save();
        }

        public void RestoreSavedValues()
        {
            emuPath = Properties.Settings.Default.emuPath;
            textBox1.Text = emuPath;


            romPath = Properties.Settings.Default.romPath;
            textBox2.Text = romPath;

            randomizerRomPath = Properties.Settings.Default.randomizerRomPath;
            textBox3.Text = randomizerRomPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Select Emulator Path

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    foreach (string file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file) == "citra-qt")
                        {
                            emuPath = file;
                            textBox1.Text = emuPath;
                            SaveValues();
                            return;
                        }
                    }

                    MessageBox.Show("citra-qt.exe was not found in that folder, cancelling!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Select OoT3D ROM Path

            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "3DS Rom files (*.3ds, *.cia, *.cxi) | *.3ds; *.cia; *.cxi";

                DialogResult result = ofd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(ofd.FileName))
                {
                    romPath = ofd.FileName;
                    textBox2.Text = romPath;
                    SaveValues();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Select OoT3D Randomizer Rom Path

            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "3dsx files (*.3dsx) | *.3dsx";

                DialogResult result = ofd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(ofd.FileName))
                {
                    randomizerRomPath = ofd.FileName;
                    textBox3.Text = randomizerRomPath;
                    SaveValues();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string emuFolder = Path.GetDirectoryName(emuPath);
            Launcher.OpenRandomizer(emuPath, randomizerRomPath, Path.Combine(emuFolder, filesPath), Path.Combine(emuFolder, modPath));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Launcher.OpenGame(emuPath, romPath);
        }
    }
}