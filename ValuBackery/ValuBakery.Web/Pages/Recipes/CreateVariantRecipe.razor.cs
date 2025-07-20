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

        private Dictionary<string, string> SizeOptions = new()
        {
            { "5x5", "5x5" },
            { "16x16", "16x16" },
            { "20x20", "20x20" }
        };

        private Dictionary<string, string> PortionOptions = new()
        {
            { "1", "1" },
            { "8 a 12", "8 a 12" },
            { "12 a 16", "12 a 16" }
        };

        protected override async Task OnInitializedAsync()
        {
            if (RecipeDto?.Variants != null && SizeOptions != null)
            {
                var existingNames = RecipeDto.Variants.Select(v => v.Name).ToHashSet();
                SizeOptions = SizeOptions
                    .Where(option => !existingNames.Contains(option.Key))
                    .ToDictionary(pair => pair.Key, pair => pair.Value);
            }
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
