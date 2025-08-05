using ValuBakery.Application.Services;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Home
{
    public partial class Home
    {
        private int TotalRecipes;
        private int TotalIngredients;
        private int TotalVariants;
        private bool isLoading;
        private List<RecipeDto> UltimasRecetas = new();

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            TotalRecipes = await _recipeService.GetCountAsync();
            TotalIngredients = await _ingredientService.GetCountAsync();
            TotalVariants = await _recipeVariantService.GetCountAsync();

            var recipes = await _recipeService.GetAllAsync();
            UltimasRecetas = recipes.Take(5).ToList();
            isLoading = false;
        }
    }
}
