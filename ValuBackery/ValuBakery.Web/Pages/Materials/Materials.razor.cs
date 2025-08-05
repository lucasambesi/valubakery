using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Services;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Materials
{
    public partial class Materials
    {
        private bool isInitialized = false;
        private bool isMobile = false;

        [Inject] IJSRuntime JS { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                int width = await JS.InvokeAsync<int>("eval", "window.innerWidth");
                isMobile = width <= 960;
                isInitialized = true;
                StateHasChanged();
            }
        }
    }
}
