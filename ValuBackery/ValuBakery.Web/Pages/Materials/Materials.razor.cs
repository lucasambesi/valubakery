using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Services;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Materials
{
    public partial class Materials
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
