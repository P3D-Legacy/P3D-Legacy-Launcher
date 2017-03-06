using System;
using System.Linq;

using P3D.Legacy.Launcher.Controls;
using P3D.Legacy.Launcher.Data;

namespace P3D.Legacy.Launcher.Forms
{
    internal partial class ProfileForm : LocalizableForm
    {
        public static ProfileForm ProfileNew(Profiles profiles) => new ProfileForm(profiles, true);
        public static ProfileForm ProfileEdit(Profiles profiles) => new ProfileForm(profiles);

        private Profiles Profiles { get; }
        private Profile Profile => new Profile(TextBox_ProfileName.Text, new Version((string) ComboBox_Version.SelectedItem));
        private Profile ProfileOld { get; }

        private ProfileForm(Profiles profiles, bool copy = false)
        {
            Profiles = profiles;

            InitializeComponent();

            ComboBox_Version.Items.Clear();
            foreach (var availableVersion in Profiles.AvailableVersions)
                ComboBox_Version.Items.Add(availableVersion.ToString());


            if (copy)
                TextBox_ProfileName.Text = $"Copy of {Profiles.CurrentProfile.Name}";
            else
            {
                TextBox_ProfileName.Text = Profiles.CurrentProfile.Name;
                ProfileOld = Profiles.CurrentProfile;
            }
            ComboBox_Version.SelectedIndex = ComboBox_Version.Items.IndexOf(Profiles.CurrentProfile.Version.ToString());
        }

        private async void Button_SaveProfile_Click(object sender, EventArgs e)
        {
            if (ProfileOld != null)
            {
                await Profiles.ReplaceAsync(ProfileOld, Profile);
            }
            else
            {
                if (Profiles.All(profile => profile.Name != Profile.Name))
                    await Profiles.Create(Profile);
            }

            await Profiles.SaveAsync();
            Close();
        }
        private void Button_Cancel_Click(object sender, EventArgs e) => Close();
    }
}
