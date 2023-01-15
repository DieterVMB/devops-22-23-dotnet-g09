using Microsoft.EntityFrameworkCore;
using VirtualIT.Persistence;
using VirtualIT.Shared.Server;

namespace VirtualIT.Services.Servers;

public class ServerService : IServerService
{
    private readonly VirtualITDbContext dbContext;

    public ServerService(VirtualITDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ServerResponse.Index> GetAllIndexAsync()
    {
        var query = dbContext.Servers.AsQueryable();

        var items = await query.OrderBy(x => x.Id)
                                .Select(x => new ServerDto.Index
                                {
                                    Id = x.Id,
                                    Naam = x.Naam
                                }).ToListAsync();

        var result = new ServerResponse.Index
        {
            Servers = items,
        };

        return result;
    }
}
