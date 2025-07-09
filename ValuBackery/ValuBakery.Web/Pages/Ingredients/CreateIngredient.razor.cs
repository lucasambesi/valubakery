using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Enums;
using ValuBakery.Web.Data;
using ValuBakery.Web.Pages.Common;
using ValuBakery.Web.Pages.Recipes;

namespace ValuBakery.Web.Pages.Ingredients
{
    public partial class CreateIngredient
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public EventCallback<int> OnCreateData { get; set; }

        [Parameter]
        public IngredientDto IngredientDto { get; set; } = new IngredientDto() { Unit = UnitEnum.Kg};


        private void Cancel() => MudDialog.Cancel();

        private async Task Submit()
        {
            if (string.IsNullOrWhiteSpace(IngredientDto.Name))
            {
                _snackbar.Add("El nombre es obligatorio", Severity.Warning);
                return;
            }

            try
            {
                var id = await _ingredientService.AddAsync(IngredientDto);
                if (id > 0)
                {
                    MudDialog?.Close(DialogResult.Ok(true));
                    await OnCreateData.InvokeAsync(id);
                }
                else
                {
                    _snackbar.Add("Error al crear", Severity.Error);
                }

                _snackbar.Add("Ingrediente creado", Severity.Success);
            }
            catch
            {
                throw;
            }
            finally
            {
                MudDialog.Close(DialogResult.Ok(IngredientDto.Id));
                StateHasChanged();
            }
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
                FullWidth = true
            };

            _dialogService.Show<CalculateVariable>("CalculateVariable", parameters, options);
        }

        private void DialogCalculateEvent(decimal total)
        {
            if(total > 0)
            {
                IngredientDto.CostPerUnit = total;
            }
        }
    }
}
