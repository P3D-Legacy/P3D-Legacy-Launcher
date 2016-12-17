using System.Collections.Generic;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.UpdateInfoBuilder.Data
{
    public class UpdateInfoYaml
    {
        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder();
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder();

        public List<UpdateFileEntryYaml> Files { get; set; } = new List<UpdateFileEntryYaml>();
    }
    public class UpdateFileEntryYaml
    {
        public string AbsoluteFilePath { get; set; }
        public string CRC32 { get; set; }
        public string SHA1 { get; set; }
        public long Size { get; set; }
    }
}