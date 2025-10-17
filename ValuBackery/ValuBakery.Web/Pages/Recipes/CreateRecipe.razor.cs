using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence;
using ValuBakery.Web.Data;
using ValuBakery.Web.Pages.Ingredients;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class CreateRecipe
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        public CreateRecipeModel CreateRecipeModel { get; set; } = new CreateRecipeModel();

        private void Cancel() => MudDialog.Cancel();

        [Parameter]
        public EventCallback<int> OnCreateData { get; set; }

        private async Task Submit()
        {
            // Normalizar entradas
            CreateRecipeModel.Name = CreateRecipeModel.Name?.Trim();
            CreateRecipeModel.Description = CreateRecipeModel.Description?.Trim();
            CreateRecipeModel.VarianteName = CreateRecipeModel.VarianteName?.Trim();
            CreateRecipeModel.Portions = CreateRecipeModel.Portions?.Trim();

            if (string.IsNullOrWhiteSpace(CreateRecipeModel.Name))
            {
                _snackbar.Add("El nombre es obligatorio", Severity.Warning);
                return;
            }

            try
            {
                var recipeDto = new RecipeDto
                {
                    Name = CreateRecipeModel.Name,
                    Description = CreateRecipeModel.Description,
                    Variants = new List<RecipeVariantDto>
                    {
                        new RecipeVariantDto
                        {
                            Name = string.IsNullOrEmpty(CreateRecipeModel.VarianteName) ? "Defecto" : CreateRecipeModel.VarianteName,
                            Portions = CreateRecipeModel.Portions
                        }
                    }
                };

                var id = await _recipeService.AddAsync(recipeDto);
                if (id > 0)
                {
                    _snackbar.Add("Receta creada", Severity.Success);
                    MudDialog?.Close(DialogResult.Ok(true));
                    await OnCreateData.InvokeAsync(id);
                }
                else
                {
                    _snackbar.Add("Error al crear", Severity.Error);
                }
            }
            catch (Exception)
            {
                _snackbar.Add("Ocurrió un error inesperado", Severity.Error);
                throw;
            }
        }

        private void CapitalizeName()
        {
            if (!string.IsNullOrWhiteSpace(CreateRecipeModel.Name))
            {
                var name = CreateRecipeModel.Name.Trim();
                CreateRecipeModel.Name = char.ToUpper(name[0]) + name[1..];
            }
        }

        private async Task HandleEnterKey(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await Submit();
            }
        }
    }
}
