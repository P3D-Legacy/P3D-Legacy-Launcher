using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using SystemInfoLibrary.OperatingSystem;

using Microsoft.Win32;

using P3D.Legacy.Launcher.Controls;
using P3D.Legacy.Launcher.Data;
using P3D.Legacy.Launcher.Extensions;
using P3D.Legacy.Launcher.Services;
using P3D.Legacy.Shared;

using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace P3D.Legacy.Launcher.Forms
{
    internal partial class MainForm : Form
    {
        private static StringBuilder Logger { get; } = new StringBuilder();

        private const string NewsUri = "https://p3d-legacy.github.io/launcher/";
        private const string FAQUri = "https://p3d-legacy.github.io/faq/";


        private OperatingSystemInfo OSInfo { get; } = OperatingSystemInfo.GetOperatingSystemInfo();

        private GameJolt GameJolt { get; set; }

        private SettingsYaml Settings { get; set; } = SettingsYaml.Load();

        private ProfilesYaml Profiles { get; set; } = ProfilesYaml.Load();
        private ProfileYaml CurrentProfile => Profiles.GetProfile();


        public MainForm()
        {
            FormPreInitialize();
            InitializeComponent();
            FormInitialize();
        }
        private void FormPreInitialize()
        {
            Logger?.Clear();
            foreach (Control control in Controls)
                control.Dispose();
            Controls.Clear();

            Thread.CurrentThread.CurrentCulture = Settings.Language;
            Thread.CurrentThread.CurrentUICulture = Settings.Language;
            CultureInfo.DefaultThreadCurrentCulture = Settings.Language;
        }
        private async void FormInitialize()
        {
            Log($"System Language: {CultureInfo.InstalledUICulture.EnglishName}");

            PictureBox_GameJolt.Visible = GameJolt != null && await GameJolt?.IsConnected();
            Label_Version.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            TabPage_Settings.VisibleChanged += TabPage_Settings_VisibleChanged;

            CheckBox_SaveCredentials.Checked = !CheckBox_SaveCredentials.Checked;
            CheckBox_SaveCredentials.Checked = !CheckBox_SaveCredentials.Checked;

            ReloadProfileList();
            ReloadSettings();

            BrowserLoad();

            CheckLauncherForUpdate();

            if (Settings.GameUpdates)
                CheckForUpdates(true);

            if (CheckBox_AutoLogIn.Checked)
                OpenGameJoltSession();
        }
        private async Task BrowserLoad()
        {
            try
            {
                var newsHtmlPanel = new HtmlPanel { Dock = DockStyle.Fill, BaseStylesheet = "" };
                newsHtmlPanel.ImageLoad += HtmlPanel_ImageLoad;
                TabPage_News.Controls.Add(newsHtmlPanel);
                newsHtmlPanel.Text = await new TimeoutWebClient { Timeout = 3000, Encoding = Encoding.UTF8 }.DownloadStringTaskAsync(NewsUri);

                var faqHtmlPanel = new HtmlPanel { Dock = DockStyle.Fill, BaseStylesheet = "" };
                faqHtmlPanel.ImageLoad += HtmlPanel_ImageLoad;
                TabPage_FAQ.Controls.Add(faqHtmlPanel);
                faqHtmlPanel.Text = await new TimeoutWebClient { Timeout = 3000, Encoding = Encoding.UTF8 }.DownloadStringTaskAsync(FAQUri);
                //Task.Run(async () => { faqHtmlPanel.Text = await new TimeoutWebClient { Timeout = 5000, Encoding = Encoding.UTF8 }.DownloadStringTaskAsync(FAQUri); });
            }
            catch (WebException) { /* do nothing */ }
        }
        private void HtmlPanel_ImageLoad(object sender, HtmlImageLoadEventArgs e)
        {
            if (e.Src.StartsWith(".."))
            {
                var uri = new Uri(NewsUri);
                var path = new Uri($"{uri.Scheme}://{e.Src.Replace("..", uri.Host)}");
                e.Callback(path.AbsoluteUri);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProfilesYaml.Save(Profiles);

            Program.ActionsBeforeExit.Add(() => Task.Run(() => GameJolt.SessionClose()).Wait(2000));
        }

        private async void Button_Start_Click(object sender, EventArgs e)
        {
            if (OSInfo.FrameworkVersion >= new Version(4, 0))
            {
                if (!IsOpenALInstalled())
                {
                    if (MessageBox.Show(MBLang.OpenALError, MBLang.OpenALErrorTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Process.Start(MBLang.OpenALErrorLink);
                        this.SafeInvoke(Close);
                        return;
                    }
                }
            }
            else
            {
                if (MessageBox.Show(MBLang.DotNetError, MBLang.DotNetErrorTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start(MBLang.DotNetErrorLink);
                    this.SafeInvoke(Close);
                    return;
                }
            }


            if (CurrentProfile.IsSupportingGameJolt && !await GameJolt.IsSessionActive())
            {
                using (var gameJoltMessageBox = new GameJoltForm())
                {
                    switch (gameJoltMessageBox.ShowDialog())
                    {
                        case DialogResult.Yes:
                            ReloadSettings();
                            if (!await OpenGameJoltSession())
                                return;
                            break;

                        case DialogResult.Cancel:
                            return;
                    }
                }
            }
            

            var path = Path.Combine(FileSystem.GameProfilesFolderPath, CurrentProfile.Name);
            var pathexe = Path.Combine(path, FileSystem.ExeFilename);
            if (Directory.Exists(path) && File.Exists(pathexe))
            {
                var startInfo = new ProcessStartInfo(pathexe, LaunchArgsHandler.CreateArgs(GameJolt.GameJoltYaml, false));
                Process.Start(startInfo);
                this.SafeInvoke(Close);
                return;
            }
            else
            {
                Putton_StartGame.Enabled = false;

                if (await DownloadCurrentProfile())
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

        private async void Button_CheckForUpdates_Click(object sender, EventArgs e)
        {
            GitHub.Update();
            await CheckForUpdates();
        }

        private void Button_NewProfile_Click(object sender, EventArgs e)
        {
            using (var profileForm = ProfileForm.ProfileNew(CurrentProfile))
                profileForm.ShowDialog();
            ReloadProfileList(SelectedProfile.Last);
        }
        private void Button_EditProfile_Click(object sender, EventArgs e)
        {
            using (var profileForm = ProfileForm.ProfileEdit(CurrentProfile))
                profileForm.ShowDialog();
            ReloadProfileList(SelectedProfile.Current);
        }
        private void Button_DeleteProfile_Click(object sender, EventArgs e)
        {
            var profiles = ProfilesYaml.Load();
            if (profiles.ProfileList.Count > ComboBox_CurrentProfile.SelectedIndex && !profiles.ProfileList[ComboBox_CurrentProfile.SelectedIndex].IsDefault)
            {
                profiles.ProfileList.RemoveAt(ComboBox_CurrentProfile.SelectedIndex);
                ProfilesYaml.Save(profiles);
                ReloadProfileList(SelectedProfile.Last);
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
            Settings = new SettingsYaml
            {
                GameUpdates = Check_Updates.Checked,
                Language = SettingsYaml.AvailableCultureInfo[ComboBox_Language.SelectedIndex],
                GameJoltUsername = TextBox_Username.Text,
                GameJoltToken = TextBox_Token.Text,
                AutoLogIn = CheckBox_AutoLogIn.Checked,
                SaveCredentials = CheckBox_SaveCredentials.Checked,
                SelectedDLIndex = ComboBox_SelectedDL.SelectedIndex
            };

            SettingsYaml.Save(Settings);

            if (!Equals(language, Settings.Language))
            {
                var tabIndex = TabControl.SelectedIndex;

                FormPreInitialize();
                InitializeComponent();
                FormInitialize();

                TabControl.SelectedIndex = tabIndex;
            }
        }

        private void LinkLabel_Pokemon3D_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("http://pokemon3d.net/forum/");

        private ToolTip ToolTip_SaveCredentials { get; } = new ToolTip();
        private void CheckBox_SaveCredentials_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_SaveCredentials.Checked)
            {
                CheckBox_AutoLogIn.ForeColor = DefaultForeColor;
                CheckBox_AutoLogIn.AutoCheck = true;

                ToolTip_SaveCredentials.RemoveAll();
            }
            else
            {
                CheckBox_AutoLogIn.Checked = false;
                CheckBox_AutoLogIn.ForeColor = Color.DimGray;
                CheckBox_AutoLogIn.AutoCheck = false;

                ToolTip_SaveCredentials.SetToolTip(CheckBox_AutoLogIn, MBLang.ToolTipSaveCredentials);
            }
        }

        private async void PictureBox_GameJolt_Offline_Click(object sender, EventArgs e)
        {
            PictureBox_GameJolt_Offline.BorderStyle = BorderStyle.Fixed3D;
            await Task.Delay(100);
            PictureBox_GameJolt_Offline.BorderStyle = BorderStyle.None;

            if (!await GameJolt.IsConnected())
                await OpenGameJoltSession();
        }
        private async void PictureBox_GameJolt_Click(object sender, EventArgs e)
        {
            PictureBox_GameJolt.BorderStyle = BorderStyle.Fixed3D;
            await Task.Delay(100);
            PictureBox_GameJolt.BorderStyle = BorderStyle.None;

            if (await GameJolt.IsConnected())
                await CloseGameJoltSession();
        }

        private async void BackgroundWorker_GameJolt_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!BackgroundWorker_GameJolt.CancellationPending)
            {
                var stopwatch = Stopwatch.StartNew();

                if (!await GameJolt.IsSessionActive())
                    break;

                stopwatch.Stop();

                if (stopwatch.ElapsedMilliseconds < 30000)
                    await Task.Delay((int) (30000 - stopwatch.ElapsedMilliseconds));
            }

            e.Cancel = true;
        }


        private void Log(string message)
        {
            Logger.AppendLine(message);
            if(TextBox_Logger != null)
                TextBox_Logger.Text = Logger.ToString();
        }

        private async Task<bool> OpenGameJoltSession()
        {
            if (string.IsNullOrEmpty(Settings.GameJoltUsername))
            {
                Log("Can't open Session! credentials not set!");
                return false;
            }

            if (!await GameJolt.IsConnected())
            {
                Log("Opening GameJolt Session...");
                await GameJolt.SessionOpen();
                if (await GameJolt.IsConnected())
                {
                    Log("GameJolt Session was opened!");
                    BackgroundWorker_GameJolt.CancelAsync();
                    BackgroundWorker_GameJolt.RunWorkerAsync();
                    PictureBox_GameJolt.Visible = true;
                }
                else
                    Log("GameJolt Session could not be opened!");
            }

            if (!await GameJolt.IsMigrated())
                Log($"GameJolt User {Settings.GameJoltUsername} has not migrated!");

            return await GameJolt.IsConnected() == true;
        }
        private async Task<bool> CloseGameJoltSession()
        {
            if (string.IsNullOrEmpty(Settings.GameJoltUsername))
            {
                Log($"Can't open Session! credentials not set!");
                return false;
            }

            if (await GameJolt.IsConnected())
            {
                Log("Closing GameJolt Session...");
                await GameJolt.SessionClose();
                if (!await GameJolt.IsConnected())
                {
                    Log("GameJolt Session was closed!");
                    PictureBox_GameJolt.Visible = false;
                }
                else
                    Log("GameJolt Session was not opened! Can't close the Session!");
            }

            return await GameJolt.IsConnected() == false;
        }

        private async Task CheckLauncherForUpdate()
        {
            var launcherVersion = Assembly.GetExecutingAssembly().GetName().Version;
            Log($"Launcher version: [{launcherVersion}].");
            Log("Checking Launcher for updates...");
            var launcherReleases = await GitHub.GetAllLauncherGitHubReleases();
            if (launcherReleases.Any())
            {
                var latestRelease = launcherReleases.First();
                if (launcherVersion < latestRelease.Version)
                {
                    Log($"Found a new Launcher version [{latestRelease.Version}]!");
                    switch (MessageBox.Show(MBLang.LauncherUpdateAvailable, MBLang.LauncherUpdateAvailableTitle, MessageBoxButtons.YesNo))
                    {
                        case DialogResult.Yes:
                            using (var directUpdater = new DirectUpdaterForm(latestRelease.ReleaseAsset, FileSystem.UpdateFolderPath))
                                directUpdater.ShowDialog();

                            Program.ActionsBeforeExit.Add(() =>
                                new Process { StartInfo =
                                    {
                                        UseShellExecute = false,
                                        FileName = Path.Combine(FileSystem.MainFolderPath, FileSystem.UpdaterExeFilename),
                                        CreateNoWindow = true
                                    } }.Start());
                            Close();
                            break;

                        default:
                            return;
                    }
                }
                else
                    Log("Launcher is up to date.");
            }
            else
            {
                Log("Error while checking Launcher for updates. Is Internet available?");
                MessageBox.Show(MBLang.NoInternet, MBLang.NoInternetTitle, MessageBoxButtons.OK);
            }
        }
        private async Task CheckForUpdates(bool onStartup = false)
        {
            Log($"Checking Profile '{CurrentProfile.Name}' for updates...");

            var path = Path.Combine(FileSystem.GameProfilesFolderPath, CurrentProfile.Name);
            var pathexe = Path.Combine(path, FileSystem.ExeFilename);
            if (!onStartup && (!Directory.Exists(path) || !File.Exists(pathexe)))
            {
                Log($"Profile '{CurrentProfile.Name}' is not downloaded!");
                await DownloadCurrentProfile();
            }

            if (!Directory.Exists(path) || !File.Exists(pathexe))
            {
                Log($"Profile '{CurrentProfile.Name}' is not downloaded!");
                return;
            }

            var gameReleases = await GitHub.GetAllGitHubReleases();
            if (gameReleases.Any())
            {
                var latestRelease = gameReleases.First();
                if ((!onStartup || CurrentProfile.IsDefault) && !(CurrentProfile.Version >= latestRelease.Version))
                {
                    Log($"Found a new Profile '{CurrentProfile.Name}' version [{latestRelease.Version}]!");
                    UpdateCurrentProfile(latestRelease);
                }
                else
                {
                    if (!onStartup)
                    {
                        Log($"Profile '{CurrentProfile.Name}' is up to date.");
                        MessageBox.Show(string.Format(MBLang.ProfileUpToDate, CurrentProfile.Name), MBLang.ProfileUpToDateTitle, MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                if (!onStartup)
                {
                    Log($"Error while checking Profile '{CurrentProfile.Name}'. Is Internet available?");
                    MessageBox.Show(MBLang.NoInternet, MBLang.NoInternetTitle, MessageBoxButtons.OK);
                }
            }
        }
        private async Task<bool> DownloadCurrentProfile()
        {
            var gameReleases = await GitHub.GetAllGitHubReleases();
            if (gameReleases.Any())
            {
                var onlineRelease = gameReleases.First(release => release.Version == CurrentProfile.Version);

                switch (MessageBox.Show(string.Format(MBLang.NotDownloaded, CurrentProfile.Name), MBLang.NotDownloadedTitle, MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:
                        using (var directUpdater = new DirectUpdaterForm(onlineRelease.ReleaseAsset, Path.Combine(FileSystem.GameProfilesFolderPath, CurrentProfile.Name)))
                        {
                            var state = directUpdater.ShowDialog();
                            return state != DialogResult.Abort && state != DialogResult.Cancel;
                        }
                }
            }

            return false;
        }
        private void UpdateCurrentProfile(GitHubRelease onlineRelease)
        {
            switch (MessageBox.Show(string.Format(MBLang.UpdateAvailable, CurrentProfile.Version, onlineRelease.Version), MBLang.UpdateAvailableTitle, MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Yes:
                    if (Settings.SelectedDL != null && !string.IsNullOrEmpty(Settings.SelectedDL.AbsolutePath))
                        using (var customUpdater = new CustomUpdaterForm(onlineRelease.UpdateInfoAsset, Path.Combine(FileSystem.GameProfilesFolderPath, CurrentProfile.Name), new Uri(Settings.SelectedDL, $"{onlineRelease.Version}/")))
                            customUpdater.ShowDialog();
                    else
                        MessageBox.Show(MBLang.DLNotSelected, MBLang.DLNotSelectedTitle, MessageBoxButtons.OK);
                    break;

                case DialogResult.No:
                    using (var directUpdater = new DirectUpdaterForm(onlineRelease.ReleaseAsset, Path.Combine(FileSystem.GameProfilesFolderPath, CurrentProfile.Name)))
                        directUpdater.ShowDialog();
                    break;

                case DialogResult.Cancel:
                    return;
            }
        }

        private void ReloadProfileList(SelectedProfile selectedProfile = SelectedProfile.Current)
        {
            var previousSelectedProfileIndex = Profiles.SelectedProfileIndex;
            Profiles = ProfilesYaml.Load();

            switch (selectedProfile)
            {
                case SelectedProfile.Current:
                    Profiles.SelectedProfileIndex = previousSelectedProfileIndex;
                    break;
                case SelectedProfile.First:
                    Profiles.SelectedProfileIndex = Profiles.ProfileList.Any() ? 0 : Profiles.SelectedProfileIndex;
                    break;
                case SelectedProfile.Last:
                    Profiles.SelectedProfileIndex = Profiles.ProfileList.Any() ? Profiles.ProfileList.Count - 1 : Profiles.SelectedProfileIndex;
                    break;
            }

            if (Controls.Count == 0)
                return;

            ComboBox_CurrentProfile.Items.Clear();
            foreach (var profile in Profiles.ProfileList)
                ComboBox_CurrentProfile.Items.Add(profile.Name);
            ComboBox_CurrentProfile.SelectedIndex = Profiles.SelectedProfileIndex;
        }
        private void ReloadSettings()
        {
            Settings = SettingsYaml.Load();

            if (Controls.Count == 0)
                return;

            Check_Updates.Checked = Settings.GameUpdates;

            ComboBox_Language.Items.Clear();
            foreach (var cultureInfo in SettingsYaml.AvailableCultureInfo)
                ComboBox_Language.Items.Add(cultureInfo.NativeName);
            ComboBox_Language.SelectedIndex = Array.IndexOf(SettingsYaml.AvailableCultureInfo, Settings.Language);

            GameJolt = new GameJolt(Settings.GameJoltUsername, Settings.GameJoltToken);
            TextBox_Username.Text = Settings.GameJoltUsername;
            TextBox_Token.Text = Settings.GameJoltToken;
            CheckBox_AutoLogIn.Checked = Settings.AutoLogIn;
            CheckBox_SaveCredentials.Checked = Settings.SaveCredentials;

            ComboBox_SelectedDL.Items.Clear();
            if (SettingsYaml.DLList.Any())
            {
                foreach (var dlUri in SettingsYaml.DLList)
                    ComboBox_SelectedDL.Items.Add(dlUri);
                ComboBox_SelectedDL.SelectedIndex = Settings.SelectedDLIndex;
            }
        }

        private enum SelectedProfile { Current, First, Last }
    }
}
    