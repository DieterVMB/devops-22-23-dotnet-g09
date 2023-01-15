namespace VirtualIT.Shared.Klanten {
    public interface IKlantService {
        Task<KlantResponse.Detail> GetAllDetailASync();
        Task<KlantResponse.Index> GetAllIndexAsync();
        Task<KlantDto.Detail> GetDetailAsync(int klantId);
        Task<int> CreateAsync(KlantDto.Mutate model);
        Task EditAsync(int klantId, KlantDto.Mutate model);
    }
}