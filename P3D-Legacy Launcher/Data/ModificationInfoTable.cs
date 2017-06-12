using System;
using System.ComponentModel;

using P3D.Legacy.Shared.Data;

namespace P3D.Legacy.Launcher.Data
{
    public class ModificationInfoTable
    {
        private ModificationInfo ModificationInfo { get; }

        public ModificationInfoTable(ModificationInfo modificationInfo) { ModificationInfo = modificationInfo; }

        [Browsable(false)]
        public string ID => ModificationInfo.ID;

        [DisplayName("Author")]
        public string Author => ModificationInfo.Author;

        [DisplayName("Name")]
        public string Name => ModificationInfo.Name;

        [Browsable(false)]
        public string Description => ModificationInfo.Description;

        [Browsable(false)]
        public string InGameDescription => ModificationInfo.InGameDescription;

        [DisplayName("Category")]
        public ModificationCategories Category => ModificationInfo.Category;

        [DisplayName("Version")]
        public Version Version => ModificationInfo.Version;

        [DisplayName("Game Version")]
        public Version GameVersion => ModificationInfo.GameVersion;

        // -- Online Info

        [DisplayName("Downloads")]
        public long Downloads => ModificationInfo.Downloads;

        [Browsable(false)]
        public byte Rating => ModificationInfo.Rating;

        [DisplayName("Rating")]
        public string RatingString => ModificationInfo.RatingString;

        // -- Online Info
    }
}
