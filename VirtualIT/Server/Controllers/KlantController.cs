using GridMvc.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualIT.Client.Pages.Klanten;
using VirtualIT.Shared.Klanten;

namespace VirtualIT.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class KlantController : ControllerBase{
        private readonly IKlantService klantService;

        public KlantController(IKlantService klantService) {
            this.klantService = klantService;
        }

        [SwaggerOperation("Wordt gebruikt om de GridBlazor te vullen")]
        [HttpGet("[action]")]
        [Authorize(Roles = "Administrator, Beheerder, Gebruiker")]
        public async Task<ActionResult> GetWithFilters() {


            var itemsTask = klantService.GetAllIndexAsync();
            var items = await itemsTask;



            if (items.Klanten == null) return Ok();


            var server = new GridServer<KlantDto.Index>(items.Klanten, Request.Query,
                true, "ordersGrid", KlantenLijst.Columns, 10).Searchable(true, false, true).Sortable();
            ;

            var i = server.ItemsToDisplay;
            return Ok(i);
        }

        [SwaggerOperation("Geeft een volledige lijst van klanten ID's.")]
        [HttpGet]
        [Authorize(Roles = "Administrator, Beheerder, Gebruiker")]
        public async Task<KlantResponse.Index> GetAllIndex() {
            return await klantService.GetAllIndexAsync();
        }

        [SwaggerOperation("Geeft een volledige lijst van klanten details.")]
        [HttpGet("[action]")]
        [Authorize(Roles = "Administrator, Beheerder, Gebruiker")]
        public async Task<KlantResponse.Detail> getAllDetails()
        {
            return await klantService.GetAllDetailASync();
        }

        [SwaggerOperation("Geeft een specifieke klant.")]
        [HttpGet("{klantId}")]
        [Authorize(Roles = "Administrator, Beheerder, Gebruiker")]
        public async Task<KlantDto.Detail> GetDetail(int klantId) {
            return await klantService.GetDetailAsync(klantId);
        }

        [SwaggerOperation("Maakt een nieuwe klant.")]
        [HttpPost]
        [Authorize(Roles = "Administrator, Beheerder")]
        public async Task<int> Create(KlantDto.Mutate request) {
            return await klantService.CreateAsync(request);
        }

        [SwaggerOperation("Past een klant aan.")]
        [HttpPut("{klantId}")]
        [Authorize(Roles = "Administrator, Beheerder")]
        public async Task<IActionResult> Edit(int klantId, KlantDto.Mutate model) {
            await klantService.EditAsync(klantId, model);
            return NoContent();
        }
    }
}
