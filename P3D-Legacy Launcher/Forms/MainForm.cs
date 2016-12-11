using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using SystemInfoLibrary.OperatingSystem;

using Microsoft.Win32;

using P3D.Legacy.Launcher.Data;
using P3D.Legacy.Launcher.Extensions;

namespace P3D.Legacy.Launcher.Forms
{
    public partial class MainForm : Form
    {
        #region FileSystem

        private const string ExeFilename = "Pokemon3D.exe";

        private const string SettingsFilename = "Settings.yml";

        private static string SettingsPath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            SettingsFilename);

        private const string ProfilesFilename = "Profiles.yml";

        private static string ProfilesPath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            ProfilesFilename);

        private const string GameReleasesFoldername = "Releases";

        public static string GameReleasesPath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            GameReleasesFoldername);

        #endregion Strings

        #region GitHub

        private Version OnlineLastGameReleaseVersion => OnlineLastGameRelease?.Version ?? new Version("0.0");

        private OnlineGameRelease OnlineLastGameRelease => OnlineGameReleases.MaxByOrDefault(release => release.Version);

        private List<OnlineGameRelease> OnlineGameReleases { get; set; } = GetOnlineGameReleases().ToList();
        private static IEnumerable<OnlineGameRelease> GetOnlineGameReleases() =>
            GitHubInfo.GetAllReleases.Any() ? GitHubInfo.GetAllReleases.Select(release => new OnlineGameRelease(release)) : new List<OnlineGameRelease>();

        private bool LocalGameReleaseUpToDate => CurrentProfile.Version >= OnlineLastGameReleaseVersion;

        #endregion GitHub

        private OperatingSystemInfo OSInfo { get; } = OperatingSystemInfo.GetOperatingSystemInfo();

        private Profiles Profiles { get; set; }
        private Profile CurrentProfile => Profiles.GetProfile();

        private Settings Settings { get; set; } = LoadSettings();


        public MainForm()
        {
            var lang = CultureInfo.CurrentUICulture.DisplayName;

            Thread.CurrentThread.CurrentCulture = Settings.Language;
            Thread.CurrentThread.CurrentUICulture = Settings.Language;
            CultureInfo.DefaultThreadCurrentCulture = Settings.Language;

            InitializeComponent();
            FormInitialize();

            var builder = new StringBuilder(TextBox_Logger.Text);
            builder.AppendLine($"System Language: {lang}");
            TextBox_Logger.Text = builder.ToString();
        }
        private void FormInitialize()
        {
            TabPage_Settings.VisibleChanged += TabPage_Settings_VisibleChanged;

            ReloadProfileList();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (Settings.GameUpdates)
                CheckForUpdates();
        }

        private void Button_Start_Click(object sender, EventArgs e)
        {
            if (OSInfo.FrameworkVersion >= new Version(4, 0))
            {
                if (!IsOpenALInstalled())
                {
                    if (MessageBox.Show(MBLang.OpenALError, MBLang.OpenALErrorTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        Process.Start(MBLang.OpenALErrorLink);
                }
            }
            else
            {
                if (MessageBox.Show(MBLang.DotNetError, MBLang.DotNetErrorTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Process.Start(MBLang.DotNetErrorLink);
            }



            var path = Path.Combine(GameReleasesPath, CurrentProfile.Name);
            var pathexe = Path.Combine(path, ExeFilename);
            if (Directory.Exists(path) && File.Exists(pathexe))
            {
                Process.Start(pathexe);
                Close();
            }
            else
            {
                Putton_StartGame.Enabled = false;

                if(DownloadCurrentProfile())
                    Button_Start_Click(sender, e);
                else
                    Putton_StartGame.Enabled = true;
            }
        }
        private bool IsOpenALInstalled()
        {
            if(OSInfo.OperatingSystemType == OperatingSystemType.Windows)
                return Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall").GetSubKeyNames()
                    .Select(item =>(string)Registry.LocalMachine.OpenSubKey($@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{item}").GetValue("DisplayName"))
                    .Any(programName => programName == "OpenAL");
            if (OSInfo.OperatingSystemType == OperatingSystemType.Unix)
                return true;

            if (OSInfo.OperatingSystemType == OperatingSystemType.Unity5)
                return true;

            if (OSInfo.OperatingSystemType == OperatingSystemType.MacOSX)
                return true;

            return false;
        }

        private void Button_CheckForUpdates_Click(object sender, EventArgs e)
        {
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
            Profiles.ProfileIndex = ComboBox_CurrentProfile.SelectedIndex;
        }

        private void TabPage_Settings_VisibleChanged(object sender, EventArgs e)
        {
            if ((sender as TabPage).Visible)
            {
                Settings = LoadSettings();

                Check_Updates.Checked = Settings.GameUpdates;
                ComboBox_Language.Items.Clear();
                foreach (var cultureInfo in Settings.AvailableCultureInfo)
                    ComboBox_Language.Items.Add(cultureInfo.NativeName);
                ComboBox_Language.SelectedIndex = Array.IndexOf(Settings.AvailableCultureInfo, Settings.Language);
            }
        }
        private void Button_SaveSettings_Click(object sender, EventArgs e)
        {
            var language = Settings.Language;
            Settings = new Settings
            {
                GameUpdates = Check_Updates.Checked,
                Language = Settings.AvailableCultureInfo[ComboBox_Language.SelectedIndex],
            };

            SaveSettings(Settings);

            if (!Equals(language, Settings.Language))
            {
                var tabIndex = TabControl.SelectedIndex;

                Thread.CurrentThread.CurrentCulture = Settings.Language;
                Thread.CurrentThread.CurrentUICulture = Settings.Language;
                CultureInfo.DefaultThreadCurrentCulture = Settings.Language;

                Controls.Clear();
                InitializeComponent();
                FormInitialize();

                TabControl.SelectedIndex = tabIndex;
            }
        }

        private void LinkLabel_Pokemon3D_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://pokemon3d.net/forum/");
        }


        private void CheckForUpdates()
        {
            OnlineGameReleases = GetOnlineGameReleases().ToList();

            if (OnlineGameReleases.Any())
            {
                if (!LocalGameReleaseUpToDate)
                    UpdateCurrentProfile(OnlineLastGameRelease);
                else
                    MessageBox.Show(string.Format(MBLang.ProfileUpToDate, CurrentProfile.Name), MBLang.ProfileUpToDateTitle, MessageBoxButtons.OK);
            }
            else
                MessageBox.Show(MBLang.NoInternet, MBLang.NoInternetTitle, MessageBoxButtons.OK);
        }
        private bool DownloadCurrentProfile()
        {
            if (!OnlineGameReleases.Any())
                return false;

            var onlineRelease = OnlineGameReleases.First(release => release.Version == CurrentProfile.Version);

            switch (MessageBox.Show(string.Format(MBLang.NotDownloaded, CurrentProfile.Name), MBLang.NotDownloadedTitle, MessageBoxButtons.YesNo))
            {
                case DialogResult.Yes:
                    using (var directDownloader = new DirectDownloaderForm(CurrentProfile, onlineRelease))
                    {
                        var state = directDownloader.ShowDialog();
                        return state != DialogResult.Abort && state != DialogResult.Cancel;
                    }

                default:
                    return false;
            }
        }
        private void UpdateCurrentProfile(OnlineGameRelease onlineRelease)
        {
            // -- Would you like to update the [%OLDVERSION%] version to [%NEWVERSION%] or would you like to download it as an independent version?
            // -- If Update, use UpdateManager
            // -- Else, just download latesr Release.Zip

            switch (MessageBox.Show(string.Format(MBLang.UpdateAvailable, CurrentProfile.Version, onlineRelease.Version), MBLang.UpdateAvailableTitle, MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Yes:
                    using (var nAppUpdater = new NAppUpdaterForm(onlineRelease))
                        nAppUpdater.ShowDialog();
                    break;

                case DialogResult.No:
                    using (var directDownloader = new DirectDownloaderForm(CurrentProfile, onlineRelease))
                        directDownloader.ShowDialog();
                    break;

                case DialogResult.Cancel:
                    return;
            }
        }

        private void ReloadProfileList()
        {
            Profiles = LoadProfiles();
            ComboBox_CurrentProfile.Items.Clear();
            foreach (var profile in Profiles.ProfileList)
                ComboBox_CurrentProfile.Items.Add(profile.Name);
            ComboBox_CurrentProfile.SelectedIndex = Profiles.ProfileIndex;
        }


        public static void SaveSettings(Settings settings)
        {
            if (!File.Exists(SettingsPath))
                File.Create(SettingsPath).Dispose();

            var serializer = Settings.SerializerBuilder.Build();
            File.WriteAllText(SettingsPath, serializer.Serialize(settings));
        }
        public static Settings LoadSettings()
        {
            if (!File.Exists(SettingsPath))
                File.Create(SettingsPath).Dispose();

            var deserializer = Settings.DeserializerBuilder.Build();
            return deserializer.Deserialize<Settings>(File.ReadAllText(SettingsPath)) ?? new Settings();
        }

        public static void SaveProfiles(Profiles profiles)
        {
            if (!File.Exists(ProfilesPath))
                File.Create(ProfilesPath).Dispose();

            var serializer = Profiles.SerializerBuilder.Build();
            File.WriteAllText(ProfilesPath, serializer.Serialize(profiles));
        }
        public static Profiles LoadProfiles()
        {
            if (!File.Exists(ProfilesPath))
                File.Create(ProfilesPath).Dispose();

            var deserializer = Profiles.DeserializerBuilder.Build();
            return deserializer.Deserialize<Profiles>(File.ReadAllText(ProfilesPath)) ?? Profiles.Default;
        }
    }
}
    