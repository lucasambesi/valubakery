using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Enums;

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

        private void OpenEditDialog()
        {
            var parameters = new DialogParameters
            {
                { nameof(EditIngredient.IngredientDto), IngredientDto }
            };

            var options = new DialogOptions { 
                CloseButton = false,
                CloseOnEscapeKey = true,
                FullWidth = true, 
                MaxWidth = MaxWidth.Medium };

            _dialogService.Show<EditIngredient>("Editar Ingrediente", parameters, options);
        }
    }
}
