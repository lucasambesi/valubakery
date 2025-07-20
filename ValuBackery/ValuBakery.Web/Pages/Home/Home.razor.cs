using ValuBakery.Application.Services;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Home
{
    public partial class Home
    {
        private int TotalRecipes;
        private int TotalIngredients;
        private int TotalVariants;

        private List<RecipeDto> UltimasRecetas = new();

        protected override async Task OnInitializedAsync()
        {
            TotalRecipes = 10;// await RecipeService.CountAsync();
            TotalIngredients = 40;// await IngredientService.CountAsync();
            TotalVariants = 50;// await VariantService.CountActiveAsync();

            var recipes = await _recipeService.GetAllAsync();
            UltimasRecetas = recipes.Take(5).ToList();
        }
    }
}
