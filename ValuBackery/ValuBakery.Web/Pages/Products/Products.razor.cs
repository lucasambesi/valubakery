using MudBlazor;
using MudBlazor.Services;

namespace ValuBakery.Web.Pages.Products
{
    public partial class Products
    {
        protected Breakpoint currentBreakpoint;
        protected bool isMobile => currentBreakpoint <= Breakpoint.SmAndDown;

        protected async Task InitBreakpointAsync(IBreakpointService breakpointService)
        {
            currentBreakpoint = await breakpointService.GetBreakpoint();
        }

        protected void OnBreakpointChanged(Breakpoint breakpoint)
        {
            currentBreakpoint = breakpoint;
            StateHasChanged();
        }
    }
}
