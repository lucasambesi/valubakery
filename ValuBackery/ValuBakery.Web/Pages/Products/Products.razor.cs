using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Services;

namespace ValuBakery.Web.Pages.Products
{
    public partial class Products
    {
        private bool isInitialized = false;
        private bool isMobile = false;

        [Inject] private IJSRuntime JS { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                int width = await JS.InvokeAsync<int>("eval", "window.innerWidth");
                isMobile = width <= 960; // 960px es el breakpoint "md"
                isInitialized = true;
                StateHasChanged();
            }
        }
    }
}
