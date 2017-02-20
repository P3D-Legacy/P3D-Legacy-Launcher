using System;
using System.Linq;
using System.Windows.Forms;

using P3D.Legacy.Launcher.Data;

namespace P3D.Legacy.Launcher.Forms
{
    internal partial class ProfileForm : Form
    {
        public static ProfileForm ProfileNew(ProfileYaml profile) => new ProfileForm(profile, true);
        public static ProfileForm ProfileEdit(ProfileYaml profile) => new ProfileForm(profile);

        private ProfileYaml Profile => new ProfileYaml { Name = TextBox_ProfileName.Text, Version = new Version((string) ComboBox_Version.SelectedItem) };
        private string ProfileOldName { get; }


        private ProfileForm(ProfileYaml profile, bool copy = false)
        {
            InitializeComponent();

            ComboBox_Version.Items.Clear();
            foreach (var availableVersion in ProfilesYaml.AvailableVersions)
                ComboBox_Version.Items.Add(availableVersion.ToString());


            if (copy)
                TextBox_ProfileName.Text = $"Copy of {profile.Name}";
            else
            {
                TextBox_ProfileName.Text = profile.Name;
                ProfileOldName = profile.Name;
            }
            ComboBox_Version.SelectedIndex = ComboBox_Version.Items.IndexOf(profile.Version.ToString());
        }


        private void Button_SaveProfile_Click(object sender, EventArgs e)
        {
            var profiles = ProfilesYaml.Load();

            if (!string.IsNullOrEmpty(ProfileOldName))
            {
                for (var i = 0; i < profiles.ProfileList.Count; i++)
                    if (profiles.ProfileList[i].Name == ProfileOldName)
                        profiles.ProfileList[i] = Profile;
            }
            else
            {
                if(profiles.ProfileList.All(profile => profile.Name != Profile.Name))
                    profiles.ProfileList.Add(Profile);
            }

            ProfilesYaml.Save(profiles);
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
