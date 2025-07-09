using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Products
{
    public partial class AddComponentsToProduct
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public ProductDto ProductDto { get; set; }

        [Parameter]
        public EventCallback<Dictionary<int, ProductComponentType>> OnCreateData
        {
            get; set;
        }

        private List<MaterialDto> MaterialDtos = new();

        private HashSet<MaterialDto> SelectedMaterialDtos = new();

        private List<RecipeVariantDto> RecipeDtos = new();

        private HashSet<RecipeVariantDto> SelectedRecipeDtos = new();

        protected override async Task OnInitializedAsync()
        {
            var materials = await _materialService.GetAllAsync();
            MaterialDtos = materials.Where(x => !ProductDto.ProductMaterials.Select(t => t.MaterialId).Contains(x.Id)).ToList();

            var recipes = await _recipeVariantService.GetAllAsync();
            RecipeDtos = recipes.Where(x => !ProductDto.ProductRecipeVariants.Select(t => t.RecipeVariantId).Contains(x.Id)).ToList();
        }

        private void OnSelectedItemsChanged(HashSet<MaterialDto> elements)
        {
            SelectedMaterialDtos = elements;
        }

        private void OnSelectedItemsChanged(HashSet<RecipeVariantDto> elements)
        {
            SelectedRecipeDtos = elements;
        }

        private async Task Submit()
        {
            try
            {
                List<ProductMaterialDto> materials = new();
                foreach (var materialDto in SelectedMaterialDtos)
                {
                    ProductMaterialDto productMaterialDto = new()
                    {
                        ProductId = ProductDto.Id,
                        MaterialId = materialDto.Id,
                        Quantity = 1
                    };

                    var id = await _productMaterialService.AddAsync(productMaterialDto);

                    productMaterialDto.Id = id;
                    materials.Add(productMaterialDto);
                }

                List<ProductRecipeVariantDto> rcps = new();
                foreach (var recipeDto in SelectedRecipeDtos)
                {
                    ProductRecipeVariantDto recipeComponentDto = new()
                    {
                        ProductId = ProductDto.Id,
                        RecipeVariantId = recipeDto.Id,
                        Quantity = 1
                    };

                    var id = await _productRecipeVariantService.AddAsync(recipeComponentDto);

                    recipeComponentDto.Id = id;
                    rcps.Add(recipeComponentDto);
                }
                Dictionary<int, ProductComponentType> componentMap = new();

                foreach (var ig in materials)
                {
                    componentMap[ig.Id] = ProductComponentType.Material ;
                }

                foreach (var rc in rcps)
                {
                    componentMap[rc.Id] = ProductComponentType.Recipe;
                }

                await OnCreateData.InvokeAsync(componentMap);
            }
            catch
            {
                throw;
            }
            finally
            {
                MudDialog.Close();
                StateHasChanged();
            }
        }
    }
}
