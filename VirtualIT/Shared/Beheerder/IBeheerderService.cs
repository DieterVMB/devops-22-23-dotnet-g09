namespace VirtualIT.Shared.Beheerder;

public interface IBeheerderService
{
    Task<BeheerderResponse.Detail> GetAllDetailASync();
    Task<BeheerderResponse.GetIndex> GetAllIndexAsync();
    Task<int> CreateAsync(BeheerderDto.Create model);
    Task<BeheerderDto.Detail> GetDetailAsync(int beheerderId);
    Task EditAsync(int beheerderId, BeheerderDto.Edit model);
    Task<BeheerderResponse.GetIndex> GetAllBeheerderRoleIndexAsync();

}
