using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using P3D.Legacy.Launcher.Converters;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Data
{
    public class SettingsYaml
    {
        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().WithTypeConverter(new CultureInfoConverter()).WithTypeConverter(new UriConverter());
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().WithTypeConverter(new CultureInfoConverter()).WithTypeConverter(new UriConverter());

        public static CultureInfo[] AvailableCultureInfo { get; } = {
            new CultureInfo("en"), new CultureInfo("ru"), new CultureInfo("lt"),
            new CultureInfo("nl"), new CultureInfo("es"), new CultureInfo("de"),
            new CultureInfo("pl") 
            /* new CultureInfo("fr"), new CultureInfo("it"), new CultureInfo("pt") */
        };

        public static SettingsYaml Default => new SettingsYaml { GameUpdates = true, Language = new CultureInfo("en") };


        public bool GameUpdates { get; set; }
        public CultureInfo Language { get; set; }

        private int _selectedDLIndex;
        public int SelectedDLIndex { get { return _selectedDLIndex; } set { if (value > 0 || value < DLList.Count) _selectedDLIndex = value; } }
        public static List<Uri> DLList { get; } = GetDLUris();
        private static List<Uri> GetDLUris()
        {
            var downloaded = new WebClient().DownloadString("https://raw.githubusercontent.com/P3D-Legacy/P3D-Legacy-Data/master/DLUris.txt");
            var strings = string.IsNullOrEmpty(downloaded) ? new string[0] : downloaded.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return strings.All(string.IsNullOrEmpty) ? new List<Uri>() : strings.Select(str => new Uri(str)).ToList();
        }

        public Uri GetDL() => DLList.Any() ? DLList[SelectedDLIndex] : null;
        public bool IsValid() => Language != null /* && SelectedDL != null*/;
    }
}