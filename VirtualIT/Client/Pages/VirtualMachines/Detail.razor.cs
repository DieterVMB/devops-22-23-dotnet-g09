using Microsoft.AspNetCore.Components;
using VirtualIT.Shared.VirtualMachines;

namespace VirtualIT.Client.Pages.VirtualMachines;

public partial class Detail
{
    private VirtualMachineDto.Detail? vm;

    [Parameter] public int Id { get; set; }

    [Inject] public IVirtualMachineService VirtualMachineService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine(NavigationManager.BaseUri);
        var response = await VirtualMachineService.GetDetailAsync(Id);
        vm = response.Vm;
    }

    private void GoBack()
    {
        NavigationManager.NavigateTo($"vm/overzicht");
    }
}
