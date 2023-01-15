using Microsoft.AspNetCore.Components;
using VirtualIT.Shared.Beheerder;

namespace VirtualIT.Client.Pages.Beheerders;

public partial class Detail
{
    private BeheerderDto.Detail? beheerder;

    [Parameter] public int Id { get; set; }

    [Inject] public IBeheerderService BeheerderService { get; set; }

    protected override async Task OnInitializedAsync() 
    {
        beheerder = await BeheerderService.GetDetailAsync(Id);
    }

    private void GoBack()
    {
        NavigationManager.NavigateTo($"gebruikers");
    }
}