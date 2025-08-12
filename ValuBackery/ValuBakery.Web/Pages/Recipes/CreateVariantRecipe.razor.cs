using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class CreateVariantRecipe
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public RecipeDto RecipeDto { get; set; }

        public CreateRecipeModel CreateRecipeModel { get; set; } = new CreateRecipeModel();

        private void Cancel() => MudDialog.Cancel();

        [Parameter]
        public EventCallback<int> OnCreateData { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

        private async Task Submit()
        {
            CreateRecipeModel.Size = CreateRecipeModel.Size?.Trim();
            CreateRecipeModel.Portions = CreateRecipeModel.Portions?.Trim();

            if (string.IsNullOrWhiteSpace(CreateRecipeModel.Size))
            {
                _snackbar.Add("El tamaño es obligatorio", Severity.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(CreateRecipeModel.Portions))
            {
                _snackbar.Add("Las porciones son obligatorias", Severity.Warning);
                return;
            }

            try
            {
                var variant = new RecipeVariantDto
                {
                    RecipeId = RecipeDto.Id,
                    Name = CreateRecipeModel.Size,
                    Portions = CreateRecipeModel.Portions
                };

                var id = await _recipeVariantService.AddAsync(variant);
                if (id > 0)
                {
                    _snackbar.Add("Variante creada", Severity.Success);
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
