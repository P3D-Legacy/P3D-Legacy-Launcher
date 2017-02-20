using System;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.YamlConverters
{
    internal class UriConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => type == typeof(Uri);

        public object ReadYaml(IParser parser, Type type)
        {
            var value = ((Scalar) parser.Current).Value;
            parser.MoveNext();
            return new Uri(value);
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var uri = (Uri) value;
            emitter.Emit(new Scalar(null, null, uri.AbsoluteUri, ScalarStyle.Plain, true, false));
        }
    }
}