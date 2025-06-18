using Microsoft.AspNetCore.Components;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class ViewRecipeResumen
    {
        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public RecipeVariantDto? RecipeDto { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (RecipeDto == null)
            {
                RecipeDto = await _recipeVariantService.GetByIdAsync(Id);
            }
        }
    }
}
