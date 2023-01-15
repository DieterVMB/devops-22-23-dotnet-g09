using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualIT.Shared.Templates;

namespace VirtualIT.Server.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TemplateController: ControllerBase {
        private readonly ITemplateService templateService;

        public TemplateController(ITemplateService templateService) {
            this.templateService = templateService;
        }

        [HttpGet]
        [SwaggerOperation("Geef een lijst van templates met hun Id & Naam")]
        [Authorize(Roles = "Administrator, Beheerder, Gebruiker")]
        public async Task<TemplateResponse.Index> GetAllIndexAsync() {
            var response = await templateService.GetAllIndexAsync();
            return response!;
        }
    }
}
