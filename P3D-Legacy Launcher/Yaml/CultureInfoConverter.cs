using System;
using System.Globalization;
using System.Linq;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Yaml
{
    public class CultureInfoConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => type == typeof(CultureInfo);

        private CultureInfo GetCultureInfo(string nativeName) => CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(info => info.NativeName == nativeName);
        public object ReadYaml(IParser parser, Type type)
        {
            var value = ((Scalar) parser.Current).Value;
            parser.MoveNext();
            return GetCultureInfo(value);
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var version = (CultureInfo) value;
            emitter.Emit(new Scalar(null, null, version.NativeName, ScalarStyle.Plain, true, false));
        }
    }
}