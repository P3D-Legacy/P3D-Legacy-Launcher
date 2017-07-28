using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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
using P3D.Legacy.Launcher.Storage.Files;
using P3D.Legacy.Launcher.Storage.Folders;

using P3D.Legacy.Shared.Extensions;
using P3D.Legacy.Shared.Utils;

using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace P3D.Legacy.Launcher.Forms
{
    internal partial class MainForm : LocalizableForm
    {
        private static StringBuilder Logger { get; } = new StringBuilder();

        private const string NewsUri = "https://p3d-legacy.github.io/launcher/";
        private const string FAQUri = "https://p3d-legacy.github.io/faq/";


        private OperatingSystemInfo OSInfo { get; } = OperatingSystemInfo.GetOperatingSystemInfo();

        private GameJolt GameJolt { get; set; }

        private SettingsFile Settings { get; } = new SettingsFile();

        private ProfilesFile Profiles { get; } = new ProfilesFile();
        private Profile CurrentProfile => Profiles.CurrentProfile;


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

            Thread.CurrentThread.CurrentCulture = Settings.LocalizationInfo.CultureInfo;
            Thread.CurrentThread.CurrentUICulture = Settings.LocalizationInfo.CultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = Settings.LocalizationInfo.CultureInfo;
            LocalizationUI.Load(Settings.LocalizationInfo);
        }
        private async void FormInitialize()
        {
            Log($"System Language: {CultureInfo.InstalledUICulture.EnglishName}");

            PictureBox_GameJolt.Visible = GameJolt != null && await GameJolt?.IsConnectedAsync();
            Label_Version.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            TabPage_Settings.VisibleChanged += TabPage_Settings_VisibleChanged;

            CheckBox_SaveCredentials.Checked = !CheckBox_SaveCredentials.Checked;
            CheckBox_SaveCredentials.Checked = !CheckBox_SaveCredentials.Checked;

            await ReloadProfileListAsync();
            ReloadSettings();
            await Settings.SaveAsync();

            // TODO: better string.format use
            Label_Credits.Text = string.Format(Label_Credits.Text, "Aragas", string.Join(", ", LocalizationUI.Authors.Distinct().Where(author => !string.IsNullOrEmpty(author))));

#pragma warning disable 4014
            BrowserLoadAsync();

            CheckLauncherForUpdateAsync();

            if (Settings.GameUpdates)
                CheckProfileForUpdateAsync(true);

            if (CheckBox_AutoLogIn.Checked)
                OpenGameJoltSessionAsync();
#pragma warning restore 4014
        }
        private async Task BrowserLoadAsync()
        {
            try
            {
                var newsHtmlPanel = new HtmlPanel { Dock = DockStyle.Fill, BaseStylesheet = "" };
                newsHtmlPanel.ImageLoad += HtmlPanel_ImageLoad;
                TabPage_News.Controls.Add(newsHtmlPanel);
                newsHtmlPanel.Text = await new TimeoutWebClient { Timeout = 3000, Encoding = Encoding.UTF8 }.DownloadStringTaskAsync(NewsUri);
                //await Task.Run(async () => { newsHtmlPanel.Text = await new TimeoutWebClient { Timeout = 3000, Encoding = Encoding.UTF8 }.DownloadStringTaskAsync(NewsUri); });

                var faqHtmlPanel = new HtmlPanel { Dock = DockStyle.Fill, BaseStylesheet = "" };
                faqHtmlPanel.ImageLoad += HtmlPanel_ImageLoad;
                TabPage_FAQ.Controls.Add(faqHtmlPanel);
                faqHtmlPanel.Text = await new TimeoutWebClient { Timeout = 3000, Encoding = Encoding.UTF8 }.DownloadStringTaskAsync(FAQUri);
                //await Task.Run(async () => { faqHtmlPanel.Text = await new TimeoutWebClient { Timeout = 3000, Encoding = Encoding.UTF8 }.DownloadStringTaskAsync(FAQUri); });
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
            Profiles.Save();

            Program.ActionsBeforeExit.Add(() => Task.Run(() => GameJolt.SessionCloseAsync()).Wait(2000));
        }

        private async void Button_Start_Click(object sender, EventArgs e)
        {
            bool IsOpenALInstalled()
            {
                if (OSInfo.OperatingSystemType == OperatingSystemType.Windows)
                    return Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall").GetSubKeyNames()
                        .Select(item => (string)Registry.LocalMachine.OpenSubKey($@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{item}").GetValue("DisplayName"))
                        .Any(programName => programName == "OpenAL");
                if (OSInfo.OperatingSystemType == OperatingSystemType.Unix)
                    return true;

                if (OSInfo.OperatingSystemType == OperatingSystemType.MacOSX)
                    return true;

                return false;
            }

            const string OpenALLink = "https://www.openal.org/downloads/";
            const string DotNetLink = "https://www.microsoft.com/ru-RU/download/details.aspx?id=42643";

            if (OSInfo.FrameworkVersion >= new Version(4, 0, 30319, 34000)) // 4.5.2
            {
                if (!IsOpenALInstalled())
                {
                    if (MessageBox.Show(LocalizationUI.GetString("SomethingNotFoundError", "OpenAL 1.1", OpenALLink), LocalizationUI.GetString("SomethingNotFoundErrorTitle"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Process.Start(OpenALLink);
                        this.SafeInvoke(Close);
                        return;
                    }
                }
            }
            else
            {
                if (MessageBox.Show(LocalizationUI.GetString("SomethingNotFoundError", "Microsoft .NET Framework 4.5.2", DotNetLink), LocalizationUI.GetString("SomethingNotFoundErrorTitle"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start(DotNetLink);
                    this.SafeInvoke(Close);
                    return;
                }
            }


            if (CurrentProfile.IsSupportingGameJolt && !(await GameJolt.IsSessionActiveAsync()).Success)
            {
                using (var gameJoltMessageBox = new GameJoltForm(Settings))
                {
                    switch (gameJoltMessageBox.ShowDialog())
                    {
                        case DialogResult.Yes:
                            ReloadSettings();
                            if (!await OpenGameJoltSessionAsync())
                                return;
                            break;

                        case DialogResult.Cancel:
                            return;
                    }
                }
            }


            if (Settings.GameCheckServer && CurrentProfile.ProfileType == ProfileType.Game)
                new ProfileGameServerListFile(new ProfileGameSaveFolder(CurrentProfile.Folder)).CheckServers();

            if (CurrentProfile.ExecutionFile != null)
            {
                var gameJoltArgs = CurrentProfile.IsSupportingGameJolt ? LaunchArgsHandler.CreateArgs(GameJolt.GameJoltYaml, false) : "";
                CurrentProfile.ExecutionFile.Start($"{gameJoltArgs} {CurrentProfile.LaunchArgs}");
                this.SafeInvoke(Close);
                return;
            }
            else
            {
                Button_StartGame.Enabled = false;

                if (await DownloadCurrentProfileAsync())
                    Button_Start_Click(sender, e);
                else
                    Button_StartGame.Enabled = true;
            }
        }

        private async void Button_CheckForUpdates_Click(object sender, EventArgs e)
        {
            GitHub.Update();
            await CheckProfileForUpdateAsync();
        }

        private async void Button_NewProfile_Click(object sender, EventArgs e)
        {
            using (var profileForm = ProfileForm.ProfileNew(Profiles))
                profileForm.ShowDialog();
            await ReloadProfileListAsync(ProfilesFile.SelectedProfile.Last);
        }
        private async void Button_EditProfile_Click(object sender, EventArgs e)
        {
            using (var profileForm = ProfileForm.ProfileEdit(Profiles))
                profileForm.ShowDialog();
            await ReloadProfileListAsync(ProfilesFile.SelectedProfile.Current);
        }
        private async void Button_DeleteProfile_Click(object sender, EventArgs e)
        {
            await Profiles.DeleteAsync(ComboBox_CurrentProfile.SelectedIndex);
            await ReloadProfileListAsync(ProfilesFile.SelectedProfile.Last);
        }
        private void ComboBox_CurrentProfile_SelectedIndexChanged(object sender, EventArgs e) => Profiles.SelectedProfileIndex = ComboBox_CurrentProfile.SelectedIndex;

        private void TabPage_Settings_VisibleChanged(object sender, EventArgs e)
        {
            if ((sender as TabPage).Visible)
                ReloadSettings();
        }
        private async void Button_SaveSettings_Click(object sender, EventArgs e)
        {
            var oldLocalizationInfo = Settings.LocalizationInfo;
            Settings.GameUpdates = Check_Updates.Checked;
            Settings.GameCheckServer = CheckBox_CheckServerOnStart.Checked;
            Settings.LocalizationInfo = LocalizationUI.Localizations[ComboBox_Language.SelectedIndex];
            Settings.GameJoltUsername = TextBox_Username.Text;
            Settings.GameJoltToken = TextBox_Token.Text;
            Settings.AutoLogIn = CheckBox_AutoLogIn.Checked;
            Settings.SaveCredentials = CheckBox_SaveCredentials.Checked;
            Settings.SelectedDLIndex = ComboBox_SelectedDL.SelectedIndex;

            await Settings.SaveAsync();
            ReloadSettings();

            if (!Equals(oldLocalizationInfo, Settings.LocalizationInfo))
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

                ToolTip_SaveCredentials.SetToolTip(CheckBox_AutoLogIn, LocalizationUI.GetString("ToolTipSaveCredentials"));
            }
        }

        private async void PictureBox_GameJolt_Click(object sender, EventArgs e)
        {
            PictureBox_GameJolt.BorderStyle = BorderStyle.Fixed3D;
            await Task.Delay(100);
            PictureBox_GameJolt.BorderStyle = BorderStyle.None;

            if (await GameJolt.IsConnectedAsync())
                await CloseGameJoltSessionAsync();
        }
        private async void PictureBox_GameJolt_Offline_Click(object sender, EventArgs e)
        {
            PictureBox_GameJolt_Offline.BorderStyle = BorderStyle.Fixed3D;
            await Task.Delay(100);
            PictureBox_GameJolt_Offline.BorderStyle = BorderStyle.None;

            if (!await GameJolt.IsConnectedAsync())
                await OpenGameJoltSessionAsync();
        }

        private async void BackgroundWorker_GameJolt_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!BackgroundWorker_GameJolt.CancellationPending)
            {
                var stopwatch = Stopwatch.StartNew();

                if (!(await GameJolt.IsSessionActiveAsync()).Success)
                    break;

                stopwatch.Stop();

                if (stopwatch.ElapsedMilliseconds < 30000)
                    await Task.Delay((int) (30000 - stopwatch.ElapsedMilliseconds));
            }

            e.Cancel = true;
        }


        [DebuggerStepThrough]
        private void Log(string message)
        {
            Logger.AppendLine(message);
            if(TextBox_Logger != null)
                TextBox_Logger.Text = Logger.ToString();
        }

        private async Task<bool> OpenGameJoltSessionAsync()
        {
            if (string.IsNullOrEmpty(Settings.GameJoltUsername))
            {
                Log("Can't open Session! credentials not set!");
                return false;
            }

            if (!await GameJolt.IsConnectedAsync())
            {
                Log("Opening GameJolt Session...");
                var resp = await GameJolt.SessionOpenAsync();
                if (!resp.Success)
                {
                    Log($"GameJolt Error: {resp.ErrorMessage}");
                    PictureBox_GameJolt.Visible = false;
                    PictureBox_GameJolt_Offline.Visible = false;
                }
                if (await GameJolt.IsConnectedAsync())
                {
                    Log("GameJolt Session was opened!");
                    BackgroundWorker_GameJolt.CancelAsync();
                    while (BackgroundWorker_GameJolt.IsBusy)
                        await Task.Delay(50);
                    BackgroundWorker_GameJolt.RunWorkerAsync();
                    PictureBox_GameJolt.Visible = true;
                    PictureBox_GameJolt_Offline.Visible = false;
                }
                else
                    Log("GameJolt Session could not be opened!");
            }

            //if (!await GameJolt.IsMigratedAsync())
            //    Log($"GameJolt User {Settings.GameJoltUsername} has not migrated!");

            return await GameJolt.IsConnectedAsync();
        }
        private async Task<bool> CloseGameJoltSessionAsync()
        {
            if (string.IsNullOrEmpty(Settings.GameJoltUsername))
            {
                Log($"Can't open Session! Credentials not set!");
                return false;
            }

            if (await GameJolt.IsConnectedAsync())
            {
                Log("Closing GameJolt Session...");
                var resp = await GameJolt.SessionCloseAsync();
                if (!resp.Success)
                    Log($"GameJolt Error: {resp.ErrorMessage}");
                if (!await GameJolt.IsConnectedAsync())
                {
                    Log("GameJolt Session was closed!");
                    PictureBox_GameJolt.Visible = false;
                    PictureBox_GameJolt_Offline.Visible = true;
                }
                else
                    Log("GameJolt Session was not opened! Can't close the Session!");
            }

            return !await GameJolt.IsConnectedAsync();
        }

        private async Task CheckLauncherForUpdateAsync()
        {
            var launcherVersion = Assembly.GetExecutingAssembly().GetName().Version;
            Log($"Launcher version: [{launcherVersion}].");
            Log("Checking Launcher for updates...");
            var launcherReleases = await GitHub.GetAllLauncherReleasesAsync();
            if (launcherReleases.Any())
            {
                var latestRelease = launcherReleases.First();
                if (launcherVersion < latestRelease.Version)
                {
                    Log($"Found a new Launcher version [{latestRelease.Version}]!");
                    switch (MessageBox.Show(LocalizationUI.GetString("LauncherUpdateAvailable"), LocalizationUI.GetString("LauncherUpdateAvailableTitle"), MessageBoxButtons.YesNo))
                    {
                        case DialogResult.Yes:
                            using (var directUpdater = new ReleaseDownloaderForm(latestRelease.ReleaseAsset, new UpdateFolder()))
                                directUpdater.ShowDialog();

                            Program.ActionsBeforeExit.Add(() => new UpdaterFile().Start(createNoWindow: true));
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
                MessageBox.Show(LocalizationUI.GetString("NoInternet"), LocalizationUI.GetString("NoInternetTitle"), MessageBoxButtons.OK);
            }
        }
        private async Task CheckProfileForUpdateAsync(bool onStartup = false) // -- onStartup - function is called on Launcher startup.
        {
            Log($"Checking Profile '{CurrentProfile.Name}' for updates...");

            if (!onStartup && CurrentProfile.ExecutionFile == null)
            {
                Log($"Profile '{CurrentProfile.Name}' - execution file not found! It was not downloaded?");
                await DownloadCurrentProfileAsync();
            }

            if (CurrentProfile.ExecutionFile == null)
            {
                Log($"Something went wrong while checking the Profile '{CurrentProfile.Name}' - execution file not found!");
                return;
            }

            var gameReleases = await CurrentProfile.GetAvailableReleasesAsync();
            if (gameReleases.Any() && (!onStartup || CurrentProfile.IsDefault))
            {
                var latestRelease = gameReleases.First();

                if (CurrentProfile.IsDefault)
                {
                    if(CurrentProfile.Version < latestRelease.Version)
                    {
                        Log($"Found a new Profile '{CurrentProfile.Name}' version [{latestRelease.Version}]!");
                        UpdateCurrentProfile(latestRelease);
                        return;
                    }
                }


                if (CurrentProfile.Version < latestRelease.Version || CurrentProfile.VersionExe < latestRelease.Version)
                {
                    Log($"Found a new Profile '{CurrentProfile.Name}' version [{latestRelease.Version}]!");
                    UpdateCurrentProfile(latestRelease);
                }
                else if (CurrentProfile.VersionExe != new Version("0.0") && CurrentProfile.VersionExe != new Version("1.0.0.0") && CurrentProfile.VersionExe < CurrentProfile.Version)
                {
                    Log($"The version of the execution file [{CurrentProfile.VersionExe}] of Profile '{CurrentProfile.Name}' does not correspond to the Profile version [{CurrentProfile.Version}]! An update is needed.");
                    UpdateCurrentProfile(latestRelease);
                }
                else
                {
                    if (!onStartup)
                    {
                        Log($"Profile '{CurrentProfile.Name}' is up to date.");
                        MessageBox.Show(string.Format(LocalizationUI.GetString("ProfileUpToDate"), CurrentProfile.Name), LocalizationUI.GetString("ProfileUpToDateTitle"), MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                if (!onStartup)
                {
                    Log($"Error while checking Profile '{CurrentProfile.Name}'. Is Internet available?");
                    MessageBox.Show(LocalizationUI.GetString("NoInternet"), LocalizationUI.GetString("NoInternetTitle"), MessageBoxButtons.OK);
                }
            }
        }
        private async Task<bool> DownloadCurrentProfileAsync()
        {
            var gameReleases = await CurrentProfile.GetAvailableReleasesAsync();
            if (gameReleases.Any())
            {
                var onlineRelease = gameReleases.First(release => release.Version == CurrentProfile.Version);

                switch (MessageBox.Show(string.Format(LocalizationUI.GetString("NotDownloaded"), CurrentProfile.Name), LocalizationUI.GetString("NotDownloadedTitle"), MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:
                        using (var directUpdater = new ReleaseDownloaderForm(onlineRelease.ReleaseAsset, CurrentProfile.Folder))
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
            switch (MessageBox.Show(string.Format(LocalizationUI.GetString("UpdateAvailable"), CurrentProfile.Version, onlineRelease.Version), LocalizationUI.GetString("UpdateAvailableTitle"), MessageBoxButtons.YesNo))
            {
                case DialogResult.Yes:
                    using (var directUpdater = new ReleaseDownloaderForm(onlineRelease.ReleaseAsset, CurrentProfile.Folder))
                        directUpdater.ShowDialog();
                    break;

                case DialogResult.No:
                    return;

                    /*
                    case DialogResult.Yes:
                        //MessageBox.Show(LocalizationUI.GetString("UpdateDisabled"), LocalizationUI.GetString("UpdateDisabledTitle"), MessageBoxButtons.OK);
                        if (Settings.SelectedDL != null && !string.IsNullOrEmpty(Settings.SelectedDL.AbsolutePath))
                            //using (var customUpdater = new PerFileUpdaterForm(onlineRelease.UpdateInfoAsset, CurrentProfile.Folder, new Uri(Settings.SelectedDL, $"{onlineRelease.Version}/")))
                            //    customUpdater.ShowDialog();
                            ;
                        else
                            MessageBox.Show(LocalizationUI.GetString("DLNotSelected"), LocalizationUI.GetString("DLNotSelectedTitle"), MessageBoxButtons.OK);
                        break;

                    case DialogResult.No:
                    //case DialogResult.Cancel:
                        using (var directUpdater = new ReleaseDownloaderForm(onlineRelease.ReleaseAsset, CurrentProfile.Folder))
                            directUpdater.ShowDialog();
                        break;

                    case DialogResult.Cancel:
                    //case DialogResult.No:
                        return;
                        */
            }
        }

        private async Task ReloadProfileListAsync(ProfilesFile.SelectedProfile selectedProfile = ProfilesFile.SelectedProfile.Current)
        {
            var selectedIndex = Profiles.SelectedProfileIndex;

            await Profiles.ReloadAsync(selectedProfile);

            if (Controls.Count == 0)
                return;

            ComboBox_CurrentProfile.DataSource = Profiles.ToArray();
            switch (selectedProfile)
            {
                case ProfilesFile.SelectedProfile.Current:
                    ComboBox_CurrentProfile.SelectedIndex = selectedIndex;
                    break;
                case ProfilesFile.SelectedProfile.First:
                    ComboBox_CurrentProfile.SelectedIndex = Profiles.Any() ? 0 : -1;
                    break;
                case ProfilesFile.SelectedProfile.Last:
                    ComboBox_CurrentProfile.SelectedIndex = Profiles.Any() ? Profiles.Count() - 1 : -1;
                    break;
            }
            
        }
        private async void ReloadSettings()
        {
            await Settings.ReloadAsync();

            if (Controls.Count == 0)
                return;

            Check_Updates.Checked = Settings.GameUpdates;
            CheckBox_CheckServerOnStart.Checked = Settings.GameCheckServer;

            ComboBox_Language.Items.Clear();
            var localizations = LocalizationUI.Localizations;
            foreach (var localizationInfo in LocalizationUI.Localizations)
            {
                var cultureInfo = localizationInfo.CultureInfo;
                var nativeNameTitleCase = cultureInfo.TextInfo.ToTitleCase(cultureInfo.NativeName);

                var locaizationName = !string.IsNullOrEmpty(localizationInfo.SubLanguage)
                    ? $"{nativeNameTitleCase} [{localizationInfo.SubLanguage}]"
                    : nativeNameTitleCase;
                ComboBox_Language.Items.Add(locaizationName);
            }
            ComboBox_Language.SelectedIndex = localizations.IndexOf(localizationInfo => Equals(localizationInfo.CultureInfo, Settings.LocalizationInfo.CultureInfo) && Equals(localizationInfo.SubLanguage, Settings.LocalizationInfo.SubLanguage));

            GameJolt = new GameJolt(Settings.GameJoltUsername, Settings.GameJoltToken);
            TextBox_Username.Text = Settings.GameJoltUsername;
            TextBox_Token.Text = Settings.GameJoltToken;
            CheckBox_AutoLogIn.Checked = Settings.AutoLogIn;
            CheckBox_SaveCredentials.Checked = Settings.SaveCredentials;

            ComboBox_SelectedDL.Items.Clear();
            if (SettingsFile.DLList.Any())
            {
                foreach (var dlUri in SettingsFile.DLList)
                    ComboBox_SelectedDL.Items.Add(dlUri);
                ComboBox_SelectedDL.SelectedIndex = Settings.SelectedDLIndex;
            }

            await OpenGameJoltSessionAsync();
        }
    }
}
    