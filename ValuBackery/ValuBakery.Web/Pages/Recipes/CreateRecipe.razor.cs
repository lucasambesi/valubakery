using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class CreateRecipe
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        public CreateRecipeModel CreateRecipeModel { get; set; } = new CreateRecipeModel();

        private void Cancel() => MudDialog.Cancel();

        private async Task Submit()
        {
            if (string.IsNullOrWhiteSpace(CreateRecipeModel.Name))
            {
                _snackbar.Add("El nombre es obligatorio", Severity.Warning);
                return;
            }

            try
            {
                RecipeDto RecipeDto = new RecipeDto()
                {
                    Name = CreateRecipeModel.Name,
                    Description = CreateRecipeModel.Description,
                    Variants = new List<RecipeVariantDto>()
                    {
                        new RecipeVariantDto()
                        {
                            Portions = CreateRecipeModel.Portions,
                            Name = CreateRecipeModel.Size,
                        }
                    }
                };

                var id = await _recipeService.AddAsync(RecipeDto);
                if (id > 0)
                {
                    MudDialog?.Close(DialogResult.Ok(true));
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
