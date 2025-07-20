using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Helpers;

namespace ValuBakery.Web.Pages.Recipes.Mobile
{
    public partial class RecipesMobile
    {
        private string search = "";

        private IEnumerable<RecipeDto> filteredRecipes =>
            string.IsNullOrWhiteSpace(search)
                ? RecipeDtos
                : RecipeDtos.Where(i =>
                    StringHelper.RemoveDiacritics(i.Name).Contains(StringHelper.RemoveDiacritics(search), StringComparison.OrdinalIgnoreCase));

        private Color GetColor(string unit) => unit?.ToLower() switch
        {
            "kg" => Color.Error,
            "lt" => Color.Info,
            "ud" => Color.Primary,
            _ => Color.Primary
        };
    }
}
