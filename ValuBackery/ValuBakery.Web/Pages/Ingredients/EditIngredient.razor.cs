using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Pages.Common;

namespace ValuBakery.Web.Pages.Ingredients
{
    public partial class EditIngredient
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public IngredientDto IngredientDto { get; set; } = new();

        [Parameter]
        public EventCallback<decimal> OnChanged
        {
            get; set;
        }

        public decimal NewCost { get; set; } = 0;

        private async Task Submit()
        {
            IngredientDto.CostPerUnit= NewCost;

            var result = await _ingredientService.UpdateAsync(IngredientDto);

            if (result)
            {
                await OnChanged.InvokeAsync(IngredientDto.CostPerUnit);
                _snackbar.Add("Costo actualizado", Severity.Success);
            }
            else
            {
                _snackbar.Add("Error al actualizar el costo", Severity.Error);
            }

            MudDialog?.Close(DialogResult.Ok(IngredientDto));
        }

        private void OpenCalculateDialog()
        {
            var parameters = new DialogParameters
            {
                {nameof(CalculateVariable.Title), "Calcular costo" },
                {nameof(CalculateVariable.FirstField), "Total gastado" },
                {nameof(CalculateVariable.SecondField), "Cantidad de unidades" },
                { nameof(CalculateVariable.OnChanged),
                    EventCallback.Factory.Create<decimal>(this, DialogCalculateEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                CloseOnEscapeKey = true,
                FullWidth = true
            };

            _dialogService.Show<CalculateVariable>("CalculateVariable", parameters, options);
        }

        private void DialogCalculateEvent(decimal total)
        {
            if (total > 0)
            {
                NewCost= total;
            }
        }

        private async Task HandleEnterKey(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await Submit();
            }
        }

        private void Cancel() => MudDialog?.Cancel();
    }
}
