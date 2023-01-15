using GridShared;
using System.Net.Http.Json;
using VirtualIT.Client.Extensions;
using VirtualIT.Shared.VirtualMachines;

namespace VirtualIT.Client.Pages.VirtualMachines {
    public class VirtualMachineService : IVirtualMachineService {
        private readonly HttpClient client;
        private const string endpoint = "api/virtualmachine";
        
        public VirtualMachineService(HttpClient client) {
            this.client = client;
        }

        public async Task<VirtualMachineResponse.SingleDetail> GetDetailAsync(int virtualMachineId) {
            var response = await client.GetFromJsonAsync<VirtualMachineResponse.SingleDetail>($"{endpoint}/{virtualMachineId}");
            return response!;
        }

        public async Task<int> CreateAsync(VirtualMachineRequest.Create request) {
            var response = await client.PostAsJsonAsync(endpoint, request);
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task EditAsync(int vmId, VirtualMachineRequest.Edit request) {
            var response = await client.PutAsJsonAsync($"{endpoint}/{vmId}", request);
        }

        public Task DeleteAsync(int virtualMachineId)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineResponse.Index> GetAll()
        {
            throw new NotSupportedException();
        }

        public Task<VirtualMachineResponse.Detail> GetAllDetails()
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineResponse.Detail> GetAllDetailsVerlopenVms() {
            throw new NotSupportedException();
        }
    }
}
