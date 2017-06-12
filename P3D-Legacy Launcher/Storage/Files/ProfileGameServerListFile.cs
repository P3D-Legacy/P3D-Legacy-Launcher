using System.Linq;

using P3D.Legacy.Launcher.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Storage.Files
{
    internal class ProfileGameServerListFile : BaseFile
    {
        public ProfileGameServerListFile(ProfileGameSaveFolder saveFolder) : base(saveFolder.CreateFile("server_list.dat", CreationCollisionOption.OpenIfExists)) { }


        public void CheckServers()
        {
            var foundOfficial = false;

            var servers = this.ReadAllLines().ToList();
            foreach (var server in servers)
            {
                var args = server.Split(',');
                var serverName = args[0];
                var serverHost = args[1];

                if (serverHost.Contains("karp.pokemon3d.net"))
                    foundOfficial = true;
            }

            if (!foundOfficial)
            {
                servers.Insert(0, "The Official Pokémon3D Server,karp.pokemon3d.net");
                this.WriteAllLines(servers);
            }
        }
    }
}
