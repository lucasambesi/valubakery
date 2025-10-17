using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Enums;
using ValuBakery.Web.Pages.Common;

namespace ValuBakery.Web.Pages.Ingredients
{
    public partial class ViewIngredientResumen
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public EventCallback<int> OnCreateData { get; set; }

        [Parameter]
        public IngredientDto IngredientDto { get; set; } = new IngredientDto() { Unit = UnitEnum.Kg };

        private void Cancel() => MudDialog.Cancel();

        void EditIngredientCost(IngredientDto item)
        {
            var parameters = new DialogParameters
            {
                { nameof(EditIngredient.IngredientDto), IngredientDto },
                { nameof(EditIngredient.OnChanged),
                    EventCallback.Factory.Create<decimal>(this, DialogEditEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = false,
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small
            };

            _dialogService.Show<EditIngredient>("Editar Ingrediente", parameters, options);
        }

        private void DialogEditEvent(decimal total)
        {
        }
    }
}
