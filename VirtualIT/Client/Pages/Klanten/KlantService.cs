using System.Net.Http.Json;
using VirtualIT.Client.Extensions;
using VirtualIT.Shared.Klanten;

namespace VirtualIT.Client.Pages.Klanten {
    public class KlantService : IKlantService {
        private readonly HttpClient client;
        private const string endpoint = "api/Klant";
        
        public KlantService(HttpClient client) {
            this.client = client;
        }

        public async Task<KlantResponse.Index> GetAllIndexAsync() {
            var response = await client.GetFromJsonAsync<KlantResponse.Index>($"{endpoint}");
            return response!;
        }

        public async Task<KlantDto.Detail> GetDetailAsync(int klantId) {
            var response = await client.GetFromJsonAsync<KlantDto.Detail>($"{endpoint}/{klantId}");
            return response!;
        }

        public async Task<int> CreateAsync(KlantDto.Mutate model) {
            var response = await client.PostAsJsonAsync(endpoint, model);
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task EditAsync(int klantId, KlantDto.Mutate model) {
            await client.PutAsJsonAsync($"{endpoint}/{klantId}", model);
        }

        public Task<KlantResponse.Detail> GetAllDetailASync()
        {
            throw new NotImplementedException();
        }
    }
}
