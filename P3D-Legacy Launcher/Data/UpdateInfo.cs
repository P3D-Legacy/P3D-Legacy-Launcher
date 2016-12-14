using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Data
{
    public class UpdateInfo
    {
        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder();
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder();

        public static Uri[] DLUris { get; } = GetDLUris();
        private static Uri[] GetDLUris()
        {
            var downloaded = new WebClient().DownloadString("https://raw.githubusercontent.com/P3D-Legacy/P3D-Legacy-Data/master/DLUris.txt");
            var strings = string.IsNullOrEmpty(downloaded) ? new string[0] : downloaded.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return strings.All(string.IsNullOrEmpty) ? new Uri[0] : strings.Select(str => new Uri(str)).ToArray();
        }

        public List<UpdateFileEntry> Files { get; set; } = new List<UpdateFileEntry>();
    }

    public class UpdateFileEntry
    {
        public string AbsoluteFilePath { get; set; }
        public string CRC32 { get; set; }
        public string SHA1 { get; set; }
        public long Size { get; set; }
    }
}