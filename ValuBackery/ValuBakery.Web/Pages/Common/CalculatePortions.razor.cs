using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Common
{
    public partial class CalculatePortions
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        [EditorRequired]
        public string Title { get; set; }

        [Parameter]
        [EditorRequired]
        public string FirstField { get; set; }

        [Parameter]
        [EditorRequired]
        public string SecondField { get; set; }

        [Parameter]
        public RecipeComponentTable RecipeComponentTable { get; set; }

        private decimal _nestedCost;

        private decimal _nestedQuantity;

        [Parameter]
        public EventCallback<CalculatePortionsResult> OnChanged
        {
            get; set;
        }

        private async Task Submit()
        {
            decimal total = _nestedCost > 0 && _nestedQuantity > 0 ? _nestedCost / _nestedQuantity : 0;

            await OnChanged.InvokeAsync(new CalculatePortionsResult
            {
                Portions= total,
                Component = RecipeComponentTable
            });

            MudDialog.Close();
        }
    }
}
