using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Enums;

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
    }
}
