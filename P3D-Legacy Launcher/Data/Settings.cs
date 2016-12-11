using System.Globalization;

using P3D.Legacy.Launcher.Yaml;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Data
{
    public class Settings
    {
        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().WithTypeConverter(new CultureInfoConverter());
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().WithTypeConverter(new CultureInfoConverter());

        public static CultureInfo[] AvailableCultureInfo { get; } = { new CultureInfo("en"), new CultureInfo("ru"), new CultureInfo("lt") };

        public bool GameUpdates { get; set; } = false;
        public CultureInfo Language { get; set; } = new CultureInfo("en");
    }
}