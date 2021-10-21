using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CE_Redef
{
    public partial class Window : Form
    {
        private IniFile config;
        private string ceDir, backupPath;
        private double bulkMult = -1234,
            carryBulkMult = -1234,
            wornBulkMult = -1234,
            reloadSpeedMult = -1234,
            reloadTimeMult = -1234,
            rangedCooldownMult = -1234,
            meleeCooldownMult = -1234,
            warmupMult = -1234;
        private string mode = "backup",
            ceVersion,
            runButtonTextQueue,
            backupVersion;
        private int progressBarMaxQueue = -1,
            progressBarValueQueue = -1;
        private bool executeFinished;

        public Window()
        {
            InitializeComponent();
        }

        // Window loaded
        private void Window_Load(object sender, EventArgs e)
        {
            config = new IniFile("config.ini");
            ceDir = FixPath(config.ReadOrDefault("dirs", "cedir", @"C:\Program Files\Steam\steamapps\workshop\content\294100\1631756268\"), true);
            backupPath = FixPath(config.ReadOrDefault("dirs", "backuppath", new FileInfo(@"data\backup.txt").FullName), false);
            mode = config.ReadOrDefault("state", "mode", "backup");
            switch (mode)
            {
                default:
                    mode = "backup";
                    backupButton.Checked = true;
                    break;

                case "backup":
                    backupButton.Checked = true;
                    break;

                case "restore":
                    restoreButton.Checked = true;
                    break;

                case "redefine":
                    redefineButton.Checked = true;
                    break;

            }
            ceDirTB.Text = ceDir;
            backupPathTB.Text = backupPath;
            bulkMult = config.ReadOrDefault("multipliers", "bulk", 1.0);
            bulkTB.Text = bulkMult.ToString();
            carryBulkMult = config.ReadOrDefault("multipliers", "carrybulk", 1.0);
            carryBulkTB.Text = carryBulkMult.ToString();
            wornBulkMult = config.ReadOrDefault("multipliers", "wornbulk", 1.0);
            wornBulkTB.Text = wornBulkMult.ToString();
            reloadSpeedMult = config.ReadOrDefault("multipliers", "reloadspeed", 1.0);
            reloadSpeedTB.Text = reloadSpeedMult.ToString();
            reloadTimeMult = config.ReadOrDefault("multipliers", "reloadtime", 1.0);
            reloadTimeTB.Text = reloadTimeMult.ToString();
            rangedCooldownMult = config.ReadOrDefault("multipliers", "rangedcooldown", 1.0);
            rangedCooldownTB.Text = rangedCooldownMult.ToString();
            meleeCooldownMult = config.ReadOrDefault("multipliers", "meleecooldown", 1.0);
            meleeCooldownTB.Text = meleeCooldownMult.ToString();
            warmupMult = config.ReadOrDefault("multipliers", "warmup", 1.0);
            warmupTB.Text = warmupMult.ToString();
            SetRunButton();
        }

        // Normalizes the path
        private string FixPath(string path, bool isDir)
        {
            try
            {
                string newPath = new FileInfo(path).FullName;
                if (isDir && !newPath.EndsWith("\\"))
                {
                    newPath += '\\';
                }
                return newPath;
            }
            catch
            {
                return path;
            }
        }

        private void BackupButton_CheckedChanged(object sender, EventArgs e)
        {
            if (backupButton.Checked)
            {
                mode = "backup";
                config.Write("state", "mode", mode);
                SetRunButton();
            }
        }

        private void RestoreButton_CheckedChanged(object sender, EventArgs e)
        {
            if (restoreButton.Checked)
            {
                mode = "restore";
                config.Write("state", "mode", mode);
                SetRunButton();
            }
        }

        private void RedefineButton_CheckedChanged(object sender, EventArgs e)
        {
            if (redefineButton.Checked)
            {
                mode = "redefine";
                config.Write("state", "mode", mode);
                SetRunButton();
            }
        }

        private void BulkTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bulkMult = double.Parse(bulkTB.Text);
                config.Write("multipliers", "bulk", bulkMult);
            }
            catch
            {
                bulkMult = -1234;
            }
            SetRunButton();
        }

        private void CarryBulkTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                carryBulkMult = double.Parse(carryBulkTB.Text);
                config.Write("multipliers", "carrybulk", carryBulkMult);
            }
            catch
            {
                carryBulkMult = -1234;
            }
            SetRunButton();
        }

        private void WornBulkTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                wornBulkMult = double.Parse(wornBulkTB.Text);
                config.Write("multipliers", "wornbulk", wornBulkMult);
            }
            catch
            {
                wornBulkMult = -1234;
            }
            SetRunButton();
        }

        private void ReloadSpeedTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                reloadSpeedMult = double.Parse(reloadSpeedTB.Text);
                config.Write("multipliers", "reloadspeed", reloadSpeedMult);
            }
            catch
            {
                reloadSpeedMult = -1234;
            }
            SetRunButton();
        }

        private void ReloadTimeTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                reloadTimeMult = double.Parse(reloadTimeTB.Text);
                config.Write("multipliers", "reloadtime", reloadTimeMult);
            }
            catch
            {
                reloadTimeMult = -1234;
            }
            SetRunButton();
        }

        private void RangedCooldownTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rangedCooldownMult = double.Parse(rangedCooldownTB.Text);
                config.Write("multipliers", "rangedcooldown", rangedCooldownMult);
            }
            catch
            {
                rangedCooldownMult = -1234;
            }
            SetRunButton();
        }

        private void MeleeCooldownTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                meleeCooldownMult = double.Parse(meleeCooldownTB.Text);
                config.Write("multipliers", "meleecooldown", meleeCooldownMult);
            }
            catch
            {
                meleeCooldownMult = -1234;
            }
            SetRunButton();
        }

        private void WarmupTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                warmupMult = double.Parse(warmupTB.Text);
                config.Write("multipliers", "warmup", warmupMult);
            }
            catch
            {
                warmupMult = -1234;
            }
            SetRunButton();
        }

        // Choose Combat Extended directory
        private void CeDirButton_Click(object sender, EventArgs e)
        {
            FolderPicker dialog = new FolderPicker();
            string fixedDir = FixPath(ceDirTB.Text, true);
            if (Directory.Exists(fixedDir))
            {
                // Set initial browser location to the path in the textbox
                dialog.InputPath = fixedDir;
            }
            else if (Directory.Exists(ceDir))
            {
                // Set initial browser location to existing Combat Extended directory
                dialog.InputPath = ceDir;
            }
            if (dialog.ShowDialog() == true)
            {
                // Folder selected
                ceDir = dialog.ResultPath + '\\';
                ceDirTB.Text = ceDir;
                config.Write("dirs", "cedir", ceDir);
                ceVersion = null;
                SetRunButton();
            }
        }

        // Choose backup output
        private void BackupPathButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "txt files (*.txt)|*.txt",
                RestoreDirectory = true
            };
            string fixedPath = FixPath(backupPathTB.Text, false);
            fixedPath = fixedPath.Substring(0, fixedPath.LastIndexOf('\\'));
            if (Directory.Exists(fixedPath))
            {
                dialog.InitialDirectory = fixedPath;
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Output file selected
                backupPath = dialog.FileName;
                backupPathTB.Text = backupPath;
                config.Write("dirs", "backuppath", backupPath);
                backupVersion = null;
                SetRunButton();
            }
        }

        // Try to set the Combat Extended if textbox changed
        private void CeDirTB_TextChanged(object sender, EventArgs e)
        {
            ceDir = FixPath(ceDirTB.Text, true);
            if (CheckCEDir(ceDir))
            {
                // Set Combat Extended dir to this
                config.Write("dirs", "cedir", ceDir);
                ceVersion = null;
            }
            SetRunButton();
        }

        // Try to set the backup output if textbox changed
        private void BackupPathTB_TextChanged(object sender, EventArgs e)
        {
            backupPath = FixPath(backupPathTB.Text, false);
            if (CheckBackup(backupPath))
            {
                // Set backup output path to this
                config.Write("dirs", "backuppath", backupPath);
                backupVersion = null;
            }
            SetRunButton();
        }

        // Check if the Combat Extended directory is valid
        private bool CheckCEDir(string dir)
        {
            return Directory.Exists(dir) &&
                Directory.Exists(dir + @"Patches\") &&
                Directory.Exists(dir + @"Defs\") &&
                File.Exists(dir + @"About\Manifest.xml");
        }

        // Check if the backup output is valid
        private bool CheckBackup(string path)
        {
            if (!path.EndsWith(".txt"))
            {
                return false;
            }
            try
            {
                _ = new FileInfo(path); // Check if path is valid
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Enables or disables the run button
        private void SetRunButton()
        {
            // Check for valid Combat Extended directory
            if (!CheckCEDir(ceDir))
            {
                runButton.Text = "Invalid CE Directory";
                runButton.Enabled = false;
                return;
            }
            // Check for valid backup file type
            if (!CheckBackup(backupPath))
            {
                runButton.Text = "Invalid Backup Path";
                runButton.Enabled = false;
                return;
            }
            // Check for valid bulk multiplier
            if (bulkMult == -1234)
            {
                runButton.Text = "Invalid Bulk Multiplier";
                runButton.Enabled = false;
                return;
            }
            // Check for valid carry bulk multiplier
            if (carryBulkMult == -1234)
            {
                runButton.Text = "Invalid Carry Bulk Multiplier";
                runButton.Enabled = false;
                return;
            }
            // Check for valid worn bulk multiplier
            if (wornBulkMult == -1234)
            {
                runButton.Text = "Invalid Worn Bulk Multiplier";
                runButton.Enabled = false;
                return;
            }
            // Check for valid reload speed multiplier
            if (reloadSpeedMult == -1234)
            {
                runButton.Text = "Invalid Reload Speed Multiplier";
                runButton.Enabled = false;
                return;
            }
            // Check for valid reload time multiplier
            if (reloadTimeMult == -1234)
            {
                runButton.Text = "Invalid Reload Time Multiplier";
                runButton.Enabled = false;
                return;
            }
            // Check for valid ranged cooldown multiplier
            if (rangedCooldownMult == -1234)
            {
                runButton.Text = "Invalid Ranged Cooldown Multiplier";
                runButton.Enabled = false;
                return;
            }
            // Check for valid melee cooldown multiplier
            if (meleeCooldownMult == -1234)
            {
                runButton.Text = "Invalid Melee Cooldown Multiplier";
                runButton.Enabled = false;
                return;
            }
            // Check for valid warmup multiplier
            if (warmupMult == -1234)
            {
                runButton.Text = "Invalid Bulk Multiplier";
                runButton.Enabled = false;
                return;
            }

            // Check if backup file exists
            bool backupExists = backupPath.EndsWith(".txt") && File.Exists(backupPath);

            switch (mode)
            {
                default:
                case "backup":
                    if (backupExists)
                    {
                        // Backup file exists
                        try
                        {
                            // Check if the version of the backup matches the version of Combat Extended
                            string backupVersion = GetBackupVersion();
                            if (backupVersion == "NULL")
                            {
                                // Backup file has no version
                                runButton.Text = $"Backup (Overwrite existing backup) (Backup version is missing)";
                                runButton.Enabled = true;
                            }
                            else if (backupVersion != GetCEVersion())
                            {
                                // Backup version and current Combat Extended version do not match
                                runButton.Text = $"Backup (Overwrite existing backup) (Backup version is {backupVersion} but CE is {GetCEVersion()})";
                                runButton.Enabled = true;
                            }
                            else
                            {
                                // Backup version and Combat Extended version match
                                runButton.Text = "Backup (Overwrite existing backup)";
                                runButton.Enabled = true;
                            }
                        }
                        catch
                        {
                            // Error
                            runButton.Text = "Could not parse existing backup file";
                            runButton.Enabled = false;
                        }
                    }
                    else
                    {
                        // Backup file does not exist
                        runButton.Text = "Backup";
                        runButton.Enabled = true;
                    }
                    break;

                case "restore":
                    if (backupExists)
                    {
                        // Backup file exists
                        try
                        {
                            // Check if the version of the backup matches the version of Combat Extended
                            string backupVersion = GetBackupVersion();
                            if (backupVersion == "NULL")
                            {
                                // Backup file has no version
                                runButton.Text = $"Restore (Backup version is missing)";
                                runButton.Enabled = true;
                            }
                            else if (backupVersion != GetCEVersion())
                            {
                                // Backup version and current Combat Extended version do not match
                                runButton.Text = $"Backup version is {backupVersion} but CE is {GetCEVersion()}";
                                runButton.Enabled = false;
                            }
                            else
                            {
                                // Backup version and Combat Extended version match
                                runButton.Text = "Restore";
                                runButton.Enabled = true;
                            }
                        }
                        catch
                        {
                            // Error
                            runButton.Text = "Could not parse backup file";
                            runButton.Enabled = false;
                        }
                    }
                    else
                    {
                        // Backup file does not exist
                        runButton.Text = "Backup file does not exist";
                        runButton.Enabled = false;
                    }
                    break;

                case "redefine":
                    if (backupExists)
                    {
                        // Backup file exists
                        try
                        {
                            // Check if the version of the backup matches the version of Combat Extended
                            string backupVersion = GetBackupVersion();
                            if (backupVersion == "NULL")
                            {
                                // Backup file has no version
                                runButton.Text = $"Redefine (Backup version is missing)";
                                runButton.Enabled = true;
                            }
                            else if (backupVersion != GetCEVersion())
                            {
                                // Backup version and current Combat Extended version do not match
                                runButton.Text = $"Backup version is {backupVersion} but CE is {GetCEVersion()}";
                                runButton.Enabled = false;
                            }
                            else
                            {
                                // Backup version and Combat Extended version match
                                runButton.Text = "Redefine";
                                runButton.Enabled = true;
                            }
                        }
                        catch
                        {
                            // Error
                            runButton.Text = "Could not parse backup file";
                            runButton.Enabled = false;
                        }
                    }
                    else
                    {
                        // Backup file does not exist
                        runButton.Text = "Backup file does not exist";
                        runButton.Enabled = false;
                    }
                    break;
            }
        }

        private string GetCEVersion()
        {
            if (!(ceVersion is null))
            {
                return ceVersion;
            }
            try
            {
                string manifestPath = ceDir + @"About\Manifest.xml";
                if (File.Exists(manifestPath))
                {
                    using (StreamReader reader = new StreamReader(manifestPath))
                    {
                        string line;
                        Regex rx = new Regex(@"(?<=<version>)[0-9.]*(?=<\/version>)");
                        while ((line = reader.ReadLine()) != null)
                        {
                            Match match = rx.Match(line);
                            if (match.Success)
                            {
                                ceVersion = match.ToString();
                                return ceVersion;
                            }
                        }
                    }
                }
            }
            catch
            {
                return "NULL";
            }
            return "NULL";
        }

        private string GetBackupVersion()
        {
            if (backupVersion != null)
            {
                return backupVersion;
            }
            try
            {
                using (BinaryReader reader = new BinaryReader(new FileStream(backupPath, FileMode.Open)))
                {
                    int versionLength = reader.ReadInt32();
                    backupVersion = Encoding.ASCII.GetString(reader.ReadBytes(versionLength));
                }
                return backupVersion;
            }
            catch
            {
                return "NULL";
            }
        }

        private void ToggleInteractability(bool enabled)
        {
            ceDirButton.Enabled = enabled;
            ceDirTB.Enabled = enabled;
            backupPathButton.Enabled = enabled;
            backupPathTB.Enabled = enabled;
            backupButton.Enabled = enabled;
            restoreButton.Enabled = enabled;
            redefineButton.Enabled = enabled;
            bulkTB.Enabled = enabled;
            carryBulkTB.Enabled = enabled;
            wornBulkTB.Enabled = enabled;
            reloadSpeedTB.Enabled = enabled;
            reloadTimeTB.Enabled = enabled;
            rangedCooldownTB.Enabled = enabled;
            meleeCooldownTB.Enabled = enabled;
            warmupTB.Enabled = enabled;
        }

        private void UpdateForm_Tick(object sender, EventArgs e)
        {
            if (runButtonTextQueue != null)
            {
                runButton.Text = runButtonTextQueue;
                runButtonTextQueue = null;
            }
            if (progressBarMaxQueue != -1)
            {
                progressBar.Maximum = progressBarMaxQueue;
                progressBarMaxQueue = -1;
            }
            progressBar.Value = progressBarValueQueue;
            if (executeFinished)
            {
                executeFinished = false;
                ToggleInteractability(true);
                SetRunButton();
                updateForm.Stop();
            }
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            ToggleInteractability(false);
            runButton.Enabled = false;
            runButton.Text = "Starting...";
            progressBarValueQueue = 0;
            progressBar.Value = 0;
            updateForm.Start();
            execute.RunWorkerAsync();
        }

        private void Execute_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                runButtonTextQueue = "Finding files...";

                // Get files
                IEnumerable<string> files = Directory.GetFiles(ceDir + @"Defs\", "*.xml", SearchOption.AllDirectories).Concat(
                    Directory.GetFiles(ceDir + @"Patches\", "*.xml", SearchOption.AllDirectories));
                progressBarValueQueue = 0;
                progressBarMaxQueue = files.Count();

                // Regexes for finding numbers in specific tags
                Regex[] getNums = new Regex[] {
                    new Regex(@"(?<=<Bulk>\s*)[+-]?([0-9]*[.])?[0-9]+(?=\s*<\/Bulk>)"),
                    new Regex(@"(?<=<CarryBulk>\s*)[+-]?([0-9]*[.])?[0-9]+(?=\s*<\/CarryBulk>)"),
                    new Regex(@"(?<=<WornBulk>\s*)[+-]?([0-9]*[.])?[0-9]+(?=\s*<\/WornBulk>)"),
                    new Regex(@"(?<=<ReloadSpeed>\s*)[+-]?([0-9]*[.])?[0-9]+(?=\s*<\/ReloadSpeed>)"),
                    new Regex(@"(?<=<reloadTime>\s*)[+-]?([0-9]*[.])?[0-9]+(?=\s*<\/reloadTime>)"),
                    new Regex(@"(?<=<RangedWeapon_Cooldown>\s*)[+-]?([0-9]*[.])?[0-9]+(?=\s*<\/RangedWeapon_Cooldown>)"),
                    new Regex(@"(?<=<cooldownTime>\s*)[+-]?([0-9]*[.])?[0-9]+(?=\s*<\/cooldownTime>)"),
                    new Regex(@"(?<=<warmupTime>\s*)[+-]?([0-9]*[.])?[0-9]+(?=\s*<\/warmupTime>)")
                };
                Regex num = new Regex(@"[+-]?([0-9]*[.])?[0-9]+");

                // Multipliers put into array for easy iteration later
                double[] multipliers = new double[] { bulkMult, carryBulkMult, wornBulkMult, reloadSpeedMult, reloadTimeMult, rangedCooldownMult, meleeCooldownMult, warmupMult };

                switch (mode)
                {
                    default:
                    case "backup":
                        // Backup targeted stats to backup file
                        runButtonTextQueue = "Backing up";

                        List<byte> bytes = new List<byte>();
                        string version = GetCEVersion();
                        bytes.AddRange(BitConverter.GetBytes(version.Length));
                        bytes.AddRange(Encoding.ASCII.GetBytes(version));

                        // Iterate through files
                        foreach (string file in files)
                        {
                            try
                            {
                                string section = file.Substring(ceDir.Length + 1);
                                string data = File.ReadAllText(file);

                                // Iterate through multipliers
                                foreach (Regex tag in getNums)
                                {
                                    int offsetOpen = data.IndexOf("<equippedStatOffsets>");
                                    int offsetClose = -1;
                                    if (offsetOpen != -1)
                                    {
                                        offsetClose = data.IndexOf("</equippedStatOffsets>", offsetOpen + 21);
                                        if (offsetClose == -1)
                                        {
                                            offsetOpen = -1;
                                        }
                                    }

                                    // Iterate through matches
                                    foreach (Match match in tag.Matches(data))
                                    {
                                        if (offsetOpen == -1)
                                        {
                                            // No offset tags
                                            bytes.AddRange(BitConverter.GetBytes(double.Parse(match.ToString())));
                                        }
                                        else
                                        {
                                            // Offset tags exist
                                            do
                                            {
                                                if (match.Index < offsetOpen)
                                                {
                                                    // This is before the opening offset tag
                                                    bytes.AddRange(BitConverter.GetBytes(double.Parse(match.ToString())));
                                                    break;
                                                }
                                                else if (match.Index > offsetClose)
                                                {
                                                    // This is after the closing offset tag so find new pair
                                                    offsetOpen = data.IndexOf("<equippedStatOffsets>", offsetClose + 22);
                                                    if (offsetOpen == -1)
                                                    {
                                                        // No more offset tags
                                                        offsetClose = -1;
                                                        bytes.AddRange(BitConverter.GetBytes(double.Parse(match.ToString())));
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        // Found opening offset tag
                                                        offsetClose = data.IndexOf("</equippedStatOffsets>", offsetOpen + 21);
                                                        if (offsetClose == -1)
                                                        {
                                                            // No closing offset tag
                                                            offsetOpen = -1;
                                                            bytes.AddRange(BitConverter.GetBytes(double.Parse(match.ToString())));
                                                            break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // This is inside an offset tag so ignore it
                                                    break;
                                                }
                                            } while (true);
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                progressBarValueQueue++;
                            }
                        }


                        // Write to backup file
                        runButtonTextQueue = "Saving...";
                        try
                        {
                            File.Delete(backupPath);
                        }
                        catch { }
                        Directory.CreateDirectory(backupPath.Substring(0, backupPath.LastIndexOf('\\')));
                        File.WriteAllBytes(backupPath, bytes.ToArray());
                        break;

                    case "restore":
                        // Restore targeted stats from backup file
                        runButtonTextQueue = "Restoring";

                        using (BinaryReader reader = new BinaryReader(new FileStream(backupPath, FileMode.Open)))
                        {
                            // Move reader to start of data
                            int versionLength = reader.ReadInt32();
                            reader.ReadBytes(versionLength);

                            // Iterate through files
                            foreach (string file in files)
                            {
                                try
                                {
                                    string section = file.Substring(ceDir.Length + 1);
                                    string data = File.ReadAllText(file);

                                    // Iterate through multipliers
                                    foreach (Regex tag in getNums)
                                    {
                                        int offsetOpen = data.IndexOf("<equippedStatOffsets>");
                                        int offsetClose = -1;
                                        if (offsetOpen != -1)
                                        {
                                            offsetClose = data.IndexOf("</equippedStatOffsets>", offsetOpen + 21);
                                            if (offsetClose == -1)
                                            {
                                                offsetOpen = -1;
                                            }
                                        }

                                        // Iterate through matches
                                        Match match = tag.Match(data);
                                        while (match.Success)
                                        {
                                            if (offsetOpen == -1)
                                            {
                                                // No offset tags
                                                data = num.Replace(data, reader.ReadDouble().ToString(), 1, match.Index);
                                            }
                                            else
                                            {
                                                // Offset tags exist
                                                do
                                                {
                                                    if (match.Index < offsetOpen)
                                                    {
                                                        // This is before the opening offset tag
                                                        data = num.Replace(data, reader.ReadDouble().ToString(), 1, match.Index);
                                                        break;
                                                    }
                                                    else if (match.Index > offsetClose)
                                                    {
                                                        // This is after the closing offset tag so find new pair
                                                        offsetOpen = data.IndexOf("<equippedStatOffsets>", offsetClose + 22);
                                                        if (offsetOpen == -1)
                                                        {
                                                            // No more offset tags
                                                            offsetClose = -1;
                                                            data = num.Replace(data, reader.ReadDouble().ToString(), 1, match.Index);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            // Found opening offset tag
                                                            offsetClose = data.IndexOf("</equippedStatOffsets>", offsetOpen + 21);
                                                            if (offsetClose == -1)
                                                            {
                                                                // No closing offset tag
                                                                offsetOpen = -1;
                                                                data = num.Replace(data, reader.ReadDouble().ToString(), 1, match.Index);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // This is inside an offset tag so ignore it
                                                        break;
                                                    }
                                                } while (true);
                                            }
                                            match = tag.Match(data, match.Index + match.Length);
                                        }
                                    }
                                    File.WriteAllText(file, data);
                                }
                                finally
                                {
                                    progressBarValueQueue++;
                                }
                            }
                        }
                        break;

                    case "redefine":
                        // Multiply targeted values using the ones from backup file
                        runButtonTextQueue = "Redefining";

                        using (BinaryReader reader = new BinaryReader(new FileStream(backupPath, FileMode.Open)))
                        {
                            // Move reader to start of data
                            int versionLength = reader.ReadInt32();
                            reader.ReadBytes(versionLength);

                            // Iterate through files
                            foreach (string file in files)
                            {
                                try
                                {
                                    string section = file.Substring(ceDir.Length + 1);
                                    string data = File.ReadAllText(file);

                                    // Iterate through multipliers
                                    for (int i = 0; i < getNums.Length; i++)
                                    {
                                        Regex tag = getNums[i];
                                        double mult = multipliers[i];

                                        int offsetOpen = data.IndexOf("<equippedStatOffsets>");
                                        int offsetClose = -1;
                                        if (offsetOpen != -1)
                                        {
                                            offsetClose = data.IndexOf("</equippedStatOffsets>", offsetOpen + 21);
                                            if (offsetClose == -1)
                                            {
                                                offsetOpen = -1;
                                            }
                                        }

                                        // Iterate through matches
                                        Match match = tag.Match(data);
                                        while (match.Success)
                                        {
                                            if (offsetOpen == -1)
                                            {
                                                // No offset tags
                                                data = num.Replace(data, (reader.ReadDouble() * mult).ToString(), 1, match.Index);
                                            }
                                            else
                                            {
                                                // Offset tags exist
                                                do
                                                {
                                                    if (match.Index < offsetOpen)
                                                    {
                                                        // This is before the opening offset tag
                                                        data = num.Replace(data, (reader.ReadDouble() * mult).ToString(), 1, match.Index);
                                                        break;
                                                    }
                                                    else if (match.Index > offsetClose)
                                                    {
                                                        // This is after the closing offset tag so find new pair
                                                        offsetOpen = data.IndexOf("<equippedStatOffsets>", offsetClose + 22);
                                                        if (offsetOpen == -1)
                                                        {
                                                            // No more offset tags
                                                            offsetClose = -1;
                                                            data = num.Replace(data, (reader.ReadDouble() * mult).ToString(), 1, match.Index);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            // Found opening offset tag
                                                            offsetClose = data.IndexOf("</equippedStatOffsets>", offsetOpen + 21);
                                                            if (offsetClose == -1)
                                                            {
                                                                // No closing offset tag
                                                                offsetOpen = -1;
                                                                data = num.Replace(data, (reader.ReadDouble() * mult).ToString(), 1, match.Index);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // This is inside an offset tag so ignore it
                                                        break;
                                                    }
                                                } while (true);
                                            }
                                            match = tag.Match(data, match.Index + match.Length);
                                        }
                                    }
                                    File.WriteAllText(file, data);
                                }
                                finally
                                {
                                    progressBarValueQueue++;
                                }
                            }
                        }
                        break;
                }
            }
            finally
            {
                executeFinished = true;
            }
        }
    }
}
