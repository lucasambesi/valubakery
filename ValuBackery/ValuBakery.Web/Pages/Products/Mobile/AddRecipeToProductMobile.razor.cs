using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Products.Mobile
{
    public partial class AddRecipeToProductMobile
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

        private List<RecipeVariantDto> RecipeDtos = new();
        private HashSet<int> SelectedRecipeIds = new();

        protected override async Task OnInitializedAsync()
        {
            var ings = await _recipeVariantService.GetAllAsync();
            RecipeDtos = ings
                .Where(x => !ProductDto.ProductRecipeVariants
                    .Select(t => t.Id)
                    .Contains(x.Id))
                .ToList();
        }

        private async Task Submit()
        {
            try
            {
                List<ProductRecipeVariantDto> igs = new();
                foreach (var recipeDto in SelectedRecipeIds)
                {
                    ProductRecipeVariantDto prodRecipeDto = new()
                    {
                        ProductId = ProductDto.Id,
                        RecipeVariantId = recipeDto,
                        Quantity = 1
                    };

                    var id = await _productRecipeVariantService.AddAsync(prodRecipeDto);

                    prodRecipeDto.Id = id;
                    igs.Add(prodRecipeDto);
                }

                Dictionary<int, ProductComponentType> componentMap = new();

                foreach (var ig in igs)
                {
                    componentMap[ig.Id] = ProductComponentType.Recipe;
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
        private void OnCheckedChanged(bool value, int id)
        {
            if (value)
                SelectedRecipeIds.Add(id);
            else
                SelectedRecipeIds.Remove(id);
        }
    }
}
