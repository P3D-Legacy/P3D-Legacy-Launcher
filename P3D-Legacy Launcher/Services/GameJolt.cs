using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using JoltNet;

using P3D.Legacy.Shared.Data;

namespace P3D.Legacy.Launcher.Services
{
    internal class GameJolt
    {
        private static readonly EncodedString Host = "gamejolt.com";

        private static string GameId = "";
        private static string GameKey = "";

        private static string OldGameId = "";
        private static string OldGameKey = "";

        private static GameJoltApiClient Client { get; } = new GameJoltApiClient(EncodedString.FromEncodedData(GameId), EncodedString.FromEncodedData(GameKey));
        private static GameJoltApiClient OldClient { get; } = new GameJoltApiClient(EncodedString.FromEncodedData(OldGameId), EncodedString.FromEncodedData(OldGameKey));

        private static WebsiteChecker WebsiteChecker { get; } = new WebsiteChecker(Host);
        public static bool WebsiteIsUp => WebsiteChecker.Check();


        public GameJoltYaml GameJoltYaml => new GameJoltYaml() { GameId = OldGameId, GameKey = OldGameKey, Username = Username, Token = Token };

        private string Username { get; }
        private string Token { get; }

        private DateTime _lastSessionPing = DateTime.MinValue;


        public GameJolt() { }
        public GameJolt(string username, string token) { Username = username; Token = token; }


        public async Task<bool> IsConnectedAsync()
        {
            // BUG: Is this a bug? After creating new GameJolt(), _lastSessionPing is a MinValue, it returns false info.
            if (_lastSessionPing == DateTime.MinValue)
                return false;

            if (DateTime.UtcNow - _lastSessionPing > TimeSpan.FromSeconds(30))
                await IsSessionActiveAsync();

            return true;
        }

        public async Task<GameJoltResponse> IsSessionActiveAsync()
        {
            if (!WebsiteIsUp)
            {
                var ctor = typeof(GameJoltResponse).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(string) }, null);
                return (GameJoltResponse) ctor.Invoke(new object[] {"Website is down!"});
            }

            var pingRequest = RequestProvider.Sessions.Ping(SessionStatus.Active, Username, Token);
            await Client.ExecuteRequestAsync(pingRequest);

            if (pingRequest.Response.Success)
                _lastSessionPing = DateTime.UtcNow;
            else
                _lastSessionPing = DateTime.MinValue;

            return pingRequest.Response;
        }
        public async Task<GameJoltResponse> SessionOpenAsync()
        {
            if (!WebsiteIsUp)
            {
                var ctor = typeof(GameJoltResponse).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(string) }, null);
                return (GameJoltResponse) ctor.Invoke(new object[] {"Website is down!"});
            }

            var openRequest = RequestProvider.Sessions.Open(Username, Token);
            await Client.ExecuteRequestAsync(openRequest);

            if (openRequest.Response.Success)
                _lastSessionPing = DateTime.UtcNow;
            else
                _lastSessionPing = DateTime.MinValue;

            return openRequest.Response;
        }
        public async Task<GameJoltResponse> SessionCloseAsync()
        {
            if (!WebsiteIsUp)
            {
                var ctor = typeof(GameJoltResponse).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(string) }, null);
                return (GameJoltResponse) ctor.Invoke(new object[] {"Website is down!"});
            }

            var closeRequest = RequestProvider.Sessions.Close(Username, Token);
            await Client.ExecuteRequestAsync(closeRequest).ConfigureAwait(false);

            if (closeRequest.Response.Success)
                _lastSessionPing = DateTime.MinValue;

            return closeRequest.Response;
        }

        public async Task<bool> IsMigratedAsync()
        {
            if (!await IsConnectedAsync() || !WebsiteIsUp) return false;

            var userRequest = RequestProvider.Users.Fetch(Username);
            await Client.ExecuteRequestAsync(userRequest);
            if (userRequest.Response?.Users?.Any() == null) return false;

            var request = RequestProvider.Storage.Fetch($"saveStorageV1|{userRequest.Response.Users[0].Id}|migrated", Username, Token);
            await OldClient.ExecuteRequestAsync(request).ConfigureAwait(false);
            return request.Response.Success;
        }
        public async Task MigrateAsync()
        {
            var userRequest = RequestProvider.Users.Fetch(Username);
            await Client.ExecuteRequestAsync(userRequest);



            var keysRequest = RequestProvider.Storage.GetKeys(Username, Token);
            await OldClient.ExecuteRequestAsync(keysRequest).ConfigureAwait(false);
            var keyDictionary = new Dictionary<string, string>();
            foreach (var key in keysRequest.Response.Keys)
            {
                var keyRequest = RequestProvider.Storage.Fetch(key, Username, Token);
                await OldClient.ExecuteRequestAsync(keyRequest).ConfigureAwait(false);
                keyDictionary.Add(key, keyRequest.Response.Data);
            }

            foreach (var keyValuePair in keyDictionary)
            {
                var keyRequest = RequestProvider.Storage.Set(keyValuePair.Key, keyValuePair.Value, Username, Token);
                await Client.ExecuteRequestAsync(keyRequest).ConfigureAwait(false);
            }


            var emblemRequest = RequestProvider.Storage.Fetch($"saveStorageV1|{userRequest.Response.Users[0].Id}|emblem", Username, Token);
            var genderRequest = RequestProvider.Storage.Fetch($"saveStorageV1|{userRequest.Response.Users[0].Id}|gender", Username, Token);
            var pointsRequest = RequestProvider.Storage.Fetch($"saveStorageV1|{userRequest.Response.Users[0].Id}|points", Username, Token);
            await OldClient.ExecuteRequestAsync(emblemRequest).ConfigureAwait(false);
            await OldClient.ExecuteRequestAsync(genderRequest).ConfigureAwait(false);
            await OldClient.ExecuteRequestAsync(pointsRequest).ConfigureAwait(false);

            var emblemRequest1 = RequestProvider.Storage.Set($"saveStorageV1|{userRequest.Response.Users[0].Id}|emblem", emblemRequest.Response.Data, Username, Token);
            var genderRequest1 = RequestProvider.Storage.Set($"saveStorageV1|{userRequest.Response.Users[0].Id}|gender", genderRequest.Response.Data, Username, Token);
            var pointsRequest1 = RequestProvider.Storage.Set($"saveStorageV1|{userRequest.Response.Users[0].Id}|points", pointsRequest.Response.Data, Username, Token);
            await Client.ExecuteRequestAsync(emblemRequest1).ConfigureAwait(false);
            await Client.ExecuteRequestAsync(genderRequest1).ConfigureAwait(false);
            await Client.ExecuteRequestAsync(pointsRequest1).ConfigureAwait(false);


            var migratedRequest = RequestProvider.Storage.Set($"saveStorageV1|{userRequest.Response.Users[0].Id}|migrated", "", Username, Token);
            await OldClient.ExecuteRequestAsync(migratedRequest).ConfigureAwait(false);
        }
    }
}
