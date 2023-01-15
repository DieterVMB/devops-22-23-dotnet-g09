using GridMvc.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualIT.Client.Pages.Beheerders;
using VirtualIT.Shared.Beheerder;

namespace VirtualIT.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BeheerderController : ControllerBase
{
    private readonly IBeheerderService beheerderService;
   
    public BeheerderController(IBeheerderService beheerderService)
    {
        this.beheerderService = beheerderService;
    }

    [SwaggerOperation("Wordt gebruikt om de GridBlazor te vullen")]
    [HttpGet("[action]")]
    [Authorize(Roles = "Administrator, Beheerder, Gebruiker")]
    public async Task<ActionResult> GetWithFilters()
    {


        var itemsTask = beheerderService.GetAllIndexAsync();
        var items = await itemsTask;



        if (items.Beheerders == null) return Ok();


        var server = new GridServer<BeheerderDto.Index>(items.Beheerders, Request.Query,
            true, "ordersGrid", BeheerdersLijst.Columns, 10).Searchable(true, false, true).Sortable();
        ;

        var i = server.ItemsToDisplay;
        return Ok(i);
    }

    [SwaggerOperation("Geeft een volledige lijst van beheerder ID's.")]
    [HttpGet]
    public async Task<BeheerderResponse.GetIndex> GetIndexAsync()
    {
        return await beheerderService.GetAllIndexAsync();
    }

    [SwaggerOperation("Maakt een nieuwe Beheerder.")]
    [HttpPost]
    [Authorize(Roles ="Administrator")]
    public async Task<int> CreateAsync(BeheerderDto.Create model)
    {
        return await beheerderService.CreateAsync(model);
    }

    [SwaggerOperation("Geeft de details van een beheerder voor het gegeven ID")]
    [HttpGet("{beheerderId}")]
    [Authorize(Roles = "Administrator, Beheerder, Gebruiker")]
    public async Task<BeheerderDto.Detail> GetDetailAsync(int beheerderId) {
        return await beheerderService.GetDetailAsync(beheerderId);
    }

    [SwaggerOperation("Past de beheerder met de gegeven ID aan")]
    [HttpPut("{beheerderId}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> EditAsync(int beheerderId, BeheerderDto.Edit model) {
        await beheerderService.EditAsync(beheerderId, model);
        return NoContent();
    }
    [SwaggerOperation("Geeft een lijst van Beheerders die alleen de Beheerder Role hebben")]
    [HttpGet("[action]")]
    [Authorize(Roles = "Administrator, Beheerder")]
    public async Task<BeheerderResponse.GetIndex> GetBeheerdersBeheerderRole() {
        return await beheerderService.GetAllBeheerderRoleIndexAsync();
    }

    [SwaggerOperation("Geeft een volledige lijst van beheerder details.")]
    [HttpGet("[action]")]
    [Authorize(Roles = "Administrator, Beheerder")]
    public async Task<BeheerderResponse.Detail> GetAllDetails() {
        return await beheerderService.GetAllDetailASync();
    }
}
