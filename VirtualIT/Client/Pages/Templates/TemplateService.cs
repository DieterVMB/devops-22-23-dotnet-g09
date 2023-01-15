using System.Net.Http.Json;
using VirtualIT.Shared.Templates;

namespace VirtualIT.Client.Pages.Templates {
    public class TemplateService : ITemplateService {
        private readonly HttpClient client;
        private const string endpoint = "api/Template";

        public TemplateService(HttpClient client) {
            this.client = client;
        }

        public async Task<TemplateResponse.Index> GetAllIndexAsync(){
            var response = await client.GetFromJsonAsync<TemplateResponse.Index>($"{endpoint}");
            return response!;
        }
    }
}
