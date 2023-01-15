using Microsoft.EntityFrameworkCore;
using VirtualIT.Persistence;
using VirtualIT.Shared.Server;
using VirtualIT.Shared.Templates;

namespace VirtualIT.Services.Templates {
    public class TemplateService : ITemplateService {

        private readonly VirtualITDbContext dbContext;

        public TemplateService(VirtualITDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<TemplateResponse.Index> GetAllIndexAsync() {
            var query = dbContext.Templates.AsQueryable();

            var items = await query.OrderBy(x => x.Id)
                                    .Select(x => new TemplateDto.Index {
                                        Id = x.Id,
                                        Naam = x.Naam
                                    }).ToListAsync();

            var response = new TemplateResponse.Index {
                Templates = items
            };

            return response;
        }
    }
}
