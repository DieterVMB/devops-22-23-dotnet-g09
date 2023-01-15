using Microsoft.AspNetCore.Components;
using VirtualIT.Shared.Beheerder;

namespace VirtualIT.Client.Pages.Beheerders;

public partial class BeheerderAanmaken
{
    private BeheerderDto.Create beheerder = new();
    private bool bestaatAl { get; set; } = false;
    [Inject] IBeheerderService BeheerderService { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }

    private async Task HandleCreate()
    {
        bestaatAl = false;
        try {
            var response = await BeheerderService.CreateAsync(beheerder);

            NavigationManager.NavigateTo($"beheerder/{response}");
        } catch (Exception e) {
            bestaatAl = true;
        }
    }
}

