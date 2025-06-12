using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class CreateRecipe
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public RecipeDto RecipeDto { get; set; } = new RecipeDto();

        private void Cancel() => MudDialog.Cancel();

        private async Task Submit()
        {
            try
            {
                var id = await _recipeService.AddAsync(RecipeDto);
                if (id > 0)
                {
                    MudDialog?.Close(DialogResult.Ok(true));
                    _navigationManager.NavigateTo($"/recipe/{id}");
                }
                else
                {
                    _snackbar.Add("Error al crear", Severity.Error);
                }

                _snackbar.Add("Receta creada", Severity.Success);
            }
            catch
            {
                _snackbar.Add("Error al crear", Severity.Error);
                throw;
            }
            finally
            {                
                StateHasChanged();
            }
        }
    }
}
