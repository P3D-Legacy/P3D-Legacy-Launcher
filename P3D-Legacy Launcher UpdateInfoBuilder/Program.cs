using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

using DamienG.Security.Cryptography;

using P3D.Legacy.Launcher.UpdateInfoBuilder.Data;

namespace P3D.Legacy.Launcher.UpdateInfoBuilder
{
    public static class Program
    {
        private static string MainFolderPath { get; } = AppDomain.CurrentDomain.BaseDirectory;

        private const string OutputFilename = "UpdateInfo.yml";
        private static string OutputFilePath { get; } = Path.Combine(MainFolderPath, OutputFilename);

        public static void Main(string[] args)
        {
            var updateInfoPath = args[0];


            //var allAbsoluteFilePaths = Directory.GetFiles(updateInfoPath, "*.*", SearchOption.AllDirectories).Select(filePath => filePath.Replace(updateInfoPath, ""));
            var allAbsoluteFilePaths = Directory.GetFiles(updateInfoPath, "*.*", SearchOption.AllDirectories).Select(filePath => filePath.Substring(updateInfoPath.Length + 1));

            var crc32 = new Crc32();
            var sha1 = new SHA1Managed();
            var updateFileEntries = new List<UpdateFileEntryYaml>();
            foreach (var absoluteFilePath in allAbsoluteFilePaths)
            {
                var filePath = Path.Combine(updateInfoPath, absoluteFilePath);
                var length = new FileInfo(filePath).Length;
                using (var fs = File.OpenRead(filePath))
                {
                    var crc32Hash = string.Empty;
                    var sha1Hash = string.Empty;
                    crc32Hash = crc32.ComputeHash(fs).Aggregate(crc32Hash, (current, b) => current + b.ToString("x2").ToLower());
                    sha1Hash = sha1.ComputeHash(fs).Aggregate(sha1Hash, (current, b) => current + b.ToString("x2").ToLower());
                    updateFileEntries.Add(new UpdateFileEntryYaml { AbsoluteFilePath = absoluteFilePath, CRC32 = crc32Hash, SHA1 = sha1Hash, Size = length });
                }
            }

            var serializer = UpdateInfoYaml.SerializerBuilder.Build();
            var content = serializer.Serialize(new UpdateInfoYaml { Files = updateFileEntries });

            File.WriteAllText(OutputFilePath, content);
        }
    }
}
