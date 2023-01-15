using GridMvc.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualIT.Client.Pages.VirtualMachines;
using VirtualIT.Shared.VirtualMachines;

namespace VirtualIT.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VirtualMachineController : ControllerBase
{
    private readonly IVirtualMachineService virtualMachineService;

    public VirtualMachineController(IVirtualMachineService virtualMachineService)
    {
        this.virtualMachineService = virtualMachineService;
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "Administrator, Beheerder, Gebruiker")]
    public async Task<ActionResult> GetWithFilters()
    {


        var itemsTask = virtualMachineService.GetAllDetails();
        var items = await itemsTask;


        
        if (items.VirtualMachineDetails == null)        return Ok();


        var server = new GridServer<VirtualMachineDto.Detail>(items.VirtualMachineDetails, Request.Query,
            true, "ordersGrid", VMOverzicht.Columns, 10).Searchable(true, false, true).Sortable();
        ;

        var i = server.ItemsToDisplay;
        return Ok(i);
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "Administrator, Beheerder, Gebruiker")]
    public async Task<ActionResult> GetVerlopenWithFilters() {


        var itemsTask = virtualMachineService.GetAllDetailsVerlopenVms();
        var items = await itemsTask;



        if (items.VirtualMachineDetails == null) return Ok();


        var server = new GridServer<VirtualMachineDto.Detail>(items.VirtualMachineDetails, Request.Query,
            true, "ordersGrid", VMOverzicht.Columns, 10).Searchable(true, false, true).Sortable();
        ;

        var i = server.ItemsToDisplay;
        return Ok(i);
    }

    //GET BY ID action
    [SwaggerOperation("Returns a specific virtual machine.")]
    [HttpGet("{virtualMachineId}")]
    [Authorize(Roles = "Administrator, Beheerder, Gebruiker")]
    public async Task<VirtualMachineResponse.SingleDetail> GetDetail(int virtualMachineId)
    {
        return await virtualMachineService.GetDetailAsync(virtualMachineId);
    }

    //POST action
    [SwaggerOperation("Creates a new virtual machine.")]
    [HttpPost]
    [Authorize(Roles = "Administrator, Beheerder")]
    public async Task<int> Create(VirtualMachineRequest.Create request)
    {
        var virtualMachineId = await virtualMachineService.CreateAsync(request);

        return virtualMachineId;
    }

    //PUT action
    [SwaggerOperation("Edites an existing virtual machine.")]
    [HttpPut("{vmId}")]
    [Authorize(Roles = "Administrator, Beheerder")]
    public async Task<IActionResult> Edit(int vmId, VirtualMachineRequest.Edit request)
    {
        await virtualMachineService.EditAsync(vmId, request);

        return NoContent();
    }
}

