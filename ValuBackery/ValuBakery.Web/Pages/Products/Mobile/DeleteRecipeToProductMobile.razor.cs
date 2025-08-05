using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Products.Mobile
{
    public partial class DeleteRecipeToProductMobile
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

        private List<RecipeVariantDto> RecipeDtos = new();
        private HashSet<int> SelectedRecipeIds = new();

        protected override async Task OnInitializedAsync()
        {
            var recipes = await _recipeVariantService.GetAllAsync();

            RecipeDtos = recipes
                .Where(x => ProductDto.ProductRecipeVariants
                    .Select(t => t.RecipeVariantId)
                    .Contains(x.Id))
                .ToList();
        }

        private async Task Submit()
        {
            try
            {
                Dictionary<int, ProductComponentType> componentMap = new();

                foreach (var ig in SelectedRecipeIds)
                {
                    componentMap[ig] = ProductComponentType.Recipe;
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

        private void OnCheckedChanged(bool value, int id)
        {
            if (value)
                SelectedRecipeIds.Add(id);
            else
                SelectedRecipeIds.Remove(id);
        }
    }
}
