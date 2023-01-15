using System.Net.Http.Json;
using VirtualIT.Client.Extensions;
using VirtualIT.Shared.Beheerder;

namespace VirtualIT.Client.Pages.Beheerders;

public class BeheerderService : IBeheerderService
{
    private readonly HttpClient client;
    private const string endpoint = "api/Beheerder";

    public BeheerderService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<BeheerderResponse.GetIndex> GetAllIndexAsync()
    {
        var response = await client.GetFromJsonAsync<BeheerderResponse.GetIndex>($"{endpoint}");

        return response!;
    }

    public async Task<int> CreateAsync(BeheerderDto.Create model)
    {
        var response = await client.PostAsJsonAsync(endpoint, model);

        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task EditAsync(int beheerderId, BeheerderDto.Edit model) {
        var response = await client.PutAsJsonAsync($"{endpoint}/{beheerderId}", model);
    }

    public async Task<BeheerderDto.Detail> GetDetailAsync(int beheerderId) {
        var beheerder = await client.GetFromJsonAsync<BeheerderDto.Detail>($"{endpoint}/{beheerderId}");
        
        return beheerder!;
    }

    public async Task<BeheerderResponse.GetIndex> GetAllBeheerderRoleIndexAsync() {
        var response = await client.GetFromJsonAsync<BeheerderResponse.GetIndex>($"{endpoint}/GetBeheerdersBeheerderRole");

        return response!;
    }

    public Task<BeheerderResponse.Detail> GetAllDetailASync() {
        throw new NotImplementedException();
    }
}
