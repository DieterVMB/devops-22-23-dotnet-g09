namespace VirtualIT.Shared.VirtualMachines;

public interface IVirtualMachineService
{
    Task<VirtualMachineResponse.Index> GetAll();
    Task<VirtualMachineResponse.SingleDetail> GetDetailAsync(int virtualMachineId);
    Task<VirtualMachineResponse.Detail> GetAllDetails();
    Task<VirtualMachineResponse.Detail> GetAllDetailsVerlopenVms();
    Task<int> CreateAsync(VirtualMachineRequest.Create request);
    Task EditAsync(int vmId, VirtualMachineRequest.Edit request);
}

