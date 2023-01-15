using Microsoft.AspNetCore.Components;
using VirtualIT.Shared.Klanten;

namespace VirtualIT.Client.Pages.Klanten;

public partial class Detail
{
    private KlantDto.Detail? klant;

    [Parameter] public int Id { get; set; }

    [Inject] public IKlantService KlantService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        klant = await KlantService.GetDetailAsync(Id);
    }
}
