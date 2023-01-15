using VirtualIT.Shared.Klanten;
using Microsoft.AspNetCore.Components;
using VirtualIT.Domain.Exceptions;

namespace VirtualIT.Client.Pages.Klanten;

public partial class KlantForm
{
    private KlantDto.Mutate klant;
    private bool bestaatAl { get; set; } = false;
    [Parameter] public int Id { get; set; }
    [Inject] IKlantService KlantService { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if(Id == 0) {
            klant = new();
            klant.Aanspreekpunt = new AanspreekpuntDto();
            klant.BackupAanspreekpunt = new AanspreekpuntDto();
        } else {
            var details = await KlantService.GetDetailAsync(Id);

            klant = new KlantDto.Mutate {
                KlantNaam = details.KlantNaam,
                Extern = details.Extern,
                Departement = details.Departement,
                Opleiding = details.Opleiding,
                ExternType = details.ExternType,
                Aanspreekpunt = details.Aanspreekpunt,
                BackupAanspreekpunt = details.BackupAanspreekpunt
            };
        }
    }

    private async Task HandleForm()
    {
        if(Id == 0) {
            bestaatAl = false;
            try {
                var response = await KlantService.CreateAsync(klant);
                NavigationManager.NavigateTo($"klant/{response}");
            } catch (Exception e) {
                bestaatAl = true;
            }
        } else {
            await KlantService.EditAsync(Id, klant);
            NavigationManager.NavigateTo($"klant/{Id}");
        }
    }
}

