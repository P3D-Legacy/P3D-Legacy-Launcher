using System;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.YamlConverters
{
    internal class VersionConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => type == typeof(System.Version);

        public object ReadYaml(IParser parser, Type type)
        {
            var value = ((Scalar) parser.Current).Value;
            parser.MoveNext();
            return new System.Version(value);
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var version = (System.Version) value;
            emitter.Emit(new Scalar(null, null, version.ToString(), ScalarStyle.Plain, true, false));
        }
    }
}