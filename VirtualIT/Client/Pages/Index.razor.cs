using Microsoft.AspNetCore.Components;

namespace VirtualIT.Client.Pages {
    public partial class Index {
        [Inject] public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync() {
            NavigationManager.NavigateTo("vm/overzicht");
        }
    }
}