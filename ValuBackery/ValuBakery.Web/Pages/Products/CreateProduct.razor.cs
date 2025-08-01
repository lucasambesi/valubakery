using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Products
{
    public partial class CreateProduct
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public EventCallback<int> OnCreateData { get; set; }

        [Parameter]
        public ProductDto ProductDto { get; set; } = new ProductDto() { ApplyProfitToMaterials = 100, ApplyProfitToRecipes = 100};

        private void Cancel() => MudDialog.Cancel();

        private RecipeVariantDto? SelectedRecipe { get; set; } = null;

        private List<RecipeVariantDto> Recipes = new();

        protected override async Task OnInitializedAsync()
        {
            Recipes = await _recipeVariantService.GetAllAsync();
        }

        private void OnRecipeChanged(RecipeVariantDto? recipe)
        {
            SelectedRecipe = recipe;

            if (recipe != null)
            {
                ProductDto.Name = recipe.GetName();
                ProductDto.Description = recipe.Recipe.Description;
            }
            else
            {
                ProductDto.Name = string.Empty;
                ProductDto.Description = string.Empty;
            }
        }


        private async Task Submit()
        {
            ProductDto.Name = ProductDto.Name?.Trim();
            ProductDto.Description = ProductDto.Description?.Trim();

            if (string.IsNullOrWhiteSpace(ProductDto.Name))
            {
                _snackbar.Add("El nombre es obligatorio", Severity.Warning);
                return;
            }

            try
            {
                var id = await _productService.AddAsync(ProductDto);
                if (id > 0)
                {

                    if (SelectedRecipe != null)
                    {
                        var productRecipeVariant = new ProductRecipeVariantDto()
                        {
                            ProductId = id,
                            RecipeVariantId = SelectedRecipe.Id,
                            Quantity = 1,
                        };

                        var componentId = await _productRecipeVariantService.AddAsync(productRecipeVariant);

                        if (componentId == default)
                        {
                            _snackbar.Add("Error al agregar la receta", Severity.Error);
                        }
                    }

                    MudDialog?.Close(DialogResult.Ok(true));
                    await OnCreateData.InvokeAsync(id);
                }
                else
                {
                    _snackbar.Add("Error al crear", Severity.Error);
                }

                _snackbar.Add("Product creado", Severity.Success);
            }
            catch
            {
                throw;
            }
            finally
            {
                MudDialog.Close(DialogResult.Ok(ProductDto.Id));
                StateHasChanged();
            }
        }

        private void CapitalizeName()
        {
            if (!string.IsNullOrWhiteSpace(ProductDto.Name))
            {
                var name = ProductDto.Name.Trim();
                ProductDto.Name = char.ToUpper(name[0]) + name[1..];
            }
        }
    }
}
