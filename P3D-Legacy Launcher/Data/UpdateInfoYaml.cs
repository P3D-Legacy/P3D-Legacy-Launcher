using System.Collections;
using System.Collections.Generic;

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Data
{
    internal class UpdateInfoYaml : IEnumerable<UpdateFileEntryYaml>
    {
        private static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder();
        private static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder();

        public static string Serialize(UpdateInfoYaml updateInfo)
        {
            var serializer = SerializerBuilder.Build();
            return serializer.Serialize(updateInfo);
        }
        public static UpdateInfoYaml Deserialize(string data)
        {
            var deserializer = DeserializerBuilder.Build();
            try { return deserializer.Deserialize<UpdateInfoYaml>(data); }
            catch (YamlException) { return null; }
        }


        [YamlMember(Alias = "Files")]
        private List<UpdateFileEntryYaml> Files { get; set; }

        public UpdateInfoYaml(List<UpdateFileEntryYaml> updateFileEntries) { Files = updateFileEntries; }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<UpdateFileEntryYaml> GetEnumerator() => Files.GetEnumerator();
    }
    internal class UpdateFileEntryYaml
    {
        [YamlMember(Alias = "AbsoluteFilePath")]
        public string AbsoluteFilePath { get; set; }
        [YamlMember(Alias = "CRC32")]
        public string CRC32 { get; set; }
        [YamlMember(Alias = "SHA1")]
        public string SHA1 { get; set; }
        [YamlMember(Alias = "Size")]
        public long Size { get; set; }
    }
}