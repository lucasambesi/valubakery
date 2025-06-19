using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Enums;

namespace ValuBakery.Web.Pages.Materials
{
    public partial class ViewMaterialResumen
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public EventCallback<int> OnCreateData { get; set; }

        [Parameter]
        public MaterialDto MaterialDto { get; set; } = new MaterialDto() { Unit = UnitMaterialEnum.Ud };

        private void Cancel() => MudDialog.Cancel();
    }
}
