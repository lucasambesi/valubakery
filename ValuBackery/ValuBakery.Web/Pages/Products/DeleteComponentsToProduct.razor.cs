using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Products
{
    public partial class DeleteComponentsToProduct
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public ProductDto ProductDto { get; set; }

        [Parameter]
        public EventCallback<Dictionary<int, ProductComponentType>> OnDeleteData
        {
            get; set;
        }

        private List<ProductMaterialDto> MaterialDtos = new();

        private HashSet<ProductMaterialDto> SelectedMaterialDtos = new();

        private List<ProductRecipeVariantDto> RecipeDtos = new();

        private HashSet<ProductRecipeVariantDto> SelectedRecipeDtos = new();

        protected override async Task OnInitializedAsync()
        {
            MaterialDtos = await _productMaterialService.GetByProductIdAsync(ProductDto.Id);
            RecipeDtos = await _productRecipeVariantService.GetByProductIdAsync(ProductDto.Id);
        }

        private void OnSelectedItemsChanged(HashSet<ProductMaterialDto> elements)
        {
            SelectedMaterialDtos = elements;
        }

        private void OnSelectedItemsChanged(HashSet<ProductRecipeVariantDto> elements)
        {
            SelectedRecipeDtos = elements;
        }

        private async Task Submit()
        {
            try
            {
                List<int> deletedIds = new();
                Dictionary<int, ProductComponentType> componentMap = new();

                foreach (var materialDto in SelectedMaterialDtos)
                {
                    deletedIds.Add(materialDto.Id);
                    componentMap[materialDto.Id] = ProductComponentType.Material;
                    //var success = await _productMaterialService.DeleteAsync(materialDto.Id);
                    //if (success)
                    //{
                    //    materialDto.IsDeleted = true;
                    //    deletedIds.Add(materialDto.Id);
                    //    componentMap[materialDto.Id] = ProductComponentType.Material;
                    //}
                }

                foreach (var recipeDto in SelectedRecipeDtos)
                {
                    componentMap[recipeDto.Id] = ProductComponentType.Recipe;
                    deletedIds.Add(recipeDto.Id);
                    //var success = await _productRecipeVariantService.DeleteAsync(recipeDto.Id);

                    //if (success)
                    //{
                    //    recipeDto.IsDeleted = true;
                    //    componentMap[recipeDto.Id] = ProductComponentType.Recipe;
                    //    deletedIds.Add(recipeDto.Id);
                    //}
                }

                await OnDeleteData.InvokeAsync(componentMap);
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
