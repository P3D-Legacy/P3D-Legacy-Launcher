using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using SystemInfoLibrary.OperatingSystem;

using Microsoft.Win32;

using P3D.Legacy.Launcher.Data;
using P3D.Legacy.Launcher.Extensions;

using YamlDotNet.Core;

namespace P3D.Legacy.Launcher.Forms
{
    public partial class MainForm : Form
    {
        public static StringBuilder Logger { get; } = new StringBuilder();

        #region GitHub

        private System.Version OnlineLastGameReleaseVersion => OnlineLastGameRelease?.Version ?? new System.Version("0.0");
        private OnlineGameRelease OnlineLastGameRelease => OnlineGameReleases.MaxByOrDefault(release => release.Version);
        private List<OnlineGameRelease> OnlineGameReleases { get; set; } = GetOnlineGameReleases().ToList();
        private static IEnumerable<OnlineGameRelease> GetOnlineGameReleases() =>
            GitHubInfo.GetAllReleases.Any() ? GitHubInfo.GetAllReleases.Select(release => new OnlineGameRelease(release)) : new List<OnlineGameRelease>();

        private bool LocalGameReleaseUpToDate => CurrentProfile.Version >= OnlineLastGameReleaseVersion;

        #endregion GitHub

        private OperatingSystemInfo OSInfo { get; } = OperatingSystemInfo.GetOperatingSystemInfo();

        private Settings Settings { get; set; } = LoadSettings();

        private Profiles Profiles { get; set; } = LoadProfiles();
        private Profile CurrentProfile => Profiles.GetProfile();


        public MainForm()
        {
            FormPreInitialize();
            InitializeComponent();
            FormInitialize();
        }
        private void FormPreInitialize()
        {
            foreach (Control control in Controls)
                control.Dispose();
            Controls.Clear();

            Thread.CurrentThread.CurrentCulture = Settings.Language;
            Thread.CurrentThread.CurrentUICulture = Settings.Language;
            CultureInfo.DefaultThreadCurrentCulture = Settings.Language;
        }
        private void FormInitialize()
        {
            Logger.Clear();
            Log($"System Language: {CultureInfo.InstalledUICulture.EnglishName}");

            Label_Version.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            TabPage_Settings.VisibleChanged += TabPage_Settings_VisibleChanged;

            ReloadProfileList();
            ReloadSettings();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            CheckLauncherForUpdate();

            if (Settings.GameUpdates)
                CheckForUpdates(true);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveProfiles(Profiles);
        }

