using MudBlazor;
using MudBlazor.Services;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class Recipes
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
