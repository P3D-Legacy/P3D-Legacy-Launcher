using System.Threading;
using System.Threading.Tasks;

using PCLExt.FileStorage;

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Storage.Files
{
    public abstract class BaseYamlSettingsFile<TYamlSettings> : BaseFile where TYamlSettings : class, IYamlSettings
    {
        protected abstract SerializerBuilder SerializerBuilder { get; }
        protected abstract DeserializerBuilder DeserializerBuilder { get; }

        protected abstract TYamlSettings Default { get; }

        private readonly SemaphoreSlim _ioLock = new SemaphoreSlim(1);


        protected BaseYamlSettingsFile(IFile yamlSettingsFile) : base(yamlSettingsFile) { Load(); }


        public virtual void Save() => Save(ToYaml());
        public virtual void Reload() => Load();

        public virtual async Task SaveAsync() => await SaveAsync(ToYaml());
        public virtual async Task ReloadAsync() => await LoadAsync();

        protected abstract TYamlSettings ToYaml();
        protected abstract void FromYaml(TYamlSettings yaml);

        private void Save(TYamlSettings settingsYaml)
        {
            var serializer = SerializerBuilder.Build();
            _ioLock.Wait();
            this.WriteAllText(serializer.Serialize(settingsYaml));
            _ioLock.Release();
        }
        private async Task SaveAsync(TYamlSettings settingsYaml)
        {
            var serializer = SerializerBuilder.Build();
            await _ioLock.WaitAsync();
            await this.WriteAllTextAsync(serializer.Serialize(settingsYaml));
            _ioLock.Release();
        }

        private void Load()
        {
            var deserializer = DeserializerBuilder.Build();
            try
            {
                _ioLock.Wait();
                var deserialized = deserializer.Deserialize<TYamlSettings>(this.ReadAllText());
                _ioLock.Release();
                if (deserialized != null && deserialized.IsValid())
                    FromYaml(deserialized);
                else
                {
                    var @default = Default;
                    Save(@default);
                    FromYaml(@default);
                }
            }
            catch (YamlException)
            {
                Save(Default);
                _ioLock.Wait();
                FromYaml(deserializer.Deserialize<TYamlSettings>(this.ReadAllText()) ?? Default);
                _ioLock.Release();
            }
        }
        private async Task LoadAsync()
        {
            var deserializer = DeserializerBuilder.Build();
            try
            {
                await _ioLock.WaitAsync();
                var deserialized = deserializer.Deserialize<TYamlSettings>(await this.ReadAllTextAsync());
                _ioLock.Release();
                if (deserialized != null && deserialized.IsValid())
                    FromYaml(deserialized);
                else
                {
                    var @default = Default;
                    await SaveAsync(@default);
                    FromYaml(@default);
                }
            }
            catch (YamlException)
            {
                await SaveAsync(Default);
                await _ioLock.WaitAsync();
                FromYaml(deserializer.Deserialize<TYamlSettings>(await this.ReadAllTextAsync()) ?? Default);
                _ioLock.Release();
            }
        }
    }
}