        private void Button_Start_Click(object sender, EventArgs e)
        {
            if (OSInfo.FrameworkVersion >= new System.Version(4, 0))
            {
                if (!IsOpenALInstalled())
                {
                    if (MessageBox.Show(MBLang.OpenALError, MBLang.OpenALErrorTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Process.Start(MBLang.OpenALErrorLink);
                        Invoke((MethodInvoker) Close);
                        return;
                    }
                }
            }
            else
            {
                if (MessageBox.Show(MBLang.DotNetError, MBLang.DotNetErrorTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start(MBLang.DotNetErrorLink);
                    Invoke((MethodInvoker) Close);
                    return;
                }
            }



            var path = Path.Combine(FileSystemInfo.GameReleasesFolderPath, CurrentProfile.Name);
            var pathexe = Path.Combine(path, FileSystemInfo.ExeFilename);
            if (Directory.Exists(path) && File.Exists(pathexe))
            {
                Process.Start(pathexe);
                Invoke((MethodInvoker) Close);
                return;
            }
            else
            {
                Putton_StartGame.Enabled = false;

                if (DownloadCurrentProfile())
                    Button_Start_Click(sender, e);
                else
                    Putton_StartGame.Enabled = true;
            }
        }
        private bool IsOpenALInstalled()
        {
            if (OSInfo.OperatingSystemType == OperatingSystemType.Windows)
                return Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall").GetSubKeyNames()
                    .Select(item => (string) Registry.LocalMachine.OpenSubKey($@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{item}").GetValue("DisplayName"))
                    .Any(programName => programName == "OpenAL");
            if (OSInfo.OperatingSystemType == OperatingSystemType.Unix)
                return true;

            if (OSInfo.OperatingSystemType == OperatingSystemType.MacOSX)
                return true;

            return false;
        }

        private void Button_CheckForUpdates_Click(object sender, EventArgs e)
        {
            GitHubInfo.Update();
            CheckForUpdates();
        }

        private void Button_NewProfile_Click(object sender, EventArgs e)
        {
            using (var profileForm = ProfileForm.ProfileNew(CurrentProfile))
                profileForm.ShowDialog();
            ReloadProfileList();
        }
        private void Button_EditProfile_Click(object sender, EventArgs e)
        {
            using (var profileForm = ProfileForm.ProfileEdit(CurrentProfile))
                profileForm.ShowDialog();
            ReloadProfileList();
        }
        private void Button_DeleteProfile_Click(object sender, EventArgs e)
        {
            var profiles = LoadProfiles();
            if (profiles.ProfileList.Count > 1)
            {
                profiles.ProfileList.RemoveAt(ComboBox_CurrentProfile.SelectedIndex);
                SaveProfiles(profiles);
                ReloadProfileList();
            }
        }
        private void ComboBox_CurrentProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            Profiles.SelectedProfileIndex = ComboBox_CurrentProfile.SelectedIndex;
        }

        private void TabPage_Settings_VisibleChanged(object sender, EventArgs e)
        {
            if ((sender as TabPage).Visible)
                ReloadSettings();
        }
        private void Button_SaveSettings_Click(object sender, EventArgs e)
        {
            var language = Settings.Language;
            Settings = new Settings
            {
                GameUpdates = Check_Updates.Checked,
                Language = Settings.AvailableCultureInfo[ComboBox_Language.SelectedIndex],
                SelectedDLIndex = ComboBox_SelectedDL.SelectedIndex
            };

            SaveSettings(Settings);

            if (!Equals(language, Settings.Language))
            {
                var tabIndex = TabControl.SelectedIndex;

                FormPreInitialize();
                InitializeComponent();
                FormInitialize();

                TabControl.SelectedIndex = tabIndex;
            }
        }

        private void LinkLabel_Pokemon3D_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://pokemon3d.net/forum/");
        }


        private void Log(string message)
        {
            Logger.AppendLine(message);
            TextBox_Logger.Text = Logger.ToString();
        }

        private void CheckLauncherForUpdate()
        {
            var launcherReleases = GitHubInfo.GetAllLauncherReleases.ToList();

            if (launcherReleases.Any())
            {
                var latestRelease = launcherReleases.First();
                if (Assembly.GetExecutingAssembly().GetName().Version < new System.Version(latestRelease.TagName))
                {
                    switch (MessageBox.Show(MBLang.LauncherUpdateAvailable, MBLang.LauncherUpdateAvailableTitle, MessageBoxButtons.YesNo))
                    {
                        case DialogResult.Yes:
                            using (var directUpdater = new DirectUpdaterForm(latestRelease.GetRelease(), FileSystemInfo.UpdateFolderPath))
                                directUpdater.ShowDialog();

                            Program.ActionsBeforeExit.Add(() =>
                                new Process { StartInfo =
                                    {
                                        UseShellExecute = false,
                                        FileName = Path.Combine(FileSystemInfo.MainFolderPath, FileSystemInfo.UpdaterExeFilename),
                                        CreateNoWindow = true
                                    } }.Start());
                            Close();
                            break;

                        default:
                            return;
                    }
                }
            }
            else
                MessageBox.Show(MBLang.NoInternet, MBLang.NoInternetTitle, MessageBoxButtons.OK);
        }
        private void CheckForUpdates(bool onStartup = false)
        {
            var path = Path.Combine(FileSystemInfo.GameReleasesFolderPath, CurrentProfile.Name);
            var pathexe = Path.Combine(path, FileSystemInfo.ExeFilename);
            if (!onStartup && (!Directory.Exists(path) || !File.Exists(pathexe)))
                DownloadCurrentProfile();

            if (!Directory.Exists(path) || !File.Exists(pathexe))
                return;

            OnlineGameReleases = GetOnlineGameReleases().ToList();

            if (OnlineGameReleases.Any())
            {
                if (!LocalGameReleaseUpToDate)
                    UpdateCurrentProfile(OnlineLastGameRelease);
                else
                {
                    if (!onStartup)
                        MessageBox.Show(string.Format(MBLang.ProfileUpToDate, CurrentProfile.Name), MBLang.ProfileUpToDateTitle, MessageBoxButtons.OK);
                }
            }
            else
            {
                if (!onStartup)
                    MessageBox.Show(MBLang.NoInternet, MBLang.NoInternetTitle, MessageBoxButtons.OK);
            }
        }
        private bool DownloadCurrentProfile()
        {
            if (!OnlineGameReleases.Any())
                return false;

            var onlineRelease = OnlineGameReleases.First(release => release.Version == CurrentProfile.Version);

            switch (MessageBox.Show(string.Format(MBLang.NotDownloaded, CurrentProfile.Name), MBLang.NotDownloadedTitle, MessageBoxButtons.YesNo))
            {
                case DialogResult.Yes:
                    using (var directUpdater = new DirectUpdaterForm(onlineRelease.ReleaseAsset, Path.Combine(FileSystemInfo.GameReleasesFolderPath, CurrentProfile.Name)))
                    {
                        var state = directUpdater.ShowDialog();
                        return state != DialogResult.Abort && state != DialogResult.Cancel;
                    }

                default:
                    return false;
            }
        }
        private void UpdateCurrentProfile(OnlineGameRelease onlineRelease)
        {
            switch (MessageBox.Show(string.Format(MBLang.UpdateAvailable, CurrentProfile.Version, onlineRelease.Version), MBLang.UpdateAvailableTitle, MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Yes:
                    if (Settings.GetDL() != null && !string.IsNullOrEmpty(Settings.GetDL().AbsolutePath))
                    {
                        using (var nAppUpdater = new CustomUpdaterForm(onlineRelease.UpdateInfoAsset, Path.Combine(FileSystemInfo.GameReleasesFolderPath, CurrentProfile.Name), new Uri(Settings.GetDL(), $"{CurrentProfile.Version}/")))
                            nAppUpdater.ShowDialog();
                    }
                    else
                        MessageBox.Show(MBLang.DLNotSelected, MBLang.DLNotSelectedTitle, MessageBoxButtons.OK);
                    break;

                case DialogResult.No:
                    using (var directUpdater = new DirectUpdaterForm(onlineRelease.ReleaseAsset, Path.Combine(FileSystemInfo.GameReleasesFolderPath, CurrentProfile.Name)))
                        directUpdater.ShowDialog();
                    break;

                case DialogResult.Cancel:
                    return;
            }
        }

        private void ReloadProfileList()
        {
            Profiles = LoadProfiles();

            if (Controls.Count == 0)
                return;

            ComboBox_CurrentProfile.Items.Clear();
            foreach (var profile in Profiles.ProfileList)
                ComboBox_CurrentProfile.Items.Add(profile.Name);
            ComboBox_CurrentProfile.SelectedIndex = Profiles.SelectedProfileIndex;
        }
        private void ReloadSettings()
        {
            Settings = LoadSettings();

            if (Controls.Count == 0)
                return;

            Check_Updates.Checked = Settings.GameUpdates;

            ComboBox_Language.Items.Clear();
            foreach (var cultureInfo in Settings.AvailableCultureInfo)
                ComboBox_Language.Items.Add(cultureInfo.NativeName);
            ComboBox_Language.SelectedIndex = Array.IndexOf(Settings.AvailableCultureInfo, Settings.Language);

            ComboBox_SelectedDL.Items.Clear();
            if (Settings.DLList.Any())
            {
                foreach (var dlUri in Settings.DLList)
                    ComboBox_SelectedDL.Items.Add(dlUri);
                ComboBox_SelectedDL.SelectedIndex = Settings.SelectedDLIndex;
            }
        }

        private static void SaveSettings(Settings settings)
        {
            if (!File.Exists(FileSystemInfo.SettingsFilePath))
                File.Create(FileSystemInfo.SettingsFilePath).Dispose();

            var serializer = Settings.SerializerBuilder.Build();
            File.WriteAllText(FileSystemInfo.SettingsFilePath, serializer.Serialize(settings));
        }
        private static Settings LoadSettings()
        {
            if (!File.Exists(FileSystemInfo.SettingsFilePath))
                File.Create(FileSystemInfo.SettingsFilePath).Dispose();

            var deserializer = Settings.DeserializerBuilder.Build();
            try
            {
                var deserialized = deserializer.Deserialize<Settings>(File.ReadAllText(FileSystemInfo.SettingsFilePath));
                return deserialized != null && deserialized.IsValid() ? deserialized : Settings.Default;
            }
            catch (YamlException)
            {
                SaveSettings(Settings.Default);
                return deserializer.Deserialize<Settings>(File.ReadAllText(FileSystemInfo.SettingsFilePath)) ?? Settings.Default;
            }
        }

        public static void SaveProfiles(Profiles profiles)
        {
            if (!File.Exists(FileSystemInfo.ProfilesFilePath))
                File.Create(FileSystemInfo.ProfilesFilePath).Dispose();

            var serializer = Profiles.SerializerBuilder.Build();
            File.WriteAllText(FileSystemInfo.ProfilesFilePath, serializer.Serialize(profiles));
        }
        public static Profiles LoadProfiles()
        {
            if (!File.Exists(FileSystemInfo.ProfilesFilePath))
                File.Create(FileSystemInfo.ProfilesFilePath).Dispose();

            var deserializer = Profiles.DeserializerBuilder.Build();
            try
            {
                var deserialized = deserializer.Deserialize<Profiles>(File.ReadAllText(FileSystemInfo.ProfilesFilePath));
                return deserialized != null && deserialized.IsValid() ? deserialized : Profiles.Default;
            }
            catch (YamlException)
            {
                SaveProfiles(Profiles.Default);
                return deserializer.Deserialize<Profiles>(File.ReadAllText(FileSystemInfo.ProfilesFilePath));
            }
            
        }
    }
}
    