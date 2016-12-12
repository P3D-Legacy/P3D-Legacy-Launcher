﻿using System.Globalization;

using P3D.Legacy.Launcher.Yaml;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Data
{
    public class Settings
    {
        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().WithTypeConverter(new CultureInfoConverter());
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().WithTypeConverter(new CultureInfoConverter());

        public static CultureInfo[] AvailableCultureInfo { get; } = {
            new CultureInfo("en"), new CultureInfo("ru"), new CultureInfo("lt"),
            new CultureInfo("lt"), new CultureInfo("nl"), new CultureInfo("es"),
            new CultureInfo("de"), /* new CultureInfo("fr"), new CultureInfo("it"),
            new CultureInfo("pl"), new CultureInfo("pt") */
        };

        public static Settings Default => new Settings { GameUpdates = true, Language = new CultureInfo("en") };


        public bool GameUpdates { get; set; }
        public CultureInfo Language { get; set; }
    }
}