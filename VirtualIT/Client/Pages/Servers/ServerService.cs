using System.Net.Http.Json;
using VirtualIT.Shared.Klanten;
using VirtualIT.Shared.Server;

namespace VirtualIT.Client.Pages.Servers {
    public class ServerService : IServerService {
        private readonly HttpClient client;
        private const string endpoint = "api/Server";

        public ServerService(HttpClient client) {
            this.client = client;
        }

        public async Task<ServerResponse.Index> GetAllIndexAsync() {
            var response = await client.GetFromJsonAsync<ServerResponse.Index>($"{endpoint}");
            return response!;
        }
    }
}
