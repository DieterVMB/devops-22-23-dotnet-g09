using Microsoft.AspNetCore.Components;
using VirtualIT.Shared.Beheerder;

namespace VirtualIT.Client.Pages.Beheerders;

public partial class BeheerderAanpassen
{
    private BeheerderDto.Edit beheerder;
    [Parameter] public int Id { get; set; }
    [Inject] IBeheerderService BeheerderService { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync() {
        var detail = await BeheerderService.GetDetailAsync(Id);

        beheerder = new BeheerderDto.Edit {
            Voornaam = detail.Voornaam,
            Naam = detail.Naam,
            Email = detail.Email,
            Departement = detail.Departement,
            Rol = detail.Rol,
            IsActief = detail.IsActief
        };
    }

    private async Task HandleEdit()
    {
        await BeheerderService.EditAsync(Id, beheerder);

        NavigationManager.NavigateTo($"beheerder/{Id}");
    }
}

