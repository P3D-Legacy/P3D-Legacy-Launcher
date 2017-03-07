using System;
using System.Diagnostics;
using System.Linq;

using P3D.Legacy.Launcher.Controls;
using P3D.Legacy.Launcher.Data;

using P3D.Legacy.Shared.Extensions;

namespace P3D.Legacy.Launcher.Forms
{
    internal partial class ProfileForm : LocalizableForm
    {
        public static ProfileForm ProfileNew(Profiles profiles) => new ProfileForm(profiles, true);
        public static ProfileForm ProfileEdit(Profiles profiles) => new ProfileForm(profiles);

        private Profiles Profiles { get; }
        private Profile CurrentProfile => Profiles.CurrentProfile;
        private Profile NewProfile => new Profile(ProfileType.GetProfileType(ComboBox_Type.SelectedIndex), TextBox_ProfileName.Text, new Version((string) ComboBox_Version.SelectedItem), TextBox_LaunchArgs.Text);
        private Profile OldProfile { get; }

        private ProfileForm(Profiles profiles, bool copy = false)
        {
            Profiles = profiles;

            InitializeComponent();

            if (!copy && CurrentProfile.IsDefault)
            {
                Label_NoEdit.Visible = true;
                ComboBox_Type.Enabled = false;
                TextBox_ProfileName.Enabled = false;
                ComboBox_Version.Enabled = false;
            }

            ComboBox_Type.DataSource = CurrentProfile.ProfileType.ToArray();
            ComboBox_Type.SelectedIndex = ProfileType.GetIndex(CurrentProfile.ProfileType);

            if (copy)
                TextBox_ProfileName.Text = $"Copy of {CurrentProfile.Name}";
            else
            {
                TextBox_ProfileName.Text = CurrentProfile.Name;
                OldProfile = CurrentProfile;
            }
            ComboBox_Version.SelectedIndex = ComboBox_Version.Items.IndexOf(CurrentProfile.Version.ToString());
            TextBox_LaunchArgs.Text = CurrentProfile.LaunchArgs;
        }

        private void Button_OpenProfileDir_Click(object sender, EventArgs e) => Process.Start(CurrentProfile.Folder.Path);
        private async void Button_SaveProfile_Click(object sender, EventArgs e)
        {
            if (OldProfile != null)
            {
                await Profiles.ReplaceAsync(OldProfile, NewProfile);
            }
            else
            {
                if (Profiles.All(profile => profile.Name != NewProfile.Name))
                    await Profiles.CreateAsync(NewProfile);
            }

            await Profiles.SaveAsync();
            Close();
        }
        private void Button_Cancel_Click(object sender, EventArgs e) => Close();

        private void ComboBox_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            var profileType = ProfileType.GetProfileType(ComboBox_Type.SelectedIndex);
            ComboBox_Version.DataSource = AsyncExtensions.RunSync(async () => await Profile.GetAvailableVersionsAsync(profileType)).Select(profile => profile.ToString()).ToArray();

            if (profileType == ProfileType.Game)
            {
                if (CurrentProfile.ProfileType == ProfileType.Game && ComboBox_Version.Items.Count > 0)
                    ComboBox_Version.SelectedIndex = ComboBox_Version.Items.IndexOf(CurrentProfile.Version.ToString());
                else if (ComboBox_Version.Items.Count > 0)
                    ComboBox_Version.SelectedIndex = 0;
                else
                    ComboBox_Version.SelectedIndex = -1;
            }
            else if (profileType == ProfileType.Server1)
            {
                if (CurrentProfile.ProfileType == ProfileType.Server1 && ComboBox_Version.Items.Count > 0)
                    ComboBox_Version.SelectedIndex = ComboBox_Version.Items.IndexOf(CurrentProfile.Version.ToString());
                else if (ComboBox_Version.Items.Count > 0)
                    ComboBox_Version.SelectedIndex = 0;
                else
                    ComboBox_Version.SelectedIndex = -1;
            }
            else if (profileType == ProfileType.Server2)
            {
                    if (CurrentProfile.ProfileType == ProfileType.Server2 && ComboBox_Version.Items.Count > 0)
                        ComboBox_Version.SelectedIndex = ComboBox_Version.Items.IndexOf(CurrentProfile.Version.ToString());
                    else if (ComboBox_Version.Items.Count > 0)
                        ComboBox_Version.SelectedIndex = 0;
                    else
                        ComboBox_Version.SelectedIndex = -1;
            }
        }
    }
}
