using System;

using P3D.Legacy.Launcher.Data;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.YamlConverters
{
    internal class ProfileTypeConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => type == typeof(ProfileType);

        public object ReadYaml(IParser parser, Type type)
        {
            var value = ((Scalar) parser.Current).Value;
            parser.MoveNext();
            return ProfileType.GetProfileType(value);
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var profileType = (ProfileType) value;
            emitter.Emit(new Scalar(null, null, profileType.Name, ScalarStyle.Plain, true, false));
        }
    }
}