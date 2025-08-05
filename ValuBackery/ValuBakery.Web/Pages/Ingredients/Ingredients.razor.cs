using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Services;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;

namespace ValuBakery.Web.Pages.Ingredients
{
    public partial class Ingredients
    {
        private bool isInitialized = false;
        private bool isMobile = false;

        [Inject] IJSRuntime JS { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                int width = await JS.InvokeAsync<int>("eval", "window.innerWidth");
                isMobile = width <= 960; // breakpoint md de MudBlazor
                isInitialized = true;
                StateHasChanged();
            }
        }
    }
}
