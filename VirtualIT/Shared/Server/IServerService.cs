using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualIT.Shared.VirtualMachines;

namespace VirtualIT.Shared.Server
{
    public interface IServerService
    {

        Task<ServerResponse.Index> GetAllIndexAsync();
        //Task<ServerResponse.Create> CreateAsync(ServerDto.Mutate model);
        /*
        Task EditAsync(int ServerId, ServerDto.Mutate model);
        Task DeleteAsync(int ServerId);*/

    }
}
