using System.Collections.Generic;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.UpdateInfoBuilder.Data
{
    public class UpdateInfo
    {
        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder();
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder();

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